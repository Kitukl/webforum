using Application;
using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Persistence.Contracts;
using Persistence.Repositiries;
using WebForumAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepositories, UserRepositories>();

builder.Services.AddScoped<DiscussionService>();
builder.Services.AddScoped<IDiscussionRepositories,DiscussionRepositories>();

builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<ICommentRepositories, CommentRepositories>();

builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<ICourseRepositories, CoursesReposotories>();

builder.Services.AddScoped<LessonService>();
builder.Services.AddScoped<ILessonRepositories, LessonsRepositories>();

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));
var jwtOptions = builder.Configuration.GetSection(nameof(JWTOptions)).Get<JWTOptions>();
builder.Services.AddApiAuthentication(jwtOptions);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddScoped<IJWTProvider, JWTProvider>();
builder.Services.AddDbContext<Context>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("WebForum")));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapControllers();

app.UseCookiePolicy(new CookiePolicyOptions()
{
  MinimumSameSitePolicy = SameSiteMode.Strict,
  HttpOnly = HttpOnlyPolicy.Always,
  Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();
app.Run();