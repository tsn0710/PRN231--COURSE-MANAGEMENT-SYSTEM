using CMX_client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CMX_client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;
        private string MemberApiUrl = "";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberApiUrl = "https://localhost:7083/odata/Users";
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(User user)
        {
            var builder = new ConfigurationBuilder().
                                                SetBasePath(Directory.GetCurrentDirectory()).
                                                AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            string adminEmail = configuration.GetConnectionString("email");
            string adminPassword = configuration.GetConnectionString("password");

            if (user.Password == null || user.Password.Length == 0 || user.Email == null || user.Email.Length == 0)
                return View();
            //get list of User
            HttpResponseMessage response = await client.GetAsync(MemberApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(strData);
            var listUserj = data["value"];
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<User> listUsers = JsonSerializer.Deserialize<List<User>>(listUserj.ToString(), options);
            //is list contain this User ?
            User user1 = null;
            if (user.Password.Trim().Equals(adminPassword) && user.Email.Trim().Equals(adminEmail))
            {
                user = listUsers.Where(x => x.UserId == 1).First();
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Email", user.Email);
                return RedirectToAction("Index", "Order");
            }
            foreach (User m in listUsers)
            {
                if (m.Email.Equals(user.Email.Trim()) && m.Password.Equals(user.Password.Trim()))
                {
                    user1 = new User();
                    user1.UserId = m.UserId;
                    user1.Email = m.Email;
                    break;
                }
            }
            if (user1 == null)
                return View();
            else
            {
                HttpContext.Session.SetInt32("UserId", user1.UserId);
                HttpContext.Session.SetString("Email", user1.Email);
                return RedirectToAction("Index", "Order");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Index", "Home");
        }
    }
}