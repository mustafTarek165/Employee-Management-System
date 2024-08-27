using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClientLibrary.Helpers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly LocalStorageService localStorageService;

        public CustomAuthenticationStateProvider(LocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        private readonly ClaimsPrincipal anonymous = new ClaimsPrincipal();

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string stringToken=String.Empty;
            try
            {
                //The exception is thrown at this line
               stringToken = await localStorageService.GetToken();
            }
            catch (InvalidOperationException)
            {

            }
            
            if (string.IsNullOrEmpty(stringToken))
            {
                return new AuthenticationState(anonymous);
            }

            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
            if (deserializeToken == null)
            {
                return new AuthenticationState(anonymous);
            }

            var getUserClaims = DecryptToken(deserializeToken.Token);
            if (getUserClaims == null)
            {
                return new AuthenticationState(anonymous);
            }

            var claimsPrincipal = SetClaimPrincipal(getUserClaims);
            return new AuthenticationState(claimsPrincipal);
        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal = anonymous;

            if (userSession.Token != null || userSession.RefreshToken != null)
            {
                var serializeSession = Serializations.SerializeObj(userSession);
                await localStorageService.SetToken(serializeSession);

                var getUserClaims = DecryptToken(userSession.Token);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                await localStorageService.RemoveToken();
            }
           
           string fakestringToken = await localStorageService.GetToken();
            Console.WriteLine(fakestringToken);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            // Check if any of the required properties are null
            if (claims == null || string.IsNullOrEmpty(claims.Email) || string.IsNullOrEmpty(claims.Id)
                || string.IsNullOrEmpty(claims.Name) || string.IsNullOrEmpty(claims.Role))
            {
                return anonymous;
            }

            // Create the ClaimsIdentity with non-null properties
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, claims.Id),
                new Claim(ClaimTypes.Name, claims.Name),
                new Claim(ClaimTypes.Email, claims.Email),
                new Claim(ClaimTypes.Role, claims.Role),
            }, "JwtAuth");

            return new ClaimsPrincipal(identity);
        }

        private CustomUserClaims DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var name = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var email = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var role = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            return new CustomUserClaims(userId, name, email, role);
        }
        public void ForceAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
