using Microsoft.EntityFrameworkCore;
using VehicleManagement.Service.Data;
using VehicleManagement.Service.Models;

namespace VehicleManagement.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _context;

        public VehicleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleMake>> GetVehicleMakesAsync()
        {
            return await _context.VehicleMakes.ToListAsync();
        }

        public async Task<VehicleMake> GetVehicleMakeByIdAsync(Guid id)
        {
            return await _context.VehicleMakes.FindAsync(id);
        }

        public async Task CreateVehicleMakeAsync(VehicleMake vehicleMake)
        {
            _context.VehicleMakes.Add(vehicleMake);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVehicleMakeAsync(VehicleMake vehicleMake)
        {
            _context.VehicleMakes.Update(vehicleMake);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicleMakeAsync(VehicleMake vehicleMake)
        {
            _context.VehicleMakes.Remove(vehicleMake);
            await _context.SaveChangesAsync();
        }

        public async Task<List<VehicleModel>> GetVehicleModelsAsync()
        {
            return await _context.VehicleModels.ToListAsync();
        }

        public async Task<VehicleModel> GetVehicleModelByIdAsync(Guid id)
        {
            return await _context.VehicleModels.FindAsync(id);
        }

        public async Task CreateVehicleModelAsync(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Add(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVehicleModelAsync(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Update(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicleModelAsync(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Remove(vehicleModel);
            await _context.SaveChangesAsync();
        }
    }
}
