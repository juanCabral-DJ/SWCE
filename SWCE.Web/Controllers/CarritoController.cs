using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.Carrito;

namespace SWCE.Web.Controllers
{
    public class CarritoController : Controller
    {
        // GET: CarritoController
        public async Task<IActionResult> Index()
        {
            List<CarritoModel> carritos = new List<CarritoModel>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.GetAsync("Carrito/GetAllCarts");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        var options = new System.Text.Json.JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var getAllCarritoResponse = System.Text.Json.JsonSerializer.Deserialize<GetAllCarritoResponse>(responseString, options);

                        if (getAllCarritoResponse != null && getAllCarritoResponse.isSuccess)
                        {
                            carritos = getAllCarritoResponse.data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                carritos = new List<CarritoModel>();
            }

            return View(carritos);
        }

        // GET: CarritoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetCarritoResponse getCarritoResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.GetAsync($"Carrito/GetCartById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        var options = new System.Text.Json.JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        getCarritoResponse = System.Text.Json.JsonSerializer.Deserialize<GetCarritoResponse>(responseString, options);

                    }
                    else
                    {
                        getCarritoResponse = new GetCarritoResponse
                        {
                            message = "No se encontró el carrito",
                            isSuccess = false,
                            data = null
                        };
                    }
                }
            }
            catch (Exception)
            {
                getCarritoResponse = new GetCarritoResponse
                {
                    message = "Error al obtener el carrito",
                    isSuccess = false,
                    data = null
                };
                throw;
            }
            return View(getCarritoResponse.data);
        }

        // GET: CarritoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarritoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarritoCreateModel model)
        {
            CarritoCreateResponse createResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");

                    var response = await client.PostAsJsonAsync("Carrito/CreateCart", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        createResponse = System.Text.Json.JsonSerializer.Deserialize<CarritoCreateResponse>(responseString);
                    }
                }
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarritoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetCarritoResponse getCarritoResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.GetAsync($"Carrito/GetCartById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        var options = new System.Text.Json.JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        getCarritoResponse = System.Text.Json.JsonSerializer.Deserialize<GetCarritoResponse>(responseString, options);

                    }
                    else
                    {
                        getCarritoResponse = new GetCarritoResponse
                        {
                            message = "No se encontró el carrito",
                            isSuccess = false,
                            data = null
                        };
                    }
                }
            }
            catch (Exception)
            {
                getCarritoResponse = new GetCarritoResponse
                {
                    message = "Error al obtener el carrito",
                    isSuccess = false,
                    data = null
                };
                throw;
            }
            return View(getCarritoResponse.data);
        }

        // POST: CarritoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult>Edit(CarritoEditModel model)
        {
            CarritoEditResponse editResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.PostAsJsonAsync("Carrito/UpdateCart", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var options = new System.Text.Json.JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        editResponse = System.Text.Json.JsonSerializer.Deserialize<CarritoEditResponse>(responseString, options);

                        if (editResponse != null && editResponse.isSuccess)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        editResponse = new CarritoEditResponse
                        {
                            message = "Error al actualizar el carrito",
                            isSuccess = false,
                            data = null
                        };
                    }
                }
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarritoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            GetCarritoResponse getCarritoResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.GetAsync($"Carrito/GetCartById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        getCarritoResponse = System.Text.Json.JsonSerializer.Deserialize<GetCarritoResponse>(responseString, options);

                        if (getCarritoResponse?.data == null)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(getCarritoResponse.data);
        }

        // POST: CarritoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var model = new { Id = id };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");


                    var response = await client.PostAsJsonAsync("Carrito/DisableCart", model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {

                return View(id); 
            }

            return RedirectToAction(nameof(Delete), new { id = id });
        }

    }
}
