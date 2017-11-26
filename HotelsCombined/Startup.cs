using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HotelsCombined.Service.Contracts;
using HotelsCombined.Service.Implementations;
using HotelsCombined.ActionFilters;
using HotelsCombined.Repository.Implementations;
using HotelsCombined.Repository.Contracts;
using HotelsCombined.Transformers;

namespace HotelsCombined
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<PlacesPostModelValidation>();
            services.AddSingleton<IPlaceRepository, DummyPlaceRepository>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IModelTransformer, ModelTransformer>();
            services.AddMvc(config => {
                        config.Filters.Add(typeof(CustomExceptionFilter));
                    }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=home}/{action=index}/{id?}");
			});
        }
    }
}
