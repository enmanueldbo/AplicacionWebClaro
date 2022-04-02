using AplicacionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace AplicacionWeb.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        private string url = "https://localhost:7090/api/book";
        public async Task<ActionResult> Index()
        {
          

            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetAsync(url);
              
                    var respuestaString = await respuesta.Content.ReadAsStringAsync();
                    var listModel = JsonSerializer.Deserialize<List<Book>>(respuestaString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                  
              
                return View(listModel);
            }

           
        }

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Book book)
        {

           
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
                var respuesta = await httpClient.PostAsync(url, content);
                ViewBag.Posted = true;
                if (respuesta.IsSuccessStatusCode)
                {
                    return View();
                }
                else
                {                 
                    ViewBag.ErrorMessage = true;
                    return View(book);
                }          
            }
        
        }

        public async Task<ActionResult> Details(int id)
        {


            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetAsync($"{url}/{id}");
                var respuestaString = await respuesta.Content.ReadAsStringAsync();
                var model = JsonSerializer.Deserialize<Book>(respuestaString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (respuesta.IsSuccessStatusCode)
                {
                    
                    return View(model);
                }

                else
                {
                    ViewBag.ErrorMessage = true;
                    return View(model);

                }
              
            }


        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetAsync($"{url}/{id}");
                var respuestaString = await respuesta.Content.ReadAsStringAsync();
                var model = JsonSerializer.Deserialize<Book>(respuestaString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                ViewBag.GetEdit = true;
                if (respuesta.IsSuccessStatusCode)
                {
                    return View(model);
                }

                else
                {
                    ViewBag.ErrorMessage = true;
                    return View(model);

                }              
            }
        }



        [HttpPost]
        public async Task<ActionResult> Edit(Book book)
        {


            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
                var respuesta = await httpClient.PutAsync($"{url}/{book.ID}", content);
                ViewBag.Posted = true;
                if (respuesta.IsSuccessStatusCode)
                {

                    return View(book);
                }
                else
                {
                    ViewBag.ErrorMessage = true;
                    return View(book);
                }
            }


        }

        [HttpPost]
        public async Task<int> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {

                var respuesta = await httpClient.DeleteAsync($"{url}/{id}");

                if (respuesta.IsSuccessStatusCode)
                {
                    return 1;
                }
                else
                {                  
                    return -1;
                }
            }
        }

    }
}