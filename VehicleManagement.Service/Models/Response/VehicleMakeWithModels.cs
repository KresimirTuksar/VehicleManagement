using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagement.Service.Models.Response
{
    public class VehicleMakeWithModels
    {
        public VehicleMakeResponse Make { get; set; }
        public List<VehicleModelResponse> Models { get; set; }
    }
}
