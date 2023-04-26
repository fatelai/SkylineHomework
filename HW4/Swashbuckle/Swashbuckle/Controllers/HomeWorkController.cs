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

        [Route("[controller]/{id?}")]
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

        [Route("[controller]/{id}")]
        [HttpPatch]
        public bool PatchData(int id, string data)
        {
            return _homeworkService.PatchData(id, data);
        }

        [Route("[controller]/{id}")]
        [HttpPut]
        public bool PutData(int id, string data)
        {
            return _homeworkService.PatchData(id, data);
        }


        [Route("[controller]/{id}")]
        [HttpDelete]
        public bool DeleteData(int id)
        {
            return _homeworkService.DeleteData(id);
        }
    }
}
