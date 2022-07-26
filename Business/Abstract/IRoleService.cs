using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public  interface IRoleService
    {
        int Add(Role role);
        bool RoleExist(int roleCode);
        Role Get(int roleCode);
    }
}
