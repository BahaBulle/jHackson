namespace jHackson.Core.Actions
{
    public class ActionVariable : IActionVariable
    {
        #region Properties

        public string Name { get; set; }
        public string Value { get; set; }

        #endregion

        #region Constructors

        public ActionVariable()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }

        #endregion
    }
}