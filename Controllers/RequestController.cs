using Microsoft.AspNetCore.Mvc;
using frontend.Models;

namespace frontend.Controllers
{
    public class RequestController(HttpClient sriClient, HttpClient antClient) : Controller
    {
        private const string SRI_API_URL = "http://localhost:8081/sri/request?id=";
        private const string ANT_API_URL = "http://localhost:8082/ant/request?id=";

        private readonly HttpClient _sriClient = sriClient;
        private readonly HttpClient _antClient = antClient;

        [HttpPost]
        public async Task<IActionResult> MakeARequest(string id)
        {
            HttpResponseMessage sriResponse = await _sriClient.GetAsync($"{SRI_API_URL}{id}");
            HttpResponseMessage antResponse = await _antClient.GetAsync($"{ANT_API_URL}{id}");

            if (sriResponse.IsSuccessStatusCode && antResponse.IsSuccessStatusCode)
            {
                var sriData = await sriResponse.Content.ReadFromJsonAsync<SRIData>();
                var antData = await antResponse.Content.ReadFromJsonAsync<ANTData>();
                if (sriData == null || antData == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                var consulta = new Result()
                {
                    Id = id,
                    IsContributor = sriData.IsContributor ? "Sí" : "No",
                    LicencePoints = antData.LicencePoints
                };
                return RedirectToAction("Index", "Home", consulta);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}