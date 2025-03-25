namespace INDIACom.Models
{
    public class MemberDocumentModel
    {
        public int ID { get; set; }  // Primary Key (Auto-increment)
        public int UserID { get; set; } // Foreign Key to User
        public string Name { get; set; }
        public string DocumentType { get; set; } // e.g., "Resume"
        public string FilePath { get; set; } // Path where file is stored
    }
}
