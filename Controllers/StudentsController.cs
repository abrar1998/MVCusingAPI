using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Using_Api_In_MVC.Models;

namespace Using_Api_In_MVC.Controllers
{
    public class StudentsController : Controller
    {
        string url = "https://localhost:7047/api/Students/";
        private HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            //convert data comming from form to JSON
            var data = JsonConvert.SerializeObject(std);
            //convert json data into formatted text because HttpResponseMessage understands context i'e formatted text
            //StringContent class creates a formatted text appropriate for the http server/client communications
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respone =  client.PostAsync(url, content).Result;
            if(respone.IsSuccessStatusCode)
            {
                TempData["data"] = "Student Added";
                return RedirectToAction("GetAllStudents");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            List<Student> student = new List<Student>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if(response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result; //result contains json data(which we received from api)
                //Now we have to convert Json to List<Student> then pass it to the view
                var data = JsonConvert.DeserializeObject<List<Student>>(result);
                if(data!=null)
                {
                    student = data;
                }
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if(response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data  = JsonConvert.DeserializeObject<Student>(result);
                if(data!=null)
                {
                    std = data;
                }
            }
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(int id, Student std)
        {
            var data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respone = client.PutAsync(url + std.id, content).Result;
            if(respone.IsSuccessStatusCode)
            {
                TempData["data1"] = "Editted Successfully";
                return RedirectToAction("GetAllStudents");
            }
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url +id).Result;
            if(response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if(data!=null)
                {
                    std = data;
                }
            }
            
            return View(std);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(Student std)
        {
            HttpResponseMessage respone = client.DeleteAsync(url + std.id).Result;
            if (respone.IsSuccessStatusCode)
            {
                TempData["data2"] = "Student Delete Successfully";
                return RedirectToAction("GetAllStudents");
            }
            return View();
        }
    }
}
