namespace jHackson.Core.Actions
{
    public interface IActionVariable
    {
        #region Properties

        string Name { get; }
        string Value { get; set; }

        #endregion
    }
}