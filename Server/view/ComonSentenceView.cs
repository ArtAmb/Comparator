using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.view
{
    public class ComonSentenceView
    {
        public int Nr { set; get; }
        public String Sentence { set; get; }

        public int F1StartIdx { set; get; }
        public int F1EndIdx { set; get; }

        public int F2StartIdx { set; get; }
        public int F2EndIdx { set; get; }
    }
}
