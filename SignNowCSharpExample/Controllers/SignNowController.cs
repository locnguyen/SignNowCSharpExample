using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SignNowCSharpExample.SNApi;

namespace SignNowCSharpExample.Controllers
{
    public class SignNowController : ApiController
    {
        const string ClientId = "0fccdbc73581ca0f9bf8c379e6a96813";
        const string ClientSecret = "3719a124bcfc03c534d4f5c05b5a196b";
        
        private SignNowApi SignNowApi;

        public SignNowController()
        {
            SignNowApi = new SignNowApi(ClientId, ClientSecret);
        }

        [ActionName("user")]
        [HttpPost]
        public SNUser CreateUser([FromBody]SNUser user)
        {
            var newUser = SignNowApi.CreateUser(user);
            return newUser ;
        }

        [ActionName("token")]
        [HttpPost]
        public SNAccessToken CreateAccessToken([FromBody] SNTokenRequest tokenRequest)
        {
            var accessToken = SignNowApi.CreateAccessToken(tokenRequest);
            return accessToken;
        }

        [ActionName("document")]
        [HttpPost]
        public Task<SNDocument> CreateDocument()
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType); 
            }

            string tmp = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/tmp");
            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads");
            Directory.CreateDirectory(tmp);
            Directory.CreateDirectory(root);

            var provider = new MultipartFormDataStreamProvider(tmp);
            
            var task = request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<SNDocument>(o =>
                {
                    var tmpFile = provider.FileData.First().LocalFileName;
                    var savedFilePath = root + "\\" + DateTime.Now.Ticks + "-" + GetDeserializedFileName(provider.FileData.First());
                    File.Copy(tmpFile, savedFilePath);
                    
                    IEnumerable<string> headerAuthValues = request.Headers.GetValues("Authorization");
                    string authValue = headerAuthValues.FirstOrDefault();
                    string accessToken = authValue.Replace("Bearer", "");

                    return SignNowApi.CreateDocument(new SNDocument()
                    {
                        filename = savedFilePath,
                        access_token = accessToken
                    });
                }
            );
            return task;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
}
