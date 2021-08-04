using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoBadges_Repo
{
    public class BadgeRepo
    {
        public static List<Badge> allBadges = new List<Badge>();
        Dictionary<int, List<String>> badges = allBadges.ToDictionary(k => k.BadgeID, k => k.DoorAccess);

        //Create
        public void AddNewBadge(Badge newBadge)
        {
            badges.Add(newBadge.BadgeID, newBadge.DoorAccess);
            allBadges.Add(newBadge);
        }
        // Read
        public Dictionary<int, List<string>> ViewAllBadges()
        {
            return badges;
        }
        // Update
        public void UpdateExistingBadge(int id, List<string> newDoors)
        {
            foreach(KeyValuePair<int, List<string>> kvp in badges)
            {
                if (kvp.Key == id)
                {
                    badges[kvp.Key] = newDoors;
                }
            }
        }
        // Delete
        public bool RemoveBadge(int id)
        {
            bool isRemoved = badges.Remove(id);
            if (isRemoved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
