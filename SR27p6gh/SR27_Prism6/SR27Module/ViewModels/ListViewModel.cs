using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using SR27Module.Infrastructure;
using SR27Module.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace SR27Module.ViewModels
{
    public class ListViewModel : INotifyPropertyChanged
    {

        public ICommand DoFilterCommand { get; private set; }
        public string FilterStr { get; set; }

        public ObservableCollection<CompList> FOODLIST0 { get; private set; }
        public ICollectionView FOODLIST { get; private set; }

        public string Count { get; set; }

        private IUnityContainer container = null;
        //private ILoggerFacade logger = null;
        private IsvcModel svcModel = null;
        //private IEventAggregator eventAggregator = null;

        public ListViewModel(IUnityContainer co)
        {
            this.container = co;
            this.DoFilterCommand = new DelegateCommand(Filter_Click);
           
        }

        private void FOODLIST_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (FOODLIST == null)
                return;
            ListCollectionView temp = (ListCollectionView)FOODLIST;
            // this.Count = string.Format("{0} items for {1}.", FOODLIST0.Count, obj.NutDes);
            // Hack: need standards..
            this.Count = string.Format("{0} Items found for {1}", temp.Count,Nutr_desc);
            this.NotifyPropertyChanged("Count");
        }
        private string Nutr_desc = null;

        internal void Initialize(eventData obj)
        {
            if (obj != null && obj.Nutr_No > 0)
            {
                Nutr_desc = obj.NutDes;
                this.svcModel = this.container.Resolve<IsvcModel>();
                short nno = obj.Nutr_No;
                FOODLIST0 = new ObservableCollection<CompList>(svcModel.getList4(nno));

                this.Count = string.Format("{0} items for {1}.", FOODLIST0.Count, Nutr_desc);
                FOODLIST = new ListCollectionView(FOODLIST0);
                FOODLIST.MoveCurrentTo(null);
                this.FOODLIST.CollectionChanged += FOODLIST_CollectionChanged;
                return;
            }

            // error message if we get here.


        }

        /// <summary>
        /// Filtering required.
        /// </summary>
        private void Filter_Click()
        {
            if (FOODLIST == null)
                return;

            if (string.IsNullOrWhiteSpace(this.FilterStr) == false)
            {
                //_food_des = new ObservableCollection<FOOD_DES>(svcModel.getbyKeyword(this.FilterStr));
                //setFoodsList();
                FOODLIST.Filter = new Predicate<object>(FilterbyKWpredicate);
            }
            else
            {
                FOODLIST.Filter = null;
            }


        }

        private bool FilterbyKWpredicate(object sender)
        {
            CompList it = sender as CompList;
            if (it != null)
            {
                if (it.Food.Contains(this.FilterStr) == true)
                {
                    return true;
                }

            }
            return false;
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
