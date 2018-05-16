
using System;
using TelegramBot.Service;

namespace TelegramBot
{
    class Program
    {
        private static string botToken = "сюда вставить токен своего бота";
        
        static void Main(string[] args)
        {
            IService service = new SqliteService();
            BotMainWork bot = new BotMainWork(service);
            bot.Run(botToken);

            Console.ReadKey();  //без этой строки сразу вылетает из программы. Где туплю? 
        }
    }
}