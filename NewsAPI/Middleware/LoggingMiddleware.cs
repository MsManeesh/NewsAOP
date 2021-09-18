using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        string logFilePath;
        Stopwatch stopwatch;
        public LoggingMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            logFilePath = environment.ContentRootPath + @"/LogFile.txt";
            stopwatch = new Stopwatch();
        }

        /*log the information into file at given file path. 
         * Note:If File don't exist create a file i.e LogFile.txt
        */
        public async Task Invoke(HttpContext context)
        {
            stopwatch.Reset();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();
            StringBuilder builder = new StringBuilder();
            builder.Append($"Process Incoming Time: {DateTime.Now}");
            builder.Append($"; Processing Time: {stopwatch.Elapsed.TotalSeconds} seconds");
            builder.Append($"; URI: {context.Request.Path}");
            builder.Append($"; Http Verb: {context.Request.Method}");
            builder.Append($"; Status: {context.Response.StatusCode}");
            builder.AppendLine();
            bool append = true;
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Dispose();
                append = false;
            }

            using (StreamWriter sw = new StreamWriter(logFilePath, append))
            {
                await sw.WriteAsync(builder.ToString());
            }
        }
    }
}
