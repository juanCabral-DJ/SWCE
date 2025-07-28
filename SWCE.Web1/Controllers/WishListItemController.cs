using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWCE.Web1.Models.Address;
using SWCE.Web1.Models.WishListItem;
using static SWCE.Web1.Models.WishListItem.DisableItemModel;
using static SWCE.Web1.Models.WishListItem.ItemModel;

namespace SWCE.Web1.Controllers
{
    public class WishListItemController : Controller
    {
        private readonly HttpClient _Client;

        public WishListItemController(IHttpClientFactory httpClientFactory)
        {
            _Client = httpClientFactory.CreateClient("Client");
        }

        // GET: WishListItemController1
        public async Task<ActionResult> Index()
        {
            GetAllItemResponse getresponse = null;

            try
            {
                using (_Client)
                {
                    var response = await _Client.GetAsync("WishListItem");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getresponse = System.Text.Json.JsonSerializer.Deserialize<GetAllItemResponse>(responseString);
                    }
                    else
                    {
                        getresponse = new GetAllItemResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving Item"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getresponse = new GetAllItemResponse
                {
                    isSuccess = false,
                    message = "Error retrieving Address"
                };
            }
            return View(getresponse.data);
        }

        // GET: WishListItemController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            GetByIdItemResponse getresponse = null;

            try
            {
                using (_Client)
                {
                    var response = await _Client.GetAsync($"WishListItem/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getresponse = System.Text.Json.JsonSerializer.Deserialize<GetByIdItemResponse>(responseString);
                    }
                    else
                    {
                        getresponse = new GetByIdItemResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving Item"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getresponse = new GetByIdItemResponse
                {
                    isSuccess = false,
                    message = "Error retrieving Address"
                };
            }
            return View(getresponse.data);
        }

        public async Task<ActionResult> DetailsByUserid(int id_Usuario)
        {
            GetByUseridItemResponse getresponse = null;

            try
            {
                using (_Client)
                {
                    var response = await _Client.GetAsync($"WishListItem/Item/{id_Usuario}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        getresponse = System.Text.Json.JsonSerializer.Deserialize<GetByUseridItemResponse>(responseString);
                    }
                    else
                    {
                        getresponse = new GetByUseridItemResponse
                        {
                            isSuccess = false,
                            message = "Error retrieving Item"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                getresponse = new GetByUseridItemResponse
                {
                    isSuccess = false,
                    message = "Error retrieving Address"
                };
            }
            return View(getresponse.data);
        }


        // GET: WishListItemController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WishListItemController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateItemModel model)
        {
            CreateItemResponse CreateResponse = null;
            try
            {

                using (_Client)
                {
                    var response = await _Client.PostAsJsonAsync("WishListItem/CreateItemDto", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        CreateResponse = System.Text.Json.JsonSerializer.Deserialize<CreateItemResponse>(responseString);

                    }

                }
                return RedirectToAction(nameof(Index));
            }

            catch
            {
                return View();
            }
        }

        // GET: WishListItemController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WishListItemController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WishListItemController1/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new DisableItemModel { id = id };
            return View(model);
        }

        // POST: WishListItemController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DisableItemModel model)
        {
            DisableItemResponse DisableResponse = null;
            try
            {

                using (_Client)
                {
                    var response = await _Client.PostAsJsonAsync("WishListItem/DisableItemDto", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        DisableResponse = System.Text.Json.JsonSerializer.Deserialize<DisableItemResponse>(responseString);

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
