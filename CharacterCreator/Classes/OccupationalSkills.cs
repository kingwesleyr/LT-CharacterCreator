using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public class OccupationalSkill
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public Boolean Restricted { get; set; }
        public int PreReq { get; set; }
        public string Description { get; set; }
    }
}
