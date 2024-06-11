using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Context;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .WithMethods("PUT", "DELETE", "GET","POST");
        });
});*/

//Habilitacion del Cors
builder.Services.AddCors(options => options.AddPolicy("All", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<INumberAndColor, Numberandcolor>(); //injectando servicio
builder.Services.AddScoped<IRuleta, Ruleta>(); //injectando servicio
//builder.Services.AddCors();

/*builder.Services.AddCors(policy =>{
    policy.AddPolicy("CorsPolicy", 
        opt => {
            opt.SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:5173");//AllowAnyOrigin();
          //  opt.WithOrigins("http://localhost:5173");
            }
        );
});*/



builder.Services.AddDbContext<DataContext>(o => {
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());

    app.UseSwagger();
    app.UseSwaggerUI();

}
/*
app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());*/

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

//app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
