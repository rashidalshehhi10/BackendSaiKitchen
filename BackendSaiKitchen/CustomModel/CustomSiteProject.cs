using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{

    public class Excel
    {
        public List<string> Row { get; set; }
        public List<string> Block { get; set; }
        public List<string> Villa { get; set; }
        public List<string> Workscope { get; set; }
    }

    public class Root
    {
        public List<Excel> excel { get; set; }
    }

    public class row
    {
        public string Row { get; set; }
        public List<Block> blocks { get; set; }
    }
    public class Block
    {
        public string block { get; set; }
        public List<Villa> Villas { get; set; }
    }
    public class Villa
    {
        public string villa { get; set; }
        public List<workScope> workScopes { get; set; }
    }

    public class workScope
    {
        public string workscope { get; set; }
    }

    //public class CustomSiteProject
    //{
    //    public Row row { get; set; }
    //    public Block Block { get; set; }
    //    public Villa Villa { get; set; }
    //    public workScope WorkScope { get; set; }
    //}

    //public class Excel
    //{
    //    public List<CustomSiteProject> excel { get; set; }
    //}

    //public class Row
    //{
    //    public List<string> row { get; set; }
    //}


}
