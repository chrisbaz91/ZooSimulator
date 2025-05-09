using FluentValidation;
using ZooSimulator.DataAccess;
using ZooSimulator.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ZooContext>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IEnclosureRepository, EnclosureRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<FieldsModelValidator>();

// Seed test data into in-memory database
await SeedData.SetInitialData();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Animals}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
