using MvcStartApp.Data.Interfaces;
using MvcStartApp.Models.Db;


namespace MvcStartApp.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly IRequestRepository _requestRepo;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            //_requestRepo = requestRepo ?? throw new ArgumentNullException(nameof(requestRepo));
        }

        public async Task InvokeAsync(HttpContext context, IRequestRepository requestRepo)
        {
            try
            {
                var request = new RequestLog
                {
                    Id = Guid.NewGuid(),
                    Url = $"{context.Request.Path}{context.Request.QueryString}",
                    Date = DateTime.Now
                };

                await requestRepo.LogRequest(request);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                var logger = context.RequestServices.GetRequiredService<ILogger<LoggingMiddleware>>();
                logger.LogError(ex, "Ошибка при логировании запроса");
            }

            await _next(context);
        }
    }
}
