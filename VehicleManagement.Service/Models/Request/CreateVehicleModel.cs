﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagement.Service.Models.Request
{
    public class CreateVehicleModel
    {
        public string Name { get; set; }
        public string Abrv { get; set; }
        public Guid VehicleMakeId { get; set; }
    }
}
