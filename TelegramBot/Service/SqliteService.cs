using System;
using System.Collections.Generic;
using SQLite;
using TelegramBot.Model;

namespace TelegramBot.Service
{
    public class SqliteService : IService
    {
        private string bdName = "base.db";

        public SqliteService()
        {
            CreateDB();
        }
        
        public int Save(BotMessage messageToSave)
        {
            using (SQLiteConnection sql = new SQLiteConnection(bdName))
            {
                int id = sql.Insert(messageToSave);
                string sqlString = @"select last_insert_rowid()";
                int lastId = (int)sql.ExecuteScalar<long>(sqlString);
                return lastId;
            }
        }
        
        public int Save(HelpMainSection sectionToSave)
        {
            using (SQLiteConnection sql = new SQLiteConnection(bdName))
            {
                int id = sql.Insert(sectionToSave);
                string sqlString = @"select last_insert_rowid()";
                int lastId = (int)sql.ExecuteScalar<long>(sqlString);
                return lastId;
            }
        }

        public int Save(User userToSave)
        {
            using (SQLiteConnection sql = new SQLiteConnection(bdName))
            {
                int id = sql.Insert(userToSave);
                string sqlString = @"select last_insert_rowid()";
                int lastId = (int)sql.ExecuteScalar<long>(sqlString);
                return lastId;
            }
        }

        public List<SectionMenu> GetSectionMenus()
        {
            using (SQLiteConnection sql = new SQLiteConnection(bdName))
            {
                //return sql.Get<Message>(pk:id);
                //string query = "Select * From SectionMenu where Name='" + name+"'";
                List<SectionMenu> result = sql.Table<SectionMenu>().ToList();
                return result;
            }
        }

        public List<SectionMenu> GetSectionMenuByColumn(int column)
        {
            using (SQLiteConnection sql = new SQLiteConnection(bdName))
            {
                //return sql.Get<Message>(pk:id);
                string query = "Select * From SectionMenu where Column='" + column+"'";
                List<SectionMenu> result = sql.Query<SectionMenu>(query);
                return result;
            }
        }
        
        public List<HelpMainSection> GetHelpMainSectionByName(string name)
        {
            using (SQLiteConnection sql = new SQLiteConnection(bdName))
            {
                //return sql.Get<Message>(pk:id);
                string query = "Select * From HelpMainSection where Name='" + name+"'";
                List<HelpMainSection> result = sql.Query<HelpMainSection>(query);
                return result;
            }
        }
        
        public BotMessage GetMessageById(int id)
        {
            using (SQLiteConnection sql = new SQLiteConnection(bdName))
            {
                //return sql.Get<Message>(pk:id);
                return sql.Find<BotMessage>(pk:id);
            }
        }
        
        private void CreateDB()
        {


            //using (SQLiteConnection sql = new SQLiteConnection(bdName, true))
            using (SQLiteConnection sql = new SQLiteConnection(bdName))
            {
                sql.CreateTable<BotMessage>();
                sql.CreateTable<User>();
                sql.CreateTable<HelpMainSection>();
                sql.CreateTable<SectionMenu>();
            }

        }
    }
}