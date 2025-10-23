using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core
{
        public class Drug
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Form { get; set; }
            public string Dosage { get; set; }
            public DateTime ExpirationDate { get; set; }
        }    
}
