using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonWebApi_Auth0.Authentication
{
    public class ListUsers
    {
        public static UserInfo[] GetUsers(string token)
        {
            string url = $"https://dev-pn3w6qi6.us.auth0.com/api/v2/users";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);

            var userInfo = JsonSerializer.Deserialize<UserInfo[]>(response.Content);

            return userInfo;
        }
    }
}
