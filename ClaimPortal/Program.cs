var builder = WebApplication.CreateBuilder(args);

// MVC support
builder.Services.AddControllersWithViews();

// ✅ Add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

// ✅ Add typed HttpClient for the API
builder.Services.AddHttpClient<ClaimPortal.Services.ApiClientService>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ApiBaseUrl"] ?? throw new InvalidOperationException("ApiBaseUrl is not configured."));
});

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

// ✅ Enable Session
app.UseSession();

app.UseAuthorization();

// ✅ Set default route to Login page
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);

app.Run();
