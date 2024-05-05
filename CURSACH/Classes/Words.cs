using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURSACH.Classes
{
    public class Words
    {
        public string Word { get; set; }
        public string Translation { get; set; }
        public string WordStatus { get; set; }
        public int WordId { get; set; }
        public int StudentId { get; set; }
        public int LanguageId { get; set; } 
        public DateTime CreatedDate { get; set; }

    }
}
