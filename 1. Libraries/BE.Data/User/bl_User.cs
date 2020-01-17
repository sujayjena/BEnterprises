using BE.Core;
using BE.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BE.Data.User
{
    public class bl_User
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_User Create(M_User ObjUser)
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

        public M_User Update(M_User ObjUser)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_User_Repository.Update(ObjUser);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjUser;
        }

        public M_User Delete(M_User ObjUser)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_User_Repository.Delete(ObjUser.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjUser;
        }

        public List<M_User> GetList(M_User objUser)
        {
            var ObjList = new List<M_User>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_User_Repository.Query();
                    if (!string.IsNullOrWhiteSpace(objUser.Name))
                    {
                        queryObjList = queryObjList.Where(x => x.Name.Contains(objUser.Name));
                    }
                    if (!string.IsNullOrWhiteSpace(objUser.Phone))
                    {
                        queryObjList = queryObjList.Where(x => x.Phone.Contains(objUser.Phone));
                    }
                    ObjList = queryObjList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public M_User GetById(Guid UserId)
        {
            var ObjUser = new M_User();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjUser = _objUnitOfWork._M_User_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjUser;
        }

        public M_User CheckByNameNEmail(string UserName, string Email)
        {
            var ObjUser = new M_User();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = _objUnitOfWork._M_User_Repository.Query();
                    if (!string.IsNullOrWhiteSpace(UserName))
                    {
                        queryObjList = queryObjList.Where(x => x.UserName==UserName || x.Email == Email);
                    }
                    ObjUser = queryObjList.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjUser;
        }


        public List<M_Roles> GetRoleList()
        {
            var ObjList = new List<M_Roles>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjList = _objUnitOfWork._M_Roles_Repository.Get();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }
    }
}
