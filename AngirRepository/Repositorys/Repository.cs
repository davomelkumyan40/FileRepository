using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Repo.Repositorys
{
    abstract class Repository<T> : IRepository<T> where T : class, new()
    {
        public Repository() => InnerCollection = AsEnumerable().ToList();

        private IList<T> InnerCollection { get; set; }
        protected abstract string FilePath { get; }
        private const string start = "<start>";
        private const string end = "</start>";

        public IEnumerable<T> AsEnumerable()
        {
            FileStream stream = new FileStream(FilePath, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(stream);

            T model = null;
            string line = null;
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                switch (line)
                {
                    case start:
                        model = new T();
                        break;
                    case end:
                        yield return model;
                        break;
                    default:
                        ModelFill(line, model);
                        break;
                }
            }
            stream.Dispose();
            reader.Dispose();
        }

        private void ModelFill(string line, T model)
        {
            string[] data = line.Split(':');
            if (data.Length < 2)
                return;

            OnModelFill(data, model);
        }

        private void OnModelFill(string[] data, T model)
        {
            string propName = data[0];
            PropertyInfo pi = model.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
            if (pi != null)
            {
                if (byte.TryParse(data[1], out byte value))
                    pi.SetValue(model, value);
                else
                    pi.SetValue(model, data[1]);
            }
        }

        public void Add(T model) => InnerCollection.Add(model);

        public void Insert(int index, T model) => InnerCollection.Insert(index, model);

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                InnerCollection.Add(item);
        }

        public void Save()
        {
            FileStream stream = new FileStream(FilePath, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(stream);

            foreach (T person in InnerCollection)
            {
                writer.WriteLine(start);
                OnSave(writer, person);
                writer.WriteLine(end);
            }
            writer.Dispose();
            stream.Dispose();
        }

        private void OnSave(StreamWriter writer, T person)
        {
            PropertyInfo[] pis = person.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pi in pis)
            {
                if (!IsIgnored(pi.Name))
                {
                    object value = pi.GetValue(person);
                    if (pi.PropertyType.IsEnum)
                            writer.WriteLine($"{pi.Name}:{Convert.ToByte(value)}"); 
                    else
                        writer.WriteLine($"{pi.Name}:{pi.GetValue(person)}");
                }      
            }
        }

        protected abstract bool IsIgnored(string propName);
    }
}
