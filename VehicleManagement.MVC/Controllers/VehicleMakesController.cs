using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.MVC.Models;
using VehicleManagement.Service.Data;
using VehicleManagement.Service.Models.Entities;
using VehicleManagement.Service.Models.Request;
using VehicleManagement.Service.Models.Response;
using VehicleManagement.Service.Services;

namespace VehicleManagement.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehicleService _service;

        public VehicleMakesController(ApplicationDbContext context, IVehicleService service)
        {
            _context = context;
            _service = service;
        }

        // GET: VehicleMakes
        public async Task<IActionResult> Index(int currentPage = 1, int pageSize = 5)
        {
            var response = await _service.GetVehicleMakesAsync("", "", "Name","ASC", pageSize, currentPage);
            if (response.Errors.Contains("NOT_FOUND"))
            {
                return View("NotFound");
            }
            return View(response);
        }

        // GET: VehicleMakes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var vehicleMake = await _context.VehicleMakes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMake == null)
            {
                return View("NotFound");
            }

            return View(vehicleMake);
        }

        // GET: VehicleMakes/Create
        public IActionResult Create()
        {
            CreateVehicleMakeViewModel viewModel = new CreateVehicleMakeViewModel();

            return View(viewModel);
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Abrv")] CreateVehicleMake request)
        {
            CreateVehicleMakeViewModel viewModel = new CreateVehicleMakeViewModel();
            if (ModelState.IsValid)
            {
                var response = await _service.CreateVehicleMakeAsync(request);

                if (response.Result == false)
                {
                    viewModel.Response = response;
                    return View(viewModel);
                }
                return RedirectToAction(nameof(Index));
            }
            viewModel.Request = request;
            return View(viewModel);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleMake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMakeExists(vehicleMake.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMakes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vehicleMake = await _context.VehicleMakes.FindAsync(id);
            if (vehicleMake != null)
            {
                _context.VehicleMakes.Remove(vehicleMake);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(Guid id)
        {
            return _context.VehicleMakes.Any(e => e.Id == id);
        }
    }
}
