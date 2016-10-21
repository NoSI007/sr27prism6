using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SR27Module.Services;

namespace SR27Module
{
    /// <summary>
    /// 1.1. inherit from IModule
    /// 1.2. Resolve reference
    /// 1.3. implement interface.
    /// </summary>
    public class SR27Module :IModule
    {

           private IUnityContainer container = null;
           private CommandsController commands = null;
           private ResultsController resutls = null;
           //private ListController list;
    //public CustomersModule( container)
        /// <summary>
        /// 4.1 Add region manager to get reference for region manager registered by Unity.
        /// </summary>
        //private readonly IRegionManager regionManger;
        /// <summary>
        /// 4.2  get the region manager from unit bu DI;
        /// </summary>
        /// <param name="rm"></param>
        public SR27Module(IUnityContainer co)
        {
            //regionManger = rm;
            this.container = co;
            Initialize();
        }
                    
        public void Initialize()
        {

            container.RegisterType(typeof(IsvcModel), typeof(svcModel));
            /// Creating a controller for the module
            /// We are using the IOC container to get us a new SR27Controller.
            this.commands = this.container.Resolve<CommandsController>();
            this.resutls = this.container.Resolve<ResultsController>();
            //    this.list = this.container.Resolve<ListController>();
            var regionManager = this.container.Resolve<IRegionManager>();
            //4.2 add the views to the regions.
            // if we have too many views in the region the First View is displyed.
             regionManager.RegisterViewWithRegion("Commands", typeof(Views.CommandsView));
             regionManager.RegisterViewWithRegion("Results", typeof(Views.ResultsView));
             regionManager.RegisterViewWithRegion("Results", typeof(Views.ListView));

            
       
        }
    }
}
