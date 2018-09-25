using GuessResult.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessResult.Repositories.Interfaces
{
    public interface IUserRepository
    {
        GRUser GetByEmailPassword(string email, string password);
        GRUser GetByLogin(string email);
        GRUser GetById(long userId);
        long? Save(GRUser rUser);
    }
}