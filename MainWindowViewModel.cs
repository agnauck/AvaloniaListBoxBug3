using System;    
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;
using AvaloniaListBoxBug.Models;

namespace AvaloniaListBoxBug
{
    public class MainWindowViewModel : ReactiveObject
    {
        public ObservableCollection<Item> Items { get; } = new();

        public MainWindowViewModel()
        {
            GenerateItems();
        }

        private void GenerateItems()
        {
            var startDate = DateTime.Now.AddDays(-1);
            for (int i = 500; i < 1000; i++)
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

        /// <summary>
        /// Adds new items to the end.
        /// This can be used for infinite scrolling when scrolling to the end and load the next page of items
        /// </summary>
        public void AddToEnd()
        {
            var item = Items.Last();
            
            var startDate = item.TimeStamp;
            int start = item.Id + 1;
            
            for (int i = start; i < start + 10; i++)
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
        
        /// <summary>
        /// Adds new items to the beginning.
        /// This can be used for infinite scrolling when scrolling to the top and look for the previous page of items
        /// </summary>
        public void AddToStart()
        {
            var item = Items.First();
            
            var startDate = item.TimeStamp;
            int start = item.Id - 1;
            
            
            for (int i = start; i > start - 10; i--)
            {
                if (i % 2 == 0)
                {
                    Items.Insert(0, new EvenItem(i) { Name = $"Item", TimeStamp = startDate.AddMinutes(-i)});
                }
                else
                {
                    Items.Insert(0, new UnEvenItem(i) { Name = $"Item", TimeStamp = startDate.AddMinutes(-i)});
                }
            }
        }
    }
}
