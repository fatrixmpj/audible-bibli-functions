using System;
using System.Collections.Generic;
using System.Text;

namespace audible_biblio
{
    class audiobook
    {
        public string _id { get; set; }
        public string title { get; set; }
        public string series { get; set; }
        public string account { get; set; }
        public bool deleted { get; set; }
    }
}
