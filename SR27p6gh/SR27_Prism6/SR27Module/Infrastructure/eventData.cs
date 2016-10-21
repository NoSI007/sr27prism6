using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR27Module.Infrastructure
{
    public  class eventData
    {
        public short FdGrp_CD { get; set; }
        public int NDB_No     { get; set; }
        public short Nutr_No  { get; set; }
        public string keyword { get; set; }
        public string food    { get; set; }
        public string NutDes  { get; set;  }
        public eventData()
        {
              
        }
    }

	public  class conn
	{
		public static string CString()
		{
			return ConfigurationManager.ConnectionStrings["sr27fEntities"].ToString();
		}
	}
}
