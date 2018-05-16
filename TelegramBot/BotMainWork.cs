using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Model;
using TelegramBot.Service;

namespace TelegramBot
{

    
    public class BotMainWork
    {
        private readonly BackgroundWorker _backgroundWorker;
        private readonly IService _service;
        //private readonly IWebProxy _proxy;
        private ReplyKeyboardMarkup rkm = new ReplyKeyboardMarkup();
        
        public BotMainWork(IService service)
        {
            this._service = service;
            //_proxy = new HttpToSocks5Proxy("127.0.0.1", 9150);
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
        }


        
        async void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker; 
            var key = e.Argument as String; 
            try
            {
                TelegramBotClient Bot = new TelegramBotClient(key);//, _proxy);
                BotMessageParser parser = new BotMessageParser(_service);
                await Bot.SetWebhookAsync("");
                //Bot.SetWebhook(""); 
                int offset = 0;
                while (true)
                {
                    
                    Update[] updates = await Bot.GetUpdatesAsync(offset);

                    foreach(var update in updates)
                    {
                        Message message = update.Message;
                        //if (message.ReplyToMessage != null) message.Text = "/"+(message.ReplyToMessage.Text) +" " + message.Text;
                        if (message.Text != null)
                        if (message.Text.StartsWith("/меню"))
                        {
                            //PopulateMenu();

                            rkm.Selective = true;
                            rkm.Keyboard =
                                new KeyboardButton[][]
                                {
                                    PopulateMenu2(from section in _service.GetSectionMenuByColumn(0) select section.Name),
                                    PopulateMenu2(from section in _service.GetSectionMenuByColumn(1) select section.Name),
                                    PopulateMenu2(from section in _service.GetSectionMenuByColumn(2) select section.Name)
                                };
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "меню", ParseMode.Default, false, false, 0, rkm );
                        }
                        else
                        {
                            BotMessage messageToSend = parser.Parse(message);
                            await Bot.SendTextMessageAsync(message.Chat.Id, messageToSend.Body);
                        }
                        //Bot.SendTextMessageAsync(message.Chat.Id, messageToSend.Body);
                        offset = update.Id + 1;
                    }
                }
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }
        
        public void Run(string token)
        {
            if (_backgroundWorker.IsBusy != true)
            {
                _backgroundWorker.RunWorkerAsync(token); 
            }
        }

        private KeyboardButton[] PopulateMenu2(IEnumerable<string> buttons)
        {
            KeyboardButton[] keyboardButtons = new KeyboardButton[buttons.Count()];
            int i = 0;
            foreach (string text in buttons)
            {
                keyboardButtons[i] = new KeyboardButton("/"+text);
                keyboardButtons.Append(new KeyboardButton(text));
                i++;
            }

            return keyboardButtons;
        }
        
        private void PopulateMenu()
        {
            rkm.Keyboard = 
                new KeyboardButton[][]
                {
                    new KeyboardButton[]
                    {
                        new KeyboardButton("/Помощь"),
                        new KeyboardButton("/code"),
                        new KeyboardButton("/WPF"),
                        new KeyboardButton("/WinForms"),
                        new KeyboardButton("/Другое")
                    },

                    new KeyboardButton[]
                    {
                        new KeyboardButton("/Главная")
                    },

                    new KeyboardButton[]
                    {
                        new KeyboardButton("3-1"),
                        new KeyboardButton("3-2"),
                        new KeyboardButton("3-3")
                    }
                };
        }
    }
}