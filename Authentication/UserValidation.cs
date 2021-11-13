using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;

namespace PokemonWebApi_Auth0.Authentication
{
    public class UserValidation
    {
        private readonly string[] admins = {"sarwan.h.najim@gmail.com","rawand@gmail.com"};
        public bool ValidateUser(string user,string token)
        {
            string url = $"https://dev-pn3w6qi6.us.auth0.com/api/v2/users/{user}?include_fields=true";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);

            UserInfo userInfo = JsonSerializer.Deserialize<UserInfo>(response.Content);

            return admins.Contains(userInfo.email);
        }
    }
}
