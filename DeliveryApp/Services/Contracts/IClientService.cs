using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IClientService
    {
        Client GetClientById(int id);
        Client GetClientByIdentityId(string identityId);
        Client AddClient(Client newClient);
        Client UpdateClient(Client newClient);
        IEnumerable<Client> GetAllClients();
        Client DeleteClient(int clientId);
    }
}
