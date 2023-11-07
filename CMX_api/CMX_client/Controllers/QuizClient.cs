using CMX_client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CMX_client.Controllers
{
    public class QuizClient : Controller
    {
        private readonly HttpClient client = null;
        public QuizClient()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }
        [HttpGet("QuizClient/ListQuizInACourse/{courseId}")]
        public async Task<IActionResult> ListQuizInACourseAsync(int courseId)
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:5251/odata/Quizzes/GetQuizzes" + "?$filter=courseId eq "+courseId);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<QuizDTO> quizDtos = System.Text.Json.JsonSerializer.Deserialize<List<QuizDTO>>(strData, options);
            ViewData["courseId"] = courseId;
            return View(quizDtos);
        }
        [HttpGet]
        public async Task<IActionResult> RemoveAsync(int quizId,int courseId)
        {
            HttpResponseMessage response = await client.DeleteAsync("http://localhost:5251/odata/Quiz/DeleteQuiz/" + quizId);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListQuizInACourse", new { courseId = courseId });
            }
            return RedirectToAction("ListQuizInACourse", new { courseId =courseId});
        }
        [HttpGet]
        public async Task<IActionResult> AddAsync(int courseId)
        {
            ViewData["courseId"] = courseId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddQuizAsync([FromForm] Quiz quiz)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
            };
            string json = System.Text.Json.JsonSerializer.Serialize<Quiz>(quiz, options);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:5251/odata/Quiz/AddQuiz", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListQuizInACourse", new { courseId = quiz.CourseId });

            }
            return RedirectToAction("AddQuiz",  new { courseId = quiz.CourseId });
        }
        [HttpGet]
        public async Task<IActionResult> DetailAsync(int quizId, int courseId)
        {
            QuizDTO quiz ;

            HttpResponseMessage response = await client.GetAsync("http://localhost:5251/odata/Quizzes/GetQuizByKey/" + quizId);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            quiz = System.Text.Json.JsonSerializer.Deserialize<QuizDTO>(strData, options);

            ViewData["courseId"] = courseId;
            return View(quiz);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int quizId, int courseId)
        {
            QuizDTO quiz;

            HttpResponseMessage response = await client.GetAsync("http://localhost:5251/odata/Quizzes/GetQuizByKey/" + quizId);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            quiz = System.Text.Json.JsonSerializer.Deserialize<QuizDTO>(strData, options);

            ViewData["courseId"] = courseId;
            return View(quiz.GetQuizView());
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuizPostAsync(IFormCollection collection)
        {
            int courseId = Int32.Parse(collection["courseId"]);
            int quizId = Int32.Parse(collection["quizId"]);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
            };
            int max = Int32.Parse(collection["NoOfQuestion2"]);
            bool result = true;
            for (int i = 1; i <= max; i++)
            {

                if ((collection["S_" + i].Equals("d")) && (collection["questionId_" + i].ToString().Length != 0))
                {
                    result = await deleteQuestionAsync(collection["questionId_" + i]);
                }
                else if (collection["S_" + i].Equals("d") && (collection["questionId_" + i].ToString().Length == 0))
                {
                    //delete nothing
                    int iu = 0;
                }
                else if (collection["S_" + i].Equals("c"))
                {
                    result = await addQuestionAsync(quizId, collection["Q_" + i], collection["A_" + i], collection["B_" + i], collection["C_" + i], collection["D_" + i], collection["E_" + i]);
                }
                else if (collection["S_" + i].Equals("n"))
                {
                    //nothing change
                    int io = 0;
                }
                else if (collection["S_" + i].Equals("e"))
                {
                    result = await editQuestionAsync(collection["questionId_" + i], quizId, collection["Q_" + i], collection["A_" + i], collection["B_" + i], collection["C_" + i], collection["D_" + i], collection["E_" + i]);
                }
                else
                {
                    result = false;
                }
                if (result == false)
                {
                    return RedirectToAction("Update", "QuizClient", new { quizId = quizId,courseId= courseId });
                }
            }
            return RedirectToAction("ListQuizInACourse", new { courseId = courseId });
        }
        string QuestionApiUrl = "http://localhost:5251/odata/Questions";
        private async Task<bool> editQuestionAsync(StringValues stringValues1, int quizId, StringValues Q, StringValues A, StringValues B, StringValues C, StringValues D, StringValues E)
        {
            QuestionDTO a = new QuestionDTO();
            a.QuizId = quizId;
            a.QuestionId = Int32.Parse(stringValues1);
            a.Question1 = Q;
            a.OptionA = A;
            a.OptionB = B;
            a.OptionC = C;
            a.OptionD = D;
            a.CorrectOption = E;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
            };
            string json = System.Text.Json.JsonSerializer.Serialize<QuestionDTO>(a, options);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(QuestionApiUrl+"("+ a.QuestionId + ")", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
        }

        private async Task<bool> addQuestionAsync(int quizId, StringValues Q, StringValues A, StringValues B, StringValues C, StringValues D, StringValues E)
        {
            QuestionDTO a = new QuestionDTO();
            a.QuizId = quizId;
            a.QuestionId = 0;
            a.Question1 = Q;
            a.OptionA = A;
            a.OptionB = B;
            a.OptionC = C;
            a.OptionD = D;
            a.CorrectOption = E;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
            };
            string json = System.Text.Json.JsonSerializer.Serialize<QuestionDTO>(a, options);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(QuestionApiUrl, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
        }

        private async Task<bool> deleteQuestionAsync(StringValues stringValues)
        {
            HttpResponseMessage response = await client.DeleteAsync(QuestionApiUrl + "(" + Int32.Parse(stringValues) + ")");
            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
        }
        [HttpGet]
        public async Task<IActionResult> DoquizAsync(int quizId, int courseId)
        {
            QuizDTO quiz;

            HttpResponseMessage response = await client.GetAsync("http://localhost:5251/odata/Quizzes/GetQuizByKey/" + quizId);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            quiz = System.Text.Json.JsonSerializer.Deserialize<QuizDTO>(strData, options);

            ViewData["courseId"] = courseId;
            return View(quiz.GetQuizView());
        }
        [HttpPost]
        public async Task<IActionResult> DoQuizPostAsync(IFormCollection collection)
        {
            int courseId = Int32.Parse(collection["courseId"]);
            int quizId = Int32.Parse(collection["quizId"]);
            int max = Int32.Parse(collection["NoOfQuestion2"]);
            int userId = HttpContext.Session.GetInt32("UserId")??0;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
            };
            bool result = true;
            List<string> strList = new List<string>();
            strList.Add(userId + "");
            strList.Add(quizId + "");
            for (int i = 1; i <= max; i++)
            {
                strList.Add(collection["Q_" + i].ToString());
            }
            string json = System.Text.Json.JsonSerializer.Serialize<List<string>>(strList, options);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://localhost:5251/odata/Quiz/SubmitQuiz", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListQuizInACourse", new { courseId = courseId });

            }
            return RedirectToAction("Doquiz", new { quizId= quizId, courseId = courseId });
        }
    }
}
