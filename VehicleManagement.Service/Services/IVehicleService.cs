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
        public Task<PaginatedResponse<VehicleMakeResponse>> GetVehicleMakesAsync(string name, string abrv, string orderBy, string sortOrder, int pageSize, int currentPage);
        public Task<ResponseModel<VehicleMakeResponse>> GetVehicleMakeByIdAsync(Guid? id);
        public Task<ResponseModel<bool>> CreateVehicleMakeAsync(CreateVehicleMake request);
        public Task<ResponseModel<bool>> UpdateVehicleMakeAsync(EditVehicleMake vehicleMake);
        public Task<ResponseModel<bool>> DeleteVehicleMakeAsync(Guid? id);
         
        public Task<PaginatedResponse<VehicleModelResponse>> GetVehicleModelsAsync(string name, string abrv, string orderBy, string sortOrder, int pageSize, int currentPage);
        public Task<ResponseModel<VehicleModelResponse>> GetVehicleModelByIdAsync(Guid id);
        public Task<ResponseModel<bool>> CreateVehicleModelAsync(CreateVehicleModel request);
        public Task<ResponseModel<bool>> UpdateVehicleModelAsync(EditVehicleModel request);
        public Task<ResponseModel<bool>> DeleteVehicleModelAsync(Guid? id);
    }
}
