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
        /// <summary>
        /// Десериализует объект
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns>Десириализованный объект</returns>
        static T DeserializeObject<T>()
        {
            try
            {
                using (FileStream fs = new FileStream(Creature.defaultSerializationPath, FileMode.Open))
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
            List<Creature> creatures = DeserializeObject<List<Creature>>();

            creatures.ForEach(Console.WriteLine);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("a)");

            Console.WriteLine(creatures
                .Where(c => c.MovementType == MovementType.Swimming).Count());

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("b)");

            creatures.OrderBy(c => -c.Health).Take(10).ToList().ForEach(Console.WriteLine);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("c)");

            var newCreatures = creatures.GroupBy(c => c.MovementType).ToList()
                .ConvertAll((group) => group.Aggregate((a, b) => a * b));
            newCreatures.ForEach(Console.WriteLine);
            
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("d)");

            newCreatures.OrderBy(c => -c.Health).Take(10).ToList().ForEach(Console.WriteLine);
            
        }
    }
}
