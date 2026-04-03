using Microsoft.AspNetCore.Mvc;
using ClaimPortal.Api.Services;
using ClaimPortal.Api.DTOs;

namespace ClaimPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly ClaimService _service;

        public ClaimsController(ClaimService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string claimNumber = "", string status = "")
        {
            var result = await _service.GetClaims(claimNumber, status);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetClaimById(id);
            if (result == null)
                return NotFound("Claim not found");

            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] EditClaimDto model)
        {
            var updated = await _service.UpdateClaim(model);
            return Ok(new { message = updated ? "Updated" : "Failed" });
        }

        [HttpGet("History/{claimNumber}")]
        public async Task<IActionResult> History(string claimNumber)
        {
            var result = await _service.GetHistory(claimNumber);
            return Ok(result);
        }
    }
}
