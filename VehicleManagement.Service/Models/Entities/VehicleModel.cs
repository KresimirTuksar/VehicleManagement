using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagement.Service.Models.Entities
{
    public class VehicleModel
    {
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public VehicleMake Make { get; set; }
    }
}
