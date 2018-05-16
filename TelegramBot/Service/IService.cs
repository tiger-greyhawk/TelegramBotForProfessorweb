using System.Collections.Generic;
using TelegramBot.Model;

namespace TelegramBot.Service
{
    public interface IService
    {
        //int Save<T>(T t);
        int Save(BotMessage messageToSave);
        int Save(HelpMainSection sectionToSave);
        int Save(User user);
        BotMessage GetMessageById(int id);
        List<HelpMainSection> GetHelpMainSectionByName(string name);
        List<SectionMenu> GetSectionMenus();
        List<SectionMenu> GetSectionMenuByColumn(int column);
    }
}