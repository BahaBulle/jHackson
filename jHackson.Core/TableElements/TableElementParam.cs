namespace jHackson.Core.TableElements
{
    public class TableElementParam : ITableElementParam
    {
        public int NbBytes { get; set; }
        public int Position { get; set; }
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            TableElementParam param = (TableElementParam)obj;

            if (this.NbBytes == param.NbBytes && this.Position == param.Position && this.Value == param.Value)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;

            hash = hash * 23 + this.NbBytes.GetHashCode();
            hash = hash * 23 + this.Position.GetHashCode();
            hash = hash * 23 + this.Value.GetHashCode();

            return hash;
        }
    }
}