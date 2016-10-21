using SR27Module.ViewModels;
using System.Windows.Controls;

namespace SR27Module.Views
{
    /// <summary>
    /// Interaction logic for CommandsView.xaml
    /// </summary>
    public partial class CommandsView : UserControl
    {
        public CommandsView(CommandsViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }


        //public CommandsView( )
        //{
        //    InitializeComponent();
        //}
    }
}
