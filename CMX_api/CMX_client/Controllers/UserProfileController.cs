using CMX_client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CMX_client.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly HttpClient client = null;
        private string UserApiUrl = "";

        public UserProfileController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserApiUrl = "https://localhost:7083/odata/Users";
        }
        public async Task<IActionResult> EditProfileAsync()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            HttpResponseMessage response = await client.GetAsync(UserApiUrl + "(" + HttpContext.Session.GetInt32("UserId") + ")");
            string strData = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(strData);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            User user = System.Text.Json.JsonSerializer.Deserialize<User>(data.ToString(), options);
            ViewData["user"] = user;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(User user)
        {
            if (user.UserId != HttpContext.Session.GetInt32("UserId"))
            {
                return RedirectToAction("EditProfile", "UserProfile");
            }
            string json = JsonSerializer.Serialize<User>(user);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(UserApiUrl + "(" + user.UserId + ")", httpContent);
            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.Remove("UserId");
                HttpContext.Session.Remove("Email");
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Email", user.Email);
                return RedirectToAction("Index", "Order");
            }
            return RedirectToAction("EditProfile", "UserProfile");
        }
        public async Task<IActionResult> AddProfileAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(User user)
        {
            string json = JsonSerializer.Serialize<User>(user);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(UserApiUrl, httpContent);
            if (response.IsSuccessStatusCode)
            {
                //nhan lai member
                string strData = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(strData);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                User user1 = System.Text.Json.JsonSerializer.Deserialize<User>(data.ToString(), options);
                if (user1 == null)
                    return View();
                else
                {
                    HttpContext.Session.SetInt32("UserId", user1.UserId);
                    HttpContext.Session.SetString("Email", user1.Email);
                    return RedirectToAction("Index", "Product");
                }
                return RedirectToAction("Index", "Product");

            }
            return RedirectToAction("AddProfile", "UserProfile");
        }
    }
}
