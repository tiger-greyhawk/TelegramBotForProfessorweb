using SQLite;

namespace TelegramBot.Model
{
    public class BotMessage
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }

        public int UserId { get; set; }

        [MaxLength(50), NotNull]
        public string Body { get; set; }
        [MaxLength(50)] 
        public string Image { get; set; }

    }
}