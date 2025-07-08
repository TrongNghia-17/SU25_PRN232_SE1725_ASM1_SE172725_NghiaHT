using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        //options.LoginPath = new PathString("/Account/Login");
        //options.AccessDeniedPath = new PathString("/Account/Forbidden");
        //options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian sống của cookie (tùy chọn)
        options.SlidingExpiration = true; // Tự động gia hạn thời gian sống nếu người dùng hoạt động
        options.Cookie.IsEssential = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Chỉ dùng trên HTTPS
        options.Cookie.Expiration = null; // Không đặt thời gian hết hạn cố định, biến thành session cookie
        options.ExpireTimeSpan = TimeSpan.Zero; // Xác nhận cookie là session cookie
        options.LoginPath = "/Account/Login"; // Đường dẫn khi chưa đăng nhập
        options.LogoutPath = "/Account/Logout"; // Đường dẫn khi đăng xuất
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
