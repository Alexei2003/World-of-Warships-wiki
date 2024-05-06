using GeneralClasses.Messages;
using GeneralClasses.Messages.FromServer.DB;

namespace GeneralClasses.Data.FromServer.DB
{
    public class DBListMessage : Message
    {
        public List<DBObjectFromList> ItemList { get; set; } = new();

        public class DBObjectFromList
        {
            public int? Id { get; set; } = null;
            public string? Name { get; set; } = null;
            public string? PicturePath { get; set; } = null;
        }
    }
}
