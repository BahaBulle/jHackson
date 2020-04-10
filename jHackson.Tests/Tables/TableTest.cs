﻿using jHackson.Core.Common;
using jHackson.Tables;
using jHackson.Tables.TableElements;
using NUnit.Framework;
using System.Collections.Generic;

namespace jHackson.Tests.Tables
{
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

            tbl.LoadStdAscii(true);

            Assert.That(225, Is.EqualTo(tbl.Count));
        }

        [Test]
        public void ShouldLoadAsciiStandardTable()
        {
            var tbl = new Table();

            tbl.LoadStdAscii(null);

            Assert.That(97, Is.EqualTo(tbl.Count));
        }

        [Test]
        public void ShouldLoadTableWithNormalElement()
        {
            var list = new List<string>()
            {
                "20= "
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
                "%0102=<Pause value='%1' next='%1'>"
            };

            var tbl = new Table();

            tbl.Load(list);

            Assert.That(1, Is.EqualTo(tbl.Count));
        }
    }
}