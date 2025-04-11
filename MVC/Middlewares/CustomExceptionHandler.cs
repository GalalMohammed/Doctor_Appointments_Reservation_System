namespace MVC.Middlewares
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly string logFilePath;
        public CustomExceptionHandler(RequestDelegate next, IWebHostEnvironment environment)
        {
            this.next = next;
            logFilePath = Path.Combine(environment.WebRootPath, "log.txt");
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine($"--------------------------------------- {DateTime.Now} ---------------------------------------\n{exception}");
                    writer.WriteLine("");
                    writer.WriteLine("");
                }
                context.Response.Redirect("/Error");
            }
        }
    }
}
