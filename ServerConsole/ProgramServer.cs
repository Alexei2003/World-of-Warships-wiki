using GeneralClasses;
using RabbitMQ;

internal class ProgramServer
{
    private static void Main(string[] args)
    {
        var consumer = new RabbitMQConsumer(GeneralConstant.SERVER_IP, GeneralConstant.RABBTI_MQ_PORT, GeneralConstant.RABBIT_MQ_LOGIN, GeneralConstant.RABBIT_MQ_PASSWORD, GeneralConstant.RABBTI_MQ_TOPIC_TO_SERVER);
       
        var publishers = new Dictionary<string, RabbitMQPublisher>();

        while (true)
        {
            var json = consumer.GetMessage();
            Console.WriteLine(json);


        }
    }
}