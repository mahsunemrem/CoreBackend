using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        User GetByMail(string email);
        User GetByUserName(string userName);
        IDataResult<User> GetById(int id);
    }
}
