using Newtonsoft.Json;

namespace GeneralClasses.Data
{
    public class Message
    {
        public GeneralConstant.GeneralServerActions Action { get; set; } = GeneralConstant.GeneralServerActions.None;

        public string? TopicFromServer { get; set; } = null;

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
