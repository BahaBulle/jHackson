namespace jHackson.Core.TableElements
{
    public interface ITableElementParam
    {
        int NbBytes { get; set; }
        int Position { get; set; }
        string Value { get; set; }
    }
}