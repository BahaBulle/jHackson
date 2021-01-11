// <copyright file="TableElementFactory.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Scripts.Tables
{
    using JHackson.Core.Common;
    using JHackson.Core.TableElements;

    internal class TableElementFactory
    {
        private readonly string line;

        public TableElementFactory(string line)
        {
            this.line = line;
        }

        public ITableElement Build(bool warningsAsErrors = false)
        {
            foreach (var element in DataContext.GetTableElements())
            {
                var elem = DataContext.GetTableElement(element.Key);

                if (elem.IsThisElement(this.line))
                {
                    elem = elem
                        .WithLine(this.line)
                        .WithWarningsAsErrors(warningsAsErrors);

                    elem.Init();

                    return elem;
                }
            }

            return null;
        }
    }
}