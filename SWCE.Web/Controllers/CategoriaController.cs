using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models.Categoria;

namespace SWCE.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly string _apiBaseUrl = "http://localhost:5134/api/";
        // GET: CategoriaController
        public async Task<IActionResult> Index()
        {
            GetAllCategoriaResponse getAllCategoriaResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");

                    var response = await client.GetAsync("Categoria/Getall");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getAllCategoriaResponse = System.Text.Json.JsonSerializer.Deserialize<GetAllCategoriaResponse>(content)!;
                    }
                    else
                    {
                        getAllCategoriaResponse = new GetAllCategoriaResponse
                        {
                            message = "Error al obtener las categorias",
                            isSuccess = false,
                            data = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllCategoriaResponse = new GetAllCategoriaResponse
                {
                    message = $"Error al obtener las categorias {ex.Message}",
                    isSuccess = false,
                    data = null
                };
                throw;
            }

            return View(getAllCategoriaResponse.data);
        }

        // GET: CategoriaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetCategoriaByIdResponse getCategoriaByIdResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");

                    var response = await client.GetAsync($"Categoria/GetCategoriaById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getCategoriaByIdResponse = System.Text.Json.JsonSerializer.Deserialize<GetCategoriaByIdResponse>(content)!;
                    }
                    else
                    {
                        getCategoriaByIdResponse = new GetCategoriaByIdResponse
                        {
                            message = "Error al obtener la categoria",
                            isSuccess = false
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getCategoriaByIdResponse = new GetCategoriaByIdResponse
                {
                    message = $"Error al obtener la categoria {ex.Message}",
                    isSuccess = false
                };
            }
            return View(getCategoriaByIdResponse.data);
        }

        // GET: CategoriaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoria model)
        {
            CreateCategoriaResponse createCategoriaResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.PostAsJsonAsync("Categoria/CreateCategoria", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        createCategoriaResponse = System.Text.Json.JsonSerializer.Deserialize<CreateCategoriaResponse>(content)!;

                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetCategoriaByIdResponse getCategoriaByIdResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");

                    var response = await client.GetAsync($"Categoria/GetCategoriaById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getCategoriaByIdResponse = System.Text.Json.JsonSerializer.Deserialize<GetCategoriaByIdResponse>(content)!;
                    }
                    else
                    {
                        getCategoriaByIdResponse = new GetCategoriaByIdResponse
                        {
                            message = "Error al obtener la categoria",
                            isSuccess = false
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getCategoriaByIdResponse = new GetCategoriaByIdResponse
                {
                    message = $"Error al obtener la categoria {ex.Message}",
                    isSuccess = false,
                    data = null
                };
            }

            return View(getCategoriaByIdResponse.data);
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCategoriaModel model)
        {
            UpdateCategoriaResponse updateCategoriaResponse = null!;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.PutAsJsonAsync("Categoria/UpdateCategoria", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        updateCategoriaResponse = System.Text.Json.JsonSerializer.Deserialize<UpdateCategoriaResponse>(content)!;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriaController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            GetCategoriaByIdResponse getCategoriaByIdResponse = null!;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.GetAsync($"Categoria/GetCategoriaById?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        getCategoriaByIdResponse = System.Text.Json.JsonSerializer.Deserialize<GetCategoriaByIdResponse>(content)!;

                        if (getCategoriaByIdResponse?.data == null)
                        {
                            TempData["ErrorMessage"] = "Categoría no encontrada para eliminar.";
                            return RedirectToAction(nameof(Index));
                        }
                        return View(getCategoriaByIdResponse.data);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error al obtener la categoría para eliminar: {response.StatusCode}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al obtener la categoría para eliminar: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CategoriaController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiBaseUrl);
                    var response = await client.PostAsync($"Categoria/DisableCategoria?id={id}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Categoría deshabilitada exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"] = $"Error al deshabilitar la categoría: {errorContent}";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error inesperado al deshabilitar la categoría: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
