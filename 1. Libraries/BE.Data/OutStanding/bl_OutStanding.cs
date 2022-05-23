using BE.Core;
using BE.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BE.Data.Order
{
    public class bl_OutStanding
    {
        public UnitOfWork _objUnitOfWork = new UnitOfWork();

        public T_OutStanding Create(T_OutStanding ObjOutStanding)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_OutStanding_Repository.Insert(ObjOutStanding);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjOutStanding;
        }

        public T_OutStanding Update(T_OutStanding ObjOutStanding)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_OutStanding_Repository.Update(ObjOutStanding);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjOutStanding;
        }

        public T_OutStanding Delete(T_OutStanding ObjOutStanding)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_OutStanding_Repository.Delete(ObjOutStanding.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjOutStanding;
        }

        public bool BulkDelete(List<T_OutStanding> objList)
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

        public T_OutStanding GetFirstOrDefault(T_OutStanding ObjOutStanding)
        {
            var ReturnCompanyObj = new T_OutStanding();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ReturnCompanyObj = _objUnitOfWork._T_OutStanding_Repository.GetFirstOrDefault(x => x.OSNumber == ObjOutStanding.OSNumber);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ReturnCompanyObj;
        }

        public T_OutStanding GetById(Guid Id)
        {
            var ObjOutStanding = new T_OutStanding();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjOutStanding = _objUnitOfWork._T_OutStanding_Repository.GetById(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjOutStanding;
        }

        public List<T_OutStanding> GetList(T_OutStanding ObjOutStanding)
        {
            var ObjList = new List<T_OutStanding>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryOutStanding = _objUnitOfWork._T_OutStanding_Repository.Query();
                    var queryOutStandingDetails = _objUnitOfWork._T_OutStandingDetails_Repository.Query();
                    var querySalesOrder = _objUnitOfWork._T_SalesOrder_Repository.Query();

                    if (ObjOutStanding.EventDate != null && ObjOutStanding.EventDate.ToString("yyyy-MM-dd") != "0001-01-01")
                    {
                        queryOutStanding = queryOutStanding.Where(x => x.EventDate == ObjOutStanding.EventDate);
                    }
                    if (!string.IsNullOrWhiteSpace(ObjOutStanding.OSNumber))
                    {
                        queryOutStanding = queryOutStanding.Where(x => x.OSNumber.Contains(ObjOutStanding.OSNumber));
                    }
                    if (!string.IsNullOrWhiteSpace(ObjOutStanding.Name))
                    {
                        queryOutStanding = queryOutStanding.Where(x => x.Name.Contains(ObjOutStanding.Name));
                    }
                    queryOutStanding = queryOutStanding.OrderByDescending(x => x.CreatedDate);

                    ObjList = (from os in queryOutStanding
                               join so in querySalesOrder on os.SalesOrderId equals so.Id into OsSoJoin
                               from OsObj in OsSoJoin.DefaultIfEmpty()
                               select new
                               {
                                   id = os.Id,
                                   oSNumber = os.OSNumber,
                                   salesOrderId = OsObj.OrderId,
                                   amount = os.Amount,
                                   eventDate = os.EventDate,
                                   name = os.Name,
                                   remark = os.Remark,
                                   createdDate = os.CreatedDate
                               }).ToList().Select(x => new T_OutStanding
                               {
                                   Id = x.id,
                                   OSNumber = x.oSNumber,
                                   SalesOrderNumber = x.salesOrderId,
                                   Amount = x.amount,
                                   PaidAmount = queryOutStandingDetails.Where(s => s.OSId == x.id).ToList().Sum(y => y.PaidAmount),
                                   DueAmount = (x.amount - queryOutStandingDetails.Where(s => s.OSId == x.id).ToList().Sum(y => y.PaidAmount)),
                                   EventDate = x.eventDate,
                                   Name = x.name,
                                   Remark = x.remark,
                                   CreatedDate = x.createdDate
                               }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public string GetOrderNo()
        {
            var vOrderNo = string.Empty;
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vObj = _objUnitOfWork._T_OutStanding_Repository.Get().OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                    if (vObj != null)
                    {
                        string[] sVal = vObj.OSNumber.Split('-');
                        if (!string.IsNullOrWhiteSpace(sVal[2]))
                        {
                            vOrderNo = sVal[0] + "-" + sVal[1] + "-" + String.Format("{0:0000000000}", Convert.ToInt32(sVal[2]) + 1);
                        }
                        else
                            vOrderNo = "BE-OS-0000000001";
                    }
                    else
                        vOrderNo = "BE-OS-0000000001";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vOrderNo;
        }

        // For Edit Detail Pages 

        public T_OutStandingDetails CreateOutStandingDetail(T_OutStandingDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_OutStandingDetails_Repository.Insert(ObjModel);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public T_OutStandingDetails UpdateOutStandingDetail(T_OutStandingDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_OutStandingDetails_Repository.Update(ObjModel);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public T_OutStandingDetails DeleteOutStandingDetail(T_OutStandingDetails ObjModel)
        {
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    _objUnitOfWork._T_OutStandingDetails_Repository.Delete(ObjModel.Id);
                    _objUnitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjModel;
        }

        public List<T_OutStandingDetails> GetOutStandingDetailList(T_OutStanding ObjOutStanding)
        {
            var ObjList = new List<T_OutStandingDetails>();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var vList = (from OutStandingDetail in _objUnitOfWork._T_OutStandingDetails_Repository.Get()
                                 join OutStanding in _objUnitOfWork._T_OutStanding_Repository.Get() on OutStandingDetail.OSId equals OutStanding.Id
                                 where OutStandingDetail.OSId == ObjOutStanding.Id

                                 select new T_OutStandingDetails()
                                 {
                                     Id = OutStandingDetail.Id,
                                     EventDate = OutStandingDetail.EventDate,
                                     OSId = OutStandingDetail.OSId,
                                     SlNo = OutStandingDetail.SlNo,
                                     OutStandingAmount = OutStandingDetail.OutStandingAmount,
                                     PaidAmount = OutStandingDetail.PaidAmount,
                                     DueAmount = OutStandingDetail.DueAmount,
                                     Remark = OutStandingDetail.Remark,
                                     CreatedDate = OutStandingDetail.CreatedDate
                                 }).OrderBy(x => x.SlNo).ToList();

                    ObjList = vList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public T_OutStandingDetails GetOutStandingOutStandingDetail(T_OutStanding ObjOutStanding)
        {
            var ObjList = new T_OutStandingDetails();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    var queryOutStanding = _objUnitOfWork._T_OutStanding_Repository.Query();
                    var queryOutStandingDetail = _objUnitOfWork._T_OutStandingDetails_Repository.Query();

                    var vOutStanding = queryOutStanding.Where(x => x.Id == ObjOutStanding.Id).FirstOrDefault();
                    var vOutStandingDetail = queryOutStandingDetail.Where(x => x.OSId == ObjOutStanding.Id).ToList();

                    if (vOutStanding != null)
                    {
                        ObjList = vOutStandingDetail.ToList().Select(x => new T_OutStandingDetails()
                        {
                            OutStandingAmount = (vOutStanding.Amount - vOutStandingDetail.Sum(y => y.PaidAmount)),
                        }).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }

        public T_OutStandingDetails GetByIdOutStandingDetail(Guid UserId)
        {
            var ObjOutStanding = new T_OutStandingDetails();
            try
            {
                using (_objUnitOfWork = new UnitOfWork())
                {
                    ObjOutStanding = _objUnitOfWork._T_OutStandingDetails_Repository.GetById(UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ObjOutStanding;
        }
    }
}
