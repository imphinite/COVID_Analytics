using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Dimensions
{
    public class DimCase
    {
        [Key]
        public Guid DimCaseKey { get; set; }
        
        public Guid CaseID { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ReportedDate { get; set; }

        public Guid DimHealthAuthorityKey { get; set; }
        public DimHealthAuthority DimHealthAuthority { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Sex { get; set; }

        public Guid DimAgeGroupKey { get; set; }
        public DimAgeGroup DimAgeGroup { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string ClassificationReported { get; set; }
    }
}
