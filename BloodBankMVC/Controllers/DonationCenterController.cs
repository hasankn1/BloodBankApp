using Microsoft.AspNetCore.Mvc;
using BloodBankMVC.Models;
using BloodBankMVC.Services;
using BloodBank.Models;
using BloodBank.DTOs;

public class DonationCenterController : Controller
{
    private readonly DonationCenterService _donationCenterService;

    public DonationCenterController(DonationCenterService donationCenterService)
    {
        _donationCenterService = donationCenterService;
    }

    public async Task<IActionResult> Index()
    {
        var centers = await _donationCenterService.GetAllDonationCentersAsync();
        return View(centers);
    }

    public async Task<IActionResult> Details(string id)
    {
        var center = await _donationCenterService.GetDonationCenterByIdAsync(id);
        if (center == null)
        {
            return NotFound();
        }
        return View(center);
    }

    [HttpGet]
    public async Task<IActionResult> Create(DonationCenter model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var success = await _donationCenterService.AddDonationCenterAsync(model);
        if (success)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Failed to create donation center.");
        return View(model);
    }
    public async Task<IActionResult> Edit(string id)
    {
        var center = await _donationCenterService.GetDonationCenterByIdAsync(id);
        if (center == null)
        {
            return NotFound();
        }
        return View(center);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, DonationCenter model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var success = await _donationCenterService.UpdateDonationCenterAsync(id, model);
        if (success)
        {
            return RedirectToAction("Index");
        }
        ModelState.AddModelError(string.Empty, "Failed to update donation center.");
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateHours(string id, DonationCenter updateHours)
{
    if (ModelState.IsValid)
    {
        await _donationCenterService.UpdateHoursOfOperationAsync(id, updateHours);
        return RedirectToAction("Details", new { id });
    }
    return View(updateHours);
}
    public async Task<IActionResult> Delete(string id)
    {
        var center = await _donationCenterService.GetDonationCenterByIdAsync(id);
        if (center == null)
        {
            return NotFound();
        }
        return View(center);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmDelete(string id)
    {
        var success = await _donationCenterService.DeleteDonationCenterAsync(id);
        if (success)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Failed to delete donation center.");
        return RedirectToAction("Index");
    }
}
