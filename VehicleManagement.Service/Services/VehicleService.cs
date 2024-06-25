using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VehicleManagement.Service.Data;
using VehicleManagement.Service.Models.Entities;
using VehicleManagement.Service.Models.Request;
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

        public async Task<PaginatedResponse<VehicleMakeResponse>> GetVehicleMakesAsync(string name, string abrv, string orderBy = "Name", string sortOrder = "ASC", int pageSize = 10, int currentPage = 1)
        {
            var response = new PaginatedResponse<VehicleMakeResponse>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Errors = new List<string>(),
                Results = new List<VehicleMakeResponse>() // Ensure the Results list is initialized.
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
                if (currentPage > response.TotalCount)
                {
                    response.Errors.Add("NOT_FOUND");
                    return response;
                }



                // Apply pagination.
                query = query.Skip((currentPage - 1) * pageSize).Take(pageSize);

                var results = await query.ToListAsync();
                foreach (var result in results)
                {
                    response.Results.Add(new VehicleMakeResponse
                    {
                        
                        Name = result.Name,
                        Abrv = result.Abrv
                    });
                }

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

        public async Task<ResponseModel<VehicleMakeResponse>> GetVehicleMakeByIdAsync(Guid? id)
        {
            var response = new ResponseModel<VehicleMakeResponse>() { Errors = new List<string>(), Result = null };
            var result = await _context.VehicleMakes.FindAsync(id);

            if (result == null)
            {
                response.Errors.Add("NOT_FOUND");
                return response;
            }
            response.Result = new VehicleMakeResponse()
            {
                Abrv = result.Abrv,
                Name = result.Name
            };
            return response;
        }

        public async Task<ResponseModel<bool>> CreateVehicleMakeAsync(CreateVehicleMake request)
        {
            var response = new ResponseModel<bool>() { Errors = new List<string>(), Result = false };
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Abrv))
                {
                    response.Errors.Add("EMPTY_FIELDS"); return response;
                }

                bool nameExists = await _context.VehicleMakes.AnyAsync(vm => vm.Name == request.Name);
                bool abrvExists = await _context.VehicleMakes.AnyAsync(vm => vm.Abrv == request.Abrv);

                if (nameExists || abrvExists)
                {
                    response.Errors.Add("NAME_OR_ABBREVIATION_ALREADY_EXISTS");
                    response.Result = false;
                    return response;
                }
                Guid guid = Guid.NewGuid();
                VehicleMake vehicleMake = new VehicleMake() { Id = guid, Name = request.Name, Abrv = request.Abrv };
                _context.VehicleMakes.Add(vehicleMake);
                await _context.SaveChangesAsync();
                response.Result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
        }

        public async Task<ResponseModel<bool>> UpdateVehicleMakeAsync(EditVehicleMake request)
        {
            var response = new ResponseModel<bool>() { Errors = new List<string>(), Result = false };

            try
            {
                // Check for empty fields
                if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Abrv))
                {
                    response.Errors.Add("EMPTY_FIELDS");
                    return response;
                }

                // Check for existing vehicle make with the same name or abrv
                bool nameExists = await _context.VehicleMakes.AnyAsync(vm => vm.Name == request.Name && vm.Id == request.Id);
                bool abrvExists = await _context.VehicleMakes.AnyAsync(vm => vm.Abrv == request.Abrv && vm.Id == request.Id);
                if (nameExists)
                {
                    response.Errors.Add("NAME_EXISTS");
                }
                if (abrvExists)
                {
                    response.Errors.Add("ABRV_EXISTS");
                }
                if (nameExists || abrvExists)
                {
                    return response;
                }

                // Find the existing vehicle make
                var vehicleMake = await _context.VehicleMakes.FindAsync(request.Id);
                if (vehicleMake == null)
                {
                    response.Errors.Add("NOT_FOUND");
                    return response;
                }

                // Update the vehicle make
                vehicleMake.Name = request.Name;
                vehicleMake.Abrv = request.Abrv;
                _context.VehicleMakes.Update(vehicleMake);
                await _context.SaveChangesAsync();

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);

            }

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteVehicleMakeAsync(Guid? id)
        {
            var response = new ResponseModel<bool>() { Errors = new List<string>(), Result = false };

            try
            {
                var existingVehicleMake = await _context.VehicleMakes.FindAsync(id);
                if (existingVehicleMake == null)
                {
                    response.Errors.Add("NOT_FOUND");
                    return response;
                }

                _context.VehicleMakes.Remove(existingVehicleMake);
                await _context.SaveChangesAsync();

                response.Result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
        }

        public async Task<PaginatedResponse<VehicleModelResponse>> GetVehicleModelsAsync(string name, string abrv, string orderBy = "Name", string sortOrder = "ASC", int pageSize = 10, int currentPage = 1)
        {
            var response = new PaginatedResponse<VehicleModelResponse>
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Errors = new List<string>(),
                Results = new List<VehicleModelResponse>()
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

                var results = await query.ToListAsync();
                foreach (var result in results)
                {
                    response.Results.Add(new VehicleModelResponse
                    {

                        Name = result.Name,
                        Abrv = result.Abrv
                    });
                }

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
        public async Task<ResponseModel<VehicleModelResponse>> GetVehicleModelByIdAsync(Guid id)
        {
            var response = new ResponseModel<VehicleModelResponse>() { Errors = new List<string>(), Result = null };
            var result = await _context.VehicleModels.FindAsync(id);

            if (result == null)
            {
                response.Errors.Add("NOT_FOUND");
                return response;
            }
            response.Result = new VehicleModelResponse()
            {
                Abrv = result.Abrv,
                Name = result.Name
            };
            return response;
        }

        public async Task<ResponseModel<bool>> CreateVehicleModelAsync(CreateVehicleModel request)
        {
            var response = new ResponseModel<bool>() { Errors = new List<string>(), Result = false };
            var result = await _context.VehicleMakes.FindAsync(request.VehicleMakeId);

            if (result == null)
            {
                response.Errors.Add("NOT_FOUND");
                return response;
            }
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Abrv))
                {
                    response.Errors.Add("EMPTY_FIELDS");
                    return response;
                }

                bool nameExists = await _context.VehicleModels.AnyAsync(vm => vm.Name == request.Name);
                bool abrvExists = await _context.VehicleModels.AnyAsync(vm => vm.Abrv == request.Abrv);

                if (nameExists || abrvExists)
                {
                    response.Errors.Add("NAME_OR_ABBREVIATION_ALREADY_EXISTS");
                    response.Result = false;
                    return response;
                }
                Guid guid = Guid.NewGuid();
                VehicleModel vehiclemodel = new VehicleModel() { Id = guid, Name = request.Name, Abrv = request.Abrv, MakeId = request.VehicleMakeId };
                _context.VehicleModels.Add(vehiclemodel);
                await _context.SaveChangesAsync();
                response.Result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
        }

        public async Task<ResponseModel<bool>> UpdateVehicleModelAsync(EditVehicleModel request)
        {
            var response = new ResponseModel<bool>() { Errors = new List<string>(), Result = false };

            try
            {
                // Check for empty fields
                if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Abrv))
                {
                    response.Errors.Add("EMPTY_FIELDS");
                    return response;
                }

                // Check for existing vehicle make with the same name or abrv
                bool nameExists = await _context.VehicleModels.AnyAsync(vm => vm.Name == request.Name && vm.Id == request.Id);
                bool abrvExists = await _context.VehicleModels.AnyAsync(vm => vm.Abrv == request.Abrv && vm.Id == request.Id);
                if (nameExists)
                {
                    response.Errors.Add("NAME_EXISTS");
                }
                if (abrvExists)
                {
                    response.Errors.Add("ABRV_EXISTS");
                }
                if (nameExists || abrvExists)
                {
                    return response;
                }

                // Find the existing vehicle make
                var vehiclemodel = await _context.VehicleModels.FindAsync(request.Id);
                if (vehiclemodel == null)
                {
                    response.Errors.Add("NOT_FOUND");
                    return response;
                }

                // Update the vehicle make
                vehiclemodel.Name = request.Name;
                vehiclemodel.Abrv = request.Abrv;
                _context.VehicleModels.Update(vehiclemodel);
                await _context.SaveChangesAsync();

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);

            }

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteVehicleModelAsync(Guid? id)
        {
            var response = new ResponseModel<bool>() { Errors = new List<string>(), Result = false };

            try
            {
                var existingVehicleModel = await _context.VehicleModels.FindAsync(id);
                if (existingVehicleModel == null)
                {
                    response.Errors.Add("NOT_FOUND");
                    return response;
                }

                _context.VehicleModels.Remove(existingVehicleModel);
                await _context.SaveChangesAsync();

                response.Result = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                return response;
            }
        }
    }
}
