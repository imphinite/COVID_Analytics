using System;

namespace DataAccessLibrary.Models
{
    public class HealthServiceDeliveryArea
    {
        public Guid HealthServiceDeliveryAreaID { get; set; }

        public Guid HealthAuthorityID;
        public HealthAuthority HealthAuthority { get; set; }
    }
}
