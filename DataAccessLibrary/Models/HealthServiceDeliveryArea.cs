using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Models
{
    public class HealthServiceDeliveryArea
    {
        public Guid HealthServiceDeliveryAreaID { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Area { get; set; }

        public HealthAuthority HealthAuthority { get; set; }
    }
}
