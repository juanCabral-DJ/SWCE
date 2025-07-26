using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SWCE.Web1.Models.User;

namespace SWCE.Web1.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _Client;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _Client = httpClientFactory.CreateClient("Client");
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            GetAllUserResponse getAllUserResponse = null;

            try
            {
                using (_Client)
                {
                    var response = await _Client.GetAsync("User/GetUser");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAllUserResponse = System.Text.Json.JsonSerializer.Deserialize<GetAllUserResponse>(responseString);
                    }
                    else
                    {
                        getAllUserResponse = new GetAllUserResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving User"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAllUserResponse = new GetAllUserResponse
                {
                    isSuccess = false,
                    message = $"Error retrieving User {ex.Message}"
                };
            }
            return View(getAllUserResponse.data);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            GetByIdUserResponse getByidUserResponse = null;

            try
            {
                using (_Client)
                {
                    var response = await _Client.GetAsync($"User/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getByidUserResponse = System.Text.Json.JsonSerializer.Deserialize<GetByIdUserResponse>(responseString);
                    }
                    else
                    {
                        getByidUserResponse = new GetByIdUserResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving User"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getByidUserResponse = new GetByIdUserResponse
                {
                    isSuccess = false,
                    message = $"Error retrieving User {ex.Message}"
                };
            }
            return View(getByidUserResponse.data);
            
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> DetailsByEmail(string email)
        {
            GetByEmailUserResponse getByEmailUserResponse = null;

            try
            {
                using (_Client)
                {
                    var response = await _Client.GetAsync($"User/Email?email={email}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getByEmailUserResponse = System.Text.Json.JsonSerializer.Deserialize<GetByEmailUserResponse>(responseString);
                    }
                    else
                    {
                        getByEmailUserResponse = new GetByEmailUserResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving User"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getByEmailUserResponse = new GetByEmailUserResponse
                {
                    isSuccess = false,
                    message = $"Error retrieving User {ex.Message}"
                };
            }
            return View(getByEmailUserResponse.data);

        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserModelCreate model)
        {
            CreateUserResponse CreateResponse = null;
            try
            {
                model.fecha_Creacion = DateTime.Now;
                using (_Client)
                {
                    var response = await _Client.PostAsJsonAsync("User/CreateUserDto", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        CreateResponse = System.Text.Json.JsonSerializer.Deserialize<CreateUserResponse>(responseString);

                    }

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
             
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<ActionResult> Edit(UserModelEdit model)
        {
            EditUserResponse EditResponse = null;
            try
            {
                using (_Client)
                {
                    var response = await _Client.PostAsJsonAsync("User/UpdateUserDto", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        EditResponse = System.Text.Json.JsonSerializer.Deserialize<EditUserResponse>(responseString);

                    }
                    
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        } 

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DisableUserModel model)
        {
            DisableUserResponse DisableResponse = null;
            try
            {
                using (_Client)
                {
                    var response = await _Client.PostAsJsonAsync("User/DisableUserDto", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        DisableResponse = System.Text.Json.JsonSerializer.Deserialize<DisableUserResponse>(responseString);

                    }

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
