using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebAppProject.Models;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    public class TestSessionGlobalController : Controller
    {

        [HttpGet]
        public IActionResult LoadTextSession(StorageViewModel storageViewModel)
        {

            //Load Session
            string sessionString = HttpContext.Session.GetString("a5de8f5d-c99b-4308-a91a-2ecfae6993da");
            if (!String.IsNullOrEmpty(sessionString))
            {
                storageViewModel = JsonSerializer.Deserialize<StorageViewModel>(sessionString);
            }

            return View("TestSessionGlobal", storageViewModel);
        }

    }
}
