using System.Text;
using FootballNeighborhood.Infrastructure.Dependencies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsBuilder => corsBuilder
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
    );
});
builder.Services.AddOptions();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddContext(builder.Configuration);
builder.Services.AddCqrs();
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.AddDomainServices();

AddAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddAuthentication()
{
    var tokenKey = builder.Configuration.GetSection("Token")
        .GetValue<string>("Secrete");

    var key = Encoding.ASCII.GetBytes(tokenKey ??
                                      throw new ArgumentException("In appsettings secret for token was not provided"));

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.SaveToken = true;
        options.RequireHttpsMetadata = true;
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    context.Response.Headers.Add("Token-Expired", "true");
                return Task.CompletedTask;
            },
            OnMessageReceived = context =>
            {
                var token = context.Request.Query["access_token"];

                if (!string.IsNullOrWhiteSpace(token)
                    && context.Request.Path.StartsWithSegments("/hubs"))
                    context.Token = token;

                return Task.CompletedTask;
            }
        };
    });
}