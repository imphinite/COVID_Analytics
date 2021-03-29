using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Models
{
    public class AgeGroup
    {
        public Guid AgeGroupID { get; set; }

        [Column(TypeName = "nvarchar(128) default 'Unknown'")]
        public string Range { get; set; }
    }
}
