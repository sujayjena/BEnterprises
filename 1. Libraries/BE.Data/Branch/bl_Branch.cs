using System;
using System.Collections.Generic;
using System.Linq;
using BE.Core;
using BE.Services.UnitOfWork;

namespace BE.Data.Branch
{
    public class bl_Branch
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public M_Branch Create(M_Branch ObjBranch)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Branch_Repository.Insert(ObjBranch);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjBranch;
        }

        public M_Branch Update(M_Branch ObjBranch)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Branch_Repository.Update(ObjBranch);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjBranch;
        }

        public M_Branch Delete(M_Branch ObjBranch)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._M_Branch_Repository.Delete(ObjBranch.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjBranch;
        }

        public bool BulkDelete(List<M_Branch> objList)
        {
            bool bSuccess = false;
            try
            {
                foreach (var item in objList)
                {
                    var vCheckUser = GetById(item.Id);
                    if (vCheckUser != null)
                        Delete(vCheckUser);
                }
                bSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSuccess;
        }

        public M_Branch GetFirstOrDefault(M_Branch ObjBranch)
        {
            var ReturnCompanyObj = new M_Branch();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._M_Branch_Repository.GetFirstOrDefault(x => x.Name == ObjBranch.Name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public M_Branch GetById(Guid UserId)
        {
            var ObjBranch = new M_Branch();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjBranch = _objUnitOfWork._M_Branch_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjBranch;
        }

        public List<M_Branch> GetList(M_Branch ObjBranch)
        {
            var ObjList = new List<M_Branch>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryObjList = (from b in _objUnitOfWork._M_Branch_Repository.Get()
                                       join c in _objUnitOfWork._M_Company_Repository.Get() on b.CompanyId equals c.Id
                                       select new { id = b.Id, name = b.Name, email = b.Email, phone = b.Phone, address = b.Address, companyName = c.Name,createDate=b.CreatedDate }).OrderByDescending(x => x.createDate).ToList();

                    if(queryObjList.Count>0)
                    {
                        foreach (var item in queryObjList)
                        {
                            var objBranch = new M_Branch()
                            {
                                Id = item.id,
                                Name = item.name,
                                Email = item.email,
                                Phone = item.phone,
                                Address = item.address,
                                CompanyName=item.companyName,
                                CreatedDate=item.createDate
                            };
                            ObjList.Add(objBranch);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public List<M_Company> GetCompanyList()
        {
            var ObjList = new List<M_Company>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjList = _objUnitOfWork._M_Company_Repository.Get();
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
