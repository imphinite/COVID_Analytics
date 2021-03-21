using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Dimensions
{
    public class DimRegion
    {
        [Key]
        public Guid DimRegionKey { get; set; }

        public Guid RegionID { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Province { get; set; }
    }
}
