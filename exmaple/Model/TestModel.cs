using NakedORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace exmaple.Model
{
    [Table("TestTable")]
    public class TestModel
    {
        [Key]
        [PagerKey]
        public Int64? test1 { get; set; }

        public Int32? test2 { get; set; }

        public String test3 { get; set; }

        public Decimal? test4 { get; set; }

        public DateTime? test5 { get; set; }

        public Single? test6 { get; set; }
    }
}
