using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> repoClients;

        public ClientService(IRepository<Client> repoClients)
        {
            this.repoClients = repoClients;
        }

        public Client AddClient(Client newClient)
        {
            repoClients.Insert(newClient);
            return newClient;
        }

        public IEnumerable<Client> GetAllClients()
        {
            var allClients = repoClients.TableNoTracking.ToList();
            return allClients;
        }

        public Client GetClientById(int id)
        {
            var client = repoClients.TableNoTracking.Where(c => c.Id == id).FirstOrDefault();
            return client;
        }
    }
}
