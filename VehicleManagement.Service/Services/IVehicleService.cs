using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Service.Models.Entities;
using VehicleManagement.Service.Models.Request;
using VehicleManagement.Service.Models.Response;

namespace VehicleManagement.Service.Services
{
    public interface IVehicleService
    {
        public Task<PaginatedResponse<VehicleMake>> GetVehicleMakesAsync(string name, string abrv, string orderBy, string sortOrder, int pageSize, int currentPage);
        public Task<VehicleMake> GetVehicleMakeByIdAsync(Guid id);
        public Task<ResponseModel<bool>> CreateVehicleMakeAsync(CreateVehicleMake request);
        public Task UpdateVehicleMakeAsync(VehicleMake vehicleMake);
        public Task DeleteVehicleMakeAsync(VehicleMake vehicleMake);
         
        public Task<PaginatedResponse<VehicleModel>> GetVehicleModelsAsync(string name, string abrv, string orderBy, string sortOrder, int pageSize, int currentPage);
        public Task<VehicleModel> GetVehicleModelByIdAsync(Guid id);
        public Task CreateVehicleModelAsync(VehicleModel vehicleModel);
        public Task UpdateVehicleModelAsync(VehicleModel vehicleModel);
        public Task DeleteVehicleModelAsync(VehicleModel vehicleModel);
    }
}
