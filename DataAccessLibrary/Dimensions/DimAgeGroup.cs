using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Dimensions
{
    public class DimAgeGroup
    {
        [Key]
        public Guid DimAgeGroupKey { get; set; }

        public Guid AgeGroupID { get; set; }

        [Column(TypeName = "nvarchar(128) default 'Unknown'")]
        public string Range { get; set; }
    }
}
