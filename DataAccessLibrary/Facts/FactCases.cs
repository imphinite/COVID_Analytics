using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Facts
{
    public class FactCaseByRegionAndMonth
    {
        [Key]
        public Guid DimCaseKey { get; set; }

        public Guid CaseID { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ReportedDate { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string HealthAuthority { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Sex { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string AgeGroup { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string ClassificationReported { get; set; }
    }
}
