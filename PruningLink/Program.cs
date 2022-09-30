using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Подключение к БД
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


// Добавление политики CORS
builder.Services.AddCors(opions =>
{
    opions.AddPolicy(name: "CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc();

// Подключение сервисов
builder.Services.AddTransient<IShortUrlServces, ChortUrlServices>();
builder.Services.AddTransient<IUrlServices, UrlServices>();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Index}/{action=Index}");
});

app.UseDefaultFiles();
app.UseStaticFiles();

// Использование CORS
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
