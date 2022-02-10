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
        public string SiteProjectName { get; set; }
        public string SiteProjectDescription { get; set; }
        public string SiteProjectLocation { get; set; }
        public int BranchId { get; set; }
        public List<Excel> excel { get; set; }
    }
    public class AddExcel
    {
        public int SiteProjectId { get; set; }
        public List<Excel> excel { get; set; }
    }

    public class CustomRow
    {
        public string Row { get; set; }
        public List<CustomBlock> blocks { get; set; }
        public List<int> Indexes { get; set; }  
    }
    public class CustomBlock
    {
        public string block { get; set; }
        public List<CustomVilla> villas{ get; set; }
    }
    public class CustomVilla
    {
        public string villa { get; set; }
        public string workScopes { get; set; }
    }

    //public class workScope
    //{
    //    public string workscope { get; set; }
    //}

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
