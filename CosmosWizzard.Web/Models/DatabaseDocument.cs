namespace CosmosWizard.Web.Models
{
    using System.Collections.Generic;
    using CustomLogic.Database;

    public class DatabaseDocument
    {
        public List<DocumentRecords> Entities { get; set; } = new List<DocumentRecords>();

        public List<string> EntityTypes { get; set; } = new List<string>();
        public int EntityCount { get; set; }
        public List<string> DataVersions { get; set; } = new List<string>();
    }
}
