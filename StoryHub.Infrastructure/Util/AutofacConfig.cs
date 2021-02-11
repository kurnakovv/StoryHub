using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StoryHub.BL.Services;
using StoryHub.BL.Services.Abstract;
using System;

namespace StoryHub.Infrastructure.Util
{
    public static class AutofacConfig
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IStorytellerService, StorytellerService>();
            services.AddTransient<IStorytellerCRUD, StorytellerService>();
            services.AddTransient<IStoryCRUD, StoryService>();
            services.AddTransient<IStoryService, StoryService>();
        }
    }
}
