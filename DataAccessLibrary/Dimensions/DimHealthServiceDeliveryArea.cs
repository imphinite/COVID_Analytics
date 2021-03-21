﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Dimensions
{
    public class DimHealthServiceDeliveryArea
    {
        [Key]
        public Guid DimHealthServiceDeliveryAreaKey { get; set; }

        public Guid HealthServiceDeliveryAreaID { get; set; }

        public DimHealthAuthority DimHealthAuthority { get; set; }
    }
}
