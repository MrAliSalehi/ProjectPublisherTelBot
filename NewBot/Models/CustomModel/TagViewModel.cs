using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBot.Models.CustomModel
{
   public class TagViewModel
    {
        /// <summary>
        /// ProjectID Or  GUID
        /// </summary>
        public string Tag { get; set; }
        public string Category { get; set; } = "";
        public string Discription { get; set; } = "";
        public string Link { get; set; } = "";
        public string Pic { get; set; } = "";

    }
}
