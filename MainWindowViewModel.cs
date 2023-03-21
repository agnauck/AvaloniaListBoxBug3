namespace AvaloniaListBoxBug
{
    using System;    
    using System.Collections.ObjectModel;
    using System.Linq;
    using ReactiveUI;
    using AvaloniaListBoxBug.Models;
    using DynamicData.Binding;    
    using System.Reactive.Linq;    
    using DynamicData;

    public class MainWindowViewModel : ReactiveObject
    {
        public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();

        public MainWindowViewModel()
        {
            GenerateItems();
        }

        private void GenerateItems()
        {
            
            var startDate = DateTime.Now.AddDays(-1);
            for (int i = 0; i < 200; i++)
            {
                if (i % 2 == 0)
                {
                    Items.Add(new EvenItem(i) { Name = $"Item", TimeStamp = startDate.AddMinutes(i)});
                }
                else
                {
                    Items.Add(new UnEvenItem(i) { Name = $"Item", TimeStamp = startDate.AddMinutes(i)});
                }
            }
        }

    }
}
