using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using WebApplication7.Core.Core.Context;
using WebApplication7.Core.Core.Uow;
using WebApplication7.Core.Services;

#pragma warning disable
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:4200/");

builder.Services.AddMvc().AddNewtonsoftJson();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure(delegate (CookiePolicyOptions options)
{
    options.CheckConsentNeeded = (context) => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddCors(delegate (CorsOptions options)
{
    options.AddPolicy("_myAllowSpecificOrigins", delegate (CorsPolicyBuilder policy)
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(c => { 
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Teste Swagger V1",
        Version = "v1"
    });
});


string AddressRedis = "127.0.0.1";
string AddressPostgres = "127.0.0.1";

if (!builder.Environment.IsDevelopment())
{
    AddressRedis = "172.17.0.2";
    AddressPostgres = "172.17.0.3";
}


builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var config = ConfigurationOptions.Parse(AddressRedis+":6379", true);
    return ConnectionMultiplexer.Connect(config);
});


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = AddressRedis+":6379";
});


builder.Services.AddDbContext<PgsqlContext>(options => {
    options.UseNpgsql("Server="+AddressPostgres+";Database=postgres;Port=5432;User Id=postgres;Password=abacate");
});

builder.Services.AddTransient<TesteServiceUow, TesteService>();






var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "v1");
});

app.UseRouting();
app.UseExceptionHandler("/Home/Error");
app.UseHsts();
app.UseCookiePolicy();

app.UseCors("_myAllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(c =>
{
    c.MapControllers();
    c.MapSwagger();
});

app.Run();