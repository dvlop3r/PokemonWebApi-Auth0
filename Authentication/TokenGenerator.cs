using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonWebApi_Auth0.Authentication
{
    public class TokenGenerator
    {
        public string GetToken()
        {
            var client = new RestClient("https://dev-pn3w6qi6.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"LM5DLDrn2ayNqGEsptBY5KSLLDMpqHrt\",\"client_secret\":\"MAEm0NHsp2kCd4IyZtMtlYSZF8P3bouhSf_peSg98WpU0wbk1TvO6O61yjbo_26M\",\"audience\":\"https://dev-pn3w6qi6.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var jc = JsonSerializer.Deserialize<AccessToken>(response.Content);
            return jc.access_token;
        }
    }
}
