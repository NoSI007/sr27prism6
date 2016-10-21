using SR27Module.Model;
using System.Collections.ObjectModel;

namespace SR27Module.Services
{
    public interface IsvcModel
    {
         Collection<FD_GROUP> getGroups();

         Collection<FOOD_DES> getFoodsIn(short gcd);
         //Collection<FOOD_DES> getbyKeyword(string kw);

         Collection<NUTR_DEF> getNutr_Defs();
         NUTR_DEF getNutr_Def(short nno);

         Collection<NutrVal> getNutrientsIn(int ndb);
         Collection<CompList> getList4(short nno);
         //Collection<CompList> getList4f(short nno, string fil);//filter by keyword
      
    }


    public class NutrVal
    {
        public NutrVal()
        {

        }
        private float? _val;

        public float? Value
        {
            get { return _val; }
            set { _val = value; }
        }
        private short _nutr_no;

        public short Nutr_No
        {
            get { return _nutr_no; }
            set { _nutr_no = value; }

        }

        private string _units;

        public string Units
        {
            get { return _units; }
            set { _units = value; }
        }

        public string Nutrient { get; set; }

        public string FootNote { get; set; }
    }

    public class CompList
    {
        public CompList()
        {

        }
        public float? Value { get; set; }
        public string Food { get; set; }
    }
}
