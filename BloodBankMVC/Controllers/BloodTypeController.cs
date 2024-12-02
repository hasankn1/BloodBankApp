using BloodBank.DTOs;
using BloodBank.Models;
using BloodBankMVC.Models;
using BloodBankMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankMVC.Controllers
{
    public class BloodTypeController : Controller
    {
        private readonly BloodTypeService _service;

        public BloodTypeController(BloodTypeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string id)
        {
            var bloodTypes = await _service.GetAllBloodTypesAsync(id);
            return View(bloodTypes);
        }

        public async Task<IActionResult> Details(string id, string bloodTypeId)
        {
            var bloodTypeDetails = await _service.GetBloodTypeByIdAsync(id, bloodTypeId);
            return View(bloodTypeDetails);
        }

        [HttpGet]
        public IActionResult Create(string id)
        {
            var model = new BloodTypeInfo
            {
                Id = id,
                LastUpdated = DateTime.UtcNow
            };

            return View(model); // Render the form with an empty model
        }
        [HttpPost]
        public async Task<IActionResult> Create(string id, BloodTypeInfo model)
        {
            if (ModelState.IsValid)
            {
                model.Id = id;
                model.LastUpdated = DateTime.UtcNow;

                bool result = await _service.AddBloodTypeAsync(id, model);
                if (result)
                {
                    return RedirectToAction(nameof(Index), new { id });
                }
                else
                {
                    ModelState.AddModelError("", "Failed to add Blood Type.");
                }
            }
            return View(model); // If the form fails validation, show the same view with errors
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id, BloodTypeInfo model)
        {
            await _service.UpdateBloodTypeAsync(id, model);
            return RedirectToAction(nameof(Index), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStockLevel(string id, string bloodTypeId, BloodTypeInfo model)
        {
            await _service.UpdateBloodTypeStockLevelAsync(id, bloodTypeId, model.StockLevel);
            return RedirectToAction(nameof(Details), new { id, bloodTypeId });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id, string bloodTypeId)
        {
            var isDeleted = await _service.DeleteBloodTypeAsync(id, bloodTypeId);

            if (isDeleted)
            {
                return RedirectToAction(nameof(Index), new { id });
            }
            else
            {
                ModelState.AddModelError("", $"Failed to delete blood type '{bloodTypeId}' for Donation Center '{id}'.");
                return View(); // Return to the view in case of failure
            }
        }
    }
}
