using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CreaturesLibrary
{
    /// <summary>
    /// Существо
    /// </summary>
    [DataContract]
    public class Creature
    {
        [DataMember]
        private string name;

        [DataMember]
        private MovementType movementType;

        [DataMember]
        private double health;

        /// <summary>
        /// Имя существа - [6, 10] символов
        /// </summary>
        public string Name { get => name; }

        /// <summary>
        /// Способ передвижения существа
        /// </summary>
        public MovementType MovementType { get => movementType; }

        /// <summary>
        /// Здоровье существа - [0, 5)
        /// </summary>
        public double Health { get => health; }

        /// <summary>
        /// Создает существо по всем заданым параметрам
        /// </summary>
        /// <param name="name">Имя существа - [6, 10] символов</param>
        /// <param name="movementType">Способ передвижения существа</param>
        /// <param name="health">Здоровье существа - [0, 5)</param>
        public Creature(string name, MovementType movementType, double health)
        {
            if (name == null) throw new ArgumentNullException("No name :(");
            if (name.Length > 10 || name.Length < 6)
                throw new ArgumentException("Invalid name");
            if (health < 0 || health >= 10 || double.IsNaN(health))
                throw new ArgumentOutOfRangeException("Invalid health");

            this.health = health;
            this.name = name;
            this.movementType = movementType;
        }

        public override string ToString()
        {
            return $"{MovementType} creature {Name}: Health = {Health:F3}";
        }

        public static Creature operator *(Creature a, Creature b)
        {
            string firstPart = (a.Name.Length > b.Name.Length) ? a.Name : b.Name;
            string secondPart = (a.Name.Length > b.Name.Length) ? b.Name : a.Name;

            /*
             Примечание:
             У строки нечетной длины нет первой/второй половины,
             ввиду того что по критериям длины имен подходит нормально,
             я брал подстроку включая середину.
             */
            firstPart = firstPart.Substring(0, (firstPart.Length + 1) / 2);
            secondPart = secondPart.Substring(secondPart.Length / 2);

            if (a.MovementType != b.MovementType)
                throw new ArgumentException($"Can not multiply {a.MovementType} and {b.MovementType} creature");

            Creature c = new Creature(firstPart + secondPart, a.MovementType, (a.Health + b.Health) / 2);
            return c;
        }

        public override bool Equals(object obj)
        {
            if (obj is Creature)
            {
                Creature c = obj as Creature;
                return (c.Health == Health) && (c.Name == Name) && (c.MovementType == MovementType);
            }
            return base.Equals(obj);
        }
    }
}
