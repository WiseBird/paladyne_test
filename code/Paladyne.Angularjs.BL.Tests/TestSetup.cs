﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Paladyne.Angularjs.BL.Tests
{
    [SetUpFixture]
    public class TestsSetup
    {
        [SetUp]
        public void SetupFixture()
        {
            AutoMapperConfig.RegisterMappers();
        }
    }
}
