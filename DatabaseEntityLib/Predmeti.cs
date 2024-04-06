using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DatabaseEntityLib
{
    public class Predmeti
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Zadaci> pitanje { get; set; } = new List<Zadaci>();

    }
}
