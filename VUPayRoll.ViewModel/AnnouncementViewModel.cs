using System;
using System.Collections.Generic;
using System.Text;

namespace VUPayRoll.ViewModel
{
   public class AnnouncementViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool? IsDelete { get; set; }
    }
}
