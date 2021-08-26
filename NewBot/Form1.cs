using NewBot.Models.Model;
using NewBot.Models.Controller;
using NewBot.Models.CustomModel;
using NewBot.Security.Extensions.CallBacks;
using static NewBot.Models.CustomModel.CallBackModel;
using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
namespace NewBot
{
    public partial class Form1 : Form
    {
        [Obsolete]
        public Form1()
        {
            InitializeComponent();
        }

        #region References

        public readonly int Admin = 1127927726;
        public TelegramBotClient bot;
        List<string> statuslist = new List<string>() { "admin", "Admin", "owner", "Owner", "Member", "Administrator", "administrator", "Creator" };
        DbController controller = new DbController();
        public Regex AgentCheck = new Regex(@"^/start _z(\d{3})", RegexOptions.IgnoreCase);
        public Regex ChatCheck = new Regex(@"^/start chat_(\d)", RegexOptions.IgnoreCase);
        public Regex CheckLink = new Regex(@"^t.me/", RegexOptions.IgnoreCase);
        public Regex CheckLink2 = new Regex(@"^https://t.me", RegexOptions.IgnoreCase);
        #region Text Injections
        public Regex sensitive = new Regex(@"^[a-zA-Z0-9 / ) ( \\ | @ # ا آ ب پ ت ث ج چ ح خ د ذ ر ز ژ س ش ص ض ط ظ ع غ ف ق ک گ ل م ن و ه ی]", RegexOptions.IgnoreCase);
        public string[] illegalChars =
            {"delete","where", "update", "select", "insert", "and", "or", "'", "create", "primary kry", "from", "alter", "add", "distinct", "set", "truncate","as","order by","asc","desc", "between", "not", "limit" ,"is null", "drop", "drop column", "drop database", "drop", "database", "table","group by", "having", "in", "join", "union", "union all", "exists", "like", "case"};
        #endregion
        #endregion

        #region Public Variables
        public string ForceJoinChannelID = "@testbottel_qwxp";
        public string[] Categories = new[] { "برنامه نویسی", "برنامه نویسی پایتون", "برنامه نویسی سی شارپ", "کامپیوتر", "برنامه نویس", "کمک آموزشی", "برنامه نویسی", "وبسایت", "تولید محتوا", "نگارش", "نویسندگی", "تایپ", "ترجمه", "زبان", "معماری", "طراحی", "گرافیک", "لوگو" };
        public string Tagg;
        #endregion

        #region Keyboards
        #region Buttons

        #region Public Buttons
        public static KeyboardButton[][] mainBTN = new[] { new[] { new KeyboardButton("ثبت نام") } };
        public static KeyboardButton[][] CancelBTN = new[] { new[] { new KeyboardButton("انصراف") } };
        public static KeyboardButton[][] startproccessBTN = new[] { new[] { new KeyboardButton("قوانین را خواندم و میپذیرم") } };
        public static KeyboardButton[][] RegisteredUsersBTN = new[]
        {
            new[] {
                new KeyboardButton("اگهی پروژه"),
                new KeyboardButton("اگهی استخدام"),
            },
            new[]
            {
                new KeyboardButton("اگهی تبلیغاتی")
            },
            new[]
            {
                new KeyboardButton("لینک دعوت"),
                new KeyboardButton("پروفایل من")
            }
        };
        public static KeyboardButton[][] AdsBTN = new[]
        {
            new[] {new KeyboardButton("تبلیغ کانال") },
            new[] {new KeyboardButton("تبلیغ گروه") },
            new[] {new KeyboardButton("تبلیغ کسب و کار") },
            new[] {new KeyboardButton("بازگشت به صفحه اصلی") }
        };
        #endregion

        #region Profile Buttons

        #region Select Buttons
        public static KeyboardButton[][] ProfileMenuBTN = new[]
        {
            new[] {
                new KeyboardButton("اگهی های من"),
            },
            new[]
            {
                new KeyboardButton("بخش مالی")
            },
            new[]
            {
                new KeyboardButton("برگشت به صفحه اصلی")
            }
        };
        public static KeyboardButton[][] SelectTypeBTN = new[]
        {
            new[] {
                new KeyboardButton("پروژه"),
                new KeyboardButton("استخدام"),
            },
            new[]
            {
                new KeyboardButton("تبلیغات")
            },
            new[]
            {
                new KeyboardButton("انتشار مجدد اگهی")
            },
            new[]
            {
                new KeyboardButton("بازگشت به پروفایل")
            }
        };
        #endregion

        #region Fiscal Department
        public static KeyboardButton[][] FiscalMenuBTN = new[]
        {
            new[] {
                new KeyboardButton("اطلاعات حساب من"),
            },
            new[]
            {
                new KeyboardButton("افزایش موجودی")
            },
            new[]
            {
                new KeyboardButton("خروج از بخش مالی")
            }
        };
        #endregion

        #region Profile Project Buttons
        public static KeyboardButton[][] ProjectHndlrBTN = new[]
        {
            new[] {
                new KeyboardButton("مشاهده تمام پروژه ها")
            },
            new[]
            {
                new KeyboardButton("غیر فعال کردن پروژه")
            },
            new[]
            {
                new KeyboardButton("برگشت به اگهی ها")
            }
        };
        public static KeyboardButton[][] SelectProjectBTN = new[]
        {
            new[] {
                new KeyboardButton("انتخاب از لیست پروژه ها")
            },
            new[] {
                new KeyboardButton("غیرفعال کردن اگهی پروژه از طریق هشتک")
            },
            new[]
            {
                new KeyboardButton("انصراف")
            }
        };
        public static KeyboardButton[][] CancelDisableProjectBTN = new[] { new[] { new KeyboardButton("انصراف از غیر فعال سازی پروژه") } };
        #endregion

        #region Profile Hire Buttons
        public static KeyboardButton[][] HireHndlrBTN = new[]
        {
            new[] {
                new KeyboardButton("مشاهده تمام اگهی های استخدام")
            },
            new[]
            {
                new KeyboardButton("غیر فعال کردن اگهی استخدام")
            },
            new[]
            {
                new KeyboardButton("برگشت")
            }
        };
        public static KeyboardButton[][] SelectHireBTN = new[]
        {
            new[] {
                new KeyboardButton("انتخاب از لیست اگهی های استخدام")
            },
            new[] {
                new KeyboardButton("غیرفعال کردن اگهی استخدام از طریق هشتک")
            },
            new[]
            {
                new KeyboardButton("منصرف شدم")
            }
        };
        public static KeyboardButton[][] CancelDisableHireProjectBTN = new[] { new[] { new KeyboardButton("انصراف از غیر فعال سازی اگهی استخدام") } };
        #endregion

        #region Profile Ads Buttons
        public static KeyboardButton[][] AdsOptionsButtons = new[] { new[] { new KeyboardButton("بازگشت از قسمت تبلیغات"), new KeyboardButton("مشاهده لیست تبلیغات") } };
        #endregion
        #endregion

        #endregion

        #region Reply Keyboard Markups
        #region Public RKMs
        public ReplyKeyboardMarkup startproccessRKM = new ReplyKeyboardMarkup { Keyboard = startproccessBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        public ReplyKeyboardMarkup CancelRKM = new ReplyKeyboardMarkup { Keyboard = CancelBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        public ReplyKeyboardMarkup RegisteredUsersRKM = new ReplyKeyboardMarkup { Keyboard = RegisteredUsersBTN, OneTimeKeyboard = false, ResizeKeyboard = true, Selective = true };
        public ReplyKeyboardMarkup mainRKM = new ReplyKeyboardMarkup { Keyboard = mainBTN, OneTimeKeyboard = false, ResizeKeyboard = true, Selective = true };
        public ReplyKeyboardMarkup contactBTN = new ReplyKeyboardMarkup(keyboardRow: new[] { KeyboardButton.WithRequestContact("ارسال شماره"), }, resizeKeyboard: true, oneTimeKeyboard: true);
        public ReplyKeyboardMarkup ADSRKM = new ReplyKeyboardMarkup { Keyboard = AdsBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        #endregion

        #region Profile
        #region Select
        public ReplyKeyboardMarkup ProfileMenuRKM = new ReplyKeyboardMarkup { Keyboard = ProfileMenuBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        public ReplyKeyboardMarkup SelectTypeRKM = new ReplyKeyboardMarkup { Keyboard = SelectTypeBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        #endregion

        #region Fiscal Department
        public ReplyKeyboardMarkup FiscalMenuRKM = new ReplyKeyboardMarkup { Keyboard = FiscalMenuBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        #endregion

        #region Project
        public ReplyKeyboardMarkup ProjectHndlrRKM = new ReplyKeyboardMarkup { Keyboard = ProjectHndlrBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        public ReplyKeyboardMarkup SelectProjectRKM = new ReplyKeyboardMarkup { Keyboard = SelectProjectBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        public ReplyKeyboardMarkup CancelDisableProjectRKM = new ReplyKeyboardMarkup { Keyboard = CancelDisableProjectBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        #endregion

        #region Hire
        public ReplyKeyboardMarkup HireHndlrRKM = new ReplyKeyboardMarkup { Keyboard = HireHndlrBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        public ReplyKeyboardMarkup SelectHireRKM = new ReplyKeyboardMarkup { Keyboard = SelectHireBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        public ReplyKeyboardMarkup CancelDisableHireProjectRKM = new ReplyKeyboardMarkup { Keyboard = CancelDisableHireProjectBTN, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        #endregion

        #region Ads
        public ReplyKeyboardMarkup ProfileAdsRKM = new ReplyKeyboardMarkup { Keyboard = AdsOptionsButtons, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
        #endregion
        #endregion

        #endregion

        #region Inline Keyboard Markup
        public InlineKeyboardMarkup JoinchannelIKM = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithUrl("عضویت", "https://t.me/testbottel_qwxp"), } });
        public InlineKeyboardMarkup supportIKM = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithUrl("تماس با پشتیبانی", "https://t.me/zoodadposhtiban"), InlineKeyboardButton.WithUrl("مطالعه قوانین", "https://t.me/ZoodAdHelp/22") } });
        #endregion
        #endregion

        #region Control Handlers
        [Obsolete]
        public void button1_Click(object sender, EventArgs e)
        {
            bot = new TelegramBotClient("1427790516:AAHDPOAZ_N1md540KJ8xTaD_9u0yPS4iE8o");
            bot.OnMessage += Bot_OnMessageAsync;
            bot.OnCallbackQuery += Bot_OnCallbackQuery;
            bot.StartReceiving();
            label1.Text = "Bot Started";
            label1.ForeColor = Color.Green;
        }
        #endregion

        #region Event Handler
        [Obsolete]
        private async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            #region AcceptPolicy
            //string[] PolicyStatus = e.CallbackQuery.Data.Split(':');
            //if (PolicyStatus[0] =="AP")
            //{
            //bool Policy = PolicyStatus[1] == "True" ? true : false;

            //}
            #endregion

            #region DeActive 

            #region DeActive Project ByUser
            if (e.CallbackQuery.Data.StartsWith("deactiveProject"))
            {
                #region DB Proccess
                string[] projtag = e.CallbackQuery.Data.Split(':');
                var getproj = controller.GetUserProject(new Project() { uid = e.CallbackQuery.From.Id.ToString(), ProjectId = projtag[1] });
                if (getproj.disable != null || getproj.disable != true)
                {
                    var disbl = controller.UpdateUserProjectByTag(new Project() { uid = e.CallbackQuery.From.Id.ToString(), ProjectId = projtag[1], disable = true });
                    #region Project Text
                    string projectTXT = $@"#پروژه_تکمیل_شد

کد اگهی:#{projtag[1]}
دسته بندی:{getproj.category}

توضیحات:

❌ پروژه واگذار شده است❌";
                    #endregion

                    #region Channel Proccess
                    await bot.EditMessageReplyMarkupAsync(chatId: ForceJoinChannelID, messageId: Convert.ToInt32(getproj.ChnnlMssgID));
                    await bot.EditMessageTextAsync(chatId: ForceJoinChannelID, messageId: Convert.ToInt32(getproj.ChnnlMssgID), projectTXT);
                    #endregion

                    #region User Proccess
                    await bot.EditMessageTextAsync(chatId: e.CallbackQuery.Message.Chat.Id, messageId: e.CallbackQuery.Message.MessageId, $"{e.CallbackQuery.Message.Text}\n\n❌واگذار شد❌");
                    await bot.EditMessageReplyMarkupAsync(chatId: e.CallbackQuery.Message.Chat.Id, messageId: e.CallbackQuery.Message.MessageId);
                    await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, $"پروژه شما با کد #{projtag[1]} با موفقیت غیر فعال شد");
                    #endregion
                }
                else
                {
                    await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "این پروژه از واگذار شده است");
                }
                #endregion

            }
            #endregion

            #region DeActive HireProject ByUser
            if (e.CallbackQuery.Data.StartsWith("deactiveHire"))
            {
                #region DB Proccess
                string[] hiretag = e.CallbackQuery.Data.Split(':');
                controller.UpdateHireByTag(new HireList() { employeeID = e.CallbackQuery.From.Id.ToString(), ProjectID = hiretag[1], Disable = true });
                var getHireProject = controller.GetHireProject(new HireList() { employeeID = e.CallbackQuery.From.Id.ToString(), ProjectID = hiretag[1] });
                #endregion

                #region Project Text
                string HireTXT = $@"#استخدام_تکمیل_شد

کد اگهی:#{hiretag[1]}

توضیحات:

❌ پروژه واگذار شده است❌";
                #endregion

                #region Channel Proccess
                await bot.EditMessageReplyMarkupAsync(chatId: ForceJoinChannelID, messageId: Convert.ToInt32(getHireProject.ChnnlMssgID));
                await bot.EditMessageTextAsync(chatId: ForceJoinChannelID, messageId: Convert.ToInt32(getHireProject.ChnnlMssgID), HireTXT);
                #endregion

                #region User Proccess
                await bot.EditMessageTextAsync(chatId: e.CallbackQuery.Message.Chat.Id, messageId: e.CallbackQuery.Message.MessageId, $"{e.CallbackQuery.Message.Text}\n\n❌واگذار شد❌");
                await bot.EditMessageReplyMarkupAsync(chatId: e.CallbackQuery.Message.Chat.Id, messageId: e.CallbackQuery.Message.MessageId);
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, $"اگهی استخدام شما با کد #{hiretag[1]} با موفقیت غیر فعال شد");
                #endregion
            }
            #endregion

            #region Remove Projects ByUser
            //remove*project*{sendToUser.MessageId}*{Tagg}*{msg.Chat.Id}*{msg.MessageId}
            if (e.CallbackQuery.Data.StartsWith("rm") || e.CallbackQuery.Data.StartsWith("$="))
            {
                string[] procData, adminChatIdAndMessageId;
                string type, messageID, projectTag, chatID, adminMessageId;
                if (e.CallbackQuery.Data.StartsWith("$="))
                {
                    var readCallBack = await e.CallbackQuery.Data.CallBackReaderAsync();
                    procData = readCallBack.Split('*');
                    type = procData[1];
                    messageID = procData[2];
                    projectTag = procData[3];
                    chatID = string.IsNullOrEmpty(procData[4]) ? "" : procData[4];
                    adminMessageId = string.IsNullOrEmpty(procData[5]) ? "" : procData[5];
                    adminChatIdAndMessageId = adminMessageId.Contains("-") ? adminMessageId.Split('-') : new String[2];
                }
                else
                {
                    procData = e.CallbackQuery.Data.Split('*');
                    type = procData[1];
                    messageID = procData[2];
                    projectTag = procData[3];
                    chatID = string.IsNullOrEmpty(procData[4]) ? "" : procData[4];
                    adminMessageId = string.IsNullOrEmpty(procData[5]) ? "" : procData[5];
                    adminChatIdAndMessageId = adminMessageId.Contains("-") ? adminMessageId.Split('-') : new String[2];
                }


                switch (type)
                {
                    #region Project
                    case "project":
                        var delProject = await controller.CancelUserProjectAsync(new Project() { uid = e.CallbackQuery.From.Id.ToString(), ProjectId = projectTag });
                        if (delProject)
                        {
                            controller.UpdateUser(new user() { uID = e.CallbackQuery.From.Id.ToString(), projectstep = 0 });
                            await bot.EditMessageReplyMarkupAsync(e.CallbackQuery.Message.Chat.Id,
                                Convert.ToInt32(messageID));
                            await bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, Convert.ToInt32(messageID));
                            await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "کاربر گرامی \nاگهی شما پاک شد\nاگر از ثبت اگهیه پاک شده بیش از 48 ساعت میگذرد پیام ان در ربات پاک نمیشود\nولی دکمه ان پاک میشود");
                            EditAdminMessage(new MessageDataViewModel()
                            {
                                customStautsText = "Project Canceled By User",
                                ChatID = chatID,
                                MessageID = Convert.ToInt32(adminMessageId),
                                MessageText = e.CallbackQuery.Message.Text
                            }, ContentType.Project, ContentStatus.none, ContentMessageType.Text);
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "این اگهی قبلا پاک شده است ", replyMarkup: supportIKM);
                        }
                        break;
                    #endregion

                    #region Hire
                    case "hire":
                        var delHire = await controller.CancelUserHireProjectAsync(new HireList() { employeeID = e.CallbackQuery.From.Id.ToString(), ProjectID = projectTag });
                        if (delHire)
                        {
                            controller.UpdateUser(new user() { uID = e.CallbackQuery.From.Id.ToString(), projectstep = 0, ishireing = false });
                            await bot.EditMessageReplyMarkupAsync(e.CallbackQuery.Message.Chat.Id, Convert.ToInt32(messageID));
                            await bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, Convert.ToInt32(messageID));
                            await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "کاربر گرامی \nاگهی شما پاک شد\nاگر از ثبت اگهیه پاک شده بیش از 48 ساعت میگذرد پیام ان در ربات پاک نمیشود\nولی دکمه ان پاک میشود");
                            EditAdminMessage(new MessageDataViewModel()
                            {
                                customStautsText = "Project Canceled By User",
                                ChatID = chatID,
                                MessageID = Convert.ToInt32(adminMessageId),
                                MessageText = e.CallbackQuery.Message.Text
                            }, ContentType.Hire, ContentStatus.none, ContentMessageType.Text);
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "این اگهی قبلا پاک شده است ", replyMarkup: supportIKM);
                        }
                        break;
                    #endregion

                    #region Ads
                    case "ad":
                        switch (chatID)
                        {
                            #region Group
                            case "gp":
                                var deleteGpAds = await controller.AdsAsync(new AdsModel()
                                {
                                    AdsType = AdsType.Group,
                                    AdsOperation = AdsOperation.Delete,
                                    AdsGroup = new AdsGroup() { uID = e.CallbackQuery.From.Id.ToString(), ProjectID = projectTag }
                                });
                                if (Convert.ToBoolean(deleteGpAds.OutPut))
                                {
                                    await CancelUserProjectAsync(new CancelationModel() { EArgs = e, MessageId = messageID });
                                    //await bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, Convert.ToInt32(messageID));
                                    //await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "کاربر گرامی \nاگهی شما پاک شد\nاگر از ثبت اگهیه پاک شده بیش از 48 ساعت میگذرد پیام ان در ربات پاک نمیشود\nولی دکمه ان پاک میشود");
                                }
                                else
                                {
                                    await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "این تبلیغ قبلا پاک شده است ", replyMarkup: supportIKM);
                                }
                                break;
                            #endregion

                            #region Channel
                            case "ch":
                                var deleteChAds = await controller.AdsAsync(new AdsModel()
                                {
                                    AdsType = AdsType.Channel,
                                    AdsOperation = AdsOperation.Delete,
                                    AdsChannel = new AdsChannel() { uID = e.CallbackQuery.From.Id.ToString(), ProjectID = projectTag }
                                });
                                if (Convert.ToBoolean(deleteChAds.OutPut))
                                {
                                    await CancelUserProjectAsync(new CancelationModel() { EArgs = e, MessageId = messageID });
                                    //await bot.EditMessageReplyMarkupAsync(e.CallbackQuery.Message.Chat.Id, Convert.ToInt32(messageID));
                                    //await bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, Convert.ToInt32(messageID));
                                    //await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "کاربر گرامی \nاگهی شما پاک شد\nاگر از ثبت اگهیه پاک شده بیش از 48 ساعت میگذرد پیام ان در ربات پاک نمیشود\nولی دکمه ان پاک میشود");
                                }
                                else
                                {
                                    await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "این تبلیغ قبلا پاک شده است ", replyMarkup: supportIKM);
                                }
                                break;
                            #endregion

                            #region Business

                            #region WithOutImage
                            case "bsNI":
                                var deleteBusinessAds = await controller.AdsAsync(new AdsModel()
                                {
                                    AdsType = AdsType.Business,
                                    AdsOperation = AdsOperation.Delete,
                                    AdsBusiness = new AdsBusiness() { uID = e.CallbackQuery.From.Id.ToString(), ProjectID = projectTag }
                                });
                                if (Convert.ToBoolean(deleteBusinessAds.OutPut))
                                {
                                    await CancelUserProjectAsync(
                                        new CancelationModel()
                                        {
                                            EArgs = e,
                                            MessageId = messageID,
                                            CancelForAdminModel = new CancelForAdminModel()
                                            {
                                                ChatId = adminChatIdAndMessageId[0],
                                                MessageId = adminChatIdAndMessageId[1],
                                                MediaType = MediaType.Text
                                            }
                                        });
                                }
                                else
                                {
                                    await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "این تبلیغ قبلا پاک شده است ", replyMarkup: supportIKM);
                                }
                                break;
                            #endregion

                            #region WithImage
                            case "bsI":
                                var deleteAdsWithImage = await controller.AdsAsync(new AdsModel()
                                {
                                    AdsOperation = AdsOperation.Delete,
                                    AdsType = AdsType.Business,
                                    AdsBusiness = new AdsBusiness()
                                    {
                                        uID = e.CallbackQuery.From.Id.ToString(),
                                        ProjectID = projectTag
                                    }
                                });
                                if (Convert.ToBoolean(deleteAdsWithImage.OutPut))
                                {
                                    await controller.DeleteImageAsync(new Models.Model.Image() { uID = e.CallbackQuery.From.Id.ToString(), ProjectID = projectTag });
                                    await CancelUserProjectAsync(
                                        new CancelationModel()
                                        {
                                            EArgs = e,
                                            MessageId = messageID,
                                            CancelForAdminModel = new CancelForAdminModel()
                                            {
                                                ChatId = adminChatIdAndMessageId[0],
                                                MessageId = adminChatIdAndMessageId[1],
                                                MediaType = MediaType.Photo
                                            }
                                        });
                                }
                                else
                                {
                                    await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "این تبلیغ قبلا پاک شده است ", replyMarkup: supportIKM);
                                }
                                break;
                                #endregion

                                #endregion
                        }

                        break;
                    #endregion

                    default:
                        break;
                }
            }

            #endregion
            #endregion

            #region Category Handler
            if (e.CallbackQuery.Data.Contains('>'))
            {
                string[] info1 = e.CallbackQuery.Data.Split('>');
                if (info1[0] == "User")
                {
                    string[] CallBackData = e.CallbackQuery.Data.Split(';');
                    string[] UserInformation = CallBackData[0].Split(':');
                    Tagg = UserInformation[1];
                    var UserProj = controller.GetUserProject(new Project() { uid = e.CallbackQuery.From.Id.ToString(), ProjectId = UserInformation[1] });
                    if (UserProj == null)
                    {
                        string[] info = UserInformation[0].Split('>');
                        var h = controller.UpdateUserProject(new Project() { ProjectId = UserInformation[1], uid = info[1], category = CallBackData[1].Replace(' ', '_') });
                        var t = controller.UpdateUser(new user { uID = e.CallbackQuery.From.Id.ToString(), projectstep = 2 });
                        if (h && t)
                        {
                            await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "دسته بندی تایید شد\nلطفا متن اگهی خود را ارسال کنید");
                        }
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "دسته بندی شما از قبل انتخاب شده است،میتوانید بعدا انرا تعقیر دهید");
                    }
                }
            }
            #endregion

            #region Admin Controller
            //UserID:ProjectID;Status

            var AdminList = controller.GetAllAdmins();
            if (AdminList.Any(p => p.uID == e.CallbackQuery.From.Id) && !e.CallbackQuery.Data.StartsWith("deactive") && !e.CallbackQuery.Data.StartsWith("$=") && !e.CallbackQuery.Data.StartsWith("rm"))
            {
                try
                {
                    #region Push Ads List

                    if (e.CallbackQuery.Data.StartsWith("ADL:"))
                    {
                        string[] splitid = e.CallbackQuery.Data.Remove(0, 4).Split(';');
                        List<int> idlist = new List<int>();
                        string txtToSend = "";
                        for (int i = 0; i < splitid.Length - 1; i++)
                        {
                            if (splitid[i] != null || splitid[i] != "")
                            {
                                idlist.Add(Convert.ToInt32(splitid[i]));
                            }
                        }

                        controller.UpdateAdsByIDList(idlist);
                        var adses = controller.GetAdsByIDList(idlist);
                        foreach (var ads in adses)
                        {
                            controller.UpdateUser(new user() { uID = ads.uID, adsStep = 0 });
                            txtToSend += $"\n{ads.discription}\n{ads.link}";
                        }

                        EditAdminMessage(new MessageDataViewModel()
                        {
                            ChatID = e.CallbackQuery.Message.Chat.Id.ToString(),
                            MessageID = e.CallbackQuery.Message.MessageId,
                            MessageText = e.CallbackQuery.Message.Text,
                        },
                            ContentType.none, ContentStatus.none, ContentMessageType.custom);
                        await bot.SendTextMessageAsync(ForceJoinChannelID, txtToSend);
                    }

                    #endregion

                    #region Variables For Data Proccess

                    string[] AdminCall = e.CallbackQuery.Data.Split(';');
                    string[] CallInfo = AdminCall[0].Split(':');
                    bool isAds = AdminCall[1].StartsWith("Ad") ? true : false;
                    InlineKeyboardMarkup Accept = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("درخواست همکاری",
                                $"t.me/ratheropanelbot?start=chat_{CallInfo[0]}"),
                        }
                    });
                    var GetUser = controller.GetUser(new user() { uID = CallInfo[0] });
                    var checkforfree = controller.GetAgent(new agent() { agentuid = CallInfo[0] });
                    bool CanPublish = false;

                    #endregion

                    #region Project

                    if (!AdminCall[1].StartsWith("H") && isAds != true)
                    {
                        #region Accept

                        if (AdminCall[1] == "Accept")
                        {
                            if (GetUser.ProjectChance > 0)
                            {
                                controller.UpdateUser(new user()
                                { uID = CallInfo[0], ProjectChance = GetUser.ProjectChance - 1 });
                                CanPublish = true;
                            }
                            else
                            {
                                if (checkforfree != null)
                                {
                                    if (checkforfree.FreeBalance > 0)
                                    {
                                        controller.UpdateAgent(new agent()
                                        { agentuid = CallInfo[0], FreeBalance = checkforfree.FreeBalance - 1 });
                                        CanPublish = true;
                                    }

                                }
                            }

                            if (CanPublish)
                            {
                                await bot.SendTextMessageAsync(CallInfo[0], "اگهی شما توسط ادمین تایید شد",
                                    replyMarkup: RegisteredUsersRKM);
                                EditAdminMessage(new MessageDataViewModel()
                                {
                                    ChatID = e.CallbackQuery.Message.Chat.Id.ToString(),
                                    MessageID = e.CallbackQuery.Message.MessageId,
                                    MessageText = e.CallbackQuery.Message.Text,
                                },
                                    ContentType.Project, ContentStatus.Accepted, ContentMessageType.Text);
                                var getproj = controller.GetUserProject(new Project()
                                { ProjectId = CallInfo[1], uid = CallInfo[0] });

                                #region ProjectText

                                string tx = $@"#پروژه

کد اگهی:{getproj.ProjectId}

دسته بندی:\#{getproj.category}

توضیحات:{getproj.dicription}";
                                string FinalProject =
                                    $"#پروژه \nکد اگهی: #{getproj.ProjectId} \n دسته بندی: #{getproj.category} \nتوضیحات:\n {getproj.dicription}";

                                #endregion

                                var sendtochannel = await bot.SendTextMessageAsync(ForceJoinChannelID, FinalProject,
                                    replyMarkup: Accept);
                                controller.UpdateUser(new user() { uID = CallInfo[0], projectstep = 0 });
                                controller.UpdateUserProjectByTag(new Project()
                                {
                                    uid = CallInfo[0],
                                    ProjectId = CallInfo[1],
                                    ChnnlMssgID = sendtochannel.MessageId.ToString()
                                });
                                controller.ConfirmUserProject(new Project()
                                { Checked = true, ProjectId = CallInfo[1], uid = CallInfo[0] });
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(CallInfo[0], "بازم مثلا درگاهه پرداخته",
                                    replyMarkup: RegisteredUsersRKM);
                            }

                        }

                        #endregion

                        #region Reject

                        if (AdminCall[1] == "Reject")
                        {
                            await bot.SendTextMessageAsync(CallInfo[0],
                                "اگهی شما توسط ادمین رد شد \nلطفا قوانین را مطالعه نموده و سپس اگهی خود را مجدد ارسال نمایید",
                                replyMarkup: RegisteredUsersRKM);
                            EditAdminMessage(new MessageDataViewModel()
                            {
                                ChatID = e.CallbackQuery.Message.Chat.Id.ToString(),
                                MessageID = e.CallbackQuery.Message.MessageId,
                                MessageText = e.CallbackQuery.Message.Text,
                            },
                                ContentType.Project, ContentStatus.Rejected, ContentMessageType.Text);
                            controller.ConfirmUserProject(new Project()
                            { Checked = false, ProjectId = CallInfo[1], uid = CallInfo[0] });
                            controller.UpdateUser(new user() { uID = CallInfo[0], projectstep = 0 });
                        }

                        #endregion

                        #region Block

                        if (AdminCall[1] == "Block")
                        {
                            EditAdminMessage(new MessageDataViewModel()
                            {
                                ChatID = e.CallbackQuery.Message.Chat.Id.ToString(),
                                MessageID = e.CallbackQuery.Message.MessageId,
                                MessageText = e.CallbackQuery.Message.Text,
                            },
                                ContentType.Project, ContentStatus.Blocked, ContentMessageType.Text);
                            controller.UpdateUser(new user() { uID = CallInfo[0], IsBanned = true });
                            await bot.SendTextMessageAsync(CallInfo[0], "شما توسط ادمین بن شدید");
                        }

                        #endregion

                    }

                    #endregion

                    #region Hire

                    if (AdminCall[1].StartsWith("H") && isAds != true)
                    {
                        #region Accept

                        if (AdminCall[1] == "HAccept")
                        {
                            if (GetUser.HireChance > 0)
                            {
                                controller.UpdateUser(new user()
                                { uID = CallInfo[0], HireChance = GetUser.HireChance - 1 });
                                CanPublish = true;
                            }
                            else
                            {
                                if (checkforfree != null)
                                {
                                    if (checkforfree.FreeBalance > 0)
                                    {
                                        controller.UpdateAgent(new agent()
                                        { agentuid = CallInfo[0], FreeBalance = checkforfree.FreeBalance - 1 });
                                        CanPublish = true;
                                    }
                                }
                            }

                            if (CanPublish)
                            {
                                EditAdminMessage(new MessageDataViewModel()
                                {
                                    ChatID = e.CallbackQuery.Message.Chat.Id.ToString(),
                                    MessageID = e.CallbackQuery.Message.MessageId,
                                    MessageText = e.CallbackQuery.Message.Text,
                                },
                                    ContentType.Hire, ContentStatus.Accepted, ContentMessageType.Text);
                                await bot.SendTextMessageAsync(CallInfo[0],
                                     "درخواست استخدام شما توسط ادمین تایید و در کانال قرار داده شد",
                                     replyMarkup: RegisteredUsersRKM);

                                var gethire = controller.GetHireProject(new HireList()
                                { ProjectID = CallInfo[1], employeeID = CallInfo[0] });
                                controller.UpdateUser(new user()
                                { uID = CallInfo[0], projectstep = 0, ishireing = false });

                                #region FinalText

                                string FinalText = $@"#استخدام
کد اگهی:{gethire.ProjectID}

توضیحات:
{gethire.discription}";

                                #endregion

                                var SNDToCHannel = await bot.SendTextMessageAsync(ForceJoinChannelID, FinalText,
                                    replyMarkup: Accept);
                                controller.UpdateHireByTag(new HireList()
                                {
                                    employeeID = e.CallbackQuery.From.Id.ToString(),
                                    ProjectID = CallInfo[1],
                                    ChnnlMssgID = SNDToCHannel.MessageId.ToString()
                                });
                                controller.ConfirmHireProject(new HireList()
                                { @checked = true, ProjectID = CallInfo[1], employeeID = CallInfo[0] });
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(CallInfo[0], "الان مثلا این درگاهه پرداخته",
                                     replyMarkup: RegisteredUsersRKM);
                            }
                        }

                        #endregion

                        #region Reject

                        if (AdminCall[1] == "HReject")
                        {
                            EditAdminMessage(new MessageDataViewModel()
                            {
                                ChatID = e.CallbackQuery.Message.Chat.Id.ToString(),
                                MessageID = e.CallbackQuery.Message.MessageId,
                                MessageText = e.CallbackQuery.Message.Text,
                            },
                                ContentType.Hire, ContentStatus.Rejected, ContentMessageType.Text);
                            controller.ConfirmHireProject(new HireList()
                            { @checked = false, ProjectID = CallInfo[1], employeeID = CallInfo[0] });
                            controller.UpdateUser(new user() { uID = CallInfo[0], projectstep = 0, ishireing = false });
                            await bot.SendTextMessageAsync(CallInfo[0],
                                "اگهی استخدامی شما توسط ادمین رد شد \nلطفا قوانین را مطالعه نموده و سپس اگهی خود را مجدد ارسال نمایید",
                                replyMarkup: RegisteredUsersRKM);
                        }

                        #endregion

                        #region Block

                        if (AdminCall[1] == "HBlock")
                        {
                            EditAdminMessage(new MessageDataViewModel()
                            {
                                ChatID = e.CallbackQuery.Message.Chat.Id.ToString(),
                                MessageID = e.CallbackQuery.Message.MessageId,
                                MessageText = e.CallbackQuery.Message.Text,
                            },
                                ContentType.Hire, ContentStatus.Blocked, ContentMessageType.Text);
                            await bot.SendTextMessageAsync(CallInfo[0], "شما توسط ادمین بن شدید");
                            controller.UpdateUser(new user() { uID = CallInfo[0], IsBanned = true });
                        }

                        #endregion
                    }

                    #endregion

                    #region Ads

                    if (isAds)
                    {
                        #region Accept

                        if (AdminCall[1] == "AdAccept")
                        {
                            if (GetUser.AdsChance > 0)
                            {
                                controller.UpdateUser(new user()
                                { uID = CallInfo[0], AdsChance = GetUser.AdsChance - 1 });
                                CanPublish = true;
                            }
                            else
                            {
                                if (checkforfree != null)
                                {
                                    if (checkforfree.FreeBalance > 0)
                                    {
                                        controller.UpdateAgent(new agent()
                                        { agentuid = CallInfo[0], FreeBalance = checkforfree.FreeBalance - 2 });
                                        CanPublish = true;
                                    }
                                }
                            }

                            if (CanPublish)
                            {
                                await bot.SendTextMessageAsync(CallInfo[0], "اگهی تبلیغاتی شما توسط ادمین تایید شد", replyMarkup: RegisteredUsersRKM);
                                controller.UpdateUser(new user() { uID = CallInfo[0], adsStep = 0 });
                                EditAdminMessage(new MessageDataViewModel()
                                {
                                    ChatID = e.CallbackQuery.Message.Chat.Id.ToString(),
                                    MessageID = e.CallbackQuery.Message.MessageId,
                                    MessageText = e.CallbackQuery.Message.Text == null
                                            ? e.CallbackQuery.Message.Caption
                                            : e.CallbackQuery.Message.Text,
                                }, ContentType.Ads, ContentStatus.Accepted, CallInfo[1].StartsWith("none>") ? ContentMessageType.Text : ContentMessageType.Caption);

                                if (CallInfo[1].StartsWith("none>"))
                                {
                                    string[] adsplit = CallInfo[1].Split('>');
                                    var getAd = await controller.AdsAsync(new AdsModel()
                                    {
                                        AdsOperation = AdsOperation.Get,
                                        AdsType = AdsType.Business,
                                        AdsBusiness = new AdsBusiness() { uID = CallInfo[0], ProjectID = adsplit[1] }
                                    });
                                    var results = getAd.OutPut as AdsBusiness;
                                    await bot.SendTextMessageAsync(ForceJoinChannelID, $"#تبلیغات\n{results.Discription}");
                                }
                                else
                                {
                                    var AdsImage = await controller.GetImageAsync(new Models.Model.Image() { uID = CallInfo[0], UniqueID = CallInfo[1], ProjectID = Tagg });
                                    await bot.SendPhotoAsync(ForceJoinChannelID, AdsImage.FileID, $"#تبلیغات\n{AdsImage.Discription}");
                                }
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(CallInfo[0], "یا امازاده درگاهه پرداخت",
                                      replyMarkup: RegisteredUsersRKM);
                            }

                        }

                        #endregion

                        #region Reject

                        if (AdminCall[1] == "AdReject")
                        {
                            EditAdminMessage(new MessageDataViewModel()
                            {
                                ChatID = e.CallbackQuery.Message.Chat.Id.ToString(),
                                MessageID = e.CallbackQuery.Message.MessageId,
                                MessageText = e.CallbackQuery.Message.Text == null
                                        ? e.CallbackQuery.Message.Caption
                                        : e.CallbackQuery.Message.Text,
                            },
                                ContentType.Ads, ContentStatus.Rejected,
                                msgType: CallInfo[1].StartsWith("none>")
                                    ? ContentMessageType.Text
                                    : ContentMessageType.Caption);
                            controller.UpdateUser(new user() { uID = CallInfo[0], adsStep = 0 });
                            await bot.SendTextMessageAsync(CallInfo[0],
                                  "اگهی استخدامی شما توسط ادمین رد شد \nلطفا قوانین را مطالعه نموده و سپس اگهی خود را مجدد ارسال نمایید",
                                  replyMarkup: RegisteredUsersRKM);
                        }

                        #endregion

                        #region Block

                        if (AdminCall[1] == "AdBlock")
                        {
                            EditAdminMessage(new MessageDataViewModel()
                            {
                                ChatID = e.CallbackQuery.Message.Chat.Id.ToString(),
                                MessageID = e.CallbackQuery.Message.MessageId,
                                MessageText = e.CallbackQuery.Message.Text == null
                                        ? e.CallbackQuery.Message.Caption
                                        : e.CallbackQuery.Message.Text,
                            },
                                ContentType.Ads, ContentStatus.Blocked,
                                msgType: CallInfo[1].StartsWith("none>")
                                    ? ContentMessageType.Text
                                    : ContentMessageType.Caption);
                            await bot.SendTextMessageAsync(CallInfo[0], "شما توسط ادمین بن شدید");
                            controller.UpdateUser(new user() { uID = CallInfo[0], IsBanned = true });
                        }

                        #endregion
                    }

                    #endregion
                }
                catch (Exception x) when (x.Message.ToLower().Contains("null"))
                {
                    foreach (var admin in AdminList)
                    {
                        await bot.SendTextMessageAsync(admin.uID,
                            $"مشگلی در اگهیه ارسال شده وجود دارد :\n {x.Message}");
                    }
                }
                catch (NullReferenceException nullex) when (nullex.Message.ToLower().Contains("null") ||
                                                            (nullex.Data.ToString().ToLower().Contains("null")))
                {
                    foreach (var admin in AdminList)
                    {
                        await bot.SendTextMessageAsync(admin.uID,
                            $"Null Refrence On CallBack Catched :\n {nullex.Message}");
                    }
                }
                catch
                {
                    foreach (var admin in AdminList)
                    {
                        await bot.SendTextMessageAsync(admin.uID, $"Something Catched On CallBack,ReCheckSource:OCBQ-1TC--Last[C]");
                    }
                }
            }
            #endregion
        }
        [Obsolete]
        public async void Bot_OnMessageAsync(object sender, MessageEventArgs e)
        {
            try
            {
                Invoke(new Action(() => { label1.Text = e.Message.Text; }));

                AdminMessageProccessor(e);
                if (MessageProccess(e.Message.Text, e.Message.From.Id.ToString(), e.Message.Type))
                {
                    controller.InsertNewUser(
                        new user()
                        {
                            uID = e.Message.From.Id.ToString(),
                            AdsChance = 0,
                            adsStep = 0,
                            finishedregister = false,
                            HireChance = 0,
                            ProjectChance = 0,
                            projectstep = 0,
                            registerstep = 0
                        });
                    var JoinStatus = await bot.GetChatMemberAsync(ForceJoinChannelID, e.Message.From.Id);
                    if (statuslist.Contains(JoinStatus.Status.ToString()))
                    {
                        var user = controller.GetUser(new user() { uID = e.Message.From.Id.ToString() });
                        if (e.Message.Type == MessageType.Text)
                        {
                            if (user.finishedregister == true)
                            {
                                RegisteredUsersMessageHandler(e, user);
                                if (user.projectstep > 0)
                                {
                                    ProjectRequestHandler(user, e);
                                }
                            }
                            switch (e.Message.Text)
                            {
                                case "/start":
                                    await bot.SendTextMessageAsync(e.Message.From.Id, "خوش امدید", replyMarkup: user.finishedregister == true ? RegisteredUsersRKM : mainRKM);
                                    break;
                                default:
                                    break;
                            }
                            #region Disable By Tag
                            try
                            {
                                var UserInfo = controller.GetUser(new user() { uID = e.Message.From.Id.ToString() });
                                switch (UserInfo.DisableStep)
                                {
                                    case 1:
                                        #region Disable Project
                                        #region Cancel
                                        if (e.Message.Text == "انصراف از غیر فعال سازی پروژه")
                                        {
                                            await bot.SendTextMessageAsync(e.Message.From.Id, "شما از غیرفعال سازی انصراف دادید", replyMarkup: ProfileMenuRKM);
                                            controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), DisableStep = 0 });
                                        }
                                        #endregion
                                        #region Disable
                                        else
                                        {
                                            string proccessTag = e.Message.Text.Contains('#') ? e.Message.Text.Replace('#', ' ').Trim() : e.Message.Text;
                                            var GetProject = controller.GetUserProject(new Project() { uid = e.Message.From.Id.ToString(), ProjectId = proccessTag });
                                            if (GetProject != null)
                                            {
                                                if (GetProject.disable != true)
                                                {
                                                    var disble = controller.UpdateUserProjectByTag(new Project() { uid = e.Message.From.Id.ToString(), disable = true, ProjectId = proccessTag });
                                                    if (disble)
                                                    {
                                                        #region Project Text
                                                        string projectTXT = $@"#پروژه_تکمیل_شد

کد اگهی:#{proccessTag}
دسته بندی:{GetProject.category}

توضیحات:

❌ پروژه واگذار شده است❌";
                                                        #endregion
                                                        #region Channel Proccess
                                                        await bot.EditMessageReplyMarkupAsync(chatId: ForceJoinChannelID, messageId: Convert.ToInt32(GetProject.ChnnlMssgID));
                                                        await bot.EditMessageTextAsync(chatId: ForceJoinChannelID, messageId: Convert.ToInt32(GetProject.ChnnlMssgID), projectTXT);
                                                        #endregion
                                                        #region User Proccess
                                                        await bot.SendTextMessageAsync(e.Message.From.Id, "پروژه شما با موفقیت غیرفعال شد:)", replyMarkup: ProfileMenuRKM);
                                                        controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), DisableStep = 0 });
                                                        #endregion
                                                    }
                                                    #region Else Statements

                                                    else
                                                    {
                                                        await bot.SendTextMessageAsync(e.Message.From.Id, "مشگلی پیش اومده لطفا با پشتیبانی تماس بگیرید", replyMarkup: RegisteredUsersRKM);
                                                    }
                                                }
                                                else
                                                {
                                                    await bot.SendTextMessageAsync(e.Message.From.Id, "این پروژه از قبل غیرفعال شده است", replyMarkup: CancelDisableProjectRKM);
                                                }
                                            }
                                            else
                                            {
                                                await bot.SendTextMessageAsync(e.Message.From.Id, "اوپس\nپروژه ای که دنبال هستین رو نتونستم پیدا کنم:(\nلطفا دوباره تلاش کنین");
                                            }
                                            #endregion
                                        }
                                        #endregion
                                        #endregion
                                        break;
                                    case 2:
                                        #region Disable Hire Project
                                        #region Cancel
                                        if (e.Message.Text == "انصراف از غیر فعال سازی اگهی استخدام")
                                        {
                                            controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), DisableStep = 0 });
                                            await bot.SendTextMessageAsync(e.Message.From.Id, "شما از غیرفعال سازی انصراف دادید", replyMarkup: ProfileMenuRKM);
                                        }
                                        #endregion
                                        #region Disable
                                        else
                                        {
                                            string proccessTag = e.Message.Text.Contains('#') ? e.Message.Text.Replace('#', ' ').Trim() : e.Message.Text;
                                            var GetHire = controller.GetHireProject(new HireList() { employeeID = e.Message.From.Id.ToString(), ProjectID = proccessTag });
                                            if (GetHire != null)
                                            {
                                                if (GetHire.Disable != true)
                                                {
                                                    var disble = controller.UpdateHireByTag(new HireList() { employeeID = e.Message.From.Id.ToString(), ProjectID = proccessTag, Disable = true });
                                                    if (disble)
                                                    {
                                                        #region Project Text
                                                        string HireTXT = $@"#استخدام_تکمیل_شد

کد اگهی:#{proccessTag}

توضیحات:

❌ پروژه واگذار شده است❌";
                                                        #endregion
                                                        #region Channel Proccess
                                                        await bot.EditMessageReplyMarkupAsync(chatId: ForceJoinChannelID, messageId: Convert.ToInt32(GetHire.ChnnlMssgID));
                                                        await bot.EditMessageTextAsync(chatId: ForceJoinChannelID, messageId: Convert.ToInt32(GetHire.ChnnlMssgID), HireTXT);
                                                        #endregion
                                                        #region User Proccess
                                                        await bot.SendTextMessageAsync(e.Message.From.Id, "اگهی استخدام شما با موفقیت غیرفعال شد:)", replyMarkup: ProfileMenuRKM);
                                                        controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), DisableStep = 0 });
                                                        #endregion
                                                    }
                                                    #region Else Statements
                                                    else
                                                    {
                                                        await bot.SendTextMessageAsync(e.Message.From.Id, "مشگلی پیش اومده لطفا با پشتیبانی تماس بگیرید", replyMarkup: RegisteredUsersRKM);
                                                    }
                                                }
                                                else
                                                {
                                                    await bot.SendTextMessageAsync(e.Message.From.Id, "این پروژه از قبل غیرفعال شده است");
                                                }
                                            }
                                            else
                                            {
                                                await bot.SendTextMessageAsync(e.Message.From.Id, "پروژه ای که دنبال ان میگردید پیدا نشد\nلطفا مجدد تلاش کنید");
                                            }
                                            #endregion
                                        }
                                        #endregion
                                        #endregion
                                        break;
                                    default:
                                        break;
                                }
                            }
                            catch (Exception)
                            {
                            }
                            #endregion

                            #region Send Hire Request
                            bool IsMatch = ChatCheck.IsMatch(e.Message.Text);
                            if (IsMatch)
                            {
                                string[] findEmployeeID = e.Message.Text.Split('_');
                                var t = await bot.SendTextMessageAsync(e.Message.From.Id,
                                    "لطفا پیام خود را برای کارفرما بنویسید،درصورت پذیرفته شدن درخواست شما توسط کارفرما به صورت خودکار چت ایجاد میشود",
                                    replyMarkup: new ForceReplyMarkup() { Selective = true });
                                controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), EmployeeID = findEmployeeID[1], sendtoEMPmessageID = t.MessageId.ToString() });
                            }
                            if (e.Message.ReplyToMessage != null)
                            {
                                if (e.Message.ReplyToMessage.Type == MessageType.Text)
                                {
                                    var userinfo = controller.GetUser(new user() { uID = e.Message.From.Id.ToString() });
                                    if (userinfo.sendtoEMPmessageID != null && e.Message.ReplyToMessage.MessageId.ToString() == userinfo.sendtoEMPmessageID)
                                    {
                                        var GetUserName = await bot.GetChatAsync(e.Message.From.Id);
                                        InlineKeyboardMarkup connect = new InlineKeyboardMarkup(new[] {
                                        new[] {
                                        InlineKeyboardButton.WithUrl("ارتباط", GetUserName.Username != null ? $"t.me/{GetUserName.Username}" : $"tg://openmessage?user_id={GetUserName.Id}"),
                                        }
                                    });
                                        await bot.SendTextMessageAsync(userinfo.EmployeeID,
                                            $"شما درخواست جدیدی برای همکاری دارید:\n\n{e.Message.Text}",
                                            replyMarkup: connect);
                                        await bot.SendTextMessageAsync(e.Message.From.Id, "پیام شما برای کارفرما ارسال شد", replyMarkup: ProfileMenuRKM);
                                        controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), sendtoEMPmessageID = "" });
                                    }
                                }
                            }
                            #endregion

                            CheckForSubMember(e.Message.Text, e.Message.From.Id.ToString());
                        }
                        AdsHandler(e, user);
                        MessageResponse(e.Message.Type == MessageType.Contact ? e.Message.Contact.PhoneNumber : e.Message.Text, e.Message.From.Id.ToString(), e.Message.Type);
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(e.Message.From.Id, "برای استفاده از ربات لطفا ابتدا در کانال ما عضو شوید", replyMarkup: JoinchannelIKM);
                    }
                }
            }
            catch (Exception ex)
            {
                ExeptionHandler($"OnMessage 1nd TryCatch Throwed.({ex.Message}\n\nStackTrace:[{ex.StackTrace}])", 58);
            }
        }
        #endregion

        #region Functions

        [Obsolete]
        public async Task CancelUserProjectAsync(CancelationModel info)
        {
            try
            {
                #region UserEditation
                await bot.EditMessageReplyMarkupAsync(info.EArgs.CallbackQuery.Message.Chat.Id, Convert.ToInt32(info.MessageId));
                await bot.DeleteMessageAsync(info.EArgs.CallbackQuery.Message.Chat.Id, Convert.ToInt32(info.MessageId));
                await bot.SendTextMessageAsync(info.EArgs.CallbackQuery.From.Id, "کاربر گرامی \nاگهی شما پاک شد\nاگر از ثبت اگهیه پاک شده بیش از 48 ساعت میگذرد پیام ان در ربات پاک نمیشود\nولی دکمه ان پاک میشود");
                #endregion

                #region AdminEditation
                if (info.CancelForAdminModel != null)
                {
                    await bot.EditMessageReplyMarkupAsync(chatId: info.CancelForAdminModel.ChatId, messageId: Convert.ToInt32(info.CancelForAdminModel.MessageId));
                    switch (info.CancelForAdminModel.MediaType)
                    {
                        case MediaType.Text:
                            await bot.EditMessageTextAsync(info.CancelForAdminModel.ChatId,
                                Convert.ToInt32(info.CancelForAdminModel.MessageId), "<i>This Post Has Been Canceled By User</i>", parseMode: ParseMode.Html);
                            break;
                        case MediaType.Photo:
                            await bot.EditMessageCaptionAsync(chatId: info.CancelForAdminModel.ChatId,
                                Convert.ToInt32(info.CancelForAdminModel.MessageId), "<i>This Post Has Been Canceled By User</i>", parseMode: ParseMode.Html);
                            break;
                        default:
                            break;
                    }
                }
                #endregion
            }
            catch (InvalidParameterException x)
            {
                await bot.SendTextMessageAsync(Admin, $"{x.Parameter}");
            }

        }
        public async void EditAdminMessage(MessageDataViewModel m, ContentType type, ContentStatus stat, ContentMessageType msgType)
        {
            try
            {
                await bot.EditMessageReplyMarkupAsync(chatId: m.ChatID, messageId: m.MessageID);
                switch (msgType)
                {
                    case ContentMessageType.Caption:
                        await bot.EditMessageCaptionAsync(chatId: m.ChatID, messageId: m.MessageID, $"{type} {stat}\n{m.MessageText}");
                        break;
                    case ContentMessageType.Text:
                        if (stat == ContentStatus.none)
                        {
                            await bot.EditMessageTextAsync(chatId: m.ChatID, messageId: m.MessageID, $"{m.customStautsText}\n{m.MessageText}");
                        }
                        else
                        {
                            await bot.EditMessageTextAsync(chatId: m.ChatID, messageId: m.MessageID, $"{type} {stat}\n{m.MessageText}");

                        }
                        break;
                    case ContentMessageType.custom:
                        await bot.EditMessageTextAsync(chatId: m.ChatID, messageId: m.MessageID, $"-----Pushed To Channel-----{m.MessageText}-----Pushed To Channel-----");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }
        }
        public bool SendChatAction(string id)
        {
            try
            {
                bot.SendChatActionAsync(id, ChatAction.Typing);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool MessageProccess(string Message, string id, MessageType t)
        {
            try
            {
                if (SendChatAction(id))
                {
                    if (t == MessageType.Text)
                    {
                        #region Text-I
                        if (sensitive.IsMatch(Message))
                        {
                            #region SQL-I 
                            string[] MessageSplit = Message.Split(' ');
                            int IllegalCount = 0;
                            foreach (var word in MessageSplit)
                            {
                                if (illegalChars.Contains(word))
                                {
                                    IllegalCount++;
                                    bot.SendTextMessageAsync(Admin, $"Illegal Character: [{word}]\nTargetUser: [{id}]");
                                }
                            }
                            if (IllegalCount > 0)
                            {
                                bot.SendTextMessageAsync(id, "Sql Injection Detected\n<b>Nice Try!</b>", parseMode: ParseMode.Html);
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                            #endregion
                        }
                        else
                        {
                            bot.SendTextMessageAsync(id, "لطفا از نماد ها و کاراکتر های غیر مجاز استفاده نکنید");
                            return false;
                        }
                        #endregion
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                ExeptionHandler($"Message Proccess 1nd TryCatch Throwed. [{exp}]", 88);
                return false;
            }

        }
        public async void ExeptionHandler(string exp, int Line = 0)
        {
            await bot.SendTextMessageAsync(Admin, $"ExepTion Handler\nCatched In Line[{Line}]\nThe Message:{exp}");
        }
        private List<List<InlineKeyboardButton>> GetInlineKeyboard(List<string> Buttons, user user)
        {
            #region Add Tag

            string projid = TagGenerator();
            while (true)
            {
                var Exists = controller.TagExists(new Project() { ProjectId = projid });
                if (Exists)
                {
                    projid = TagGenerator();
                }
                else
                {
                    break;
                }
            }
            #endregion

            #region Create KeyBoard
            var _keybrd = new List<List<InlineKeyboardButton>>();
            int add = 0;
            foreach (var button in Buttons)
            {
                if (add % 1 == 0)
                {
                    _keybrd.Add(new List<InlineKeyboardButton>());
                }
                _keybrd.Last().Add(button);
                for (int i = 0; i < _keybrd.Count; i++)
                {                                    //UserId:IdentityID;text
                    _keybrd[i].Last().CallbackData = $"User>{user.uID}:{projid};{button}";
                }
                add++;
            }
            #endregion

            return _keybrd;
        }
        [Obsolete]
        public async void BusinessHandler(MessageEventArgs a, user user)
        {
            try
            {
                #region WithOut Image
                if (a.Message.Type == MessageType.Text)
                {
                    if (a.Message.Text != "انصراف از تبلیغ")
                    {
                        if (a.Message.Text.Length > 10)
                        {
                            await controller.AdsAsync(new AdsModel()
                            {
                                AdsType = AdsType.Business,
                                AdsOperation = AdsOperation.Update,
                                AdsBusiness = new AdsBusiness()
                                {
                                    uID = a.Message.From.Id.ToString(),
                                    ProjectID = Tagg,
                                    Discription = a.Message.Text
                                }
                            });
                            controller.UpdateUser(new user() { uID = a.Message.From.Id.ToString(), adsStep = 0 });
                            await bot.SendTextMessageAsync(a.Message.From.Id, "بنر شما ثبت شد و پس از برسی در کانال قرار خواهد گرفت \n اگهی شما :", replyMarkup: ADSRKM);
                            var sendToUser = await bot.SendTextMessageAsync(a.Message.From.Id, $"{a.Message.Text}");
                            var adminResults = await SendToAdmins(ProjectMode.Ads, ButtonMode.AcceptBlockReject, new CallBackModel()
                            {
                                id = a.Message.From.Id.ToString(),
                                ProjectTosend = a.Message.Text,
                                StartIndex = "Ad",
                                Image = $"none>{Tagg}"
                            });
                            string callBack = await
                                $"rm*ad*{sendToUser.MessageId}*{Tagg}*bsNI*{adminResults.Chat.Id}-{adminResults.MessageId}".CallBackHandlerAsync();
                            var CancelationToken = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("انصراف", callBack) } });
                            await bot.EditMessageReplyMarkupAsync(sendToUser.Chat.Id, sendToUser.MessageId, CancelationToken);
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(a.Message.From.Id, "متن اگهی خیلی کوتاه است");
                        }
                    }
                    else
                    {
                        controller.UpdateUser(new user() { uID = a.Message.From.Id.ToString(), adsStep = 0 });
                        await bot.SendTextMessageAsync(a.Message.From.Id, "شما از تبلیغ انصراف دادید",
                            replyMarkup: ProfileMenuRKM);
                    }
                }
                #endregion

                #region With Image
                if (a.Message.Type == MessageType.Photo)
                {
                    if (a.Message.Caption != null && a.Message.Caption.Length > 10 && a.Message.Caption.Length < 299)
                    {
                        var pic = a.Message.Photo;
                        var selectpic = pic[0].Height * pic[0].Width > pic[1].Height * pic[1].Width ? pic[0] : pic[1];
                        await controller.AdsAsync(new AdsModel()
                        {
                            AdsType = AdsType.Business,
                            AdsOperation = AdsOperation.Update,
                            AdsBusiness = new AdsBusiness()
                            {
                                uID = a.Message.From.Id.ToString(),
                                ProjectID = Tagg,
                                Discription = a.Message.Caption,
                                PictureUID = selectpic.FileId
                            }
                        });
                        controller.UpdateUser(new user() { uID = a.Message.From.Id.ToString(), adsStep = 0 });
                        await controller.InsertNewImageAsync(new Models.Model.Image()
                        {
                            uID = a.Message.From.Id.ToString(),
                            UniqueID = selectpic.FileUniqueId,
                            FileID = selectpic.FileId,
                            ProjectID = Tagg,
                            Discription = a.Message.Caption
                        });
                        await bot.SendTextMessageAsync(a.Message.From.Id, "بنر تبلیغاتی شما ثبت شد و پس از برسی در کانال قرار خواهد گرفت \n اگهی شما:", replyMarkup: ADSRKM);
                        var sendToUser = await bot.SendPhotoAsync(a.Message.From.Id, selectpic.FileId, $"{a.Message.Caption}");
                        var sendToAdmin = await SendToAdmins(ProjectMode.Ads, ButtonMode.AcceptBlockReject, new CallBackModel() { id = a.Message.From.Id.ToString(), Image = selectpic.FileUniqueId, ProjectTosend = a.Message.Caption, StartIndex = "Ad" });
                        string callBack = await $"rm*ad*{sendToUser.MessageId}*{Tagg}*bsI*{sendToAdmin.Chat.Id}-{sendToAdmin.MessageId}".CallBackHandlerAsync();
                        var CancelationToken = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("انصراف", callBack) } });
                        await bot.EditMessageReplyMarkupAsync(sendToUser.Chat.Id, sendToUser.MessageId,
                            CancelationToken);
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(a.Message.From.Id, "متن اگهی نمیتواند خالی،بیشتر از 200حرف و یا کمتر از 10 حرف باشد");
                    }
                }
                #endregion
            }
            catch (Exception x)
            {
                await bot.SendTextMessageAsync(Admin, $"User:{a.Message.From.Id}\nBusinessHandler Exception:\n{x.Message}\n\n StackTrace: ``` {x.StackTrace} ```");
            }
        }
        [Obsolete]
        public async void AdminMessageProccessor(MessageEventArgs a)
        {
            try
            {
                var AdminsList = controller.GetAllAdmins();
                if (AdminsList.Any(p => p.uID == a.Message.From.Id))
                {
                    if (a.Message.Type == MessageType.Text)
                    {
                        if (a.Message.Text.Contains("/addlist"))
                        {
                            string[] adscount = a.Message.Text.Split(' ');
                            string ListOfAdsToPush = "#تبلیغات\n\n";
                            int counter = 0;
                            if (adscount.Length != 1)
                            {
                                string CallbackQuery = "";
                                bool b = int.TryParse(adscount[1], out int adsnumber);
                                adsnumber = b == true ? adsnumber : 7;
                                var getads = controller.GetUnPublishedAds();
                                foreach (var ads in getads)
                                {
                                    counter++;
                                    ListOfAdsToPush += $"\n<[{ads.ID}]:{ads.discription}\n{ads.link}>";
                                    CallbackQuery += $"{ads.ID};";
                                    if (counter == adsnumber)
                                    {
                                        break;
                                    }
                                }
                                InlineKeyboardMarkup AcceptListIKM = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("Push List", $"ADL:{CallbackQuery}"), } });
                                await bot.SendTextMessageAsync(a.Message.From.Id, $"لیست اماده به ارسال:\nتعداد:{counter}\n({ListOfAdsToPush})", replyMarkup: AcceptListIKM);
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(a.Message.From.Id, "Bad Command Syntax:\n x: [/addlist 5]");
                            }

                        }

                        if (a.Message.Text is "$key")
                        {

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        [Obsolete]
        public async void AdsHandler(MessageEventArgs e, user user)
        {
            try
            {
                switch (e.Message.Text)
                {
                    #region Group&Channel
                    case "تبلیغ گروه":
                    case "تبلیغ کانال":
                        if (user.adsStep == 0)
                        {
                            if (user.finishedregister == true)
                            {
                                #region Shared
                                await bot.SendTextMessageAsync(e.Message.From.Id, "لطفا متن اگهی تبلیغاتی خود را به صورت یک جمله ارسال کنید");
                                var gid = Guid.NewGuid();
                                Tagg = gid.ToString();
                                #endregion

                                #region Group
                                if (e.Message.Text == "تبلیغ گروه")
                                {
                                    controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), adsStep = 10 });
                                    await controller.AdsAsync(
                                        new AdsModel()
                                        {
                                            AdsOperation = AdsOperation.Insert,
                                            AdsType = AdsType.Group,
                                            AdsGroup = new AdsGroup() { uID = e.Message.From.Id.ToString(), ProjectID = gid.ToString() }
                                        });
                                }
                                #endregion

                                #region Channel
                                else
                                {
                                    controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), adsStep = 20 });
                                    await controller.AdsAsync(new AdsModel()
                                    {
                                        AdsOperation = AdsOperation.Insert,
                                        AdsType = AdsType.Channel,
                                        AdsChannel = new AdsChannel() { uID = e.Message.From.Id.ToString(), ProjectID = gid.ToString() }
                                    });
                                }
                                #endregion
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(e.Message.From.Id, "لطفا ابتدا در ربات ثبت نام کنید");
                            }
                        }
                        break;

                    #endregion

                    #region business
                    case "تبلیغ کسب و کار":
                        if (user.adsStep == 0)
                        {
                            if (user.finishedregister == true)
                            {
                                var gid = Guid.NewGuid();
                                Tagg = gid.ToString();
                                ReplyKeyboardMarkup cancelToken = new ReplyKeyboardMarkup { Keyboard = new[] { new[] { new KeyboardButton("انصراف از تبلیغ") } }, OneTimeKeyboard = true, ResizeKeyboard = true, Selective = true };
                                await bot.SendTextMessageAsync(e.Message.From.Id,
                                    "لطفا بنر تبلیغاتی خود را ارسال کنید\nبنر شما میتواند شما عکس به همراه متن و لینک باشد", replyMarkup: cancelToken);
                                await controller.AdsAsync(new AdsModel()
                                {
                                    AdsType = AdsType.Business,
                                    AdsOperation = AdsOperation.Insert,
                                    AdsBusiness = new AdsBusiness() { uID = e.Message.From.Id.ToString(), ProjectID = gid.ToString() }
                                });
                                controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), adsStep = 3 });
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(e.Message.From.Id, "لطفا ابتدا در ربات ثبت نام کنید");
                            }
                        }
                        break;
                    #endregion

                    #region CancelationToken
                    case "بازگشت به صفحه اصلی":
                        await bot.SendTextMessageAsync(e.Message.From.Id, "شما در صفحه اصلی هستید", replyMarkup: user.finishedregister == true ? RegisteredUsersRKM : mainRKM);
                        break;
                    #endregion

                    default:
                        break;
                }
                if (user.adsStep > 0)
                {
                    #region Groups&Channels Ads
                    if (e.Message.Type == MessageType.Text)
                    {
                        switch (user.adsStep)
                        {
                            #region Get Discription
                            case 20:
                            case 10:
                                if (e.Message.Text.Length < 50)
                                {
                                    #region Shared
                                    await bot.SendTextMessageAsync(e.Message.From.Id,
                                        "متن اگهی ذخیره شد\nلطفا لینک گروه یا کانال تلگرامی خود را ارسال کنید:",
                                        replyMarkup: new ForceReplyMarkup() { Selective = true });
                                    #endregion

                                    #region Group
                                    if (user.adsStep == 10)
                                    {
                                        await controller.AdsAsync(
                                            new AdsModel()
                                            {
                                                AdsType = AdsType.Group,
                                                AdsOperation = AdsOperation.Update,
                                                AdsGroup = new AdsGroup() { uID = e.Message.From.Id.ToString(), ProjectID = Tagg, Disciption = e.Message.Text }
                                            });
                                        controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), adsStep = 11 });
                                    }
                                    #endregion

                                    #region Channel
                                    else
                                    {
                                        await controller.AdsAsync(new AdsModel()
                                        {
                                            AdsType = AdsType.Channel,
                                            AdsOperation = AdsOperation.Update,
                                            AdsChannel = new AdsChannel() { uID = e.Message.From.Id.ToString(), ProjectID = Tagg, Disciption = e.Message.Text }
                                        });
                                        controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), adsStep = 21 });
                                    }
                                    #endregion
                                }
                                else
                                {
                                    await bot.SendTextMessageAsync(e.Message.From.Id, "متن اگهی خیلی طولانی است");
                                }
                                break;
                            #endregion

                            #region Get Link
                            case 21:
                            case 11:
                                try
                                {
                                    if (e.Message.Text.Length < 440)
                                    {
                                        #region Shared
                                        await bot.SendTextMessageAsync(e.Message.From.Id, "اگهی شما ثبت شد\nپس از برسی توسط ادمین در ساعت مشخص شده در کانال قرار داده خواهد شد", replyMarkup: RegisteredUsersRKM);
                                        #endregion
                                        //remove*project*{sendToUser.MessageId}*{Tagg}*{msg.Chat.Id}*{msg.MessageId}
                                        #region Group
                                        if (user.adsStep == 11)
                                        {
                                            await controller.AdsAsync(new AdsModel()
                                            {
                                                AdsType = AdsType.Group,
                                                AdsOperation = AdsOperation.Update,
                                                AdsGroup = new AdsGroup()
                                                {
                                                    uID = e.Message.From.Id.ToString(),
                                                    ProjectID = Tagg,
                                                    Link = e.Message.Text
                                                }
                                            });
                                            var getAds = await controller.AdsAsync(new AdsModel()
                                            {
                                                AdsType = AdsType.Group,
                                                AdsOperation = AdsOperation.Get,
                                                AdsGroup = new AdsGroup()
                                                { uID = e.Message.From.Id.ToString(), ProjectID = Tagg }
                                            });
                                            var results = getAds.OutPut == null ? new AdsGroup() : getAds.OutPut as AdsGroup;
                                            var sendToUser = await bot.SendTextMessageAsync(e.Message.From.Id, $"تبلیغ شما : \n {results.Disciption}\n{results.Link}");
                                            var CancelationToken = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("انصراف", $"rm*ad*{sendToUser.MessageId}*{Tagg}*gp* ") } });
                                            await bot.EditMessageReplyMarkupAsync(sendToUser.Chat.Id,
                                                sendToUser.MessageId, CancelationToken);
                                            controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), adsStep = 0 });
                                        }
                                        #endregion

                                        #region Channel
                                        else
                                        {
                                            await controller.AdsAsync(new AdsModel()
                                            {
                                                AdsType = AdsType.Channel,
                                                AdsOperation = AdsOperation.Update,
                                                AdsChannel = new AdsChannel()
                                                {
                                                    uID = e.Message.From.Id.ToString(),
                                                    ProjectID = Tagg,
                                                    Link = e.Message.Text
                                                }
                                            });
                                            var getAdsChannel = await controller.AdsAsync(new AdsModel()
                                            {
                                                AdsType = AdsType.Channel,
                                                AdsOperation = AdsOperation.Get,
                                                AdsChannel = new AdsChannel()
                                                {
                                                    uID = e.Message.From.Id.ToString(),
                                                    ProjectID = Tagg
                                                }
                                            });
                                            var results2 = getAdsChannel.OutPut == null
                                                ? new AdsChannel()
                                                : getAdsChannel.OutPut as AdsChannel;
                                            var sendToUser = await bot.SendTextMessageAsync(e.Message.From.Id, $"تبلیغ شما : \n {results2.Disciption}\n{results2.Link}");
                                            var cancelationToken = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("انصراف", $"rm*ad*{sendToUser.MessageId}*{Tagg}*ch* ") } });
                                            await bot.EditMessageReplyMarkupAsync(sendToUser.Chat.Id, sendToUser.MessageId, cancelationToken);
                                            controller.UpdateUser(new user() { uID = e.Message.From.Id.ToString(), adsStep = 0 });
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        await bot.SendTextMessageAsync(e.Message.From.Id, "لینک اگهی خیلی طولانی است");
                                    }
                                }
                                catch (Exception)
                                {

                                    throw;
                                }
                                break;
                            #endregion

                            #region RePublish Ads,Hire,Project
                            case 5:
                                #region Remove Tag
                                string GetTag = e.Message.Text;
                                if (e.Message.Text.StartsWith("#"))
                                {
                                    GetTag = GetTag.Replace("#", "");
                                }
                                #endregion

                                #region Tag Not Exists
                                var SearchForTag = controller.SearchTag(GetTag);
                                if (SearchForTag == null || SearchForTag.TagIdentifier == TagType.Null)
                                {
                                    await bot.SendTextMessageAsync(e.Message.From.Id, "تگی که به دنبال ان میگردید پیدا نشد", replyMarkup: ProfileMenuRKM);
                                    controller.UpdateUser(new user()
                                    { uID = e.Message.From.Id.ToString(), adsStep = 0 });
                                }
                                #endregion

                                #region Tag Exists
                                else
                                {
                                    await bot.SendTextMessageAsync(e.Message.From.Id, "پروژه شما پیدا شد و پس از تایید توسط ادمین در کانال قرار خواهد گرفت", replyMarkup: RegisteredUsersRKM);
                                    switch (SearchForTag.TagIdentifier)
                                    {
                                        #region Project
                                        case TagType.Project:
                                            await SendToAdmins(
                                                ProjectMode.Project,
                                                ButtonMode.AcceptBlockReject,
                                                new CallBackModel()
                                                {
                                                    id = e.Message.From.Id.ToString(),
                                                    tag = GetTag,
                                                    ProjectTosend = $"Category: [{SearchForTag.Category}#]\n Discription: [{SearchForTag.Discription}]"
                                                });
                                            controller.UpdateUser(new user()
                                            { uID = e.Message.From.Id.ToString(), adsStep = 0 });
                                            break;
                                        #endregion

                                        #region Hire
                                        case TagType.Hire:

                                            await SendToAdmins(
                                                ProjectMode.Hire,
                                                ButtonMode.AcceptBlockReject,
                                                new CallBackModel()
                                                {
                                                    id = e.Message.From.Id.ToString(),
                                                    tag = GetTag,
                                                    ProjectTosend = $"#استخدام \n کد اگهی :{SearchForTag.Tag}\n توضیحات:{SearchForTag.Discription}",
                                                    StartIndex = "H"
                                                });
                                            controller.UpdateUser(new user()
                                            { uID = e.Message.From.Id.ToString(), adsStep = 0 });
                                            break;
                                        #endregion

                                        #region Ads
                                        case TagType.Ads:
                                            await SendToAdmins(
                                                ProjectMode.Ads,
                                                ButtonMode.AcceptBlockReject,
                                                new CallBackModel()
                                                {
                                                    id = e.Message.From.Id.ToString(),
                                                    tag = GetTag,
                                                    ProjectTosend = $"#Ads \n {SearchForTag.Link} \n {SearchForTag.Discription}"
                                                });
                                            controller.UpdateUser(new user()
                                            { uID = e.Message.From.Id.ToString(), adsStep = 0 });
                                            break;

                                        #endregion
                                        default:
                                            break;
                                    }
                                }
                                #endregion
                                break;
                            #endregion
                            default:
                                break;
                        }
                    }
                    #endregion

                    #region business Ads
                    if (e.Message.Type == MessageType.Photo || e.Message.Type == MessageType.Text)
                    {
                        switch (user.adsStep)
                        {
                            case 3:
                                BusinessHandler(e, user);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(e.Message.From.Id, "بنر شما فقط میتواند عکس و متن باشد", replyMarkup: ProfileMenuRKM);
                    }

                    #endregion
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> LinkProccessAsync(string link)
        {
            if (link.StartsWith("@"))
            {
                var FindChat = await bot.GetChatAsync(link);
                string status = "";
                switch (FindChat.Type)
                {
                    case ChatType.Private:
                        status = "404";
                        break;
                    case ChatType.Channel:
                        status = "Channel";
                        break;
                    case ChatType.Group:
                        status = "Group";
                        break;
                    case ChatType.Supergroup:
                        status = "Super Group";
                        break;
                    case ChatType.Sender:
                        status = "404";
                        break;
                    default:
                        status = "404";
                        break;
                }

                return status;
            }
            return "404";


        }
        [Obsolete]
        public async void CheckForSubMember(string Text, string id)
        {
            try
            {
                int res = 0;
                bool Ismatch = AgentCheck.IsMatch(Text);
                if (Ismatch)
                {
                    string[] AgentNumber = Text.ToLower().Split('z');
                    int.TryParse(AgentNumber[1], out res);
                    try
                    {
                        if (controller.AddSubMember(res, id))
                        {
                            var usr = controller.GetUser(new user { PK = res });
                            var getagent = controller.GetAgent(new agent() { agentuid = usr.uID });
                            var sellUP = controller.UpdateAgent(new agent() { agentuid = usr.uID, sellcount = getagent.sellcount + 1, UsedBalance = getagent.UsedBalance + 1 });
                            if (controller.GetAgent(new agent() { agentuid = usr.uID }).UsedBalance == 10)
                            {
                                controller.UpdateAgent(new agent() { agentuid = usr.uID, UsedBalance = 0, FreeBalance = getagent.FreeBalance + 1 });
                            }
                            controller.UpdateUser(new user { agent = res, uID = id });
                            await bot.SendTextMessageAsync(id, $"شما با موفقیت دعوت شدید");
                            await bot.SendTextMessageAsync(usr.uID, "شما کاربری را با موفقیت دعوت نمودید");
                        }
                    }
                    catch (Exception exp)
                    {
                        ExeptionHandler($"Form1>>CheckForSubMember>>2nd TryCatch Throwed.[{exp.Message}]");
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Obsolete]
        public void MessageResponse(string Message, string uid, MessageType messageType)
        {

            #region Message Handler
            try
            {
                var UserInfo = controller.GetUser(new user { uID = uid });
                if (UserInfo.registerstep >= 0 && UserInfo.registerstep < 4)
                {
                    switch (UserInfo.registerstep)
                    {
                        #region StartProccess
                        case 0:
                            switch (Message)
                            {
                                case "ثبت نام":
                                    bot.SendTextMessageAsync(uid, "به بخش ثبت نام خوش امدید\nدرصورتی که قوانین ربات را مطالعه نمودید با زدن کلید پایین فرایند ثبت نام را شروع کنید)", replyMarkup: startproccessRKM);
                                    bot.SendTextMessageAsync(uid, "در صورتی که میخواهید قوانین را مطالعه کنید یا به پشتیبانی نیاز دارید میتوانید از دکمه های زیر استفاده کنید:", replyMarkup: supportIKM);
                                    break;
                                case "قوانین را خواندم و میپذیرم":
                                    var z = controller.UpdateUser(new user() { uID = uid, registerstep = UserInfo.registerstep += 1 });
                                    bot.SendTextMessageAsync(uid, "لطفا نام کامل خود را وارد کنید");
                                    break;
                                default:
                                    break;
                            }
                            break;
                        #endregion

                        #region Get User Name
                        case 1:
                            var UpdateName = controller.UpdateUser(new user { uID = uid, fullname = Message });
                            var UpdateAgentNameMember = controller.UpdateAgentMemberName(uid, UserInfo.PK, Message);
                            if (UpdateName && UpdateAgentNameMember)
                            {
                                controller.UpdateUser(new user { uID = uid, registerstep = UserInfo.registerstep += 1 });
                                bot.SendTextMessageAsync(uid, "نام شما با موفقیت ثبت شد \nلطفا جهت ادامه از طریق دکمه زیر شماره خود را ارسال کنید.", replyMarkup: contactBTN);
                            }
                            break;
                        #endregion

                        #region Get User Number
                        case 2:
                            if (messageType == MessageType.Contact)
                            {
                                try
                                {
                                    if (Message.Substring(0, 3) == "+98" || Message.Substring(0, 2) == "98")
                                    {
                                        string[] Number = Message.Split('+');
                                        var UpdateNumber = controller.UpdateUser(new user { uID = uid, finishedregister = true, number = Number[0] != "" ? Number[0] : Number[1] });
                                        var UpdateAgentNumberMember = controller.UpdateAgentMemberNumber(uid, UserInfo.PK, Message);
                                        if (UpdateNumber && UpdateAgentNumberMember)
                                        {
                                            controller.UpdateUser(new user { uID = uid, registerstep = UserInfo.registerstep += 1 });
                                            bot.SendTextMessageAsync(uid, "تبریک حساب کاربری شما با موفقیت ساخته شد\nاز طریق دکمه های زیر میتوانید از خدمات ربات استفاده فرمایید", replyMarkup: RegisteredUsersRKM);
                                        }
                                    }
                                    else
                                    {
                                        bot.SendTextMessageAsync(uid, "فقط شماره ایران مورد قبول میباشد.");
                                    }
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                            else
                            {
                                bot.SendTextMessageAsync(uid, "لطفا فقط شماره خود را از طریق دکمه زیر به اشتراک بگذارید", replyMarkup: contactBTN);
                            }
                            break;
                        #endregion

                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            #endregion

        }
        [Obsolete]
        public async Task<Telegram.Bot.Types.Message> SendToAdmins(ProjectMode projectMode, ButtonMode btnMode, CallBackModel callback)
        {
            #region CallBackStyles
            //12344556:textproject;imagebinery>acceptorreject
            //$"{callback.id}:{callback.ProjectTosend};{callback.Image}>{callback.StartIndex}"
            // InlineKeyboardButton.WithCallbackData("Accept",$"{f.Message.From.Id}:{hiretag};HAccept"),
            string AdsCallBack = $"{callback.id}:{callback.Image};{callback.StartIndex}";
            string HireCallBack = $"{callback.id}:{callback.tag};{callback.StartIndex}";
            string ProjectCallBack = $"{callback.id}:{callback.tag};";
            string FinalCallBack = "";
            switch (projectMode)
            {
                case ProjectMode.Project:
                    FinalCallBack = ProjectCallBack;
                    break;
                case ProjectMode.Hire:
                    FinalCallBack = HireCallBack;
                    break;
                case ProjectMode.Ads:
                    FinalCallBack = AdsCallBack;
                    break;
                default:
                    break;
            }
            #endregion

            #region Buttons
            InlineKeyboardMarkup AdminController = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {                                        //("Accept",$"{callback.id}:{callback.tag};{callback.StartIndex}Accept")
                        InlineKeyboardButton.WithCallbackData("Accept",$"{FinalCallBack}Accept"),
                        InlineKeyboardButton.WithCallbackData("Reject",$"{FinalCallBack}Reject"),
                        InlineKeyboardButton.WithCallbackData("Block", $"{FinalCallBack}Block"),
                    }
                });
            #endregion

            #region Admins
            var AdminsList = controller.GetAllAdmins();
            #endregion

            #region Send To Admins By ButtonMode
            if (btnMode == ButtonMode.AcceptBlockReject)
            {
                Telegram.Bot.Types.Message msg = null;
                foreach (var admin in AdminsList)
                {
                    await bot.SendTextMessageAsync(admin.uID, $"Dear {admin.name}\n New Project Request:");
                    switch (projectMode)
                    {
                        case ProjectMode.Project:
                            msg = await bot.SendTextMessageAsync(admin.uID, callback.ProjectTosend, replyMarkup: AdminController);
                            break;
                        case ProjectMode.Hire:
                            msg = await bot.SendTextMessageAsync(admin.uID, callback.ProjectTosend, replyMarkup: AdminController);
                            break;
                        case ProjectMode.Ads:
                            if (callback.Image.StartsWith("none>"))
                            {
                                msg = await bot.SendTextMessageAsync(admin.uID, callback.ProjectTosend, replyMarkup: AdminController);
                            }
                            else
                            {
                                var getIMG = await controller.GetImageAsync(new Models.Model.Image() { uID = callback.id, UniqueID = callback.Image, ProjectID = Tagg });
                                msg = await bot.SendPhotoAsync(admin.uID, getIMG.FileID, callback.ProjectTosend, replyMarkup: AdminController);
                            }
                            break;
                        default:
                            break;
                    }
                }

                return msg;
            }

            return null;

            #endregion
        }
        [Obsolete]
        public async void RegisteredUsersMessageHandler(MessageEventArgs s, user user)
        {
            #region Proccess Messages
            switch (s.Message.Text)
            {
                #region Request Project & Recruitment
                case "اگهی استخدام":
                case "اگهی پروژه":

                    if (user.finishedregister == true)
                    {
                        if (user.projectstep == 0 && user.adsStep == 0 || user.projectstep == null && user.adsStep == null)
                        {
                            if (s.Message.Text == "اگهی استخدام")
                            {
                                controller.AddNewRec(new HireList() { employeeID = user.uID, hirefinished = false });
                                controller.UpdateUser(new user { uID = user.uID, projectstep = 2, ishireing = true });
                                await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا متن درخواست استخدامی خود را به صورت کامل ارسال کنید", replyMarkup: CancelRKM);
                                //if (getagent != null)
                                //{
                                //    if (getagent.FreeBalance > 0)
                                //    {
                                //        controller.UpdateAgent(new agent() { agentuid = s.Message.From.Id.ToString(), FreeBalance = getagent.FreeBalance - 1 });
                                //        await bot.SendTextMessageAsync(s.Message.From.Id, "شما در حال استفاده از اگهی رایگان خود میباشید\nاین عملیات قابل بازگشت نمیباشد");
                                //        IsFree = true;
                                //    }
                                //}
                                //if (IsFree)
                                //{
                                //    controller.AddNewRec(new HireList() { employeeID = user.uID, hirefinished = false });
                                //    controller.UpdateUser(new user { uID = user.uID, projectstep = 2, ishireing = true });
                                //    await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا متن درخواست استخدامی خود را به صورت کامل ارسال کنید", replyMarkup: CancelRKM);
                                //}
                                //else
                                //{
                                //    if (user.HireChance > 0)
                                //    {
                                //        controller.AddNewRec(new HireList() { employeeID = user.uID, hirefinished = false });
                                //        controller.UpdateUser(new user { uID = user.uID, projectstep = 2, ishireing = true });
                                //        await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا متن درخواست استخدامی خود را به صورت کامل ارسال کنید", replyMarkup: CancelRKM);
                                //    }
                                //    else
                                //    {
                                //        await bot.SendTextMessageAsync(s.Message.From.Id, "موجودی حساب شما برای این اقدام کافی نیست");
                                //    }
                                //}
                            }
                            else
                            {
                                controller.UpdateUser(new user { uID = user.uID, projectstep = 1 });
                                controller.AddNewProject(new Project { uid = user.uID, ProjectFinished = false });
                                await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا دسته بندی مورد نظره پروژه خود را بفرستید", replyMarkup: CancelRKM);
                                //if (getagent != null)
                                //{
                                //    if (getagent.FreeBalance > 0)
                                //    {
                                //        controller.UpdateAgent(new agent() { agentuid = s.Message.From.Id.ToString(), FreeBalance = getagent.FreeBalance - 1 });
                                //        await bot.SendTextMessageAsync(s.Message.From.Id, "شما در حال استفاده از اگهی رایگان خود میباشید\nاین عملیات قابل بازگشت نمیباشد");
                                //        IsFree = true;
                                //    }
                                //}
                                //if (IsFree)
                                //{
                                //    controller.UpdateUser(new user { uID = user.uID, projectstep = 1 });
                                //    controller.AddNewProject(new Project { uid = user.uID, ProjectFinished = false });
                                //    await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا دسته بندی مورد نظره پروژه خود را بفرستید", replyMarkup: CancelRKM);
                                //}
                                //else
                                //{
                                //    if (user.ProjectChance > 0)
                                //    {
                                //        controller.UpdateUser(new user { uID = user.uID, projectstep = 1 });
                                //        controller.AddNewProject(new Project { uid = user.uID, ProjectFinished = false });
                                //        await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا دسته بندی مورد نظره پروژه خود را بفرستید", replyMarkup: CancelRKM);
                                //    }
                                //    else
                                //    {
                                //        await bot.SendTextMessageAsync(s.Message.From.Id, "موجودی حساب شما برای این اقدام کافی نیست");
                                //    }
                                //}


                            }
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "در حال حاظر شما نمیتوانید پروژه جدید تعریف کنید\nلطفا پروژه قبلی را تکمیل یا تا تایید پروژه منتظر بمانید");
                        }
                    }
                    break;
                #endregion

                #region ADS
                case "اگهی تبلیغاتی":
                    if (user.finishedregister == true)
                    {
                        if (user.projectstep == 0 && user.adsStep == 0)
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا نوع اگهی خود را انتخاب کنید:", replyMarkup: ADSRKM);
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "در حال حاظر شما نمیتوانید پروژه جدید تعریف کنید\nلطفا پروژه قبلی را تکمیل یا تا تایید پروژه منتظر بمانید");
                        }
                    }

                    break;
                #endregion

                #region Account Handler

                #region Main Profile Selection
                case "پروفایل من":
                    if (user.finishedregister == true)
                    {
                        await bot.SendTextMessageAsync(s.Message.From.Id, $"حساب کاربری شما:\nلطفا یک گذینه را انتخاب کنید", replyMarkup: ProfileMenuRKM);
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(s.Message.From.Id, "شما هنوز در ربات ثبت نام نکردید");
                    }
                    break;
                #endregion

                #region Type Selection
                case "اگهی های من":
                    if (user.finishedregister == true)
                    {
                        await bot.SendTextMessageAsync(s.Message.From.Id, "اطلاعات مربوط به کدام نوع از اگهی رو میخای؟:)", replyMarkup: SelectTypeRKM);
                    }
                    break;
                case "بخش مالی":
                    if (user.finishedregister == true)
                    {
                        await bot.SendTextMessageAsync(s.Message.From.Id, "به بخش مالیه حساب خود خوش امدید\nلطفا یک گذینه را انتخاب کنید:", replyMarkup: FiscalMenuRKM);
                    }
                    break;
                #endregion

                #region FiscalDepartment Handler
                case "اطلاعات حساب من":
                    if (user.finishedregister == true)
                    {
                        await bot.SendTextMessageAsync(s.Message.From.Id, "اطلاعات حساب شما به صورت زیر است:");
                        #region Project
                        var Projects = controller.GetListOfProject(new Project() { uid = s.Message.From.Id.ToString() });
                        int ProjectCount = Projects.Count == 0 ? 0 : Projects.Count;
                        #endregion
                        #region Hire
                        var Hires = controller.GetAllUserHireProject(new HireList() { employeeID = s.Message.From.Id.ToString() }, ReturnMode.ALL);
                        int HiresCount = Hires.Count == 0 ? 0 : Hires.Count;
                        #endregion
                        #region Agent
                        var Agent = controller.GetAgent(new agent() { agentuid = s.Message.From.Id.ToString() });
                        int AgentCount = Agent == null ? 0 : Convert.ToInt32(Agent.sellcount);
                        int AgentBalance = Agent == null ? 0 : Convert.ToInt32(Agent.FreeBalance);
                        #endregion
                        #region OutPut
                        string OutPutTXT = $"<b><i>Verified Projects Count:</i></b> [{ProjectCount + HiresCount}]\n--\n<b><i>Invites Counts:</i></b> [{AgentCount}]\n--\n<b><i>FreeBalance:</i></b> [{AgentBalance}]";
                        await bot.SendTextMessageAsync(s.Message.From.Id, OutPutTXT, parseMode: ParseMode.Html);
                        #endregion
                    }
                    break;
                case "افزایش موجودی":
                    break;
                #endregion

                #region Project Handler

                #region Project
                case "پروژه":
                    if (user.finishedregister == true)
                    {
                        await bot.SendTextMessageAsync(s.Message.From.Id, "گذینه های مربوط به پروژه های شما:", replyMarkup: ProjectHndlrRKM);
                    }
                    break;
                #endregion

                #region Get All Projects
                case "مشاهده تمام پروژه ها":
                    try
                    {
                        var projList = controller.GetListOfProject(new Project() { uid = s.Message.From.Id.ToString() });
                        if (projList.Count > 0)
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "لیست تمام پروژه های شما:", replyMarkup: RegisteredUsersRKM);
                            foreach (var project in projList)
                            {
                                string check = project.Checked == true ? "تایید شده" : "تایید نشده";
                                await bot.SendTextMessageAsync(s.Message.From.Id, $"ایدی پروژه: #{project.ProjectId} \n\nدسته بندی: #{project.category} \n\nتوضیحات:\n{project.dicription} \n\n\nوضعیت انتشار: {check}");
                            }
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "شما هنوز هیچ پروژه ای ندارید");
                        }
                    }
                    catch (Exception ex)
                    {
                        ExeptionHandler($"get all project>>{ex.Message}", 1154);
                        await bot.SendTextMessageAsync(s.Message.From.Id, "مشگلی پیش امده\nلطفا با پشتیبانی تماس بگیرید");
                    }
                    break;
                #endregion

                #region DeActive Project

                #region Show Options
                case "غیر فعال کردن پروژه":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا یکی از روش ها برای غیر فعال سازی یا واگذاری پروژه را انتخاب کنید:", replyMarkup: SelectProjectRKM);
                    break;
                #endregion

                #region From List
                case "انتخاب از لیست پروژه ها":
                    try
                    {
                        var projList = controller.GetListOfProject(new Project() { uid = s.Message.From.Id.ToString() });
                        if (projList.Count != 0)
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "لیست تمام پروژه های شما:", replyMarkup: RegisteredUsersRKM);
                            foreach (var project in projList)
                            {
                                if (project.disable != true && project.Checked == true)
                                {
                                    var DeactiveProjectIKM = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("واگذاری این پروژه", $"deactiveProject:{project.ProjectId}"), } });
                                    string check = project.Checked == true ? "تایید شده" : "تایید نشده";
                                    await bot.SendTextMessageAsync(s.Message.From.Id, $"ایدی پروژه: #{project.ProjectId} \n\nدسته بندی: #{project.category} \n\nتوضیحات:\n{project.dicription} \n\n\nوضعیت انتشار: {check}", replyMarkup: DeactiveProjectIKM);
                                }
                            }
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "شما هنوز هیچ پروژه ای ندارید");
                        }
                    }
                    catch (Exception ex)
                    {
                        ExeptionHandler($"get all project>>{ex.Message}", 1154);
                        await bot.SendTextMessageAsync(s.Message.From.Id, "مشگلی پیش امده\nلطفا با پشتیبانی تماس بگیرید");
                    }
                    break;
                #endregion

                #region From Tag
                case "غیرفعال کردن اگهی پروژه از طریق هشتک":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا هشتک پروژه مورد نظر خودتون رو واسم بفرستین", replyMarkup: CancelDisableProjectRKM);
                    controller.UpdateUser(new user() { uID = s.Message.From.Id.ToString(), DisableStep = 1 });
                    break;
                #endregion
                #endregion
                #endregion

                #region Hire Handler
                #region Hire
                case "استخدام":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "گذینه های مربوط به پروژه های استخدامی شما:", replyMarkup: HireHndlrRKM);
                    break;

                #endregion

                #region Get Hire Projects
                case "مشاهده تمام اگهی های استخدام":
                    try
                    {
                        var gethirelist = controller.GetAllUserHireProject(new HireList() { employeeID = s.Message.From.Id.ToString() }, ReturnMode.ALL);
                        if (gethirelist.Count > 0)
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "لیست اگهی های استخدام شما:");
                            foreach (var hire in gethirelist)
                            {
                                string hirestatus = hire.FreeLancerID == "" || hire.FreeLancerID == null ? "هنوز فردی را استخدام نکردید" : $"شما در حال حاظر فردی را استخدام کردید\nایدی شخص:[{hire.FreeLancerID}]";
                                string accptstatus = hire.@checked == true ? "تایید شده" : "هنوز تایید نشده";
                                await bot.SendTextMessageAsync(s.Message.From.Id, $"کد اگهی: #{hire.ProjectID}\n\nمتن اگهی: {hire.discription} \n\n وضعیت استخدامی: {hirestatus}\n\nوضعیت اگهی:{accptstatus}");
                            }
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "شما در حال حاظر هیچ اگهی استخدامی ندارید");
                        }
                    }
                    catch (Exception)
                    {
                    }
                    break;
                #endregion

                #region DeActive Hire Project
                #region Show Options
                case "غیر فعال کردن اگهی استخدام":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا یکی از روش ها برای غیر فعال سازی یا واگذاری اگهی استخدام خود را انتخاب کنید:", replyMarkup: SelectHireRKM);
                    break;
                #endregion

                #region From List
                case "انتخاب از لیست اگهی های استخدام":
                    try
                    {
                        var getlist = controller.GetAllUserHireProject(new HireList() { employeeID = s.Message.From.Id.ToString() }, ReturnMode.PassDisAndUnchecked);
                        if (getlist.Count != 0)
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "لیست اگهی های استخدام شما:");
                            foreach (var hire in getlist)
                            {

                                string hirestatus = hire.FreeLancerID == "" || hire.FreeLancerID == null ? "بدونه استخدامی" : $"استخدام کردید \nایدی شخص:[{hire.FreeLancerID}]";
                                string accptstatus = hire.@checked == true ? "تایید شده" : "هنوز تایید نشده";
                                var DeactiveHireIKM = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("واگذاری این اگهی استخدام", $"deactiveHire:{hire.ProjectID}"), } });
                                await bot.SendTextMessageAsync(s.Message.From.Id, $"کد اگهی: #{hire.ProjectID}\n\nمتن اگهی: {hire.discription} \n\n وضعیت استخدامی: {hirestatus}\n\nوضعیت اگهی:{accptstatus}", replyMarkup: DeactiveHireIKM);

                            }
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "شما هیچ اگهی استخدامه تایید شده یا واگذار نشده ندارید");
                        }
                    }
                    catch (Exception)
                    {
                    }
                    break;
                #endregion

                #region From Tag
                case "غیرفعال کردن اگهی استخدام از طریق هشتک":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "لطفا هشتک اگهی استخدام مورد نظر خودتون رو واسم بفرستین", replyMarkup: CancelDisableHireProjectRKM);
                    controller.UpdateUser(new user() { uID = s.Message.From.Id.ToString(), DisableStep = 2 });
                    break;
                #endregion

                #endregion

                #endregion

                #region Ads
                #region Show Options
                case "تبلیغات":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "گذینه های تبلیغات:", replyMarkup: ProfileAdsRKM);
                    break;
                #endregion
                #region Ads List
                case "مشاهده لیست تبلیغات":
                    if (user.finishedregister == true)
                    {
                        var getAdsList = controller.GetUserAdsList(new ADSList() { uID = s.Message.From.Id.ToString() });
                        if (getAdsList == null || getAdsList.Count < 1)
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, "شما هیچ تبلیغاتی تاکنون ثبت نکردید");
                        }
                        else
                        {
                            foreach (var ads in getAdsList)
                            {
                                await bot.SendTextMessageAsync(s.Message.From.Id, $"Code:{ads.GUID}\nLink: {ads.link} \n Discription: {ads.discription}\n Published: {ads.IsPublished != null} \n Has Picture: {ads.pic != null}");
                            }
                        }
                    }
                    break;
                #endregion
                #endregion

                #region RePush Project,Hire Or Ads
                case "انتشار مجدد اگهی":
                    if (user.finishedregister == true)
                    {
                        await bot.SendTextMessageAsync(s.Message.From.Id, "کد اگهی خود را ارسال کنید");
                        controller.UpdateUser(new user() { uID = s.Message.From.Id.ToString(), adsStep = 5 });
                    }
                    break;
                #endregion
                #endregion

                #region Agent Register
                case "لینک دعوت":
                    if (user.registermode != "agent")
                    {
                        var AgentRegister = controller.UpdateUser(new user()
                        {
                            uID = s.Message.From.Id.ToString(),
                            registermode = "agent",
                        });
                        var AddToAgentList = controller.AddNewAgent(new agent()
                        {
                            agentname = user.fullname,
                            agentnumber = user.number,
                            agentuid = user.uID
                        });
                        var createTable = controller.CreateTable(Convert.ToInt32(user.PK));
                        if (AgentRegister && AddToAgentList == "OK" && createTable)
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, $"حساب نماینده شما با موفقیت ساخته شد \nلینک معرف شما: t.me/ratheropanelbot?start=_z{user.PK.ToString()}");
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(s.Message.From.Id, AddToAgentList);
                        }
                    }
                    else
                    {
                        await bot.SendTextMessageAsync(s.Message.From.Id, $"حساب شما از قبل ساخته شده\nلینک شخصی شما: t.me/ratheropanelbot?start=_z{user.PK.ToString()}");
                    }
                    break;
                #endregion

                #region Cancelation Tokens
                case "بازگشت از قسمت تبلیغات":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "بازگشت", replyMarkup: ProfileMenuRKM);
                    break;
                case "خروج از بخش مالی":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "شما به عقب برگشتید", replyMarkup: ProfileMenuRKM);
                    break;
                case "برگشت":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "بازگشت به عقب", replyMarkup: user.finishedregister == true ? SelectTypeRKM : mainRKM);
                    break;
                case "منصرف شدم":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "بازگشت به صفحه استخدام", replyMarkup: user.finishedregister == true ? HireHndlrRKM : mainRKM);
                    break;
                case "انصراف":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "بازگشت به صفحه پروژه", replyMarkup: user.finishedregister == true ? ProjectHndlrRKM : mainRKM);
                    break;
                case "برگشت به اگهی ها":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "بازگشت به عقب", replyMarkup: user.finishedregister == true ? SelectTypeRKM : mainRKM);
                    break;
                case "بازگشت به پروفایل":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "بازگشت به صفحه پروفایل", replyMarkup: user.finishedregister == true ? ProfileMenuRKM : mainRKM);
                    break;
                case "برگشت به صفحه اصلی":
                    await bot.SendTextMessageAsync(s.Message.From.Id, "بازگشت به صفحه اصلی", replyMarkup: user.finishedregister == true ? RegisteredUsersRKM : mainRKM);
                    break;
                case "انصراف از غیر فعال سازی پروژه":
                    controller.UpdateUser(new user() { uID = s.Message.From.Id.ToString(), DisableStep = 0 });
                    await bot.SendTextMessageAsync(s.Message.From.Id, "شما از غیر فعال سازی پروژه انصراف دادید", replyMarkup: RegisteredUsersRKM);
                    break;
                case "انصراف از غیر فعال سازی اگهی استخدام":
                    controller.UpdateUser(new user() { uID = s.Message.From.Id.ToString(), DisableStep = 0 });
                    await bot.SendTextMessageAsync(s.Message.From.Id, "شما از غیر فعال سازی اگهی استخدام انصراف دادید", replyMarkup: RegisteredUsersRKM);
                    break;
                #endregion
                default:
                    break;
            }
            #endregion
        }
        [Obsolete]
        public async void ProjectRequestHandler(user usr, MessageEventArgs f)
        {
            try
            {
                switch (usr.projectstep)
                {
                    #region Category
                    case 1:
                        if (f.Message.Text.Length > 3)
                        {
                            #region Cancel Token
                            if (f.Message.Text == "انصراف")
                            {

                                var reset = controller.UpdateUser(new user() { uID = f.Message.From.Id.ToString(), projectstep = 0 });
                                var rm = controller.RemoveUserProject(new Project() { uid = f.Message.From.Id.ToString() });
                                if (reset && rm)
                                {
                                    await bot.SendTextMessageAsync(f.Message.From.Id, "بازگشت به صفحه اصلی", replyMarkup: usr.finishedregister == true ? RegisteredUsersRKM : mainRKM);
                                }
                                else
                                {
                                    await bot.SendTextMessageAsync(f.Message.From.Id, "مشگلی در بازگردانی اطلاعات شما پیش امده\nلطفا با پشتیبانی تماس بگیرید", replyMarkup: usr.finishedregister == true ? RegisteredUsersRKM : mainRKM);
                                }
                            }
                            #endregion

                            #region Proccess Category
                            else
                            {
                                #region Build List
                                List<string> ButtonItems = new List<string>();
                                foreach (var category in Categories)
                                {
                                    if (category.StartsWith(f.Message.Text.Substring(0, 2)) || category.EndsWith(f.Message.Text.Substring(0, 2)) || category.Contains(f.Message.Text))
                                    {
                                        ButtonItems.Add(category);
                                    }
                                }
                                #endregion

                                #region Send To User
                                if (ButtonItems.Count >= 1)
                                {
                                    var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(ButtonItems, new user() { uID = f.Message.From.Id.ToString() }));
                                    await bot.SendTextMessageAsync(f.Message.From.Id, "دسته بندی های مرتبط با موضوع شما،جهت ادامه روی هرکدام از این دسته بندی ها کلیک کنید", replyMarkup: keyboardMarkup);
                                }
                                else
                                {
                                    await bot.SendTextMessageAsync(f.Message.From.Id, "دسته بندی مرتبط با موضوع شما پیدا نشد لطفا مجدد تلاش کنید");
                                }
                                #endregion

                            }
                            #endregion

                        }
                        break;
                    #endregion

                    #region Description
                    case 2:
                        try
                        {
                            var SendToAdmins = controller.GetAllAdmins();

                            #region Project Handller
                            if (usr.ishireing == false || usr.ishireing == null)
                            {
                                #region Cancel Token
                                if (f.Message.Text == "انصراف")
                                {
                                    var rm = controller.RemoveUserProject(new Project() { uid = f.Message.From.Id.ToString() });
                                    var reset = controller.UpdateUser(new user() { uID = f.Message.From.Id.ToString(), projectstep = 0, ishireing = false });
                                    if (reset && rm)
                                    {
                                        await bot.SendTextMessageAsync(f.Message.From.Id, "بازگشت به صفحه اصلی", replyMarkup: usr.finishedregister == true ? RegisteredUsersRKM : mainRKM);
                                    }
                                    else
                                    {
                                        await bot.SendTextMessageAsync(f.Message.From.Id, "مشگلی در بازگردانی اطلاعات شما پیش امده\nلطفا با پشتیبانی تماس بگیرید", replyMarkup: usr.finishedregister == true ? RegisteredUsersRKM : mainRKM);
                                    }
                                }
                                #endregion

                                else
                                {
                                    #region Get Discrip
                                    //InlineKeyboardMarkup PolicyIKM = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("accept", "AP:True"), } });
                                    //var t= await bot.SendTextMessageAsync(f.Message.From.Id, "لطفا قوانین را تایید کنید", replyMarkup: PolicyIKM);

                                    if (f.Message.Text.Length <= 600)
                                    {
                                        controller.UpdateUser(new user() { uID = f.Message.From.Id.ToString(), projectstep = 4 });
                                        controller.UpdateUserProject(new Project() { uid = f.Message.From.Id.ToString(), dicription = f.Message.Text, ProjectFinished = true });
                                        await bot.SendTextMessageAsync(f.Message.From.Id, "اگهی شما با موفقیت ثبت شد\nپس از تایید توسط ادمین ربات اگهی شما در کانال قرار خواهد گرفت\nاگهی شما:", replyMarkup: RegisteredUsersRKM);
                                        var project = controller.GetUserProject(new Project() { uid = f.Message.From.Id.ToString(), ProjectId = Tagg });

                                        #region ProjectText
                                        string FinalProject = $@"کد اگهی:{project.ProjectId}

دسته بندی:#{project.category}

توضیحات:{project.dicription}";
                                        #endregion



                                        #region Send Project To Admin
                                        //UserID:ProjectID;Status
                                        InlineKeyboardMarkup AdminController = new InlineKeyboardMarkup(new[]{new[]{
                                        InlineKeyboardButton.WithCallbackData("Accept",$"{f.Message.From.Id}:{Tagg};Accept"),
                                        InlineKeyboardButton.WithCallbackData("Reject",$"{f.Message.From.Id}:{Tagg};Reject"),
                                        InlineKeyboardButton.WithCallbackData("Block", $"{f.Message.From.Id}:{Tagg};Block"),}});

                                        Telegram.Bot.Types.Message msg = null;
                                        foreach (var admin in SendToAdmins)
                                        {
                                            await bot.SendTextMessageAsync(admin.uID, $"Dear {admin.name}\n New Project Request:");
                                            msg = await bot.SendTextMessageAsync(admin.uID, FinalProject, replyMarkup: AdminController);
                                        }

                                        #region ProccessUserMessage

                                        var sendToUser = await bot.SendTextMessageAsync(f.Message.From.Id, FinalProject);
                                        var UserCancelation = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("انصراف", $"rm*project*{sendToUser.MessageId}*{Tagg}*{msg.Chat.Id}*{msg.MessageId}"), } });
                                        await bot.EditMessageReplyMarkupAsync(chatId: f.Message.Chat.Id,
                                            sendToUser.MessageId, replyMarkup: UserCancelation);

                                        #endregion
                                        #endregion

                                    }
                                    else
                                    {
                                        await bot.SendTextMessageAsync(f.Message.From.Id, $"متن اگهی طولانی است \n[{f.Message.Text.Length - 600}]حرف اضافه است");
                                    }
                                    #endregion

                                }

                            }
                            #endregion

                            #region Hire Handler
                            else
                            {
                                if (f.Message.Text.Length <= 1000)
                                {
                                    #region Cancel Token
                                    if (f.Message.Text == "انصراف")
                                    {
                                        var rm = controller.RemoveHireProject(new HireList() { employeeID = f.Message.From.Id.ToString() });
                                        var reset = controller.UpdateUser(new user() { uID = f.Message.From.Id.ToString(), projectstep = 0, ishireing = false });
                                        if (reset && rm)
                                        {
                                            await bot.SendTextMessageAsync(f.Message.From.Id, "بازگشت به صفحه اصلی", replyMarkup: usr.finishedregister == true ? RegisteredUsersRKM : mainRKM);
                                        }
                                        else
                                        {
                                            await bot.SendTextMessageAsync(f.Message.From.Id, "مشگلی در بازگردانی اطلاعات شما پیش امده\nلطفا با پشتیبانی تماس بگیرید", replyMarkup: usr.finishedregister == true ? RegisteredUsersRKM : mainRKM);
                                        }
                                    }
                                    #endregion

                                    #region Proccess Hireing
                                    else
                                    {
                                        controller.UpdateUser(new user() { uID = f.Message.From.Id.ToString(), projectstep = 4, ishireing = null });
                                        string hiretag = TagGenerator();
                                        controller.UpdateHireProject(new HireList() { employeeID = f.Message.From.Id.ToString(), discription = f.Message.Text, ProjectID = hiretag, hirefinished = true });
                                        await bot.SendTextMessageAsync(f.Message.From.Id, "اگهی شما با موفقیت ثبت شد\nپس از تایید توسط ادمین ربات اگهی شما در کانال قرار خواهد گرفت\nاگهی شما:", replyMarkup: RegisteredUsersRKM);

                                        #region FinalText
                                        string FinalText = $@"#استخدام
کد اگهی:#{hiretag}

توضیحات:
{f.Message.Text}";
                                        #endregion

                                        #region Send Project To Admin
                                        //UserID:ProjectID;Status
                                        InlineKeyboardMarkup AdminController = new InlineKeyboardMarkup(new[] {
                                    new[] {
                                        InlineKeyboardButton.WithCallbackData("Accept",$"{f.Message.From.Id}:{hiretag};HAccept"),
                                        InlineKeyboardButton.WithCallbackData("Reject",$"{f.Message.From.Id}:{hiretag};HReject"),
                                        InlineKeyboardButton.WithCallbackData("Block", $"{f.Message.From.Id}:{hiretag};HBlock"),
                                    }
                                });
                                        Telegram.Bot.Types.Message msg = null;
                                        foreach (var admin in SendToAdmins)
                                        {
                                            await bot.SendTextMessageAsync(admin.uID, $"Dear {admin.name}\n New Hire Project Request:");
                                            msg = await bot.SendTextMessageAsync(admin.uID, FinalText, replyMarkup: AdminController);
                                        }
                                        #endregion

                                        #region ProccessUserMessage

                                        var sendToUser = await bot.SendTextMessageAsync(f.Message.From.Id, FinalText);
                                        var userCancelation = new InlineKeyboardMarkup(new[] { new[] { InlineKeyboardButton.WithCallbackData("انصراف", $"rm*hire*{sendToUser.MessageId}*{hiretag}*{msg.Chat.Id}*{msg.MessageId}"), } });
                                        await bot.EditMessageReplyMarkupAsync(chatId: f.Message.Chat.Id,
                                            sendToUser.MessageId, replyMarkup: userCancelation);
                                        #endregion
                                    }
                                    #endregion


                                }
                                else
                                {
                                    await bot.SendTextMessageAsync(f.Message.From.Id, $"متن اگهی طولانی است \n[{f.Message.Text.Length - 1000}]حرف اضافه است");
                                }
                            }
                            #endregion
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
            }
            catch (Exception r)
            {
                await bot.SendTextMessageAsync(f.Message.From.Id, $"{r.Message} Trace:[{r.StackTrace}]");
                throw;
            }
        }
        public string TagGenerator()
        {
            try
            {
                #region Create List Of Single Chars And Numbers
                List<string> Chars = new List<string>();
                for (char c = 'A'; c <= 'Z'; ++c)
                {
                    Chars.Add(c.ToString());
                }
                for (int i = 0; i <= 9; i++)
                {
                    Chars.Add(i.ToString());
                }
                #endregion

                #region Create Random Index Number
                Random rnd = new Random();
                #endregion

                #region Create Random Tag
                string Tag = "T";
                for (int i = 0; i < 6; i++)
                {
                    int Index = rnd.Next(0, Chars.Count);
                    Tag += Chars[Index];
                }
                #endregion

                return $"{Tag}";
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
    }
}