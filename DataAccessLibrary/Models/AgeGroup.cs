using System;

namespace DataAccessLibrary.Models
{
    public class AgeGroup
    {
        public Guid AgeGroupID { get; set; }

        public int LowerBound { get; set; }

        public int UpperBound { get; set; }
    }
}
