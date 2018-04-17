using System;

namespace FT.Helper.Model
{
    public class FileModel
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public string FileID { get; set; }
        public string Extention { get; set; }
        public string Status { get; set; }
        public double Size { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
