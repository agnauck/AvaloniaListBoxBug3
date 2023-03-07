namespace AvaloniaListBoxBug.Models
{
    public class Items : Collection<Item, int>
    {
        public Items() : base(i => i.Id)
        {
        }
    }
}
