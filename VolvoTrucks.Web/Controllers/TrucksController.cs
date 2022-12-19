using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VolvoTrucks.Data;
using VolvoTrucks.Models;

namespace VolvoTrucks.Controllers
{
    public class TrucksController : Controller
    {
        private readonly VolvoVehiclesContext _context;

        public TrucksController(VolvoVehiclesContext context)
        {
            _context = context;    
        }

        public async Task<IActionResult> Index()
        {
            var Trucks = _context.Trucks
                .Include(c => c.TruckModel)
                .AsNoTracking();
            return View(await Trucks.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks
                .Include(c => c.TruckModel)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.TruckID == id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        private void PopulateTruckModelsDropDownList(object selectedTruckModel = null)
        {
            var truckModelsQuery = from d in _context.TruckModels
                                   where (d.ModelName.StartsWith("FH") || d.ModelName.StartsWith("FM"))
                                   orderby d.ModelName
                                   select d;
            ViewBag.TruckModelID = new SelectList(truckModelsQuery.AsNoTracking(), "TruckModelID", "ModelName", selectedTruckModel);
        }

        private void PopulateTruckModelYearsDropDownList(object selectedTruckModel = null)
        {
            ViewBag.ModelYears = Enumerable.Range(DateTime.Now.Year, 2)
                                         .Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() })
                                         .ToList();
        }

        public IActionResult Create()
        {
            PopulateTruckModelsDropDownList();
            PopulateTruckModelYearsDropDownList();
            //var truck = new VolvoTrucks.Models.ViewModels.Truck();
            //truck.ModelYears = Enumerable.Range(DateTime.Now.Year, 1)
            //                             .Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() })
            //                             .ToList();
            var truck = new Truck();
            truck.ManufacturingYear = DateTime.Now.Year;
            return View(truck);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TruckID,ModelYear,ManufacturingYear,TruckModelID")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truck);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateTruckModelsDropDownList(truck.TruckModelID);
            PopulateTruckModelYearsDropDownList();
            //var viewModel = new VolvoTrucks.Models.ViewModels.Truck(truck);
            //viewModel.ModelYears = Enumerable.Range(DateTime.Now.Year, 1)
            //                             .Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() })
            //                             .ToList();
            return View(truck);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.TruckID == id);
            if (truck == null)
            {
                return NotFound();
            }
            PopulateTruckModelsDropDownList(truck.TruckModelID);
            PopulateTruckModelYearsDropDownList();
            //var viewModel = new Models.ViewModels.Truck(truck);
            //viewModel.ModelYears = Enumerable.Range(DateTime.Now.Year, 1)
            //                 .Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() })
            //                 .ToList();

            return View(truck);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckToUpdate = await _context.Trucks
                .SingleOrDefaultAsync(c => c.TruckID == id);

            if (await TryUpdateModelAsync<Truck>(truckToUpdate,
                "",
                c => c.ModelYear, c => c.TruckModelID, c => c.ManufacturingYear))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. ");
                }
                return RedirectToAction("Index");
            }
            PopulateTruckModelsDropDownList(truckToUpdate.TruckModelID);
            PopulateTruckModelYearsDropDownList();
            //var viewModel = new Models.ViewModels.Truck(truckToUpdate);
            //viewModel.ModelYears = Enumerable.Range(DateTime.Now.Year, 1)
            //                 .Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() })
            //                 .ToList();

            return View(truckToUpdate);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks
                .Include(c => c.TruckModel)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.TruckID == id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var truck = await _context.Trucks.SingleOrDefaultAsync(m => m.TruckID == id);
            _context.Trucks.Remove(truck);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
