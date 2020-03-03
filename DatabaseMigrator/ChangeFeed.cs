using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DatabaseMigrator
{
    using System;
    using System.Linq;
    using System.Text;
    using CustomLogic.Database;
    using DatabaseMigrator.JsonCSharpClassGeneratorLib;
    using DatabaseMigrator.JsonCSharpClassGeneratorLib.CodeWriters;
    using RecruitMe.Web.Database;

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
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                foreach (var document in input)
                {
                    var partition = document.GetPropertyValue<string>("Shard");
                    var entityType = document.GetPropertyValue<string>("Bucket");
                    JsonClassGenerator cSharpClassGenerator = new JsonClassGenerator(new CSharpCodeWriter());
                    JsonClassGenerator tsClassGenerator = new JsonClassGenerator(new TypeScriptCodeWriter());
                    cSharpClassGenerator.GenerateClasses(document);
                    tsClassGenerator.GenerateClasses(document);

                        var cSharpeClass = cSharpClassGenerator.StringBuilder.ToString();
                        var tsClass = tsClassGenerator.StringBuilder.ToString();
                        var ts = document.GetPropertyValue<long>("_ts");
                        var dateTime = DateTimeOffset.FromUnixTimeSeconds(ts);

                        var sameItem = _databaseMigratorContext.CreatedFiles.FirstOrDefault(c => c.CsFiles == cSharpeClass); // we only check the c# class

                        var upsertItem = new DocumentRecords
                        {
                            EntityType = string.IsNullOrWhiteSpace(entityType) ? "Unknown" : entityType,
                            Partition = partition,
                            Id = document.Id,
                            Ts = dateTime.UtcDateTime,
                            JsonDocument = document.ToString()
                        };  

                        if (sameItem != null)
                        {
                            upsertItem.CreatedFilesId = sameItem.Id;
                            _databaseMigratorContext.DocumentRecords.Add(upsertItem);
                            log.LogInformation("Added another document with existing class type found");
                        }
                        else
                        {
                            upsertItem.CreatedFiles = new CreatedFiles
                            {
                                CsFiles = cSharpeClass,
                                TsFile = tsClass
                            };
                            _databaseMigratorContext.DocumentRecords.Add(upsertItem);
                            _databaseMigratorContext.SaveChanges();
                            log.LogInformation("Added another class type to document");
                        }
                }

                _databaseMigratorContext.SaveChanges();

            }
        }
    }
}
