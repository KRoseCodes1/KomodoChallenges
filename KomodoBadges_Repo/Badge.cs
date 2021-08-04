using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoBadges_Repo
{
    public class Badge
    {
        public int BadgeID { get; set; }
        public List<string> DoorAccess { get; set; }
        public string BadgeName { get; set; }

        public Badge() { }
        public Badge(int id, List<string> doors, string name) 
        {
            BadgeID = id;
            DoorAccess = doors;
            BadgeName = name;
        }
    }
}
