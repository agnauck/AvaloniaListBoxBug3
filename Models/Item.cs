namespace AvaloniaListBoxBug.Models
{
    using ReactiveUI;

    public class EvenItem : Item
    {
        public EvenItem(int id) : base(id){}
    }
    
    public class UnEvenItem : Item
    {
        public UnEvenItem(int id) : base(id){}
    }
    
    public class Item : ReactiveObject
    {
        public Item(int id)
        {
            _id = id;
        }
        
        private int _id;
        private string _name;
        public int Id => _id;

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
    }

}
