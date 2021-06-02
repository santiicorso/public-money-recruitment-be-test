using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using VacationalRental.Infrastructure.Memory;
using VacationalRental.Infrastructure.Memory.Booking;
using VacationalRental.Infrastructure.Memory.Rental;
using VacationRental.AppService.Booking.Services;
using VacationRental.AppService.Booking.Services.Impl;
using VacationRental.AppService.Calendar.Services;
using VacationRental.AppService.Calendar.Services.Impl;
using VacationRental.AppService.Rental.Services;
using VacationRental.AppService.Rental.Services.Impl;
using VacationRental.Domain.Booking.Services;
using VacationRental.Domain.Rental.Services;

namespace VacationRental.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(opts => opts.SwaggerDoc("v1", new Info { Title = "Vacation rental information", Version = "v1" }));

            //AppService 
            services.AddTransient<IRentalAppService, RentalAppService>();
            services.AddTransient<IBookingAppService, BookingAppService>();
            services.AddTransient<ICalendarAppService, CalendarAppService>();

            //Infrastructure 
            services.AddSingleton<IStorageManager, MemoryStorageManager>();
            services.AddTransient<IBookingDomainService, BookingDomainService>();
            services.AddTransient<IRentalDomainService, RentalDomainService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "VacationRental v1"));
        }
    }
}
