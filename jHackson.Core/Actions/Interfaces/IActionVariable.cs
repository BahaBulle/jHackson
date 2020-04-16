namespace jHackson.Core.Actions
{
    public interface IActionVariable
    {
        string Name { get; }
        string Value { get; set; }
    }
}