using jHackson.Core.Common;
using jHackson.Core.TableElements;
using System;
using System.Collections.Generic;

namespace jHackson.Tables.Common
{
    public class TableElementFactory
    {
        private readonly string _line;

        public TableElementFactory(string line)
        {
            this._line = line;
        }

        public ITableElement Build(bool warningsAsErrors = false)
        {
            foreach (KeyValuePair<string, Type> element in DataContext.GetTableElements())
            {
                var elem = DataContext.GetTableElement(element.Key);

                if (elem.IsThisElement(this._line))
                {
                    elem = elem
                        .WithLine(this._line)
                        .WithWarningsAsErrors(warningsAsErrors);

                    elem.Init();

                    return elem;
                }
            }

            return null;
        }
    }
}