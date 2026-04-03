using Microsoft.AspNetCore.Mvc;
using ClaimPortal.Filters;
using ClaimPortal.Services;
using ClaimPortal.ViewModels;

namespace ClaimPortal.Controllers
{
    [AuthFilter]
    public class ClaimController : Controller
    {
        private readonly ApiClientService _api;

        public ClaimController(ApiClientService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index(string claimNumber = "", string status = "")
        {
            var list = await _api.GetAsync<List<ClaimViewModel>>(
                $"Claims?claimNumber={claimNumber}&status={status}"
            );

            return View(list);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var claim = await _api.GetAsync<EditClaimViewModel>($"Claims/{id}");
            return View(claim);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditClaimViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ProcessingStatus))
            {
                ViewBag.Error = "Processing Status is mandatory";
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(model.Remarks))
            {
                ViewBag.Error = "Remarks is mandatory";
                return View(model);
            }

            // ✅ FIX: Expect JSON object, NOT string
            var result = await _api.PostAsync<dynamic>("Claims/Update", model);

            // You can inspect the message if needed:
            // string msg = result.message;

            return RedirectToAction("Index");
        }
    }
}