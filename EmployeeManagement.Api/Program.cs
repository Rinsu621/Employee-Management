using EmployeeManagement.Api;
using EmployeeManagement.Application.Interface;
using EmployeeManagement.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Swagger with JWT support

builder.Services.AddEndpointsApiExplorer();// generate documentation automatically for API endpoints
builder.Services.AddSwaggerGen(c =>
{
    //configure swagger for documentation of API
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeManagement API", Version = "v1" }); //define swagger documentation with title and version
    //tells bearer security scheme is used
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        //Http header name used to send the token, sent in Authorization header
        Name = "Authorization",
        //Http authentication
        Type = SecuritySchemeType.Http,
        //use Bearer <token> format
        Scheme = "Bearer",

        BearerFormat = "JWT",
        //token goes to Http header
        In = ParameterLocation.Header,
        //helps user to unbderstand how to enter the token 
        Description = "Enter 'Bearer' [space] and then your token"
    });


    //all endpoints in this API require a Bearer token in the Authorization header
    //after this every endpoint shows a lock icon and requires authorization
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
            Array.Empty<string>()
        }
    });
});

// Register application dependencies
builder.Services.AddApiDI(builder.Configuration);

// Read JWT settings from appsettings.json
var jwtSecret = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtLifespan = int.Parse(builder.Configuration["Jwt:ExpireMinutes"] ?? "60");

// Register a singleton service that generated JWT tokens i.e only one instance created for lifetime of the applications
builder.Services.AddSingleton<IJwtTokenService>(new JwtTokenService(jwtSecret, jwtLifespan));

// Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        ClockSkew = TimeSpan.Zero //Extra time allowed for expiration
    };
});

var app = builder.Build();

// Configure HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //Extracts token from authorization header and validates it
app.UseAuthorization(); //check authorize attribute in controller

app.MapControllers();

app.Run();
