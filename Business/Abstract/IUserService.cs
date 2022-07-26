using Core.Dtos;
 
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
    
        int Add(User user);
        int Update(User user);
        void Delete(User user);
        User GetByUserName(string userName);
        User Get(int userId);
        
       



    }
}
