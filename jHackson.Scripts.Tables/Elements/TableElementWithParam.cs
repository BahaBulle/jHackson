// <copyright file="TableElementWithParam.cs" company="BahaBulle">
// Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace JHackson.Scripts.Tables.Elements
{
    using System.Globalization;
    using System.Text.RegularExpressions;
    using JHackson.Core.Exceptions;
    using JHackson.Core.Localization;
    using JHackson.Core.TableElements;

    public class TableElementWithParam : TableElementBase
    {
        public TableElementWithParam()
            : base()
        {
            this.Identifier = '%';
            this.Name = "PARAM";
            this.RegexLine = new Regex($@"^{this.Identifier}([0-9A-Fa-f]+)=(.+)$");
        }

        protected override void SetParam()
        {
            this.SetRegexValue();

            var rgxParam = new Regex($@"{this.Identifier}(\d+)");

            var matches = rgxParam.Matches(this.Value);
            this.NbParam = matches.Count;

            string temp = this.RegexValue;
            foreach (Match match in matches)
            {
                var param = new TableElementParam
                {
                    Position = match.Index,
                    Value = match.Groups[0].Value,
                };

                var result = int.TryParse(match.Value.Substring(1), out int nb);
                param.NbBytes = nb;

                this.ListParam.Add(param);

                this.ValueSize = this.ValueSize - (nb.ToString(CultureInfo.InvariantCulture).Length + 1) + (nb * 2);

                var reg = new Regex(this.Identifier.ToString() + nb);
                temp = reg.Replace(temp, string.Format(CultureInfo.InvariantCulture, "[0-9A-Fa-f]{{{0}}}", nb * 2), 1);
                reg = null;
            }

            this.RegexValue = temp;
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

                    this.SetParam();
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