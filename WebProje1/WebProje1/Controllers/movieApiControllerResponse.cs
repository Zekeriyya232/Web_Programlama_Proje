using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebProje1.Models;
using System.Reflection.Metadata;
using static System.Reflection.Metadata.BlobBuilder;
using System.Text;
using WebProje1.Entity;
using WebProje1.Controllers;

namespace WebProje1.Controllers
{
    public class movieApiControllerResponse : Controller
    {

        HttpClientHandler clientHandler = new HttpClientHandler();
        MovieDB movie = new MovieDB(); 
        List<MovieDB> _movieList = new List<MovieDB>();

        public movieApiControllerResponse()
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<MovieDB>> GetAllMovies()
        {
            _movieList = new List<MovieDB>();
            using (var httpclient = new HttpClient(clientHandler))
            {
                using (var response = await httpclient.GetAsync("https://localhost:7082/api/MovieApi"))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    _movieList = JsonConvert.DeserializeObject<List<MovieDB>>(apiresponse);
                }
            }
            return _movieList;
        }


  



        [HttpDelete]
        public async Task<string> Delete(int Id)
        {
            string messages = "";
            using (var httpclient = new HttpClient(clientHandler))
            {
                using (var response = await httpclient.DeleteAsync("https://localhost:7082/api/MovieApi/" + Id))
                {
                    messages = await response.Content.ReadAsStringAsync();

                }
            }
            return messages;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}

