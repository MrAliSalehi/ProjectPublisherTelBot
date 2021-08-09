using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBot.Models.CustomModel
{
    public class CallBackModel
    {
        public string tag { get; set; }
        public string id { get; set; }
        public string ProjectTosend { get; set; }
        [DefaultValue("none")]
        public string Image { get; set; }
        [DefaultValue("")]
        public string StartIndex { get; set; }
        public enum GetProjectType
        {
            PullAllUserProjects = 0,
            GetByProjectId = 1
        }
        public enum ContentType
        {
            Project = 0,
            Hire = 1,
            Ads = 2,
            none = 3
        }
        public enum ContentStatus
        {
            Accepted = 0,
            Rejected = 1,
            Blocked = 2,
            none = 3
        }
        public enum ContentMessageType
        {
            Caption = 0,
            Text = 1,
            custom = 2
        }
        public enum ButtonMode
        {
            AcceptReject = 0,
            AcceptBlock = 1,
            AcceptBlockReject = 2
        }
        public enum ProjectMode
        {
            Project = 0,
            Hire = 1,
            Ads = 2,
        }
        public enum ReturnMode
        {
            ALL = 0,
            PassDisAndUnchecked = 1
        }
    }
}
