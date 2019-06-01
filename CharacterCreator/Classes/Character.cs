using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public class Character
    {
        public string Name { get; set; }
        public Race Race { get; set; }
        public Faction Faction { get; set; }
        public List<string> Skills { get; set; }
        public List<OccupationalSkill> OccupationalSkills { get; set; }
        public string SpecialNotes { get; set; }
        public List<Year> Progression { get; set; }
    }
public enum Race
    {
        Human,
        Dwarf,
        Elf,
        Fey,
        Olog,
        Beastkin,
        Drow,
        Halfling,
        Uruck,
        Umbral,
       
      
        
    }
    public enum Faction
    {
        Bears,
        Dragons,
        Gryphons,
        Harts,
        Jackals,
        Lions,
        Tarantulas,
        Unicorns,
        Vipers,
        Wolves
    }

    public class Year
    {
        public int id { get; set; }
        public int YearlyCost { get; set; }
        public List<OccupationalSkill> SelectedSkills { get; set; }
    }

    public class Options
    {
        public static Dictionary<string, int> CharacterSkills = new Dictionary<String, int>()
        {
            {"Body Development",4},
            {"Body Development 2",8},
            {"Light Armour Use",2},
            {"Medium Armour Use",3},
            {"Heavy Armour Use",4},
            {"Shield Use",2},
            {"Large Weapon Use",2},
            {"Projectile Weapon",4},
            {"Thrown Weapon",1},
            {"Amidexterity",2},
            {"Healing",4},
            {"Healing 2",8},
            {"Incantation",4},
            {"Incantation 2",8},
            {"Spellcasting",4},
            {"Spellcasting 2",8},
            {"Ritual",2},
            {"Ritual 2",4},
            {"Ritual 3",6},
            {"Contribute",1},
            {"Power 4",2},
            {"Power 8",4},
            {"Power 12",6},
            {"Power 16",8},
            {"Invocation",3},
            {"Potion Lore",3},
            {"Poison Lore",4},
            {"Cartography",1},
            {"Sense Magic",1},
            {"Evaluate",1},
            {"Recognise Forgery",1},
            {"Physician",2},
            {"Bind Wounds",1}
        };
    }
}
