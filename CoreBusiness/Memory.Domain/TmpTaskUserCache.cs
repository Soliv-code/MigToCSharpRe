using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Domain
{
    public class TmpTaskUserCache
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public byte TaskListCategoryID { get; set; }
    }
}
