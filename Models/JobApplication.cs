using System;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationManager.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public string JobTitle { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; } = DateTime.Today;

        [EmailAddress]
        public string? ContactEmail { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        public string? Notes { get; set; }
    }
}
