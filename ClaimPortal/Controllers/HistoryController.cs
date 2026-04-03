using Microsoft.AspNetCore.Mvc;
using ClaimPortal.Filters;
using ClaimPortal.Services;
using ClaimPortal.ViewModels;

namespace ClaimPortal.Controllers
{
    [AuthFilter]
    public class HistoryController : Controller
    {
        private readonly ApiClientService _api;

        public HistoryController(ApiClientService api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index(string claimNumber)
        {
            var list = await _api.GetAsync<List<ClaimHistoryViewModel>>(
                $"Claims/History/{claimNumber}"
            );

            ViewBag.ClaimNumber = claimNumber;
            return View(list);
        }
    }
}
