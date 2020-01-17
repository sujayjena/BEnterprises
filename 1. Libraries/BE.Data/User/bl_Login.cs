using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.User
{
   public class bl_Login
    {
       public readonly UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_User CheckUser(M_User ObjUser)
        {
            try
            {
                var vItem = _objUnitOfWork._M_User_Repository.GetFirstOrDefault(x => x.UserName == ObjUser.UserName && x.UserPassword == ObjUser.UserPassword);
                return vItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetUserRoleName(M_User ObjUser)
        {
            try
            {
                var vItem = (from u in _objUnitOfWork.M_User_Repository.Get()
                            join r in _objUnitOfWork._M_Roles_Repository.Get() on u.RoleId equals r.Id
                            where u.UserName == ObjUser.UserName
                            select new { RoleName = r.RoleName }).FirstOrDefault();

                return vItem.RoleName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
