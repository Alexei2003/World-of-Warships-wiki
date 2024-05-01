using GeneralClasses;
using GeneralClasses.Data;
using RabbitMQ;

namespace WorldOfWarshipsWiki
{
    internal static class RabbitMQ
    {
        public static string TopicFromServer = GeneralConstant.RABBTI_MQ_TOPIC_TO_CLIENT + "1";

        public static RabbitMQPublisher Publisher = new(GeneralConstant.SERVER_IP,
                                                        GeneralConstant.RABBTI_MQ_PORT,
                                                        GeneralConstant.RABBIT_MQ_LOGIN,
                                                        GeneralConstant.RABBIT_MQ_PASSWORD,
                                                        GeneralConstant.RABBTI_MQ_TOPIC_TO_SERVER);

        public static RabbitMQConsumer Consumer = new(GeneralConstant.SERVER_IP,
                                                      GeneralConstant.RABBTI_MQ_PORT,
                                                      GeneralConstant.RABBIT_MQ_LOGIN,
                                                      GeneralConstant.RABBIT_MQ_PASSWORD,
                                                      TopicFromServer);

        public static void Start()
        {
            var firstMessage = new FirstMessage()
            {
                Action = GeneralConstant.GeneralServerActions.Start,
                TopicFromServer = TopicFromServer,
            };
            Publisher.SendMessage(firstMessage.ToJson());

            string json = null;
            while (json != GeneralConstant.SUCCESS)
            {
                json = Consumer.GetMessage();
            }
        }

        public static void Finish()
        {
            var lastMessage = new FirstMessage()
            {
                Action = GeneralConstant.GeneralServerActions.Finish,
                TopicFromServer = TopicFromServer,
            };
            Publisher.SendMessage(lastMessage.ToJson());
        }
    }
}
