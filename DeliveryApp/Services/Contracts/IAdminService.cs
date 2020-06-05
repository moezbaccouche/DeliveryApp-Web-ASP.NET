using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Contracts
{
    public interface IAdminService
    {
        Admin AddAdmin(Admin admin);
        Admin GetAdminById(int id);
        Admin GetAdminByIdentityId(string identityId);
        Admin GetAdminByEmail(string email);
        Admin EditAdmin(Admin admin);
    }
}
