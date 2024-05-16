using RepositoriesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesLayer.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> CreateContact(Contact contact);
        Task<Contact> GetContact(string id);
        Task<List<Contact>> SearchByName(string name);
        Task<List<Contact>> SearchByPhoneNumber(string phoneNumber);
        Task<Contact> UpdateContact(string id, Contact updatedContact);
        Task<bool> DeleteContact(string id);
    }
}
