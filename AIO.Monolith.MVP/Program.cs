using System.Text.Json.Serialization;
using GetFundingFees.Lambda.ApiClients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SPA services to the container
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist"; // Specify the path to the built frontend app (e.g., Vue.js build directory)
});

builder.Services.AddTransient<BitgetClient>();
builder.Services.AddTransient<MexcClient>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Serve static files for SPA in production
app.UseSpaStaticFiles();


// Handle SPA requests
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp"; // Path to Vue.js app source
    
    if (app.Environment.IsDevelopment())
    {
        // Use the Vue.js development server during development
        spa.UseProxyToSpaDevelopmentServer("http://localhost:3000"); // Ensure you have 'vue-cli' installed and configured
    }
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();