using AutoMapper;
using CustomersManagement.DataAccess.Databases;
using CustomersManagement.Domain.Customer.Repositories;
using CustomersManagement.Domain.Customer.Repositories.Interfaces;
using CustomersManagement.Domain.Customer.Services;
using CustomersManagement.Domain.Customer.Services.Interfaces;
using CustomersManagement.Domain.Customer.Validations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomersManagement
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
            services.AddControllers();
            services.AddDbContext<CustomerDbContext>(optionsBuilder => optionsBuilder
                                                    .UseSqlServer(Configuration.GetConnectionString("customers"), 
                                                    providerOptions => providerOptions.CommandTimeout(60)));

            services.AddValidatorsFromAssembly(typeof(Domain.Customer.Handlers.CommandHandlers.CreateCustomerHandler).Assembly, ServiceLifetime.Singleton);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddMediatR(typeof(Domain.Customer.Handlers.CommandHandlers.CreateCustomerHandler).Assembly);
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerService, CustomerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
