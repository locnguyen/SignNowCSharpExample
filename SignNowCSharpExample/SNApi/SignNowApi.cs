using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace SignNowCSharpExample.SNApi
{
    public class SignNowApi
    {
        const string BaseUrl = "http://api.loc.signnow.com";
        readonly string _client_id;
        readonly string _client_secret;

        public SignNowApi(string client_id, string client_secret)
        {
            _client_id = client_id;
            _client_secret = client_secret;
        }

        public T Execute<T>(RestRequest request) where T: new()
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;

            var response = client.Execute<T>(request);
            if (response.ErrorException != null)
            {
                Console.Write("An error occured!");
            }

            return response.Data;
        }

        public SNUser CreateUser(SNUser user)
        {
            var request = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/user"
            };

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("Authorization", GetEncodedBasicAuth());
            request.AddBody(user);

            return Execute<SNUser>(request);
        }

        public SNAccessToken CreateAccessToken(SNTokenRequest tokenRequest)
        {
            var request = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/oauth2/token"
            };
            request.AddHeader("Content-Type", "x-www-form-urlencoded");
            request.AddHeader("Authorization", GetEncodedBasicAuth());
            request.AddParameter("username", tokenRequest.username);
            request.AddParameter("password", tokenRequest.password);
            request.AddParameter("grant_type", tokenRequest.grant_type);
            request.AddParameter("scope", tokenRequest.scope);

            return Execute<SNAccessToken>(request);
        }

        public SNDocument CreateDocument(SNDocument document)
        {
            var request = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/document"
            };

            request.AddFile()
            return null;
        }

        private string GetEncodedBasicAuth()
        {
            return "Basic " + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_client_id + ":" + _client_secret));
        }
    }
}