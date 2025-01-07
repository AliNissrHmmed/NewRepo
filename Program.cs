
using ERP;
using ERP.PURCHASES.Interfaces;
using ERP.Repositories;
using ERPPurchases.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
//Data Source=DESKTOP-N2F5HN5\SQLEXPRESS;
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ERP-PURCHASES", Version = "v1" });

    // Add JWT Bearer authentication to Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });

});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
#pragma warning disable CS8604 // Possible null reference argument.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
#pragma warning restore CS8604 // Possible null reference argument.
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ================================================================
// Dependency Injection Configuration
// ================================================================
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddScoped<IItemsrepository, ItemsRepository>();

builder.Services.AddScoped<ImainCategoryRepo,MainCategoryRepo>();

builder.Services.AddScoped<IIt_RateRepo, It_RateRepository>();

builder.Services.AddScoped<ISubCategoryRepo,SubCategoryRepo>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();

builder.Services.AddScoped<IOr_SectionRepo, Or_SectionRepository>();

builder.Services.AddScoped<IAdd_Main_Groups_Repository, AddMainGroupsRepository>();

builder.Services.AddScoped<IMain_Groups_Repository, Or_maingroupRepo>();

builder.Services.AddScoped<IMaSubgroupRepo, MaSubgroupRepo>();
 

// ================================================================



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
/*var rootFolder = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "uploads");
if (!Directory.Exists(rootFolder))
{
    Directory.CreateDirectory(rootFolder);
}

app.MapGet("/Files/{fileName}", (string fileName) =>
{
    var contentRoot = app.Environment.ContentRootPath;
    var imagePath = Path.Combine(contentRoot, "wwwroot", "uploads", "Files", fileName);

    if (!System.IO.File.Exists(imagePath))
    {
        return Results.NotFound("Files not found.");
    }

    var imageExtension = Path.GetExtension(fileName).TrimStart('.').ToLower();
    var contentType = $"image/{imageExtension}";

    return Results.File(imagePath, contentType);
}).WithDisplayName("ShowFiles");
*/


app.UseStaticFiles(); // To serve static files like images, CSS, JS

app.UseCors("AllowAllOrigins"); // Enable CORS

app.UseRouting();    // Enables routing
app.UseAuthentication();  // Adds authentication to the request pipeline
app.UseAuthorization();   // Adds authorization to the request pipeline

// Mapping controllers
#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseCors(x => x
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials()
      //.WithOrigins("https://localhost:44351))
      .SetIsOriginAllowed(origin => true));


app.MapControllers();
//EnsureAdminUserCreated(app.Services).Wait();
app.UseStaticFiles();


app.Run();


// static async Task EnsureAdminUserCreated(IServiceProvider serviceProvider)
// {
//     using (var scope = serviceProvider.CreateScope())
//     {
//         var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//         // Check if users already exist in the database
//         if (!context.Users.Any(u => u.UserName == "admin"))
//         {
//             // If no users exist, create an admin user
//             var adminUser = new User
//             {
//                 UserName = "admin",
//                 FullName = "Admin",
//                 Role = "Admin", // Assuming "Admin" is the role for admin users
//                 Password = BCrypt.Net.BCrypt.HashPassword("admin"), // Set the admin password 
//             };

//             context.Users.Add(adminUser);
//             await context.SaveChangesAsync();
//         }
//     }
// }


