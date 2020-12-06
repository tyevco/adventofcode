using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCalendar2020.D06
{
    class CustomsGroup
    {
        public List<PersonAnswers> People { get; set; } = new List<PersonAnswers>();
    }

    class PersonAnswers
    {
        public string Answers { get; set; }
    }
}
