using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingAjaxJquery.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "⚠ Please enter the patient's name.")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "⚠ Please enter the age.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "⚠ Please enter the gender.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "⚠ Please enter the contact number.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "⚠ Phone number must be exactly 10 digits.")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "⚠ Please enter the address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "⚠ Please enter the disease.")]
        public string Disease { get; set; }

        // Many-to-Many Relationship
        public ICollection<Doctor>? Doctors { get; set; } = new List<Doctor>();
    }
}
