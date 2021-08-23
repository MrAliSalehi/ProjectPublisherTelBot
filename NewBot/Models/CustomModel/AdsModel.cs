using NewBot.Models.Model;

namespace NewBot.Models.CustomModel
{
    public class AdsModel
    {
        public AdsType AdsType { get; set; }
        public AdsOperation AdsOperation { get; set; }
        public AdsGroup AdsGroup { get; set; }
        public AdsChannel AdsChannel { get; set; }
        public AdsBusiness AdsBusiness { get; set; }
    }

    public class AdsOutPutModel
    {
        public AdsType AdsType { get; set; }
        public OutPutType OutPutType { get; set; }
        public dynamic OutPut { get; set; }
    }
    public enum OutPutType{STRING,BOOL,OBJECT}
    public enum AdsType { Channel, Group, Business }
    public enum AdsOperation { Get, Insert, Update, Delete }
}