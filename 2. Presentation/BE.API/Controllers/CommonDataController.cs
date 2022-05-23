using System;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BE.Core.ViewModel.API.Common;


namespace BE.API.Controllers
{
    public class CommonDataController : ApiController
    {
        [HttpPost]
        [Route("api/PostCommonAPI")]
        [ActionName("PostCommonAPI")]
        public async Task<IHttpActionResult> PostCommonAPI([FromBody] VM_Common vmCommonModel)
        {
            var result = new VM_CommonData();

            var vResult = "";

            //var rtnResult = _bllUser.GetUserDetailById(dtoUser);
            return Ok(new { Results = "" });
        }

        [HttpGet]
        [Route("api/GetCommonAPI")]
        [ActionName("GetCommonAPI")]
        public async Task<IHttpActionResult> GetCommonAPI([FromBody] VM_Common vmCommonModel)
        {
            //var rtnResult = _bllUser.GetUserDetailById(dtoUser);
            return Ok(new { Results = "" });
        }

        [ActionName("PostCommonData")]
        [HttpPost]
        public CommonDataResponseModel PostCommonData([FromBody] CommonDataRequestModel model)
        {
            string clientAddress = HttpContext.Current.Request.UserHostAddress;
            string logData = Convert.ToString(JsonConvert.SerializeObject(model));
            //   // LogDataComponent.CallLog("", clientAddress, "PostCommonData", logData);
            var resultString = "";
            int outputType = 0;
            //    CommonData result = new CommonData();
            //    try
            //    {
            //        if (string.IsNullOrEmpty(model.APIKey)
            //            || string.IsNullOrEmpty(model.Action)
            //            || string.IsNullOrEmpty(model.Param)
            //            || string.IsNullOrEmpty(model.Type))
            //            return GetResponseModel(CommonConst.COMMONDATA_ERRORCODE_INVALIDPARAM);

            //        if (!int.TryParse(model.Type, out outputType))
            //            return GetResponseModel(CommonConst.COMMONDATA_ERRORCODE_INVALIDPARAM);

            //        var outputTypeEnum = (CommonConst.COMMONDATA_OUTPUTTYPE)outputType;
            //        result = CommonDataComponent.ProcessCommonData(model.APIKey, model.Action, model.Param, outputTypeEnum);
            //        resultString = result.ResultData;

            //        //convert the result to url if result type is file
            //        //if (outputTypeEnum == CommonConst.COMMONDATA_OUTPUTTYPE.ZIP)
            //        //{
            //        //    //use the file path return from sp to make it http download
            //        //    resultString = GetDownloadUrl(Request.RequestUri, resultString);
            //        //}

            //        return new CommonDataResponseModel
            //        {
            //            Result = resultString,
            //            ErrorCode = result.ErrorCode
            //        };
            //    }
            //    catch (Exception ex)
            //    {
            //        //LogDataComponent.CallLog("", clientAddress, "PostCommonData", string.Format("[ERROR]: {0}: {1}", ex.Message, JsonConvert.SerializeObject(result)));
            //        //return GetResponseModel(CommonConst.COMMONDATA_ERRORCODE_SERVERERROR);
            //    }
            return new CommonDataResponseModel
            {
                Result = "NA",
                ErrorCode = "0001"
            };
        }

        ////File download
        //public HttpResponseMessage Get(String Id)
        //{
        //    string clientAddress = HttpContext.Current.Request.UserHostAddress;
        //    string logData = Id;
        //    //LogDataComponent.CallLog("", clientAddress, "Get-CommonData", logData);
        //    var path = "";
        //    HttpResponseMessage response;

        //    if (string.IsNullOrEmpty(Id))
        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);

        //    try
        //    {
        //        path = SystemDataComponent.GetReference("COMMONDATA_EXPORT_PATH", "").CfgValue2; //(System.Configuration.ConfigurationManager.AppSettings["TempSyncZip"]);

        //        response = new HttpResponseMessage(HttpStatusCode.OK);

        //        var filename = string.Format("{0}.zip", Id);
        //        var filePath = Path.Combine(path, filename);
        //        //LogDataComponent.CallLog("", clientAddress, "Get-CommonData", string.Format("filePath: {0}", filePath));
        //        if (File.Exists(filePath))
        //        {
        //            FileStream fileStream = File.OpenRead(filePath);
        //            response.Content = new StreamContent(fileStream);
        //            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
        //            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //            {
        //                FileName = filename,
        //            };

        //            return response;
        //        }
        //        else
        //            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }
        //    catch (Exception ex)
        //    {
        //        //LogDataComponent.CallLog("", clientAddress, "Get-CommonData", string.Format("[ERROR]: {0}: {1}", ex.Message, ex.StackTrace));
        //        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //    }

        //}

        //private CommonDataResponseModel GetResponseModel(string errorCode, string result = "")
        //{
        //    return new CommonDataResponseModel
        //    {
        //        Result = result,
        //        ErrorCode = errorCode
        //    };
        //}

        //private string GetDownloadUrl(Uri requestUri, string keyString)
        //{
        //    var url = requestUri.AbsoluteUri.Replace(requestUri.AbsolutePath.Substring(requestUri.AbsolutePath.IndexOf("/api/"), requestUri.AbsolutePath.Length - requestUri.AbsolutePath.IndexOf("/api/")), "");
        //    var filenameWithoutExt = Path.GetFileNameWithoutExtension(keyString);
        //    return string.Format("{0}/api/CommonData/Get/{1}", url, filenameWithoutExt);
        //}
    }
}