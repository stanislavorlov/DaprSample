using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DaprClient _daprClient;

        public IndexModel(
            ILogger<IndexModel> logger,
            DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task OnGet()
        {
            var configuration = await _daprClient.GetSecretAsync("localsecretstore", "secretKey");

            var forecasts = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
            HttpMethod.Get,
            "MyBackEnd",
            "weatherforecast");

            ViewData["WeatherForecastData"] = forecasts;
            ViewData["configuration"] = configuration["secretKey"];
        }
    }
}