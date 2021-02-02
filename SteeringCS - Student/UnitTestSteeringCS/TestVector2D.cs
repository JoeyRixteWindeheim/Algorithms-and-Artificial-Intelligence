using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteeringCS;

namespace UnitTestSteeringCS
{
    [TestClass]
    public class TestVector2D
    {
        [TestMethod]
        [DataRow(4, 3, 0.8,0.6)]
        [DataRow(-4, -3, -0.8,-0.6)]
        [DataRow(0.4, 0.3, 0.8,0.6)]
        public void TestNormalize(double vectorX, double vectorY,double expectedResultX, double expectedResultY)
        {
            Vector2D vector = new Vector2D(vectorX, vectorY);

            var result = vector.Normalize();

            Assert.AreEqual(expectedResultX, result.X);
            Assert.AreEqual(expectedResultY, result.Y);
        }

        [TestMethod]

        [DataRow(4, 3, 5)]
        [DataRow(-4, -3, 5)]
        [DataRow(0.4, 0.3, 0.5)]
        public void TestLength(double vectorX,double vectorY, double expected)
        {
            Vector2D vector = new Vector2D(vectorX, vectorY);

            var result = vector.Length();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAdd()
        {
            Vector2D vector = new Vector2D(4, 3);
            Vector2D vector2 = new Vector2D(3, 4);
            var result = vector.Length();

            double expected = 5;

            Assert.AreEqual(expected, result);
        }
    }
}
