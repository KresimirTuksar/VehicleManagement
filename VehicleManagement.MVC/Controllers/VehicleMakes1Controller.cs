using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class VehicleMakes1Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehicleService _service;


        public VehicleMakes1Controller(ApplicationDbContext context, IVehicleService service)
        {
            _context = context;
            _service = service;

        }

        // GET: api/VehicleMakes1
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<VehicleMakeResponse>>> GetVehicleMakes(string name = "", string abrv = "", string sortBy = "Name", string sortOrder = "ASC", int pageSize = 5, int currentPage = 1)
        {
            var response = await _service.GetVehicleMakesAsync(name, abrv, sortBy, sortOrder, pageSize, currentPage);
            
            if (response.Errors.Contains("NOT_FOUND"))
            {
                return NotFound();
            }
            return Ok(response);
        }

        // GET: api/VehicleMakes1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<VehicleMakeResponse>>> GetVehicleMake(Guid id)
        {
            var response = await _service.GetVehicleMakeByIdAsync(id);

            if (response.Errors.Contains("NOT_FOUND"))
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("vehicleMake/{id}")]
        public async Task<IActionResult> GetVehicleMakeByIdWithModels(Guid id)
        {
            var response = await _service.GetVehicleMakeByIdWithModelsAsync(id);

            if (response.Errors.Contains("NOT_FOUND"))
            {
                return NotFound();
            }

            return Ok(response);
        }

        // PUT: api/VehicleMakes1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<bool>>> PutVehicleMake(Guid id, EditVehicleMake request)
        {
            request.Id = id;
            if (ModelState.IsValid)
            {

                var response = await _service.UpdateVehicleMakeAsync(request);

                if (response.Success == false)
                {
                    return BadRequest(response.Errors.FirstOrDefault());
                }
                return NoContent();
            }
            return BadRequest();
        }

        // POST: api/VehicleMakes1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponseModel<VehicleMake>>> PostVehicleMake([Bind("Name,Abrv")] CreateVehicleMake request)
        {
            if (ModelState.IsValid)
            {
                
                var response = await _service.CreateVehicleMakeAsync(request);

                if (response.Success == false)
                {
                    return BadRequest(response.Errors.FirstOrDefault());
                }
                return CreatedAtAction("GetVehicleMake", response);
            }
            return BadRequest();
        }

        // DELETE: api/VehicleMakes1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMake(Guid? id)
        {
            if (id != null)
            {

                var response = await _service.DeleteVehicleMakeAsync(id);

                if (response.Success == false)
                {
                    return BadRequest(response.Errors.FirstOrDefault());
                }
                return NoContent();
            }
            return BadRequest();
        }

    }
}
