using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models
{
    public class Case
    {
        public Guid CaseID { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ReportedDate { get; set; }

        public Guid HealthAuthorityID { get; set; }
        public HealthAuthority HealthAuthority { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(32)")]
        public string Sex { get; set; }

        public Guid AgeGroupID { get; set; }
        public AgeGroup AgeGroup { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(128)")]
        public string ClassificationReported { get; set; }
    }
}
