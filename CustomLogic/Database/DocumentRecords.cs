namespace CustomLogic.Database
{
    using System;

    public partial class DocumentRecords
    {
        public string Id { get; set; }
        public string Partition { get; set; }
        public string EntityType { get; set; }
        public DateTime Ts { get; set; }
        public string JsonDocument { get; set; }
        public int? CreatedFilesId { get; set; }

        public virtual CreatedFiles CreatedFiles { get; set; }
    }
}
