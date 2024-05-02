
using GeneralClasses.Messages;

namespace GeneralClasses.Data.ToServer
{
    public class MessageToServer : Message
    {
        public GeneralConstant.GeneralServerActions Action { get; set; } = GeneralConstant.GeneralServerActions.None;

        public string? TopicFromServer { get; set; } = null;
    }
}
