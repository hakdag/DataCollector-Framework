using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataCollector.IO
{
    public class PlanFile
    {
        private object content;

        public void Load<T>(string filename)
        {
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                var bformatter = new BinaryFormatter();

                content = (T)bformatter.Deserialize(stream);
            }
        }

        public T GetContent<T>()
        {
            return (T)content;
        }

        public void AddContent<T>(T content)
        {
            this.content = content;
        }

        public void Save(string filename)
        {
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                var bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, content);
            }
        }
    }
}
