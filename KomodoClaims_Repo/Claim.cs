using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoClaims_Repo
{
    public class Claim
    {
        public enum ClaimType
        {
            Car = 1, House, Theft, Renter
        }
        public int ClaimID { get; set; }
        public string Description { get; set; }
        public double ClaimAmount { get; set; }
        public DateTime DateOfIncident { get; set; }
        public DateTime DateOfClaim { get; set; }
        public bool IsValid { get; set; }
        public ClaimType TypeOfClaim { get; set; }

        public Claim() { }
        public Claim(int id, string desc, double amt, DateTime doi, DateTime doc, bool valid, ClaimType type)
        {
            ClaimID = id;
            Description = desc;
            ClaimAmount = amt;
            DateOfIncident = doi;
            DateOfClaim = doc;
            TypeOfClaim = type;
            IsValid = valid;
        }
    }
}