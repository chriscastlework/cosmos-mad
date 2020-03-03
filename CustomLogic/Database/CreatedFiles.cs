namespace CustomLogic.Database
{
    using System.Collections.Generic;
    using RecruitMe.Web.Database;

    public partial class CreatedFiles
    {
        public CreatedFiles()
        {
            DocumentRecords = new HashSet<DocumentRecords>();
        }

        public string CsFiles { get; set; }
        public string TsFile { get; set; }
        public int Id { get; set; }

        public virtual ICollection<DocumentRecords> DocumentRecords { get; set; }
    }
}
