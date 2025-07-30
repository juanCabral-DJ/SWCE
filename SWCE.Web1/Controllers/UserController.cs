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
            var model = new DisableUserModel { id = id };
            return View(model);
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
