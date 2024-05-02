using GeneralClasses.Messages;
using GeneralClasses.Messages.FromServer.DB;

namespace GeneralClasses.Data.FromServer.DB
{
    public class DBListMessage : Message
    {
        public List<DBObjectOfList> List { get; set; } = new();
    }
}
