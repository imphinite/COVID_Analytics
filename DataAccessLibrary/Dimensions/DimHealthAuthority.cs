using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Dimensions
{
    public class DimHealthAuthority
    {
        [Key]
        public Guid DimHealthAuthorityKey { get; set; }

        public Guid HealthAuthorityID { get; set; }

        public Guid DimRegionKey { get; set; }
        public DimRegion DimRegion { get; set; }

        public List<DimHealthServiceDeliveryArea> DimHealthServiceDeliveryAreas { get; set; } = new List<DimHealthServiceDeliveryArea>();
    }
}
