using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public SNDocument CreateDocument([FromBody] SNDocument document)
        {
            return new SNDocument();
        }
    }
}
