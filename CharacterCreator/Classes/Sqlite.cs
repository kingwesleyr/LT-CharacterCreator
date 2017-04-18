using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MSDL.SQLite;

namespace CharacterCreator
{
    class Sqlite : SQLiteAdapter
    {
        public Sqlite() : base("osp.sqlite")
        {

        }

        public List<OccupationalSkill> GetSkills()
        {
            DataTable dt = Query("SELECT * FROM oslist LEFT JOIN osdesc ON osdesc.rowid = oslist.osid WHERE osid IS NOT NULL");
            List<OccupationalSkill> list = new List<OccupationalSkill>();
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    OccupationalSkill os = new OccupationalSkill();
                    
                    os.ID = Convert.ToInt32(row["id"].ToString());
                    Console.WriteLine(row["id"].ToString());
                    os.Name = row["os"].ToString();
                    os.Restricted = MSDL.Common.CheckBoolean(row["restrict"].ToString());
                    if (row["prereq"].ToString() != null && row["prereq"].ToString() != string.Empty)
                        os.PreReq = Convert.ToInt32(row["prereq"].ToString());
                    os.Cost = Convert.ToInt32(row["cost"].ToString());
                    os.Description = row["desc"].ToString();
                    list.Add(os);
                }
                catch { Console.WriteLine(row["id"].ToString() + " " + row["os"].ToString()); }
            }
            return list;
        }
    }
}
