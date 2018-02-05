using NUnit.Framework;
using Ploeh.AutoFixture;
using System;

namespace AguaSB.Views.Conversores.Tests
{
    [TestFixture]
    public class ArithmeticConverterTests
    {
        private readonly Fixture Any = new Fixture();
        private readonly ArithmeticConverter Converter = new ArithmeticConverter();

        [Test]
        public void ShouldThrowArgumentNullException_WhenParameterIsNullOrWhitespace() =>
            Assert.Throws<ArgumentNullException>(() => Converter.Convert(Any.Create<int>(), null, "", null));

        [Test]
        public void ShouldThrowArgumentException_When_AOrParameterReference_IsNullOrWhitespace()
        {
            //Assert.Throws<ArgumentException>(() => Converter.Convert(Any.Create<int>))
        }
    }
}
