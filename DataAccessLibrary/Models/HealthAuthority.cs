using System;
using System.Collections.Generic;

namespace DataAccessLibrary.Models
{
    public class HealthAuthority
    {
        public Guid HealthAuthorityID { get; set; }

        public Guid RegionID { get; set; }
        public Region Region { get; set; }

        public List<HealthServiceDeliveryArea> HealthServiceDeliveryAreas { get; set; } = new List<HealthServiceDeliveryArea>();
    }
}
