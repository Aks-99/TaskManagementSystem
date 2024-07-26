using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using System.Text.Json;
using TaskManagement.UI.Models;
using TaskManagement.UI.Models.DTO;

namespace TaskManagement.UI.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public TeamsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TeamDto> response = new List<TeamDto>();

            try
            {
                // Get all teams from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7249/api/teams");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<TeamDto>>());

            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add() 
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTeamViewModel model)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7249/api/teams"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            await httpResponseMessage.Content.ReadFromJsonAsync<TeamDto>();

            if (Response is not null) { return RedirectToAction("Index", "Teams"); }

           return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<TeamDto>($"https://localhost:7249/api/teams/{id.ToString()}");

            if (response is not null) { return View(response); }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeamDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7249/api/teams/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<TeamDto>();

            if (response is not null) { return RedirectToAction("Edit", "Teams"); }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TeamDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7249/api/teams/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Teams");
            }
            catch (Exception ex)
            {
                // Add message
            }

            return View("Edit");
        }
    }
}
