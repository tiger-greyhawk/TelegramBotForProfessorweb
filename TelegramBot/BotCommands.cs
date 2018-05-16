using Telegram.Bot.Types;
using TelegramBot.Model;
using TelegramBot.Service;

namespace TelegramBot
{
    /***
     * От этого класса откажемся - команды надо по-другому обрабатывать
     */
    public class BotCommands
    {
        private readonly IService _service;

        public BotCommands(IService service)
        {
            _service = service;
        }

        public BotMessage Command(Message message)
        {
            BotMessage result = new BotMessage();
            result.Body = "Переделать нах!!!";
            string command = message.Text.Substring(1);
            if (command.StartsWith("помощь") )
            {
                result.Body = "Доступные команды:\r\n" +
                              "/показать [№]\r\n" +
                              "/добавить [текст сообщения]\r\n" +
                              "/правила\r\n";
            }
            if (command.StartsWith("показать") )
            {
                int id;
                int.TryParse(command.Split("показать")[1], out id);
                result = _service.GetMessageById(id);
                if (result != null)
                    return result;
                else 
                {
                    result = new BotMessage();
                    result.Body = "Сообщение под номером " + id + " не найдено";
                    return result;
                }
            }

            if (command.StartsWith("добавить"))
            {
                BotMessage messageToSave = new BotMessage();
                Model.User user = new Model.User();
                user.FirstName = message.From.FirstName;
                user.TelegramId = message.From.Id;
                user.LastName = message.From.LastName;
                user.Id = _service.Save(user);
                messageToSave.UserId = user.Id;
                messageToSave.Body = message.Text.Split("добавить")[1];
                int messageId = _service.Save(messageToSave);

                //service.Save()
                if (messageId != 0)
                    result.Body = "Сообщение добавлено под номером " + messageId;

            }

            if (command.StartsWith("WPF"))
            {
                //result.Body = "Справки по WPF пока нет";
                result.Body = _service.GetHelpMainSectionByName(command)[0].Body;
            }

            return result;
        }
    }
}