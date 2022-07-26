using Business.Abstract;
using Business.Constants;
using Core.Dtos;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hasing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        public IUserService _userService;
        

        public AuthManager(IUserService userService)
        {
            _userService = userService;
           

        }
      

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByUserName(userForLoginDto.Username);
            if (userToCheck == null)
            {

                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }


        public DataResult<User> UserExists(string userName)
        {
            var dataUser = _userService.GetByUserName(userName);
            if (dataUser != null)
                return new ErrorDataResult<User>(dataUser, "This user is already registered in the system");
            return new SuccessDataResult<User>(dataUser);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                UserName = userForRegisterDto.Username,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                Role = userForRegisterDto.Role,


            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, "Success");
        }



    }
}
