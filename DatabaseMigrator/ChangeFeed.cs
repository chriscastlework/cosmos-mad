using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DatabaseMigrator
{
    using System;
    using System.IO;
    using System.Linq;
    using CustomLogic.Database;
    using DatabaseMigrator.JsonCSharpClassGeneratorLib;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
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
                Dictionary<string, string> classes = new Dictionary<string, string>();
                log.LogInformation("Documents modified " + input.Count);
                foreach (var document in input)
                {
                    var partition = document.GetPropertyValue<string>("Shard");
                    JsonClassGenerator generator = new JsonClassGenerator();
                    var memoryStream = new MemoryStream();
                    generator.OutputStream = new StreamWriter(memoryStream);
                    generator.GenerateClasses(document, classes);
                    memoryStream.Flush();
                    var fsr = new FileStreamResult(new MemoryStream(memoryStream.ToArray()), "text/plain");

                    using (var stream = new StreamReader(fsr.FileStream))
                    {
                        var classString = stream.ReadToEnd();
                        var ts = document.GetPropertyValue<long>("_ts");
                        var dateTime = DateTimeOffset.FromUnixTimeSeconds(ts);

                        var sameItem = _databaseMigratorContext.CreatedFiles.FirstOrDefault(c => c.CsFiles == classString);

                        var upsertItem = new DocumentRecords
                        {
                            EntityType = string.IsNullOrWhiteSpace(generator.MainClass) ? "Unknown" : generator.MainClass,
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
                                CsFiles = classString,
                                TsFile = ""
                            };
                            _databaseMigratorContext.DocumentRecords.Add(upsertItem);
                            _databaseMigratorContext.SaveChanges();
                            log.LogInformation("Added another class type to ducument");
                        }
                    }
                }

                _databaseMigratorContext.SaveChanges();

            }
        }
    }
}
