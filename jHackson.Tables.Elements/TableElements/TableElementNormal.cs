// <copyright file="TableElementNormal.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Tables.TableElements
{
    using System.Text.RegularExpressions;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.TableElements;

    public class TableElementNormal : TableElementBase
    {
        public TableElementNormal()
            : base()
        {
            this.Identifier = null;
            this.Name = "NORMAL";
            this.RegexLine = new Regex(@"^([0-9A-Fa-f]+)=(.+)$");
        }

        public override bool Equals(object obj)
        {
            // TODO
            if (obj is TableElementNormal element)
            {
                if (element.Key == this.Key && element.Value == this.Value)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(this.Key, this.Value);
        }

        protected override void Split()
        {
            var matchResult = this.RegexLine.Match(this.Line);

            if (matchResult.Success)
            {
                try
                {
                    this.SetKey(matchResult.Groups[1].ToString());

                    this.SetValue(matchResult.Groups[2].ToString());

                    this.SetRegexValue();
                }
                catch (JHacksonTableException ex)
                {
                    this.AddError(ex.Message);
                }
            }
            else
            {
                this.AddError(LocalizationManager.GetMessage("core.tableElement.notConfirmingLine", this.Line));
            }
        }
    }
}