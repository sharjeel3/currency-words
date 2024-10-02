using Words.Server.Services;
using Words.Server.Services.Translation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "LocalPolicy",
        policy =>
        {
            policy.WithOrigins("https://localhost:5173")
                .WithMethods("PUT", "DELETE", "GET", "POST", "OPTIONS");
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<TranslateCentsStrategy>();
builder.Services.AddScoped<TranslateHundredsStrategy>();
builder.Services.AddScoped<TranslateThousandsStrategy>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
