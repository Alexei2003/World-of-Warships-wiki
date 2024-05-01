using GeneralClasses;
using GeneralClasses.Data;
using Newtonsoft.Json;
using RabbitMQ;

internal class ProgramServer
{
    private static void Main(string[] args)
    {
        var consumer = new RabbitMQConsumer(GeneralConstant.SERVER_IP,
                                            GeneralConstant.RABBTI_MQ_PORT,
                                            GeneralConstant.RABBIT_MQ_LOGIN,
                                            GeneralConstant.RABBIT_MQ_PASSWORD,
                                            GeneralConstant.RABBTI_MQ_TOPIC_TO_SERVER);
       
        var publishers = new Dictionary<string, RabbitMQPublisher>();

        while (true)
        {
            var json = consumer.GetMessage();
            Console.WriteLine(json);

            var basePartOfMassage = JsonConvert.DeserializeObject<Message>(json);

            RabbitMQPublisher publisher;
            switch (basePartOfMassage.Action)
            {
                case GeneralConstant.GeneralServerActions.Start:
                    publishers.TryAdd(basePartOfMassage.TopicFromServer, new RabbitMQPublisher(GeneralConstant.SERVER_IP,
                                                                                               GeneralConstant.RABBTI_MQ_PORT,
                                                                                               GeneralConstant.RABBIT_MQ_LOGIN,
                                                                                               GeneralConstant.RABBIT_MQ_PASSWORD,
                                                                                               basePartOfMassage.TopicFromServer));
                    
                    if(!publishers.TryGetValue(basePartOfMassage.TopicFromServer, out publisher))
                    {
                        break;
                    }

                    publisher.SendMessage(GeneralConstant.SUCCESS);

                    break;

                case GeneralConstant.GeneralServerActions.Finish:
                    if (!publishers.TryGetValue(basePartOfMassage.TopicFromServer, out publisher))
                    {
                        break;
                    }
                    publisher.DeleteQueue();
                    publisher.CloseConnection();

                    publishers.Remove(basePartOfMassage.TopicFromServer);
                    break;
                case GeneralConstant.GeneralServerActions.None:
                default:
                    break;
            };
        }
    }
}