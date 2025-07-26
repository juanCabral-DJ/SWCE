using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web.Models;

namespace SWCE.Web.Controllers
{
    public class EnvioController : Controller
    {
        // GET: EnvioController
        public async Task<IActionResult> Index()
        {
            GetAllEnvioModelResponse getAllEnvioModelResponse = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.GetAsync("Envio/GetEnvios");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        getAllEnvioModelResponse = System.Text.Json.JsonSerializer.Deserialize<GetAllEnvioModelResponse>(jsonResponse);
                    }
                    else
                    {
                        getAllEnvioModelResponse = new GetAllEnvioModelResponse
                        {
                            message = "Error al obtener los envíos",
                            isSuccess = false,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllEnvioModelResponse = new GetAllEnvioModelResponse
                {
                    message = $"Error al obtener los envíos {ex.Message}",
                    isSuccess = false,
                };
            };
                return View(getAllEnvioModelResponse.data ?? new List<EnvioModel>());
        }

        // GET: EnvioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            GetEnvioByIdModelResponse getEnvioByIdModelResponse = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.GetAsync($"Envio/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        getEnvioByIdModelResponse = System.Text.Json.JsonSerializer.Deserialize<GetEnvioByIdModelResponse>(jsonResponse);
                    }
                    else
                    {
                        getEnvioByIdModelResponse = new GetEnvioByIdModelResponse
                        {
                            message = "Error al obtener los envíos",
                            isSuccess = false,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getEnvioByIdModelResponse = new GetEnvioByIdModelResponse
                {
                    message = $"Error al obtener los envíos {ex.Message}",
                    isSuccess = false,
                };
            };
            return View(getEnvioByIdModelResponse.data);
        }

        // GET: EnvioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EnvioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnvioCreateModel model)
        {
            EnvioCreateModelResponse envioResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.PostAsJsonAsync($"Envio/CreateEnvio", model);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        envioResponse = System.Text.Json.JsonSerializer.Deserialize<EnvioCreateModelResponse>(jsonResponse);

                        if (envioResponse.isSuccess)
                        {
                            TempData["Succcess Message"] = "Envío creado correctamente";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            TempData["Error Message"] = envioResponse.message;
                        }
                    }
                    else
                    {
                        envioResponse = new EnvioCreateModelResponse
                        {
                            message = "Error al crear el envío",
                            isSuccess = false,
                        };
                    }
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: EnvioController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            GetEnvioByIdModelResponse getEnvioByIdModelResponse = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.GetAsync($"Envio/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        getEnvioByIdModelResponse = System.Text.Json.JsonSerializer.Deserialize<GetEnvioByIdModelResponse>(jsonResponse);
                    }
                    else
                    {
                        getEnvioByIdModelResponse = new GetEnvioByIdModelResponse
                        {
                            message = "Error al obtener los envíos",
                            isSuccess = false,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getEnvioByIdModelResponse = new GetEnvioByIdModelResponse
                {
                    message = $"Error al obtener los envíos {ex.Message}",
                    isSuccess = false,
                };
            };
            return View(getEnvioByIdModelResponse.data);
        }

        // POST: EnvioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EnvioEditModel model)
        {
            EnvioEditModelResponse envioResponse = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5134/api/");
                    var response = await client.PostAsJsonAsync($"Envio/UpdateEnvioDto", model);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        envioResponse = System.Text.Json.JsonSerializer.Deserialize<EnvioEditModelResponse>(jsonResponse);

                        if (envioResponse.isSuccess)
                        {
                            TempData["Succcess Message"] = "Envío editado correctamente";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            TempData["Error Message"] = envioResponse.message;
                        }
                    }
                    else
                    {
                        envioResponse = new EnvioEditModelResponse
                        {
                            message = "Error al editar el envío",
                            isSuccess = false,
                        };
                    }
                }
                    return View(model);
            }
            catch
            {
                return View(model);
            }
        }

    }
}
