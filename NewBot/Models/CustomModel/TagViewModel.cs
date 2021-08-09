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
        /// <summary>
        /// Unique Identifier As ID In Every Table
        /// </summary>
        public int ID { get; set; }
        public TagType TagIdentifier { get; set; }
        public string Category { get; set; } = "";
        public string Discription { get; set; } = "";
        public string Link { get; set; } = "";
        public string Pic { get; set; } = "";

    }
    public enum TagType
    {
        Project = 0,
        Hire = 1,
        Ads = 2,
        Null = 3

    }
}
