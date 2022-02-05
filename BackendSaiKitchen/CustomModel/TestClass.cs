using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class TestClass
    {
        public string RowName { get; set; }
        public List<Row> rows { get; set; }
    }

    public class Block
    {
        public List<string> Villas { get; set; }
    }

    public class Row
    {
        public List<Block> Block { get; set; }
    }
}
