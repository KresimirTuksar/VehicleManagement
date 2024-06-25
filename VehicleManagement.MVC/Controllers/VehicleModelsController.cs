using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Service.Data;
using VehicleManagement.Service.Models.Entities;
using VehicleManagement.Service.Models.Request;
using VehicleManagement.Service.Models.Response;
using VehicleManagement.Service.Services;

namespace VehicleManagement.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehicleService _service;

        public VehicleModelsController(ApplicationDbContext context, IVehicleService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/VehicleModels
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<VehicleMakeResponse>>> GetVehicleModels(string name = "", string abrv = "", string sortBy = "Name", string sortOrder = "ASC", int pageSize = 5, int currentPage = 1)
        {
            var response = await _service.GetVehicleModelsAsync(name, abrv, sortBy, sortOrder, pageSize, currentPage);

            if (response.Errors.Contains("NOT_FOUND"))
            {
                return NotFound();
            }
            return Ok(response);
        }

        // GET: api/VehicleModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModel>> GetVehicleModel(Guid id)
        {
            var response = await _service.GetVehicleModelByIdAsync(id);

            if (response.Errors.Contains("NOT_FOUND"))
            {
                return NotFound();
            }

            return Ok(response);
        }

        // PUT: api/VehicleModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleModel(Guid id, EditVehicleModel request)
        {
            request.Id = id;
            if (ModelState.IsValid)
            {

                var response = await _service.UpdateVehicleModelAsync(request);

                if (response.Success == false)
                {
                    return BadRequest(response.Errors.FirstOrDefault());
                }
                return NoContent();
            }
            return BadRequest();

        }

        // POST: api/VehicleModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleModel>> PostVehicleModel([Bind("Name,Abrv,VehicleMakeId")] CreateVehicleModel request)
        {
            if (ModelState.IsValid)
            {

                var response = await _service.CreateVehicleModelAsync(request);

                if (response.Success == false)
                {
                    if (response.Errors.Contains("NOT_FOUND")){
                        return NotFound();
                    };

                    return BadRequest(response.Errors.FirstOrDefault());
                }
                return CreatedAtAction("GetVehicleModel", response);
            }
            return BadRequest();
        }

        // DELETE: api/VehicleModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleModel(Guid? id)
        {
            if (id != null)
            {

                var response = await _service.DeleteVehicleModelAsync(id);

                if (response.Success == false)
                {
                    return BadRequest(response.Errors.FirstOrDefault());
                }
                return NoContent();
            }

            return BadRequest();
        }

        private bool VehicleModelExists(Guid id)
        {
            return _context.VehicleModels.Any(e => e.Id == id);
        }
    }
}
