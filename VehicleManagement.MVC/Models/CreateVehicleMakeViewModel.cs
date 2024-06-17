using VehicleManagement.Service.Models.Request;
using VehicleManagement.Service.Models.Response;

namespace VehicleManagement.MVC.Models
{
    public class CreateVehicleMakeViewModel
    {
        public CreateVehicleMake Request { get; set; }
        public ResponseModel<bool> Response { get; set; }
    }
}
