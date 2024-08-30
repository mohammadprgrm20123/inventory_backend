namespace Accounting.Domain.Warehouses.ValueObjects
{
    public class Avatar
    {
        public string Id { get; set; }
        public string Extension { get; set; }

        public Avatar()
        {

        }

        public Avatar(string id, string extension)
        {
            Id = id;
            Extension = extension;
        }
    }
}
