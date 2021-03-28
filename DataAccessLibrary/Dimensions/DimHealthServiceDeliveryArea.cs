using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Dimensions
{
    public class DimHealthServiceDeliveryArea
    {
        [Key]
        public Guid DimHealthServiceDeliveryAreaKey { get; set; }

        public Guid HealthServiceDeliveryAreaID { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string Area { get; set; }

        public DimHealthAuthority DimHealthAuthority { get; set; }
    }
}
