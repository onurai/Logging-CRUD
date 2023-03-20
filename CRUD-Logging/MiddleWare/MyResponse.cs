using System.Diagnostics;

namespace CRUD_Logging.MiddleWare
{
    public class MyResponse
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MyResponse> _logger;

        public MyResponse(RequestDelegate next, ILogger<MyResponse> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var watch = Stopwatch.StartNew();
            watch.Start();
            httpContext.Response.OnStarting(() =>
            {
                watch.Stop();
                var responseTime = watch.ElapsedMilliseconds;
                _logger.LogCritical($"Response generation time = {responseTime}");

                return Task.CompletedTask;
            });
            await _next(httpContext);
        }
    }
}
