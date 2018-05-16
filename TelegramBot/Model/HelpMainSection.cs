using SQLite;

namespace TelegramBot.Model
{
    /***
     * Класс не нужен. Надо сделать новый класс и его уже привязывать к SectionMenu
     */
    public class HelpMainSection
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [MaxLength(50), NotNull]
        public string Name { get; set; }
        [MaxLength(10), NotNull]
        public int Column { get; set; }
        [MaxLength(500), NotNull]
        public string Body { get; set; }
    }
}