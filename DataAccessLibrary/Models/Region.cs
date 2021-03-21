using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Models
{
    public class Region
    {
        public Guid RegionID { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Province { get; set; }
    }
}
