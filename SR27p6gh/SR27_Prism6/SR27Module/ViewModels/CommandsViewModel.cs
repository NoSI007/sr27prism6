using System;
using System.Collections.ObjectModel;
using SR27Module.Services;
using SR27Module.Model;
using Microsoft.Practices.Unity;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.Specialized;
using SR27Module.Infrastructure;
using Microsoft.Practices.Prism.PubSubEvents;
using SR27Module.Events;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
namespace SR27Module.ViewModels
{
    public class CommandsViewModel : INotifyPropertyChanged
    {
        #region Properties        
        public string FilterStr { get; set; }
        public string FOOD_DES_HDR { get; set; }
        public ICommand DoFilterCommand { get; private set; }
        public ICommand LIST { get; private set; }

        public ICollectionView FD_GROUP   { get; private set; }
        public ICollectionView FOOD_DES   { get; private set; }
        public ICollectionView NUTR_DEF   { get; private set; }

      
        #endregion

        #region privateProp
        protected ObservableCollection<FD_GROUP> _fd_group { get; private set; }
        protected ObservableCollection<FOOD_DES> _food_des { get; private set; }
        protected ObservableCollection<NUTR_DEF> _nutr_def { get; private set; }
        #endregion

        #region Fields
        //private ILoggerFacade logger = null;
        private IUnityContainer container = null;
        private IsvcModel svcModel = null;
        private IEventAggregator eventAggregator = null;

        private eventData edata = new eventData();
        #endregion

        public CommandsViewModel(IUnityContainer container)
        {
            this.container = container;
           
            this.DoFilterCommand = new DelegateCommand(Filter_Click);
            this.LIST = new DelegateCommand(NUTR_DEF_CurrentChanged);
            //this.logger = this.container.Resolve<ILoggerFacade>();
            this.eventAggregator = this.container.Resolve<IEventAggregator>();

            this.svcModel = this.container.Resolve<IsvcModel>();
            _fd_group = new ObservableCollection<FD_GROUP>(svcModel.getGroups());
            _nutr_def = new ObservableCollection<NUTR_DEF>(svcModel.getNutr_Defs());

            this.NUTR_DEF = new ListCollectionView(_nutr_def);
            this.FD_GROUP = new ListCollectionView(_fd_group);

            this.FD_GROUP.CurrentChanged += FD_GROUP_CurrentChanged;

            
        }

        /// <summary>
        /// Filtering required.
        /// </summary>
        private void Filter_Click()
        {
            if (FOOD_DES == null)
                return;

            if (string.IsNullOrWhiteSpace(this.FilterStr) == false)
            {
                //_food_des = new ObservableCollection<FOOD_DES>(svcModel.getbyKeyword(this.FilterStr));
                //setFoodsList();
                FOOD_DES.Filter = new Predicate<object>(FilterbyKWpredicate);
            }
            else
            {
                FOOD_DES.Filter = null;
            }
            

        }


        /// <summary>
        /// new NUTR_DEF selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NUTR_DEF_CurrentChanged()
        {
            NUTR_DEF selected = this.NUTR_DEF.CurrentItem as NUTR_DEF;
            if (selected != null)
            {
                // Publish the ShowResultEvent event.
                edata.Nutr_No = selected.Nutr_No;
                edata.NDB_No = 0;
                edata.food = "";
                edata.NutDes = selected.NutrDesc;
                this.eventAggregator.GetEvent<ShowResultEvent>().Publish(this.edata);
            }

        }

        private bool FilterbyKWpredicate(object sender)
        {
            FOOD_DES it = sender as FOOD_DES;
            if( it != null)
            {
                if (it.Long_Desc.Contains(this.FilterStr) == true)
                {
                    return true;
                }

            }
            return false;
        }
        /// <summary>
        /// New group selction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FD_GROUP_CurrentChanged(object sender, EventArgs e)
        {
            var cg = this.FD_GROUP.CurrentItem as FD_GROUP;

            if(_food_des != null)
                _food_des.Clear();

            _food_des = new ObservableCollection<FOOD_DES>(svcModel.getFoodsIn(cg.FdGrp_CD));

            setFoodsList();

        }

        private void setFoodsList()
        {
            this.FOOD_DES = new ListCollectionView(_food_des);
           
            this.FOOD_DES.MoveCurrentTo(null);
            this.NotifyPropertyChanged("FOOD_DES");
            this.FOOD_DES.CurrentChanged += FOOD_DES_CurrentChanged;
            this.FOOD_DES.CollectionChanged += FOOD_DES_CollectionChanged;

            
        }

        void FOOD_DES_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (FOOD_DES == null)
                return;
            ListCollectionView temp = (ListCollectionView)FOOD_DES;
            this.FOOD_DES_HDR = string.Format("{0} items Found.", temp.Count);
            this.NotifyPropertyChanged("FOOD_DES_HDR");
        }
        /// <summary>
        /// new food selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FOOD_DES_CurrentChanged(object sender, EventArgs e)
        {
            FOOD_DES selected = this.FOOD_DES.CurrentItem as FOOD_DES;
            if (selected != null)
            {
                // Publish the EmployeeSelectedEvent event.
                edata.NDB_No = selected.NDB_No;
                edata.food = selected.Long_Desc;
                edata.Nutr_No = 0;

                this.eventAggregator.GetEvent<ShowResultEvent>().Publish(this.edata);
            }
        }




        #region NotifyPropertyChange
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
        #endregion

        
    }
}
