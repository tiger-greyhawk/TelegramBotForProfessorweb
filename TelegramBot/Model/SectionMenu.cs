using SQLite;

namespace TelegramBot.Model
{
    public class SectionMenu
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [MaxLength(50), NotNull, Unique]
        public string Name { get; set; }
        [MaxLength(10), NotNull]
        public int Column { get; set; }
    }
}