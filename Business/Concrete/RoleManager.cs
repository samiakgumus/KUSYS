using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleManager:IRoleService
    {
        IRoleDal _roleDal;


        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;

        }

        public int Add(Role role)
        {
            return _roleDal.Add(role);
        }

        public Role Get(int roleCode)
        {
           return _roleDal.Get(filter: r => r.Code == roleCode);
             
        }


        public bool RoleExist(int roleCode)
        {
            var dataRole = _roleDal.Get(filter: r => r.Code == roleCode);

            return dataRole!=null?true:false;
        }
    }
}
