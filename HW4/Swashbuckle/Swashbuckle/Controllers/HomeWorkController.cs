using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Service;

namespace Swashbuckle.Controllers
{
    [Route("[controller]")]
    public class HomeWorkController : ControllerBase
    {
        readonly HomeworkService _homeworkService;

        public HomeWorkController(HomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
        }

        [HttpGet]
        public List<string> GetData(int? id)
        {
            return _homeworkService.getData(id);
        }

        [HttpPost]
        public bool AddData(string data)
        {
            return _homeworkService.AddData(data);
        }

        [HttpPatch]
        public bool PatchData(int id, string data)
        {
            return _homeworkService.PatchData(id, data);
        }

        [HttpDelete]
        public bool DeleteData(int id)
        {
            return _homeworkService.DeleteData(id);
        }
    }
}
