using Microsoft.EntityFrameworkCore;
using VehicleManagement.Service.Data;
using VehicleManagement.Service.Models.Entities;
using VehicleManagement.Service.Models.Response;

namespace VehicleManagement.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _context;

        public VehicleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResponse<VehicleMake>> GetVehicleMakesAsync(string name, string abrv, string orderBy = "Name", string sortOrder = "ASC", int pageSize = 10, int currentPage = 1)
        {
            var response = new PaginatedResponse<VehicleMake>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Errors = new List<string>(),
                Results = new List<VehicleMake>() // Ensure the Results list is initialized.
            };

            try
            {
                // Start with a query that may be further refined.
                var query = _context.VehicleMakes.AsQueryable();

                // Filtering by name if it's provided.
                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(v => v.Name.Contains(name));
                }

                // Filtering by abbreviation if it's provided.
                if (!string.IsNullOrWhiteSpace(abrv))
                {
                    query = query.Where(v => v.Abrv.Contains(abrv));
                }

                // Sorting the results.
                if (sortOrder.ToLower() == "asc")
                {
                    query = orderBy.ToLower() == "name" ? query.OrderBy(v => v.Name) : query.OrderBy(v => v.Abrv);
                }
                else
                {
                    query = orderBy.ToLower() == "name" ? query.OrderByDescending(v => v.Name) : query.OrderByDescending(v => v.Abrv);
                }

                // Calculate total count before pagination is applied.
                response.TotalCount = await query.CountAsync();

                // Apply pagination.
                query = query.Skip((currentPage - 1) * pageSize).Take(pageSize);

                // Execute the query and set the Results.
                response.Results = await query.ToListAsync();

                // Calculate the total pages.
                response.TotalPages = (int)Math.Ceiling((double)response.TotalCount / pageSize);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur and log them if necessary.
                response.Errors.Add(ex.Message);
            }

            return response;
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

        public async Task<PaginatedResponse<VehicleModel>> GetVehicleModelsAsync(string name, string abrv, string orderBy = "Name", string sortOrder = "ASC", int pageSize = 10, int currentPage = 1)
        {
            var response = new PaginatedResponse<VehicleModel>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Errors = new List<string>(),
                Results = new List<VehicleModel>()
            };

            try
            {
                // Start with a query that may be further refined.
                var query = _context.VehicleModels.AsQueryable();

                // Filtering by name if it's provided.
                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(m => m.Name.Contains(name));
                }

                // Filtering by abbreviation if it's provided.
                if (!string.IsNullOrWhiteSpace(abrv))
                {
                    query = query.Where(m => m.Abrv.Contains(abrv));
                }

                // Sorting the results.
                if (sortOrder.ToLower() == "asc")
                {
                    query = orderBy.ToLower() == "name" ? query.OrderBy(m => m.Name) : query.OrderBy(m => m.Abrv);
                }
                else
                {
                    query = orderBy.ToLower() == "name" ? query.OrderByDescending(m => m.Name) : query.OrderByDescending(m => m.Abrv);
                }

                // Calculate total count before pagination is applied.
                response.TotalCount = await query.CountAsync();

                // Apply pagination.
                query = query.Skip((currentPage - 1) * pageSize).Take(pageSize);

                // Execute the query and set the Results.
                response.Results = await query.ToListAsync();

                // Calculate the total pages.
                response.TotalPages = (int)Math.Ceiling((double)response.TotalCount / pageSize);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur and log them if necessary.
                response.Errors.Add(ex.Message);
            }

            return response;
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
