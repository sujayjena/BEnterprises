using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.User
{
   public class bl_User
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_User Save(M_User ObjUser)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_User_Repository.Insert(ObjUser);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjUser;
        }
    }
}
