using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Dimensions
{
    public class DimAgeGroup
    {
        [Key]
        public Guid DimAgeGroupKey { get; set; }

        public Guid AgeGroupID { get; set; }

        public int LowerBound { get; set; }

        public int UpperBound { get; set; }
    }
}
