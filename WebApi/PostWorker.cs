using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DataAccess.Repositories;

namespace WebApi
{
    public class PostWorker : BackgroundService
    {
        private readonly IServiceProvider _service;
        private readonly ILogger<PostWorker> _logger;

        public PostWorker(IServiceProvider service, ILogger<PostWorker> logger)
        {
            _service = service;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _service.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<PostRepository>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    bool save = false;
                    save = await repo.DeleteExpiredPosts();
                    if (save)
                    {
                        _logger.LogInformation("Found some expired posts: {time}", DateTimeOffset.Now);
                    }
                    else
                    {
                        _logger.LogInformation("No expired posts detected: {time}", DateTimeOffset.Now);
                    }
                    await Task.Delay(1000 * 60 * 60, stoppingToken); // milliseconds * seconds * minutes = 1 hour
                }
            }
        }
    }
}
