namespace FileRecieverSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            var app = builder.Build();

            app.Map("/upload", async (context) =>
            {
                var req = context.Request;
                IFormFileCollection files = req.Form.Files;

                var uploadPath = $"{Directory.GetCurrentDirectory()}/files";
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                if (files != null && files.Count > 0)
                {
                    IFormFile file = req.Form.Files[0];

                    if (file != null)
                        using (FileStream file_stream = new FileStream($"{uploadPath}/{file.Name}", FileMode.Create))
                        {
                            file.CopyToAsync(file_stream);
                        }
                }
            });
            app.UseMiddleware<ParseMiddleware>();
            app.Run();
        }
    }

    public class ParseMiddleware
    {
        private readonly RequestDelegate _next;

        public ParseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var Response = context.Response;

            Response.ContentType = "text/html; charset=utf-8";

            await Response?.SendFileAsync("wwwroot\\index.html");
            await _next.Invoke(context);
        }
    }
}
