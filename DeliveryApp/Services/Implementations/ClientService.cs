using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
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
            var allClients = repoClients.TableNoTracking
                .Include(c => c.Location)
                .ToList();
            return allClients;
        }

        public Client GetClientById(int id)
        {
            var client = repoClients.TableNoTracking
                .Where(c => c.Id == id)
                .Include(c => c.Location)
                .Include(c => c.Orders)
                .FirstOrDefault();
            return client;
        }
    }
}
