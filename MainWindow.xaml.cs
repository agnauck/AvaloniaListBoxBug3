using System;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaListBoxBug
{
    public class MainWindow : Window
    {
        private readonly ListBox _messageList;
        private ScrollViewer scroll;
        private readonly TextBox _txtItem;
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            
            _messageList = this.FindControl<ListBox>("MessageList");
            _txtItem = this.FindControl<TextBox>("TxtItem");
            _messageList.TemplateApplied += MessageListOnTemplateApplied;
        }

        private void MessageListOnTemplateApplied(object? sender, TemplateAppliedEventArgs e)
        {
            scroll = e.NameScope.Find<ScrollViewer>("PART_ScrollViewer");
            scroll.ScrollChanged += ScrollOnScrollChanged;
        }
       
        private void ScrollOnScrollChanged(object? sender, ScrollChangedEventArgs e)
        {
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void BtnScrollToEnd_Click(object sender, RoutedEventArgs e)
        {
            ScrollToEnd();
        }
            
        private void BtnScrollToStart_Click(object sender, RoutedEventArgs e)
        {
            scroll.ScrollToHome();
        }

        private void ScrollToEnd()
        {
            // hack to scroll always down.
            // See also: https://github.com/AvaloniaUI/Avalonia/issues/14365
            Observable.FromEventPattern<EventHandler<ScrollChangedEventArgs>, ScrollChangedEventArgs>(
                    handler => scroll.ScrollChanged += handler,
                    handler => scroll.ScrollChanged -= handler)
                .Take(1)
                .Subscribe(_ =>
                    {
                        var isScrolledToEnd =
                            Math.Abs(scroll.Offset.Y - scroll.Extent.Height + scroll.Viewport.Height) == 0;
                        if (!isScrolledToEnd)
                        {
                            ScrollToEnd();
                        }
                    }
                );
            
            scroll.ScrollToEnd();
        }
        
        private void AddTop_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel =  DataContext as MainWindowViewModel;
            var lastItemBeforeInsert = viewModel.Items.First();
            
            Observable.FromEventPattern<EventHandler<ScrollChangedEventArgs>, ScrollChangedEventArgs>(
                    handler => scroll.ScrollChanged += handler,
                    handler => scroll.ScrollChanged -= handler)
                .Take(1)
                .Subscribe(_ =>
                    {
                        _messageList.ScrollIntoView(lastItemBeforeInsert);
                    }
                );
            
            viewModel.AddToStart();
        }
        
        private void AddTop2_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel =  DataContext as MainWindowViewModel;
            var lastItemBeforeInsert = viewModel.Items.First();
            
            Observable.FromEventPattern<EventHandler<ScrollChangedEventArgs>, ScrollChangedEventArgs>(
                    handler => scroll.ScrollChanged += handler,
                    handler => scroll.ScrollChanged -= handler)
                .Take(1)
                .Subscribe(_ =>
                    {
                        _messageList.ScrollIntoView(viewModel.Items.Count -1);
                        _messageList.ScrollIntoView(lastItemBeforeInsert);
                    }
                );
            
            viewModel.AddToStart();
        }

        private void AddEnd_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel =  DataContext as MainWindowViewModel;
            viewModel.AddToEnd();
        }
      
    }
}
