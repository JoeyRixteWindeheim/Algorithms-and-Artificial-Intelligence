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
        [DataRow(4, 3, 5, 4, 9, 7)]
        [DataRow(-4, -3, -2, -5, -6, -8)]
        [DataRow(-0.4, 3, 5, -0.8, 4.6, 2.2)]
        public void TestAdd(double vectorX, double vectorY, double additionVectorX, double additionVectorY, double expectedX, double expectedY)
        {
            
            Vector2D vector = new Vector2D(vectorX, vectorY);
            Vector2D vectorToAdd = new Vector2D(additionVectorX, additionVectorY);
            Vector2D expectedVector = new Vector2D(expectedX, expectedY);
            Vector2D resultingVector = vector.Add(vectorToAdd);

            Assert.AreEqual(expectedVector.X, resultingVector.X);
            Assert.AreEqual(expectedVector.Y, resultingVector.Y);
        }


        [TestMethod]
        [DataRow(4, 3, 2, 2, 1.5)]
        [DataRow(-4, -3, -2, 2, 1.5)]
        [DataRow(-0.4, 3, 0.1, -4, 30)]
        public void TestDevide(double vectorX, double vectorY, double division, double expectedX, double expectedY)
        {

            Vector2D vector = new Vector2D(vectorX, vectorY);
            Vector2D resultingVector = vector.Divide(division);
            Vector2D expectedVector = new Vector2D(expectedX, expectedY);
            

            Assert.AreEqual(expectedVector.X, resultingVector.X);
            Assert.AreEqual(expectedVector.Y, resultingVector.Y);
        }
    }
}
