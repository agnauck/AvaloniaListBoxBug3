using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace AvaloniaListBoxBug
{
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Markup.Xaml;

    public class MainWindow : Window
    {
        private readonly ListBox _messageList;
        private ScrollViewer scroll;
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            
            _messageList = this.FindControl<ListBox>("MessageList");
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
           scroll.ScrollToEnd();
        }
    }
}
