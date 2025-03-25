using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingAjaxJquery.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "⚠ Please enter the doctor's name.")]
        [DisplayName("Doctor's Name")]
        [StringLength(100, ErrorMessage = "⚠ Name cannot exceed 100 characters.")]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "⚠ Please select a specialization.")]
        [StringLength(50, ErrorMessage = "⚠ Specialization cannot exceed 50 characters.")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "⚠ Please enter the contact number.")]
        [Phone(ErrorMessage = "⚠ Please enter a valid phone number.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "⚠ Phone number must be exactly 10 digits.")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "⚠ Please enter the email address.")]
        [EmailAddress(ErrorMessage = "⚠ Please enter a valid email address.")]
        public string Email { get; set; }

        // Many-to-Many Relationship
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
