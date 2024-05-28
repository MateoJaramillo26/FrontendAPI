using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly HttpClient _httpClient;

        public ConsultasController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> Consultar(string cedula)
        {
            var responseSRI = await _httpClient.GetAsync($"http://localhost:5000/api/sri/{cedula}");
            var dataSRI = await responseSRI.Content.ReadFromJsonAsync<SRIResponse>();

            var responseANT = await _httpClient.GetAsync($"http://localhost:5001/api/ant/{cedula}");
            var dataANT = await responseANT.Content.ReadFromJsonAsync<ANTResponse>();

            var model = new ConsultaViewModel
            {
                ContribuyenteSRI = dataSRI.Nombre,
                PuntosANT = dataANT.Puntos
            };

            return View("Index", model);
        }
    }

    public class SRIResponse
    {
        public String? Nombre { get; set; }
    }

    public class ANTResponse
    {
        public int Puntos { get; set; }
    }

    public class ConsultaViewModel
    {
        public string ContribuyenteSRI { get; set; }
        public int PuntosANT { get; set; }
    }
}