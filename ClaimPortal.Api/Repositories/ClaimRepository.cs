using ClaimPortal.Api.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClaimPortal.Api.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly IConfiguration _config;
        private readonly string _conn;

        public ClaimRepository(IConfiguration config)
        {
            _config = config;
            _conn = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<List<ClaimDto>> GetClaims(string claimNumber, string status)
        {
            using var con = new SqlConnection(_conn);

            var result = await con.QueryAsync<ClaimDto>(
                "sp_GetAllClaims",
                new { ClaimNumber = claimNumber, Status = status },
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }

        public async Task<ClaimDto?> GetClaimById(int id)
        {
            using var con = new SqlConnection(_conn);

            return await con.QueryFirstOrDefaultAsync<ClaimDto>(
                "sp_GetClaimById",
                new { ClaimId = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> UpdateClaim(EditClaimDto model)
        {
            using var con = new SqlConnection(_conn);

            var affected = await con.ExecuteAsync(
                "sp_UpdateClaim",
                new
                {
                    model.ClaimId,
                    model.ProcessingStatus,
                    model.Remarks,
                    UpdatedBy = "admin"
                },
                commandType: CommandType.StoredProcedure
            );

            return affected > 0;
        }

        public async Task<List<ClaimHistoryDto>> GetClaimHistory(string claimNumber)
        {
            using var con = new SqlConnection(_conn);

            var result = await con.QueryAsync<ClaimHistoryDto>(
                "sp_GetClaimHistory",
                new { ClaimNumber = claimNumber },
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }
    }
}