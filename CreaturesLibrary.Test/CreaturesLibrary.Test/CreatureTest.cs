using System;
using Xunit;
using System.Text;
using System.Collections.Generic;
using CreaturesLibrary;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace CreaturesLibrary.Test
{
    public class CreatureTest
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Creature("1", MovementType.Flying, 3);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                new Creature("10 10 10 10 10 10 10 10 ---4-40494i2904243fgnlkj", MovementType.Flying, 3);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new Creature(null, MovementType.Flying, 3);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new Creature("Alexander", MovementType.Flying, 1000);
            });

            Assert.IsType<Creature>(new Creature("Alexander", MovementType.Flying, 3));
        }

        [Fact]
        public void TestToString()
        {
            Creature c = new Creature("Alexander", MovementType.Walking, 5.555555);

            Assert.Equal($"Walking creature Alexander: Health = {5.556}", c.ToString());
        }

        [Fact]
        public void TestMultiplication()
        {
            Creature a = new Creature("Alexander", MovementType.Swimming, 5.555555);
            Creature b = new Creature("Brevius", MovementType.Swimming, 0.1);
            Creature t = new Creature("flying", MovementType.Flying, 9);

            Creature c = a * b;
            Creature d = b * a;

            Assert.Throws<ArgumentException>(() => { Creature er = a * t; });
            Assert.Equal(c, d);
            Assert.Equal(new Creature("Alexavius", MovementType.Swimming, (5.555555 + 0.1) / 2), c);
        }

        [Fact]
        public void TestSerialization()
        {
            Creature c = new Creature("Alexander", MovementType.Flying, 3);

            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Creature));
                serializer.WriteObject(ms, c);

                ms.Seek(0, SeekOrigin.Begin);
                
                Assert.Equal(c, serializer.ReadObject(ms));
            }
        }
    }
}
