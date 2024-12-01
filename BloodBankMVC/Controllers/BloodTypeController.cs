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

        [HttpPost]
        public async Task<IActionResult> Create(string id, BloodTypeInfo model)
        {
            model.Id = id; // Set the Id to the current Donation Center
            await _service.AddBloodTypeAsync(id, model);
            return RedirectToAction(nameof(Index), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, BloodTypeInfo model)
        {
            await _service.UpdateBloodTypeAsync(id, model);
            return RedirectToAction(nameof(Index), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStockLevel(string id, string bloodTypeId, BloodTypeInfo model)
        {
            await _service.UpdateBloodTypeStockLevelAsync(id, bloodTypeId, model.StockLevel);
            return RedirectToAction(nameof(Details), new { id, bloodTypeId });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id, string bloodTypeId)
        {
            await _service.DeleteBloodTypeAsync(id, bloodTypeId);
            return RedirectToAction(nameof(Index), new { id });
        }
    }
}
