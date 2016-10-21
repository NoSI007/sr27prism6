using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SR27Module.ViewModels;
using SR27Module.Views;

namespace SR27Module
{
    public class CommandsController
    {
        private IUnityContainer container = null;
        //private ILoggerFacade logger = null;
        private IRegionManager regionManager = null;
        //private IEventAggregator eventAggregator = null;

        public CommandsController(IUnityContainer container)
        {
            this.container = container;
            //this.logger = this.container.Resolve<ILoggerFacade>();
            //this.eventAggregator = this.container.Resolve<IEventAggregator>();
            this.regionManager = this.container.Resolve<IRegionManager>();
            ShowCommands();
            /// ***  The controller remains active because event subscriptions(see keepSubscriberReferenceAlive) - but that is it's reason for existence. ***
            //eventAggregator.GetEvent<ShowCustomerEvent>().Subscribe(ShowCustomer, true);
            //eventAggregator.GetEvent<ShowCustomerEvent>().Subscribe(ShowCustomer, true);
        }

        // Using View Injection to show the CommandsView.
        private void ShowCommands()
        {
            IRegion region = getregion4View();
            var viewModel = this.container.Resolve<CommandsViewModel>();
        
            var view = this.container.Resolve<CommandsView>();

            region.Add(view, typeof(CommandsView).Name);
            view.DataContext = viewModel;
            region.Activate(view);
        }

        private IRegion getregion4View()
        {
            var region = this.regionManager.Regions["Commands"];
            foreach (var v in region.Views)
            {
                region.Remove(v);
            }

            return region;
        }
    }





}
