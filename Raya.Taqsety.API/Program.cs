using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure;
using Raya.Taqsety.Infrastructure.AddressDetailsRepository;
using Raya.Taqsety.Infrastructure.AttachmentRepository;
using Raya.Taqsety.Infrastructure.CityRepository;
using Raya.Taqsety.Infrastructure.CustomerRepository;
using Raya.Taqsety.Infrastructure.InstallmentCardRepository;
using Raya.Taqsety.Infrastructure.JobDetailsRepository;
using Raya.Taqsety.Infrastructure.LookupRepository;
using Raya.Taqsety.Infrastructure.MobileNumberVerificationRepository;
using Raya.Taqsety.Infrastructure.RepositoryPattern;
using Raya.Taqsety.Infrastructure.UserRepository;
using Raya.Taqsety.Service.AddressDetailsService;
using Raya.Taqsety.Service.AttachmentService;
using Raya.Taqsety.Service.Configuration;
using Raya.Taqsety.Service.CustomerService;
using Raya.Taqsety.Service.InstallmentCardService;
using Raya.Taqsety.Service.JobDetailsService;
using Raya.Taqsety.Service.LookUpService;
using Raya.Taqsety.Service.MobileNumberVerificationService;
using Raya.Taqsety.Service.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Raya.Taqsety.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


builder.Services.AddAuthorization();
// adding identitiy 
builder.Services.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();


// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]))
    };
});
//adding session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromHours(1);//You can set Time   
});

//connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//connection string
builder.Services.AddDbContext<InstallmentContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("InstallmentConnection"));
});
builder.Services.AddAutoMapper(typeof(MapperConfig));
// Add services to the container.


builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(ILookupRepository<>), typeof(LookupRepository<>));
builder.Services.AddTransient(typeof(ILookupService<>), typeof(LookupService<>));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IInstallmentCardRepository, InstallmentCardRepository>();
builder.Services.AddTransient<IInstallmentCardService, InstallmentCardService>();
builder.Services.AddTransient<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddTransient<IAttachmentServie, AttachmentService>();
builder.Services.AddTransient<ICityRepository, CityRepository>();
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<IAddressDetailsRepository, AddressDetailsRepository>();
builder.Services.AddTransient<IJobDetailsRepository, JobDetailsRepository>();
builder.Services.AddTransient<IAddressDetailsService, AddressDetailsService>();
builder.Services.AddTransient<IJobDetailsService, JobDetailsService>();
builder.Services.AddTransient<IMobileNumberVerificationRepository, MobileNumberVerificationRepository>();
builder.Services.AddTransient<IMobileNumberVerificationService, MobileNumberVerificationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseSession();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.UseMvc();

app.Run();
