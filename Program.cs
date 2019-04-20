using System;
using CloudDoorCs.Backend;
using CloudDoorCs.Local;

namespace CloudDoorCs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting CloudDoor Bot...");
            var bot = new Bot(
                new BackendService(new Uri("http://localhost:7071")),
                new FileService()
            );

            try {
                bot.Start();
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                //TODO: implement some retry strategy here
            }
        }
    }
}
