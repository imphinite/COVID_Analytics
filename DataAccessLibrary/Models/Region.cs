using DataAccessLibrary.Dimensions;
using DataAccessLibrary.Triggers;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Models
{
    public class Region
    {
        public Guid RegionID { get; set; }

        [Column(TypeName = "nvarchar(128) default 'Unknown'")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(128) default 'Unknown'")]
        public string Province { get; set; }
    }
}
