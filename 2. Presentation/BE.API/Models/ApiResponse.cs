using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BE.API.Controllers
{
    public class ApiResponseResult : IHttpActionResult
    {
        ReturnModel _returnModel;
        HttpRequestMessage _request;

        public ReturnModel ReturnModel { get { return _returnModel; } }

        public ApiResponseResult(HttpRequestMessage request, ReturnModel model)
        {
            _returnModel = model;
            _request = request;
        }

        public ApiResponseResult(HttpRequestMessage request, string status, string message = "", object data = null)
        {
            object dataList = string.Empty;
            if (data != null)
            {
                if (!(data is IList))
                {
                    dataList = new List<object>();
                    ((List<object>)dataList).Add(data);
                }
                else
                {
                    dataList = data;
                }
            }

            _returnModel = new ReturnModel()
            {
                Status = status,
                Message = message,
                Data = dataList
            };
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ObjectContent<ReturnModel>(_returnModel, new JsonMediaTypeFormatter()),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }

    }

    public class ReturnModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class ApiRequestContentType
    {
        public const string REQUEST_CONTENTTYPE_JSON = "application/json";
        public const string REQUEST_CONTENTTYPE_FORMDATA = "application/form-data";
        public const string REQUEST_CONTENTTYPE_FORMURLENCODED = "application/x-www-form-urlencoded";
    }

    public class ApiTokenResponseModel
    {
        public ApiTokenResponseModel() { }

        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}