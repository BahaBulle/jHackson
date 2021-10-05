// <copyright file="TableTest.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tests.Tables
{
    using System.Collections.Generic;
    using JHackson.Core.Common;
    using JHackson.Text.Tables;
    using JHackson.Text.Tables.Elements;
    using NUnit.Framework;

    public class TableTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            DataContext.AddTableElement("NORMAL", typeof(TableElementNormal));
            DataContext.AddTableElement("PARAM", typeof(TableElementWithParam));
        }

        [Test]
        public void ShouldLoadAsciiExtendTable()
        {
            var tbl = new Table();

            tbl.LoadStandardAscii(true);

            Assert.That(225, Is.EqualTo(tbl.Count));
        }

        [Test]
        public void ShouldLoadAsciiStandardTable()
        {
            var tbl = new Table();

            tbl.LoadStandardAscii(null);

            Assert.That(97, Is.EqualTo(tbl.Count));
        }

        [Test]
        public void ShouldLoadTableWithNormalElement()
        {
            var list = new List<string>()
            {
                "20= ",
            };

            var tbl = new Table();

            tbl.Load(list);

            Assert.That(1, Is.EqualTo(tbl.Count));
        }

        [Test]
        public void ShouldLoadTableWithParamElement()
        {
            var list = new List<string>()
            {
                "%0102=<Pause value='%1' next='%1'>",
            };

            var tbl = new Table();

            tbl.Load(list);

            Assert.That(1, Is.EqualTo(tbl.Count));
        }
    }
}