using ContactsApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RepositoriesLayer.Models;
using ServicesLayer.Implementation;
using ServicesLayer.Interfaces;

namespace ContactsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactViewModel contactVM)
        {
            try
            {
                var contact = new Contact
                {
                    FirstName = contactVM.FirstName,
                    LastName = contactVM.LastName,
                    Email = contactVM.Email,
                    PhoneNumbers = GetPhoneNumbers(contactVM.PhoneNumbers)

                };

                var createdContact = await _contactService.CreateContact(contact);
                return CreatedAtAction(nameof(GetContact), new { id = createdContact.Id }, createdContact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(string id)
        {
            var contact = await _contactService.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }

            var contactVM = new ContactViewModel
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                PhoneNumbers = GetPhoneNumbersVM(contact.PhoneNumbers)
            };

            return Ok(contactVM);
        }

        [HttpGet("search/name")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var contacts = await _contactService.SearchByName(name);
            var contactsVM = new List<ContactViewModel>();

            foreach (var contact in contacts)
            {
                contactsVM.Add(new ContactViewModel
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    PhoneNumbers = GetPhoneNumbersVM(contact.PhoneNumbers)
                });
            }

            return Ok(contactsVM);
        }

        [HttpGet("search/phonenumber")]
        public async Task<IActionResult> SearchByPhoneNumber([FromQuery] string phoneNumber)
        {
            var contacts = await _contactService.SearchByPhoneNumber(phoneNumber);
            var contactsVM = new List<ContactViewModel>();

            foreach (var contact in contacts)
            {
                contactsVM.Add(new ContactViewModel
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    PhoneNumbers = GetPhoneNumbersVM(contact.PhoneNumbers)
                });
            }

            return Ok(contactsVM);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(string id, ContactViewModel updatedContactVM)
        {
            var updatedContact = new Contact
            {
                FirstName = updatedContactVM.FirstName,
                LastName = updatedContactVM.LastName,
                Email = updatedContactVM.Email,
                PhoneNumbers = GetPhoneNumbers(updatedContactVM.PhoneNumbers)

            };
            var existingContact = await _contactService.UpdateContact(id, updatedContact);
            if (existingContact == null)
            {
                return NotFound();
            }

            var existingContactVM = new ContactViewModel
            {
                FirstName = existingContact.FirstName,
                LastName = existingContact.LastName,
                Email = existingContact.Email,
                PhoneNumbers = GetPhoneNumbersVM(existingContact.PhoneNumbers)
            };
            return Ok(existingContactVM);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            var result = await _contactService.DeleteContact(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        public List<PhoneNumber> GetPhoneNumbers(List<PhoneNumberViewModel> phonesVM)
        {
            var phones = new List<PhoneNumber>();
            foreach (var phone in phonesVM)
            {
                phones.Add(new PhoneNumber
                {
                    Number = phone.Number,
                    Type = phone.Type
                });
            }

            return phones;
        }

        public List<PhoneNumberViewModel> GetPhoneNumbersVM(List<PhoneNumber> phones)
        {
            var phonesVM = new List<PhoneNumberViewModel>();
            foreach (var phone in phones)
            {
                phonesVM.Add(new PhoneNumberViewModel
                {
                    Number = phone.Number,
                    Type = phone.Type
                });
            }

            return phonesVM;
        }
    }
}
