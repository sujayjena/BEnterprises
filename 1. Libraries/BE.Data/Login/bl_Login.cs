using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.Login
{
   public class bl_Login
    {
        UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_User CheckUser(M_User ObjUser)
        {
            var vItem = _objUnitOfWork._M_User_Repository.GetFirstOrDefault(x => x.UserName == ObjUser.UserName && x.UserPassword == ObjUser.UserPassword);
            return vItem;
        }
    }
}
