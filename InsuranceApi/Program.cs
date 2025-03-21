

using InsuranceApi.Data;
using InsuranceApi.Services;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<FnfProjectContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FnfProject"));
            });
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IInsuredPolicyService, InsuredPolicyService>();
            builder.Services.AddTransient<IBlacklistService, BlacklistService>();
            builder.Services.AddTransient<IClaimService, ClaimService>();
            builder.Services.AddTransient<IPaymentService, PaymentService>();
            builder.Services.AddTransient<IInsuranceTypeService, InsuranceTypeService>();
            builder.Services.AddTransient<IPolicyHolderService, PolicyHolderService>();
            builder.Services.AddTransient<IPolicyService, PolicyService>();
            builder.Services.AddTransient<IInsuredService, InsuredService>();
            builder.Services.AddTransient<IHospitalService, HospitalService>();
            builder.Services.AddTransient<IIllnessService, IllnessService>();
            builder.Services.AddTransient<IInsuredIllnessService, InsuredIllnessService>();


            builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDirectoryBrowser();

			builder.Services.AddCors(setup =>
			{
				setup.AddPolicy("cors", setup =>
				{
					setup.AllowAnyHeader();
					setup.AllowAnyMethod();
					setup.AllowAnyOrigin();
				});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("cors");
			
			app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDirectoryBrowser();
            app.UseAuthorization();

			
			app.MapControllers();

			app.Run();
		}
	}
}
