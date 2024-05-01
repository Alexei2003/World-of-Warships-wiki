using GeneralClasses;
using GeneralClasses.Data;
using RabbitMQ;

internal class ProgramClient
{
    private static void Main(string[] args)
    {
        var topicFromServer = GeneralConstant.RABBTI_MQ_TOPIC_TO_CLIENT + "1";

        var publisher = new RabbitMQPublisher(GeneralConstant.SERVER_IP,
                                              GeneralConstant.RABBTI_MQ_PORT,
                                              GeneralConstant.RABBIT_MQ_LOGIN,
                                              GeneralConstant.RABBIT_MQ_PASSWORD,
                                              GeneralConstant.RABBTI_MQ_TOPIC_TO_SERVER);

        var consumer = new RabbitMQConsumer(GeneralConstant.SERVER_IP,
                                            GeneralConstant.RABBTI_MQ_PORT,
                                            GeneralConstant.RABBIT_MQ_LOGIN,
                                            GeneralConstant.RABBIT_MQ_PASSWORD,
                                            topicFromServer);

        var firstMessage = new FirstMessage()
        {
            Action = GeneralConstant.GeneralServerActions.Start,
            TopicFromServer = topicFromServer,
        };
        publisher.SendMessage(firstMessage.ToJson());

        while (true)
        {
            var json = consumer.GetMessage();
            Console.WriteLine(json);

            if (json == GeneralConstant.SUCCESS)
            {

            }

            var lastMessage = new FirstMessage()
            {
                Action = GeneralConstant.GeneralServerActions.Finish,
                TopicFromServer = topicFromServer,
            };
            publisher.SendMessage(lastMessage.ToJson());
            return;
        }
    }
}