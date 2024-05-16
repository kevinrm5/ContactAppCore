using RepositoriesLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace ContactsApp.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name must be between 1 and 50 characters", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name must be between 1 and 50 characters", MinimumLength = 1)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public List<PhoneNumberViewModel> PhoneNumbers { get; set; }
    }
}
