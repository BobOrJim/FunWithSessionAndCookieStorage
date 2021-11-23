using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebAppProject.Models;
using WebAppProject.ViewModels;
using System.Web;

namespace WebAppProject.Controllers
{
    public class StorageController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StorageController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            //System.Net.Cookie asdf = new();
        }

        [HttpPost]
        public IActionResult AddTextSession(StorageViewModel storageViewModel)
        {
            string TextToAdd = storageViewModel.StringStorage;

            //Load Session
            string sessionString = HttpContext.Session.GetString(ReadCookie("user.cookie"));
            if (!String.IsNullOrEmpty(sessionString))
            {
                storageViewModel = JsonSerializer.Deserialize<StorageViewModel>(sessionString);
            }
            //Append text
            storageViewModel.StringStorage += TextToAdd;
            //Save Session
            HttpContext.Session.SetString(ReadCookie("user.cookie"), JsonSerializer.Serialize(storageViewModel));
            return View("Storage", storageViewModel);
        }

        [HttpGet]
        public IActionResult LoadTextSession(StorageViewModel storageViewModel)
        {
            //Load Session
            if (HttpContext.Session.["user.cookie"] != null)
            {
                string sessionKey = ReadCookie("user.cookie");
            }

            string sessionString = HttpContext.Session.GetString(sessionKey);
            if (!String.IsNullOrEmpty(sessionString))
            {
                storageViewModel = JsonSerializer.Deserialize<StorageViewModel>(sessionString);
            }
            return View("Storage", storageViewModel);
        }

        //This will be another microservice in h-pax
        [HttpGet]
        public IActionResult IDP_LoginSimulation()
        {
            CreateCookie("user.cookie", "a5de8f5d-c99b-4308-a91a-2ecfae6993da");
            return View("Storage", new StorageViewModel());
        }


        public string ReadCookie(string key)
        {
            // string cookieValueFromContext = HttpContext.Request.Cookies["key"];
            // string cookieValueFromReq = Request.Cookies["Key"];
            Debug.WriteLine($"Value in cookie {ReadCookie("user.cookie")}");
            return Request.Cookies[key];
        }

        public void CreateCookie(string key, string value)
        {
            CookieOptions option = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false, //prevent client-side JS from accessing the cookie vlaue
                Secure = false, //allow the cookie to be served/received over HTTPS
                Expires = DateTime.Now.AddMinutes(10)
            };

            Response.Cookies.Append(key, value, option);
        }


        public void RemoveCookie(string key)
        {
            Response.Cookies.Delete(key);
        }

    }
}
