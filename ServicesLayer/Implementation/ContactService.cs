using MongoDB.Driver;
using RepositoriesLayer.Interfaces;
using RepositoriesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Implementation
{
    public class ContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> CreateContact(Contact contact)
        {
            try
            {
                await _contactRepository.CreateContact(contact);
                return contact;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Contact> GetContact(string id)
        {
            var contact = await _contactRepository.GetContact(id);
            return contact;
        }

        public async Task<List<Contact>> SearchByName(string name)
        {
            var contacts = await _contactRepository.SearchByName(name);
            return contacts;
        }

        public async Task<List<Contact>> SearchByPhoneNumber(string phoneNumber)
        {
            var contacts = await _contactRepository.SearchByPhoneNumber(phoneNumber);
            return contacts;
        }

        public async Task<Contact> UpdateContact(string id, Contact updatedContact)
        {
            var existingContact = await _contactRepository.UpdateContact(id, updatedContact);
            return existingContact;
        }

        public async Task<bool> DeleteContact(string id)
        {
            var result = await _contactRepository.DeleteContact(id);
            return result;
        }
    }
}
