using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SR27Module.Events;
using SR27Module.Infrastructure;
using SR27Module.ViewModels;
using SR27Module.Views;

namespace SR27Module
{
    public class ResultsController
    {
        private IUnityContainer container = null;
        //private ILoggerFacade logger = null;
        private IRegionManager regionManager = null;
        private IEventAggregator eventAggregator = null;

        public ResultsController(IUnityContainer container)
        {
            this.container = container;
            //this.logger = this.container.Resolve<ILoggerFacade>();
            this.eventAggregator = this.container.Resolve<IEventAggregator>();
            this.regionManager = this.container.Resolve<IRegionManager>();
          
            /// ***  The controller remains active because event subscriptions(see keepSubscriberReferenceAlive) - but that is it's reason for existence. ***
            eventAggregator.GetEvent<ShowResultEvent>().Subscribe(SelectView, true);

        }

        private void SelectView(eventData obj)
        {
            if (obj.Nutr_No > 0)
                ShowList(obj);
            else
                ShowResult(obj);
        }

        private void ShowList(eventData obj)
        {
            IRegion region = getRegion4View();

            var view = this.container.Resolve<ListView>();

            // we cannot use RequestNavigate() 
            // DataContext must be set.
            region.Add(view, typeof(ListView).Name);
            view.DataContext = getLvm(obj); ;
            region.Activate(view);
        }

        private ListViewModel getLvm(eventData obj)
        {
            var viewModel = this.container.Resolve<ListViewModel>();
            viewModel.Initialize(obj);
            return viewModel;
        }


        // Using View Injection to show the ResultsView.
        private void ShowResult(eventData obj)
        {
            IRegion region = getRegion4View();

        //ResultsViewModel viewModel = getVm(obj);

            var view = this.container.Resolve<ResultsView>();

            // we cannot use RequestNavigate() 
            // DataContext must be set.
            region.Add(view, typeof(ResultsView).Name);
            view.DataContext = getVm(obj);
            region.Activate(view);
        }

        private ResultsViewModel getVm(eventData obj)
        {
            var viewModel = this.container.Resolve<ResultsViewModel>();
            viewModel.Initialize(obj);
            return viewModel;
        }

        private IRegion getRegion4View()
        {
            var region = this.regionManager.Regions["Results"];
            foreach (var v in region.Views)
            {
                region.Remove(v);
            }

            return region;
        }

    }





}
