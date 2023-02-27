using Microsoft.Extensions.Diagnostics.HealthChecks;
//using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;

namespace Movies.WEB
{
    public static class SD
    {
        public static string APIBase { get; set; } = string.Empty;
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            List<Claim> claims = keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)).ToList()!;
            claims.Add(new Claim("Token", jwt));
            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch(base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
