namespace GeneralClasses.Data
{
    public class BaseDataMessage : Message
    {
        public int? Id { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? PicturePath { get; set; } = null;
    }
}
