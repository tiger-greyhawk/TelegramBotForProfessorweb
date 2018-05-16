using Telegram.Bot.Types;
using TelegramBot.Model;
using TelegramBot.Service;

namespace TelegramBot
{
    public class BotMessageParser
    {
        private readonly IService _service;

        public BotMessageParser(IService service)
        {
            _service = service;
        }

        public BotMessage Parse(Message message)
        {
            BotMessage result = new BotMessage();
            BotCommands botCommands;
                if (message != null) 
                    if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                    {
                        if (message.Text.StartsWith("/"))
                        {
                            botCommands = new BotCommands(_service);
                            result = botCommands.Command(message);
                        }

                        else
                        {
                            botCommands = new BotCommands(_service);
                            result = botCommands.Command(message);
                        }

                    }
                    else result.Body = "Непонятно о.0";
            return result;
        }
    }
}