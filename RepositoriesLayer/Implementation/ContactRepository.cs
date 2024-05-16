using MongoDB.Driver;
using RepositoriesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesLayer.Implementation
{
    public class ContactRepository
    {
        private readonly IMongoCollection<Contact> _contactsCollection;

        public ContactRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("YourDatabaseName");
            _contactsCollection = database.GetCollection<Contact>("Contacts");
        }

        public async Task<Contact> CreateContact(Contact contact)
        {
            await _contactsCollection.InsertOneAsync(contact);
            return contact;
        }

        public async Task<Contact> GetContact(string id)
        {
            var contact = await _contactsCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            return contact;
        }

        public async Task<List<Contact>> SearchByName(string name)
        {
            var contacts = await _contactsCollection.Find(c => c.FirstName.Contains(name) || c.LastName.Contains(name)).ToListAsync();
            return contacts;
        }

        public async Task<List<Contact>> SearchByPhoneNumber(string phoneNumber)
        {
            var contacts = await _contactsCollection.Find(c => c.PhoneNumbers.Exists(p => p.Number == phoneNumber)).ToListAsync();
            return contacts;
        }

        public async Task<Contact> UpdateContact(string id, Contact updatedContact)
        {
            var existingContact = await _contactsCollection.FindOneAndReplaceAsync(c => c.Id == id, updatedContact);
            return existingContact;
        }

        public async Task<bool> DeleteContact(string id)
        {
            var result = await _contactsCollection.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
