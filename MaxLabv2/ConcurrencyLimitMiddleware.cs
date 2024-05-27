namespace MaxLabv2
{
    public class ConcurrencyLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SemaphoreSlim _semaphore;

        public ConcurrencyLimitMiddleware(RequestDelegate next, int maxConcurrentRequests)
        {
            _next = next;
            _semaphore = new SemaphoreSlim(maxConcurrentRequests, maxConcurrentRequests);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!_semaphore.Wait(0))
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Too many concurrent requests.");
                return;
            }

            try
            {
                await _next(context);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
    public static class ConcurrencyLimitMiddlewareExtensions
    {
        public static IApplicationBuilder UseConcurrencyLimit(
            this IApplicationBuilder builder, int maxConcurrentRequests)
        {
            return builder.UseMiddleware<ConcurrencyLimitMiddleware>(maxConcurrentRequests);
        }
    }
}
