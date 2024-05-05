namespace GeneralClasses.Messages.FromServer.DB.DBObjects
{
    public class DBObjectMessage : Message
    {
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? PicturePath { get; set; } = null;
    }
}
