using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Service.Models;

namespace VehicleManagement.Service.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleMake>> GetVehicleMakesAsync();
        Task<VehicleMake> GetVehicleMakeByIdAsync(Guid id);
        Task CreateVehicleMakeAsync(VehicleMake vehicleMake);
        Task UpdateVehicleMakeAsync(VehicleMake vehicleMake);
        Task DeleteVehicleMakeAsync(VehicleMake vehicleMake);

        Task<List<VehicleModel>> GetVehicleModelsAsync();
        Task<VehicleModel> GetVehicleModelByIdAsync(Guid id);
        Task CreateVehicleModelAsync(VehicleModel vehicleModel);
        Task UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task DeleteVehicleModelAsync(VehicleModel vehicleModel);
    }
}
