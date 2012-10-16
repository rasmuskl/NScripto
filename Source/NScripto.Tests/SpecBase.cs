using System.Linq;
using System.Collections.Generic;
using System;
using NUnit.Framework;

namespace NScripto.Tests
{
    [TestFixture]
    public abstract class SpecBase
    {
        [SetUp]
        public void SetUp()
        {
            Arrange();
            Act();
        }

        protected abstract void Arrange();
        protected abstract void Act();
    }
}