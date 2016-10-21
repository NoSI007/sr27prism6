using Microsoft.Practices.Unity;
using SR27Module.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace SR27Module.ViewModels
{
    public class ResultsViewModel
    {

        //private ILoggerFacade logger = null;
        private IsvcModel svcModel = null;
        //private IEventAggregator eventAggregator = null;

        public ObservableCollection<NutrVal> NUTR_VAL0 { get; private set; }
        public ICollectionView NUTR_VAL { get; private set; }

        public string Count { get; set; }

        private IUnityContainer container = null;


        public ResultsViewModel(IUnityContainer container)
        {
            this.container = container;

            //this.logger = this.container.Resolve<ILoggerFacade>();
            //this.eventAggregator = this.container.Resolve<IEventAggregator>();
       
            // resolve selected food by event

        }


        public ResultsViewModel()
        {

        }

        internal void Initialize(Infrastructure.eventData obj)
        {
            if(obj != null && obj.NDB_No > 0)
            {
                this.svcModel = this.container.Resolve<IsvcModel>();
                int ndbno = obj.NDB_No;
                NUTR_VAL0 = new ObservableCollection<NutrVal>(svcModel.getNutrientsIn(ndbno));
                
                this.Count = string.Format("{0} items for {1}.", NUTR_VAL0.Count,obj.food);
                NUTR_VAL = new ListCollectionView(NUTR_VAL0);
                NUTR_VAL.MoveCurrentTo(null);
                return;
            }

            // error message if we get here.

        }
    }
}
