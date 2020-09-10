using System;
using fifth;
using NUnit.Framework;

namespace fifth_test
{
    public class FuncStackTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCanAddFunc()
        {
            FuncStack sut = new();
            Assert.That(sut.Stack, Is.Empty);
            sut.Push(5.AsFun());
            Assert.That(sut.Stack, Is.Not.Empty);
        }


    }
}