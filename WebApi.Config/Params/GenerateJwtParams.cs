using System.Security.Claims;
using Newtonsoft.Json;
using WebApi.Config.Factories.Interfaces;
using WebApi.Config.Models;

namespace WebApi.Config.Params
{
    public class GenerateJwtParams
    {
        public ClaimsIdentity Identity { get; set; }
        public ITokenFactory TokenFactory { get; set; }
        public string UserName { get; set; }
        public JwtIssuerOptions JwtOptions { get; set; }
        public JsonSerializerSettings SerializerSettings { get; set; }
    }
}
