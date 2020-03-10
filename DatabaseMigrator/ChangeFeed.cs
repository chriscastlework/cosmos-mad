using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DatabaseMigrator
{
    using System;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using CustomLogic.Database;
    using DatabaseMigrator.JsonCSharpClassGeneratorLib;
    using DatabaseMigrator.JsonCSharpClassGeneratorLib.CodeWriters;

    public class ChangeFeed
    {
        private readonly DatabaseMigratorContext _databaseMigratorContext;

        public ChangeFeed(DatabaseMigratorContext databaseMigratorContext)
        {
            _databaseMigratorContext = databaseMigratorContext;
        }
        [FunctionName("ChangeFeed")]
        public void Run([CosmosDBTrigger(
            databaseName: "quantum",
            collectionName: "entity",
            StartFromBeginning = true,
            ConnectionStringSetting = "CosmosDbKey",
            LeaseCollectionName = "leases", LeaseCollectionPrefix = "castle-3")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                foreach (var document in input)
                {
                    var entityType = document.GetPropertyValue<string>("Bucket"); // no bucket then we don't care!
                    if (string.IsNullOrWhiteSpace(entityType)) continue;

                    var partition = document.GetPropertyValue<string>("Shard");
                    var ts = document.GetPropertyValue<long>("_ts");

                    var tsClassGenerator = new JsonClassGenerator(new TypeScriptCodeWriter());
                    var cSharpClassGenerator = new JsonClassGenerator(new CSharpCodeWriter());
                    cSharpClassGenerator.GenerateClasses(document);
                    tsClassGenerator.GenerateClasses(document);
                    var cSharpeClass = cSharpClassGenerator.StringBuilder.ToString();
                    var tsClass = tsClassGenerator.StringBuilder.ToString();
                  
                    var dateTime = DateTimeOffset.FromUnixTimeSeconds(ts);

                    // check if we have existing items
                    var documentUpsert = _databaseMigratorContext.DocumentRecords.FirstOrDefault(c => c.Id == document.Id && c.Partition == partition); // we only check the c# class
                    var createdFileUpsert = _databaseMigratorContext.CreatedFiles.FirstOrDefault(c => c.CsFiles == cSharpeClass); // we only check the c# class

                    var isCreate = true;

                    if (documentUpsert == null)
                    {
                        documentUpsert = new DocumentRecords
                        {
                            EntityType = entityType,
                            Partition = partition,
                            Id = document.Id,
                            Ts = dateTime.UtcDateTime,
                            JsonDocument = document.ToString()
                        };
                    }
                    else
                    {
                        isCreate = false;
                    }

                    if (createdFileUpsert == null)
                    {
                        documentUpsert.CreatedFiles = new CreatedFiles
                        {
                            CsFiles = cSharpeClass,
                            TsFile = tsClass
                        };
                        log.LogInformation("Added another class type to document");
             
                    }
                    else
                    {
                        documentUpsert.CreatedFilesId = createdFileUpsert.Id;
                        log.LogInformation("Added another document with existing class type found");
                    }

                    if (isCreate)
                    {
                        _databaseMigratorContext.Add(documentUpsert);
                    }
                    
                    _databaseMigratorContext.SaveChanges();
                }
            }
        }
    }
}
