namespace EventHubTester
{
    using Microsoft.Azure.EventHubs;
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.EventHubs.Processor;
    using System.Collections.Generic;
    using EventHubTester.FileDealing;
    using Newtonsoft.Json;

    class Program
    {
        private static EventHubClient eventHubClient;
        private const string EhConnectionString = "Endpoint=sb://zikuneventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=s0zEU54FUGqah27rZIlWi7PFeisUgxOm1O2CGQih4oA=";
        private const string EhEntityPath = "eventhubtest";
        private const string StorageContainerName = "zikunblobcontainer";
        private const string StorageAccountName = "zikunehstorages";
        private const string StorageAccountKey = "kQEAzrFp4kzOLpJzawWSHGVBkR1+Npfv+PSz/hB+DMZdUcd4eXo4/44voBKJN4Lrvw6KmFwiPa2/Blq95p/LJg==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        //week ~> 10min
        public static readonly int timeDiffDelayTimes = 1;
        static void Main(string[] args)
        {
            FileSorting.SortFileByTimeStamp(args[0], args[1]);
            //MainAsync(args).GetAwaiter().GetResult();
        }
        private static async Task MainReceiverAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            var eventProcessorHost = new EventProcessorHost(
                EhEntityPath,
                PartitionReceiver.DefaultConsumerGroupName,
                EhConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press ENTER to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
        private static async Task MainAsync(string[] args)
        {
            // Creates an EventHubsConnectionStringBuilder object from the connection string, and sets the EntityPath.
            // Typically, the connection string should have the entity path in it, but this simple scenario
            // uses the connection string from the namespace.
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
            {
                EntityPath = EhEntityPath
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            await SendMessagesToEventHub(args[0]);

            await eventHubClient.CloseAsync();

            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }
        // Creates an event hub client and sends 100 messages to the event hub.
        private static async Task SendMessagesToEventHub(string jsonFilePath)
        {
            var buildMsgs = LoadMsgs(jsonFilePath);
            if (buildMsgs == null) return ;
            var prevDateTime = buildMsgs[0].timeStamp;
            foreach (var buildMsg in buildMsgs)
            {
                var delay = (int)(buildMsg.timeStamp.Subtract(prevDateTime).TotalMilliseconds / timeDiffDelayTimes);
                var msg = JsonConvert.SerializeObject(buildMsg, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                try
                {
                    Console.WriteLine($"Sending message: {msg}");
                    await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(msg)));
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
                }
                Console.WriteLine($"Delay: {delay} ms");
                prevDateTime = buildMsg.timeStamp;
                await Task.Delay(delay);
            }
        }
        private static List<BuildMsgTemplate> LoadMsgs(string jsonFilePath)
        {
            var content = LocalFileAccess.ReadFile(jsonFilePath);
            if (content == null) return null;
            return JsonConvert.DeserializeObject<List<BuildMsgTemplate>>(content);
        }
    }
}
