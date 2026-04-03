using ClaimPortal.Api.DTOs;

namespace ClaimPortal.Api.Repositories
{
    public interface IClaimRepository
    {
        Task<List<ClaimDto>> GetClaims(string claimNumber, string status);
        Task<ClaimDto?> GetClaimById(int id);
        Task<bool> UpdateClaim(EditClaimDto model);
        Task<List<ClaimHistoryDto>> GetClaimHistory(string claimNumber);
    }
}
