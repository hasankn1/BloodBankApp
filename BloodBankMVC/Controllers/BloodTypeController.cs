using BloodBank.DTOs;
using BloodBank.Models;
using BloodBankMVC.Models;
using BloodBankMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        // [HttpGet]
        public async Task<IActionResult> Details(string id, string bloodTypeId)
        {
            var bloodTypeDetails = await _service.GetBloodTypeByIdAsync(id, bloodTypeId);

            // Handle case where no blood type was found
            if (bloodTypeDetails == null)
            {
                // You can either return a "Not Found" view or an error message
                return NotFound($"Blood type {bloodTypeId} not found for Id: {id}");
            }

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


        // GET: Display the update form
        [HttpGet]
        public async Task<IActionResult> Edit(string id, string bloodTypeId)
        {
            // Fetch the blood type info to show current details
            var bloodTypeDetails = await _service.GetBloodTypeByIdAsync(id, bloodTypeId);
            if (bloodTypeDetails == null)
            {
                return NotFound();
            }

            return View(bloodTypeDetails); // Pass the model to the view
        }

        // POST: Handle the update of the blood type
        [HttpPost]
        public async Task<IActionResult> Edit(string id, string bloodTypeId, BloodTypeInfo model)
        {
            if (ModelState.IsValid)
            {
                var success = await _service.UpdateBloodTypeAsync(id, model);
                if (!success)
                {
                    ModelState.AddModelError("", "Failed to update blood type");
                    return View(model); // Return to the form with error
                }

                return RedirectToAction(nameof(Index)); // Redirect to a success page (like index or details)
            }

            return View(model); // Return to the form with validation errors
        }





        // GET: Display the update form
        [HttpGet]
        public async Task<IActionResult> UpdateStockLevel(string id, string bloodTypeId)
        {
            // Fetch the blood type info to show current stock level
            var bloodTypeDetails = await _service.GetBloodTypeByIdAsync(id, bloodTypeId);
            if (bloodTypeDetails == null)
            {
                return NotFound();
            }

            return View(bloodTypeDetails); // Pass the model to the view
        }

        // POST: Handle the stock level update
        [HttpPost]
        public async Task<IActionResult> UpdateStockLevel(string id, string bloodTypeId, int stockLevel)
        {
            // Update the blood type stock level
            bool success = await _service.UpdateBloodTypeStockLevelAsync(id, bloodTypeId, stockLevel);

            if (!success)
            {
                // Handle failure case if needed
                ModelState.AddModelError("", "Failed to update stock level");
                return View(); // Re-display the form with an error message
            }

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
