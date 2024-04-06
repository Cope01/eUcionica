using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntityLib
{
    public class Zadaci
    {
        [Key]
        public int ID { get; set; }
        public int IDPredmet { get; set; }
        [ForeignKey("IDPredmet")]

        public Predmeti Predmet { get; set; }
        public int IDOblast { get; set; }
        [ForeignKey("IDOblast")]

        public Oblasti Oblast { get; set; }

        public string Nivo { get; set; }

        public string Pitanje { get; set; }

        public string Odgovor { get; set; }

        public string? Slika { get; set; }


    }
}
