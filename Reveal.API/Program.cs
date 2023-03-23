using Reveal.Sdk;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddReveal();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
      builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );
});

var app = builder.Build();

app.MapGet("/api/dashboard-names", async (HttpContext context) =>
 {
     var directoryPath = "./Dashboards";
     var dashboardNames=new List<string>();
     try
     {
         var files=Directory.GetFiles(directoryPath);
         foreach (var file in files)
         {
             dashboardNames.Add(Path.GetFileNameWithoutExtension(file));
         }
         await context.Response.WriteAsJsonAsync(dashboardNames);
     }
     catch (Exception ex)
     {

     }
 });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
