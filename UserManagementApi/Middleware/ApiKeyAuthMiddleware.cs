namespace UserManagementApi.Middleware
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "X-Api-Key";
        private const string ExpectedApiKey = "demo-secret-key-123"; // In production, load this from configuration/secrets

        public ApiKeyAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Allow Swagger UI to load without a key so the API stays easy to explore/test
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API key was not provided.");
                return;
            }

            if (!ExpectedApiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid API key.");
                return;
            }

            await _next(context);
        }
    }

    public static class ApiKeyAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyAuthMiddleware>();
        }
    }
}
