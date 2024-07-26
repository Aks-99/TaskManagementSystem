using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TaskManagement.UI.Models;
using TaskManagement.UI.Models.DTO;

namespace TaskManagement.UI.Controllers
{
    public class NotesController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public NotesController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<NoteDto> response = new List<NoteDto>();

            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7249/api/notes");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<NoteDto>>());
            }
            catch (Exception ex)
            {

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
        public async Task<IActionResult> Add(AddNoteViewModel model)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7249/api/notes"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            await httpResponseMessage.Content.ReadFromJsonAsync<NoteDto>();

            if (Response is not null) { return RedirectToAction("Index", "Notes"); }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<NoteDto>($"https://localhost:7249/api/notes/{id.ToString()}");

            if (response is not null) { return View(response); }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NoteDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7249/api/notes/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<NoteDto>();

            if (Response is not null) { return RedirectToAction("Index", "Notes"); }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(NoteDto request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7249/api/notes/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Teams");
            }
            catch (Exception ex)
            {
                
            }

            return View("Edit");
        }
    }
}
