namespace Accounting.Domain
{
    public class Document
    {
        private Document()
        {

        }

        public Document(string extension, byte[] data)
        {
            Id = Guid.NewGuid().ToString();
            Data = data;
            Extension = extension;
            Status = DocumentStatus.Reserve;
            CreationDate = DateTime.UtcNow;
        }

        public void Register()
        {
            Status = DocumentStatus.Register;
        }

        public string Id { get; set; }
        public byte[] Data { get; set; }
        public DateTime CreationDate { get; set; }
        public string Extension { get; set; }
        public DocumentStatus Status { get; set; }
    }

    public enum DocumentStatus : short
    {
        Reserve = 1,
        Register = 2
    }
}
