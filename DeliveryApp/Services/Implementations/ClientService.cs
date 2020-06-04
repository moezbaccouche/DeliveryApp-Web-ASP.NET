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

        public Client DeleteClient(int clientId)
        {
            var client = repoClients.TableNoTracking
                .Where(c => c.Id == clientId)
                .FirstOrDefault();

            if(client != null)
            {
                repoClients.Delete(client);
            }
            return client;
        }

        public IEnumerable<Client> GetAllClients()
        {
            var allClients = repoClients.TableNoTracking
                .Where(c => c.HasValidatedEmail)
                .Include(c => c.Location)
                .ToList();
            return allClients;
        }

        public Client GetClientById(int id)
        {
            var client = repoClients.TableNoTracking
                .Where(c => c.Id == id)
                .Include(c => c.Location)
                .FirstOrDefault();
            return client;
        }

        public Client GetClientByIdentityId(string identityId)
        {
            var client = repoClients.TableNoTracking
                .Where(c => c.IdentityId == identityId)
                .FirstOrDefault();

            return client;
        }

        public Client UpdateClient(Client newClient)
        {
            var client = repoClients.Update(newClient);
            return client;
        }
    }
}
