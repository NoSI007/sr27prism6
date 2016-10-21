using SR27Module.ViewModels;
using System.Windows.Controls;

namespace SR27Module.Views
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : UserControl
    {
        public ListView(ListViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
