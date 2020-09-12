using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VerasWeb.Models;
using VerasWeb.Services;

namespace VerasWeb.Controllers
{
    public class Covid19ItemController : Controller
    {
        private readonly ICosmosDBService _cosmosDBService;
        public Covid19ItemController(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }
        [Authorize]
        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDBService.GetItemsAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id, Date, CPR, Name, Smittet")] Covid19Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                await _cosmosDBService.AddItemAsync(item);
                return RedirectToAction("EndView");
            }

            return View(item);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id, Date, CPR, Name, Smittet")] Covid19Item item)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDBService.UpdateItemAsync(item.Id, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [Authorize]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Covid19Item item = await _cosmosDBService.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [Authorize]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Covid19Item item = await _cosmosDBService.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await _cosmosDBService.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _cosmosDBService.GetItemAsync(id));
        }

        [ActionName("EndView")]
        public IActionResult EndView()
        {
            return View();
        }
    }
}
