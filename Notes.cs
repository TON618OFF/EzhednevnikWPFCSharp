using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_2
{
    public class Notes
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Notes(string title, string description, DateTime date)
        {
            Title = title;
            Description = description;
            Date = date;
        }
    }
}