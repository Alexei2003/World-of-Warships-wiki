using GeneralClasses;
using GeneralClasses.Data;
using RabbitMQ;
using System.Text.Json;

internal class ProgramClient
{
    private static void Main(string[] args)
    {
        var topicFromServer = GeneralConstant.RABBTI_MQ_TOPIC_TO_CLIENT +"1";
        var publisher = new RabbitMQPublisher(GeneralConstant.SERVER_IP, GeneralConstant.RABBTI_MQ_PORT, GeneralConstant.RABBIT_MQ_LOGIN, GeneralConstant.RABBIT_MQ_PASSWORD, GeneralConstant.RABBTI_MQ_TOPIC_TO_SERVER);
        var consumer = new RabbitMQConsumer(GeneralConstant.SERVER_IP, GeneralConstant.RABBTI_MQ_PORT, GeneralConstant.RABBIT_MQ_LOGIN, GeneralConstant.RABBIT_MQ_PASSWORD, topicFromServer);

        var startMessage = new StartMessage()
        {
            Action = GeneralConstant.GeneralServerActions.Start,
            TopicFromServer = topicFromServer,
        };
        publisher.SendMessage(startMessage.ToJson());

        while (true)
        {
            var json = consumer.GetMessage();  
            Console.WriteLine(json);


        }
    }
}