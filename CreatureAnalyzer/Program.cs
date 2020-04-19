using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaturesLibrary;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;
namespace CreatureAnalyzer
{
    class Program
    {
        static T DeserializeObject<T>()
        {
            try
            {
                using (FileStream fs = new FileStream(Creature.defaultSerializationPath, FileMode.Create))
                {
                    using (XmlReader reader = XmlReader.Create(fs))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(T));

                        return (T)serializer.ReadObject(reader);
                    }
                }
            }
            catch (UnauthorizedAccessException ue)
            {
                Console.WriteLine("Недостаточно прав для доступа к файлу," +
                    " запустите программу от имени администратора");
                Console.WriteLine(ue.Message);
            }
            catch (System.Security.SecurityException se)
            {
                Console.WriteLine("Ошибка безопасности.");
                Console.WriteLine(se.Message);
            }
            catch (IOException toe)
            {
                Console.WriteLine("Ошибка чтение файла, вероятно указан неверный путь");
                Console.WriteLine(toe.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Неопознанное исключение (");
                Console.WriteLine(e);
            }

            return default(T);
        }

        static void Main(string[] args)
        {
            List <Creature> creatures = DeserializeObject<List<Creature>>();

            creatures.ForEach(Console.WriteLine);
        }
    }
}
