namespace jHackson.Core.Actions
{
    public class BufferParameters
    {
        public long? AdressEnd { get; set; }
        public long? AdressStart { get; set; }
        public long? Size { get; set; }

        public void Init()
        {
            if (!this.AdressStart.HasValue)
                this.AdressStart = 0;

            if (this.AdressEnd.HasValue && !this.Size.HasValue)
                this.Size = this.AdressEnd - this.AdressStart;

            if (this.Size.HasValue && !this.AdressEnd.HasValue)
                this.AdressEnd = this.AdressStart + this.Size;
        }
    }
}