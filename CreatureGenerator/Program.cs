using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaturesLibrary;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;

namespace CreatureGenerator
{
    class Program
    {
        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        static Random random = new Random();

        /// <summary>
        /// Минимальное здоровье
        /// </summary>
        const double minHealth = 0;
        /// <summary>
        /// Максимальное здоровье
        /// </summary>
        const double maxHealth = 10;

        /// <summary>
        /// Минимальная длина имени
        /// </summary>
        const int shortName = 6;
        /// <summary>
        /// Максимальная длина имени
        /// </summary>
        const int longName = 10;

        /// <summary>
        /// Случайное здоровье
        /// </summary>
        /// <returns>Случайное здоровье</returns>
        static double RandomHealth()
        {
            double health = random.NextDouble() * (maxHealth - minHealth) + minHealth;
            return health;
        }

        /// <summary>
        /// Случайное имя
        /// </summary>
        /// <returns>Случайное имя</returns>
        static string RandomName()
        {
            int len = random.Next(shortName, longName + 1);
            StringBuilder sb = new StringBuilder(len);

            for (int i = 0; i < len; i++)
            {
                char c = (char)(random.Next('a', 'z' + 1));
                if (random.Next(2) == 0)
                {
                    c = (char)(c - 'a' + 'A');
                }
                sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Создает случайное сущ-во
        /// </summary>
        /// <returns>Случайное существо</returns>
        static Creature RandomCreature()
        {
            return new Creature(RandomName(),
                (MovementType)random.Next(3), RandomHealth());
        }

        /// <summary>
        /// Сериализует по контракту в XML объект
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="obj">Объект</param>
        static void SerializeObject<T>(T obj)
        {
            try
            {
                using(FileStream fs = new FileStream(Creature.defaultSerializationPath, FileMode.Create))
                {
                    using(XmlWriter writer = XmlWriter.Create(fs))
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(T));

                        serializer.WriteObject(writer, obj);
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
        }

        static void Main(string[] args)
        {
            List<Creature> creatures = new List<Creature>(30);

            for (int i = 0; i < 30; i++)
            {
                creatures.Add(RandomCreature()); //Генерация существ
            }

            Console.WriteLine(string.Join(",", creatures));
            /*creatures.ForEach(Console.WriteLine);*/

            SerializeObject(creatures); //Сериализация существ прямо всем списком
        }
    }
}
