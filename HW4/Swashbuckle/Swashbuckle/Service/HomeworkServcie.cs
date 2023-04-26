namespace Swashbuckle.Service
{
    public class HomeworkService
    {
        static List<string> TestDatas = new List<string>();

        public List<string> getData(int? id)
        {
            if(id == null)
            {
                return TestDatas;
            }
            if(id > 0 && id < TestDatas.Count)
            {
                return new List<string>() { TestDatas[id.Value] };
            }
            else
            {
                return new List<string>();
            }
        }

        public bool AddData(string data)
        {
            TestDatas.Add(data);

            return true;
        }

        public bool DeleteData(int id)
        {
            if (id < 0 && id >= TestDatas.Count)
                return false;

            TestDatas.Remove(TestDatas[id]);

            return true;
        }

        public bool PatchData(int id, string data)
        {
            if (id < 0 && id >= TestDatas.Count)
                return false;

            TestDatas[id] = data;

            return true;
        }
    }
}
