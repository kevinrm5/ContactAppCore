using System.ComponentModel.DataAnnotations;

namespace ContactsApp.ViewModels
{
    public class PhoneNumberViewModel
    {
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phone number must contain only digits")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Phone number type is required")]
        public string Type { get; set; } // Home, Work, Mobile, etc.
    }
}
