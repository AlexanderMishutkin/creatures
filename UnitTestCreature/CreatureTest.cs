using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreaturesLibrary;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace UnitTestCreature
{
    [TestClass]
    public class CreatureTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Creature("1", MovementType.Flying, 3);
            });

            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Creature("10 10 10 10 10 10 10 10 ---4-40494i2904243fgnlkj", MovementType.Flying, 3);
            });

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new Creature(null, MovementType.Flying, 3);
            });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                new Creature("Alexander", MovementType.Flying, 1000);
            });

            Assert.IsInstanceOfType(new Creature("Alexander", MovementType.Flying, 3), typeof(Creature));
        }

        [TestMethod]
        public void TestToString()
        {
            Creature c = new Creature("Alexander", MovementType.Walking, 5.555555);

            Assert.AreEqual("Walking creature Alexander: Health = 5,556", c.ToString());
        }

        [TestMethod]
        public void TestMultiplication()
        {
            Creature a = new Creature("Alexander", MovementType.Swimming, 5.555555);
            Creature b = new Creature("Brevius", MovementType.Swimming, 0.1);
            Creature t = new Creature("flying", MovementType.Flying, 9);

            Creature c = a * b;
            Creature d = b * a;

            Assert.ThrowsException<ArgumentException>(()=> { Creature er = a * t; });
            Assert.AreEqual(c, d);
            Assert.AreEqual(new Creature("Alexavius", MovementType.Swimming, (5.555555 + 0.1) /2), c);
        }

        [TestMethod]
        public void TestSerialization()
        {
            Creature c = new Creature("Alexander", MovementType.Flying, 3);

            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Creature));
                serializer.WriteObject(ms, c);

                ms.Seek(0, SeekOrigin.Begin);

                Assert.AreEqual(c, serializer.ReadObject(ms));
            }
        }
    }
}
