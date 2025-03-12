namespace TestWebApplication.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InsuranceQuoteRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string VehicleRegistrationNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


}
