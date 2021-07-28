using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoClaims_Repo
{
    public class ClaimRepo
    {
        public Queue<Claim> _currentQueue = new Queue<Claim>();
        // Create:
        public void AddNewClaim(Claim newClaim)
        {
            _currentQueue.Enqueue(newClaim);
        }
        // Read:
        public Queue<Claim> ViewCurrentQueue()
        {
            return _currentQueue;
        }
        // Update:
        // No need at this time for an update method

        // Delete:
        public Claim RemoveClaim()
        {
            return _currentQueue.Dequeue();
        }
    } 
}
