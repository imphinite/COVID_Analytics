using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Dimensions
{
    public class DimLabTestReport
    {
        [Key]
        public Guid DimLabTestReportKey { get; set; }

        public Guid LabTestReportID { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public Guid DimHealthAuthorityKey { get; set; }
        public DimHealthAuthority DimHealthAuthority { get; set; }

        [Column(TypeName = "integer default '0'")]
        public int NewTests { get; set; }

        [Column(TypeName = "integer default '0'")]
        public int TotalTests { get; set; }

        [Column(TypeName = "decimal(10,1) default '0.0'")]
        public decimal Positivity { get; set; }

        [Column(TypeName = "decimal(10,1) default '0.0'")]
        public decimal TurnAround { get; set; }
    }
}
