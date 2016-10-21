using SR27Module.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SR27Module.Services
{
    public class svcModel : IsvcModel
    {
        private sr27DataContext db = new sr27DataContext();
        public svcModel()
        {
          
        }

        public Collection<FD_GROUP> getGroups()
        {
            var res = db.FD_GROUPs.ToList<FD_GROUP>();
            Collection<FD_GROUP> tmp = new Collection<FD_GROUP>(res);
            return tmp;
        }

        public Collection<FOOD_DES> getFoodsIn(short gcd)
        {
            var res = db.FOOD_DES.Where(f => f.FdGrp_Cd == gcd).ToList<FOOD_DES>();
            Collection<FOOD_DES> tmp = new Collection<FOOD_DES>(res);
            return tmp;
        }

        //public Collection<FOOD_DES> getbyKeyword(string kw)
        //{
        //    var res = db.FOOD_DES.Where(f => f.Long_Desc.Contains(kw) == true).ToList<FOOD_DES>();
        //    Collection<FOOD_DES> tmp = new Collection<FOOD_DES>(res);
        //    return tmp;
        //}

        public Collection<NUTR_DEF> getNutr_Defs()
        {
            var res = db.NUTR_DEFs.ToList<NUTR_DEF>();
            Collection<NUTR_DEF> tmp = new Collection<NUTR_DEF>(res);
            return tmp;
        }

        public NUTR_DEF getNutr_Def(short nno)
        {
            var res = db.NUTR_DEFs.Where(n=>n.Nutr_No == nno).FirstOrDefault();
            return res;
        }

        public Collection<NutrVal> getNutrientsIn(int ndb)
        {
            var res = db.NUT_DATAs.Where(d => d.NDB_No == ndb).ToList<NUT_DATA>();
            var res0 = (from d in res
                        join n in db.NUTR_DEFs on
                        d.Nutr_No equals n.Nutr_No
                        select new NutrVal { Nutr_No = d.Nutr_No, Nutrient = n.NutrDesc, Units = n.Units, Value = d.Nutr_Val }).ToList<NutrVal>();

            Collection<NutrVal> tmp = new Collection<NutrVal>(res0);
            return tmp;
        }

        public Collection<CompList> getList4(short nno)
        {
            var res = db.NUT_DATAs.Where(n => n.Nutr_No == nno).ToList<NUT_DATA>();
            var res0 = (from f in res
                       join f0 in db.FOOD_DES
                       on
                       f.NDB_No equals f0.NDB_No
                       orderby f.Nutr_Val descending
                       where f.Nutr_Val > 0.0
                       select new CompList { Food = f0.Long_Desc, Value = f.Nutr_Val }).ToList<CompList>();

            Collection<CompList> tmp = new Collection<CompList>(res0);
            return tmp;
        }

        //public Collection<CompList> getList4f(short nno, string fil)
        //{
        //    var fl = (from f in db.FOOD_DES where f.Long_Desc.Contains(fil) == true select f).ToList<FOOD_DES>();
        //    var flres = fl.Distinct();
        //    var res = db.NUT_DATAs.Where(n => n.Nutr_No == nno).ToList<NUT_DATA>();

        //    var fres = (from r in res
        //                join
        //                f in flres
        //                on
        //                r.Nutr_No equals f.NDB_No
        //                orderby r.Nutr_Val descending
        //                select new CompList { Food = f.Long_Desc, Value = r.Nutr_Val }).ToList<CompList>();

        //    Collection<CompList> tmp = new Collection<CompList>(fres);
        //    return tmp;
        //}
    }


    public class Groupdam
    {
        private static sr27DataContext db = null;
        static Groupdam()
        {
            db = new sr27DataContext();
        }

        public static List<FD_GROUP> getGroups()
        {
            return db.FD_GROUPs.ToList<FD_GROUP>();
        }
    }


    public class Foodsdam
    {
        private static sr27DataContext db = null;
        static Foodsdam()
        {
            db = new sr27DataContext();
        }

        public static List<FOOD_DES> getGroups(short fgcd)
        {
            var res = from f in db.FOOD_DES where f.FdGrp_Cd == fgcd select f;
            return res.ToList<FOOD_DES>();
        }


        public static List<FOOD_DES> getGroups(string kw)
        {
            var res = from f in db.FOOD_DES where f.Long_Desc.Contains(kw)== true  select f;
            return res.ToList<FOOD_DES>();
        }
    }
}
