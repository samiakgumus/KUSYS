using Core.Entities.Concrete;
using Core.Utilities.Results;
 
using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {       
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        DataResult<User> UserExists(string userName);
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);


    }
}
