using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerBlazor
{
    public static class LayerExtensions
    {
        public static IServiceCollection AddLayer(this IServiceCollection services)
        {
            services.AddScoped<LayerHelper>();
            return services;
        }
    }
}
