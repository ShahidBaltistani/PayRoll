using System.ComponentModel.DataAnnotations;

namespace Appointment.WebApp.Models
{
    public class EmailFormModel
    {
        public string FromName { get; set; } = "Kashif";
        public string FromEmail { get; set; } = "kashifdotnetdeveloper@gmail.com";
        public string Message { get; set; } = "Thanks";
        public string ToEmail { get; set; }
    }
}