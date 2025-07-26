using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web1.Models.Address;
using SWCE.Web1.Models.User;
using static SWCE.Web1.Models.Address.DisableAddressModel;
using static SWCE.Web1.Models.WishListItem.DisableItemModel;

namespace SWCE.Web1.Controllers
{
    public class AddressController : Controller
    {
        private readonly HttpClient _Client;

        public AddressController(IHttpClientFactory httpClientFactory)
        {
            _Client = httpClientFactory.CreateClient("Client");
        }

        // GET: AddressController1
        public async Task<ActionResult> Index()
        {
            GetAllAddressResponse getAddressResponse = null;
            try
            {
                using (_Client)
                {
                    var response = await _Client.GetAsync("Address");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAddressResponse = System.Text.Json.JsonSerializer.Deserialize<GetAllAddressResponse>(responseString);
                    }
                    else
                    {
                        getAddressResponse = new GetAllAddressResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving User"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAddressResponse = new GetAllAddressResponse
                {
                    isSuccess = false,
                    message = "Error retrieving Address"
                };
            }
            return View(getAddressResponse.data);
        }

        // GET: AddressController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            GetByIdAddressResponse getAddressResponse = null;
            try
            {
                using (_Client)
                {
                    var response = await _Client.GetAsync($"Address/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAddressResponse = System.Text.Json.JsonSerializer.Deserialize<GetByIdAddressResponse>(responseString);
                    }
                    else
                    {
                        getAddressResponse = new GetByIdAddressResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving User"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAddressResponse = new GetByIdAddressResponse
                {
                    isSuccess = false,
                    message = "Error retrieving Address"
                };
            }
            return View(getAddressResponse.data);
        }

        //Get Details Address Predeterminada
        public async Task<ActionResult> DetailsByPredeterminada(int id)
        {
            GetByPredeterminadaAddressResponse getAddressResponse = null;
            try
            {
                using (_Client)
                {
                    var response = await _Client.GetAsync($"Address/Predeterminada/id?id={id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAddressResponse = System.Text.Json.JsonSerializer.Deserialize<GetByPredeterminadaAddressResponse>(responseString);
                    }
                    else
                    {
                        getAddressResponse = new GetByPredeterminadaAddressResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving User"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAddressResponse = new GetByPredeterminadaAddressResponse
                {
                    isSuccess = false,
                    message = "Error retrieving Address"
                };
            }
            return View(getAddressResponse.data);
        }

        //GetAll Details Address by userid
        public async Task<ActionResult> DetailsByUserid(int id)
        {
            GetByUserIdAddressResponse getAddressResponse = null;
            try
            {
                using (_Client)
                {
                    var response = await _Client.GetAsync($"Address/idUser/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getAddressResponse = System.Text.Json.JsonSerializer.Deserialize<GetByUserIdAddressResponse>(responseString);
                    }
                    else
                    {
                        getAddressResponse = new GetByUserIdAddressResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving User"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getAddressResponse = new GetByUserIdAddressResponse
                {
                    isSuccess = false,
                    message = "Error retrieving Address"
                };
            }
            return View(getAddressResponse.data);
        }


        // GET: AddressController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddressController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAddressModel model)
        {
            CreateAddressResponse CreateResponse = null;
            try
            {

                using (_Client)
                {
                    var response = await _Client.PostAsJsonAsync("Address/CreateAddressDto", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        CreateResponse = System.Text.Json.JsonSerializer.Deserialize<CreateAddressResponse>(responseString);

                    }

                }
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return View();
            }
        }

        // GET: AddressController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AddressController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAddressModel model)
        {
            EditAddressResponse EditResponse = null;
            try
            {

                using (_Client)
                {
                    var response = await _Client.PostAsJsonAsync("Address/UpdateAddressDto", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        EditResponse = System.Text.Json.JsonSerializer.Deserialize<EditAddressResponse>(responseString);

                    }

                }
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return View();
            }
        }

        // GET: AddressController1/Edit/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AddressController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DisableAddressModel model)
        {
            DisableAddressResponse DisableResponse = null;
            try
            {

                using (_Client)
                {
                    var response = await _Client.PostAsJsonAsync("Address/DisableAddressDto", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        DisableResponse = System.Text.Json.JsonSerializer.Deserialize<DisableAddressResponse>(responseString);

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
