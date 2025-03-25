using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentBookingAjaxJquery.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Patient")]
        [DisplayName("Patient")]
        public int PatientId { get; set; }
        public virtual Patient? Patient { get; set; } 

        [Required]
        [ForeignKey("Doctor")]
        [DisplayName("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }

        [Required(ErrorMessage = "⚠ Please select an appointment date.")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? AppointmentDate { get; set; }

        [Required(ErrorMessage = "⚠ Please enter appointment status.")]
        public string Status { get; set; }
    }
}
