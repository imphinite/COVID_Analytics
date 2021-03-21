using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Models
{
    public class AgeGroup
    {
        public Guid AgeGroupID { get; set; }

        [Column(TypeName = "integer default '0'")]
        public int LowerBound { get; set; }

        public int UpperBound { get; set; }
    }
}
