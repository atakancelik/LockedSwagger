using System.Net;
using System.Text;

namespace LockedSwagger
{
    public class SwaggerBasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public SwaggerBasicAuthMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsSwaggerPath(context.Request.Path))
            {
                if (TryGetCredentials(context.Request, out var username, out var password) && IsAuthorized(username, password))
                {
                    await _next.Invoke(context);
                    return;
                }

                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            await _next.Invoke(context);
        }

        private bool IsSwaggerPath(PathString path) => path.StartsWithSegments("/swagger");

        private bool TryGetCredentials(HttpRequest request, out string username, out string password)
        {
            username = null;
            password = null;

            string authHeader = request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                username = decodedUsernamePassword.Split(':', 2)[0];
                password = decodedUsernamePassword.Split(':', 2)[1];

                return true;
            }

            return false;
        }


        public bool IsAuthorized(string username, string password)
        {
            var users = _config.GetSection("Authentication:Users").GetChildren();

            return users.Any(user =>
            {
                var _userName = user["Username"];
                var _password = user["Password"];
                return username.Equals(_userName, StringComparison.InvariantCultureIgnoreCase) && password.Equals(_password);
            });
        }

    }
}
