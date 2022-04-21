using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using trackr.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StragoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "trackr API",
        Description = "A .NET6 Web API for managing DragonRealms character experience progression.",
        Contact = new OpenApiContact
        {
            Name = "Jack Perry, Jr.",
            Url = new Uri("https://github.com/jackfperryjr")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();
// app.UseSwaggerUI(options =>
// {
//     options.InjectStylesheet("/swagger-ui/custom.css");
// });
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.Run();