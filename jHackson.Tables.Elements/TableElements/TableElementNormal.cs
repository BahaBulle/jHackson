using jHackson.Core.Exceptions;
using jHackson.Core.Localization;
using jHackson.Core.TableElements;
using System.Text.RegularExpressions;

namespace jHackson.Tables.TableElements
{
    public class TableElementNormal : TableElementBase
    {
        public TableElementNormal() : base()
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
                    return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            // TODO
            return base.GetHashCode();
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
                catch (jHacksonTableException ex)
                {
                    this.AddError(ex.Message);
                }
            }
            else
                this.AddError(LocalizationManager.GetMessage("core.tableElement.notConfirmingLine", this.Line));
        }
    }
}