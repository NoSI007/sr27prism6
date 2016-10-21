using SR27Module.ViewModels;
using System.Windows.Controls;

namespace SR27Module.Views
{
    /// <summary>
    /// Interaction logic for ResultsView.xaml
    /// </summary>
    public partial class ResultsView : UserControl
    {
        public ResultsView(ResultsViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
