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
        readonly ReadOnlyObservableCollection<Item> _filteredItemsObservable;
        readonly Items _items = new Items();
        
        public MainWindowViewModel()
        {
            GenerateItems();

            Func<Item, bool> searchFunc(string text)
            {
                if (string.IsNullOrEmpty(text))
                    return _ => true;

                return i => i.Name != null && (i.Name + i.Id).ToUpper().Contains(text.ToUpper());
            }
            
            Func<Item, bool> evenFilterFunc(bool show)
            {
                if (show)
                    return _ => true;
                
                return i => i is not EvenItem;
            }

            var itemsSearchTextFilter = this.WhenValueChanged(@this => @this.SearchFilter)
                //.Throttle(TimeSpan.FromMilliseconds(500))
                .Select(searchFunc);
            
            var evenItemsSearchFilter = this.WhenValueChanged(@this => @this.ShowEven)
                //.Throttle(TimeSpan.FromMilliseconds(500))
                .Select(evenFilterFunc);

            _items
                .Connect()                
                .Filter(itemsSearchTextFilter)
                .Filter(evenItemsSearchFilter)
                .Sort(
                    SortExpressionComparer<Item>
                        .Ascending(i => i.Id)
                )
                .Bind(out _filteredItemsObservable)
                .Subscribe();
        }

        public ReadOnlyObservableCollection<Item> FilteredItems => _filteredItemsObservable;

        private string _searchFilter;
        private bool _showEven = true;
        public string SearchFilter
        {
            get => _searchFilter;
            set => this.RaiseAndSetIfChanged(ref _searchFilter, value);
        }
        
        public bool ShowEven
        {
            get => _showEven;
            set => this.RaiseAndSetIfChanged(ref _showEven, value);
        }

        private void GenerateItems()
        {
            for (int i = 0; i < 200; i++)
            {
                if (i % 2 == 0)
                {
                    _items.Add(new EvenItem(i) { Name = $"Item"});
                }
                else
                {
                    _items.Add(new UnEvenItem(i) { Name = $"Item"});
                }
            }
        }

    }
}
