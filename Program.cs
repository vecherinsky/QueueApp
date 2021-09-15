using System;
using System.Threading.Tasks;
using global::Microsoft.WindowsAzure.Storage;
using global::Microsoft.WindowsAzure.Storage.Queue;

namespace QueueApp
{
    class Program
    {
        private const string ConnectionString = "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=vecherstorage;AccountKey=fz5wCKL46Rf231bokSSKGvDF3PVOTQWmyKWGhCRa1JuiprBmvoag8KoJ5ugg52OOGloNlhnjwq6uYX/vZbm6sg==";

        static async Task Main(string[] args)
        {
            if (args.Length > 0) 
            {
                string value = String.Join(" ", args);
                await SendArticleAsync(value);
                Console.WriteLine($"Sent: {value}");
            }
            else
            {
                string value = await ReceiveArticleAsync();
                Console.WriteLine($"Received {value}");
            }
        }

        static CloudQueue GetQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            return queueClient.GetQueueReference("newsqueue");
        }

        static async Task SendArticleAsync(string newsMessage)
        {
            CloudQueue clQueue = GetQueue();

            bool exists = await clQueu.ExistsAsync();
            if (exists)
            {
            bool createdQueue = await clQueue.CreateIfNotExistsAsync();
            if (createdQueue) Console.WriteLine("The queue of news articles was created.");
            }

            CloudQueueMessage clQueueMes = new CloudQueueMessage(newsMessage);
            await clQueue.AddMessageAsync(clQueueMes);
        }

        static async Task<string> ReceiveArticleAsync()
        {
            CloudQueue clQueu = GetQueue();
            bool exists = await clQueu.ExistsAsync();
            if (exists)
            {
                CloudQueueMessage clQueueMes = await clQueu.GetMessageAsync();
                if (clQueueMes != null)
                {
                    string clQueueMesString = clQueueMes.AsString;
                    await clQueu.DeleteMessageAsync(clQueueMes);
                    return clQueueMesString;
                }
            }

            return "<queue empty or not created>";
        }
    }
}
