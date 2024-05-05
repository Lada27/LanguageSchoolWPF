    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURSACH.Classes
{
    public class HomeworksClass
    {
        public int HomeworkId { get; set; }
        public string HomeworkDescription { get; set; }

        public string HomeworkSolution { get; set; }

        public DateTime CreatedDate { get; set; }

        public int StudentId { get; set; }
        public string Status { get; set; }
        public int LanguageId { get; set; }

    }
}