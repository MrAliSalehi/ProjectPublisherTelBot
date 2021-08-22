using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBot.Models.CustomModel
{
    public class MessageDataViewModel
    {
        public string ChatID { get; set; }
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public string customStautsText { get; set; }
    }
    
}
