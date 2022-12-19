using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolvoTrucks.Data;
using VolvoTrucks.Models;

namespace VolvoTrucks.Controllers
{
    public class TruckModelsController : Controller
    {
        private readonly VolvoVehiclesContext _context;

        public TruckModelsController(VolvoVehiclesContext context)
        {
            _context = context;    
        }

        public async Task<IActionResult> Index()
        {
            var VolvoVehiclesContext = _context.TruckModels.OrderBy(tm => tm.ModelName);
            return View(await VolvoVehiclesContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string query = "SELECT * FROM TruckModel WHERE TruckModelID = {0}";
            var models = await _context.TruckModels
                .FromSql(query, id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (models == null)
            {
                return NotFound();
            }

            return View(models);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TruckModelID,ModelName")] TruckModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckModel = await _context.TruckModels
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.TruckModelID == id);
            if (truckModel == null)
            {
                return NotFound();
            }
            return View(truckModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckModelToUpdate = await _context.TruckModels.SingleOrDefaultAsync(m => m.TruckModelID == id);

            if (truckModelToUpdate == null)
            {
                TruckModel deletedTruckModel = new TruckModel();
                await TryUpdateModelAsync(deletedTruckModel);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The model was deleted by another user.");
                return View(deletedTruckModel);
            }

            if (await TryUpdateModelAsync<TruckModel>(
                truckModelToUpdate,
                "",
                s => s.ModelName, s => s.TruckModelID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (TruckModel)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The model was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (TruckModel)databaseEntry.ToObject();

                        if (databaseValues.ModelName != clientValues.ModelName)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.ModelName}");
                        }
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user.");
                    }
                }
            }
            return View(truckModelToUpdate);
        }

        public async Task<IActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.TruckModels
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.TruckModelID == id);
            if (model == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("Index");
                }
                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = "The record you attempted to delete was modified by another user.";
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TruckModel model)
        {
            try
            {
                if (await _context.TruckModels.AnyAsync(m => m.TruckModelID == model.TruckModelID))
                {
                    _context.TruckModels.Remove(model);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return RedirectToAction("Delete", new { concurrencyError = true, id = model.TruckModelID });
            }
        }

        private bool TruckModelExists(int id)
        {
            return _context.TruckModels.Any(e => e.TruckModelID == id);
        }
    }
}
