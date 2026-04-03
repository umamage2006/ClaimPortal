namespace ClaimPortal.Api.DTOs
{
    public class EditClaimDto
    {
        public int ClaimId { get; set; }
        public string ProcessingStatus { get; set; }
        public string Remarks { get; set; }
    }
}
