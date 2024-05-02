using Newtonsoft.Json;

namespace GeneralClasses.Messages
{
    public class Message
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
