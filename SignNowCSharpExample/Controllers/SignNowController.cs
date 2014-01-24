using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
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

            //var provider = new MultipartMemoryStreamProvider();
            //await Request.Content.ReadAsMultipartAsync(provider);
            //var filename = provider.Contents.First().Headers.ContentDisposition.FileName.Trim('\"');
            //var buffer = await provider.Contents.First().ReadAsByteArrayAsync();

            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads");
            var provider = new MultipartFormDataStreamProvider(root);
            
            var task = request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<SNDocument>(o =>
                {
                    // var bytestream = provider.Contents.First().ReadAsByteArrayAsync();
                    string file = provider.FileData.First().LocalFileName;

                    return SignNowApi.CreateDocument(new SNDocument()
                    {
                        filename = file
                    });
                }
            );
            return task;
        }
    }
}
