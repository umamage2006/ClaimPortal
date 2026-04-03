using System;

namespace ClaimPortal.ViewModels
{
    public class ClaimViewModel
    {
        public int ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public string PatientName { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public string ProcessingStatus { get; set; }
        public string Remarks { get; set; }
    }
}
