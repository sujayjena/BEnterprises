using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using static Meraki.DMS.Common.Helper.CommonConst;

namespace BE.API.Controllers
{
    public class CommonDataRequestModel
    {
        public string APIKey { get; set; }
        public string Action { get; set; }
        public string Param { get; set; }
        public string Type { get; set; }
    }

    public class CommonDataResponseModel
    {
        public string Result { get; set; }
        public string ErrorCode { get; set; }
    }

    public class CommonData 
    {
        public string ResultData { get; set; }
        public string ErrorCode { get; set; }
    }
}