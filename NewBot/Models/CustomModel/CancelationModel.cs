using Telegram.Bot.Args;

namespace NewBot.Models.CustomModel
{
    public class CancelationModel
    {
        [System.Obsolete]
        public CallbackQueryEventArgs EArgs { get; set; }
        public string MessageId { get; set; }
        public CancelForAdminModel CancelForAdminModel { get; set; }
    }

    public class CancelForAdminModel
    {
        public string ChatId { get; set; }
        public string MessageId { get; set; }
        public MediaType MediaType { get; set; }
    }
    public enum MediaType{Text,Photo}
}