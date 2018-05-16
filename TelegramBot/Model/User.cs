using SQLite;

namespace TelegramBot.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [MaxLength(50), NotNull]
        public string FirstName { get; set; }
        [MaxLength(50), NotNull]
        public int TelegramId { get; set; }
        [MaxLength(50)]
        public bool IsBot { get; set; }
        [MaxLength(50)]
        public string LanguageCode { get; set; }
        [MaxLength(50), NotNull]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        
    }
}