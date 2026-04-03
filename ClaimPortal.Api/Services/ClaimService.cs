using ClaimPortal.Api.DTOs;
using ClaimPortal.Api.Repositories;

namespace ClaimPortal.Api.Services
{
    public class ClaimService
    {
        private readonly IClaimRepository _repo;

        public ClaimService(IClaimRepository repo)
        {
            _repo = repo;
        }

        public Task<List<ClaimDto>> GetClaims(string claimNumber, string status)
            => _repo.GetClaims(claimNumber, status);

        public Task<ClaimDto?> GetClaimById(int id)
            => _repo.GetClaimById(id);

        public Task<bool> UpdateClaim(EditClaimDto model)
            => _repo.UpdateClaim(model);

        public Task<List<ClaimHistoryDto>> GetHistory(string claimNumber)
            => _repo.GetClaimHistory(claimNumber);
    }
}
