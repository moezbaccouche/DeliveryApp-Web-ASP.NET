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
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> repoAdmins;

        public AdminService(IRepository<Admin> repoAdmins)
        {
            this.repoAdmins = repoAdmins;
        }

        public Admin AddAdmin(Admin admin)
        {
            if(admin != null)
            {
                repoAdmins.Insert(admin);
            }
            return admin;
        }

        public Admin EditAdmin(Admin admin)
        {
            if(admin != null)
            {
                repoAdmins.Update(admin);
            }

            return admin;
        }

        public Admin GetAdminByEmail(string email)
        {
            var admin = repoAdmins.TableNoTracking
                .Where(a => a.Email == email)
                .Include(a => a.Location)
                .FirstOrDefault();
            return admin;
        }

        public Admin GetAdminById(int id)
        {
            var admin = repoAdmins.TableNoTracking
                .Where(a => a.Id == id)
                .Include(a => a.Location)
                .FirstOrDefault();
            return admin;
        }

        public Admin GetAdminByIdentityId(string identityId)
        {
            var admin = repoAdmins.TableNoTracking
                .Where(a => a.IdentityId == identityId)
                .Include(a => a.Location)
                .FirstOrDefault();
            return admin;
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            var admins = repoAdmins.TableNoTracking.ToList();
            return admins;
        }
    }
}
