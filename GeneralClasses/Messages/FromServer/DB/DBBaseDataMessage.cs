using GeneralClasses.Messages.FromServer.DB;

namespace GeneralClasses.Data.FromServer.DB
{
    public class DBBaseDataMessage : DBObjectOfList
    {
        public string? Description { get; set; } = null;
    }
}
