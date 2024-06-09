using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Service.Models.Entities;
using VehicleManagement.Service.Models.Response;

namespace VehicleManagement.Service.Services
{
    public interface IVehicleService
    {
        Task<PaginatedResponse<VehicleMake>> GetVehicleMakesAsync(string name, string abrv, string orderBy, string sortOrder, int pageSize, int currentPage);
        Task<VehicleMake> GetVehicleMakeByIdAsync(Guid id);
        Task CreateVehicleMakeAsync(VehicleMake vehicleMake);
        Task UpdateVehicleMakeAsync(VehicleMake vehicleMake);
        Task DeleteVehicleMakeAsync(VehicleMake vehicleMake);

        Task<PaginatedResponse<VehicleModel>> GetVehicleModelsAsync(string name, string abrv, string orderBy, string sortOrder, int pageSize, int currentPage);
        Task<VehicleModel> GetVehicleModelByIdAsync(Guid id);
        Task CreateVehicleModelAsync(VehicleModel vehicleModel);
        Task UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task DeleteVehicleModelAsync(VehicleModel vehicleModel);
    }
}
