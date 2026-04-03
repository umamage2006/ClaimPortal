using System;

namespace ClaimPortal.ViewModels
{
    public class ClaimHistoryViewModel
    {
        public string ClaimNumber { get; set; }
        public string Status { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Remarks { get; set; }
    }
}
