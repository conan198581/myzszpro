using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.Common
{
    public class AjaxResult<T>
    {
        public string Status { get; set; }
        public string ErrorMsg { get; set; }
        public T Data { get; set; }
    }
}
