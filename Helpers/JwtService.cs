using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace WebApi.Helpers
{
    public class JwtService{
        public string secureKey = "City of stars, Are you shinning just for me." +
                                  "City of stars, There's so much that i can't see. Who knows is this the start  of something wonderfull and new,"+
                                  "Or one more dream that i can't not make true.";
        public string Generate(int id){
            var symmetricSecurityKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credential = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credential);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));

            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}