using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.Categoria;
using SWCE.Web.Models.Producto;
using System.Net.Http.Json;
using System.Text.Json;

namespace SWCE.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly string _apiBaseUrl = "http://localhost:5134/api/";
        // GET: ProductoController
        public async Task<IActionResult> Index()
        {
            GetAllProductoResponse getAllProductoResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.GetAsync("Producto/GetAll");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getAllProductoResponse = System.Text.Json.JsonSerializer.Deserialize<GetAllProductoResponse>(content)!;
                    }
                    else
                    {
                        getAllProductoResponse = new GetAllProductoResponse
                        {
                            message = "Error al obtener los Productos",
                            isSuccess = false,
                            data = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllProductoResponse = new GetAllProductoResponse()
                {
                    message = $"Error al obtener los productos {ex.Message}",
                    isSuccess = false,
                    data = null
                };
            }

            return View(getAllProductoResponse.data);
        }

        // GET: ProductoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetProductoByIdResponse getProductoByIdResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");

                    var response = await client.GetAsync($"Producto/GetProductoById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getProductoByIdResponse = System.Text.Json.JsonSerializer.Deserialize<GetProductoByIdResponse>(content)!;
                    }
                    else
                    {
                        getProductoByIdResponse = new GetProductoByIdResponse
                        {
                            message = "Error al obtener el Producto",
                            isSuccess = false
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getProductoByIdResponse = new GetProductoByIdResponse
                {
                    message = $"Error al obtener el Producto {ex.Message}",
                    isSuccess = false
                };
            }
            return View(getProductoByIdResponse.data);
        }

        // GET: ProductoController/Create
       public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductoModel model)
        {
            try
            {
                CreateProductoResponse createProductoResponse = null!;
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.PostAsJsonAsync("Producto/CreateProducto", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        createProductoResponse = System.Text.Json.JsonSerializer.Deserialize<CreateProductoResponse>(content)!;
                    }
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        // Puedes loguearlo o usar TempData para mostrarlo
                        ModelState.AddModelError(string.Empty, $"Error: {errorContent}");
                        return View(model); // Devuelve el mismo modelo para que el usuario lo corrija
                    }

                }
                return RedirectToAction(nameof(Index));
               
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductoModel model)
        {
            UpdateProductoResponse updateProductoResponse = null!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.PutAsJsonAsync("Producto/Update", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        updateProductoResponse = System.Text.Json.JsonSerializer.Deserialize<UpdateProductoResponse>(content)!;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            GetProductoByIdResponse getProductoByIdResponse = null!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.GetAsync($"Producto/GetProductoById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getProductoByIdResponse = System.Text.Json.JsonSerializer.Deserialize<GetProductoByIdResponse>(content)!;
                        return View(getProductoByIdResponse.data);
                    }
                }
            }
            catch
            {
                
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: ProductoController/Delete/5
        /*[HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             try
             {
                 using (var client = new HttpClient())
                 {
                     client.BaseAddress = new Uri(_apiBaseUrl);
                     var response = await client.PostAsJsonAsync($"Producto/DisableProduct?id={id}");

                     if (response.IsSuccessStatusCode)
                     {
                         return RedirectToAction(nameof(Index));
                     }
                     else
                     {
                         ModelState.AddModelError(string.Empty, "Error al eliminar el producto.");
                     }
                 }
             }
             catch (Exception ex)
             {
                 ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
             }

             return RedirectToAction(nameof(Index));
         }*/

        // POST: ProductoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.PostAsync($"Producto/DisableProduct?id={id}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"Error al eliminar el producto: {errorContent}");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error inesperado: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
