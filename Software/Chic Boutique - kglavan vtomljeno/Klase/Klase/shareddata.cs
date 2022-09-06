using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace PI___INVENTura___kglavan_vtomljeno
{
    public static class shareddata
    {
        public static BindingList<itemmodel> Items { get; set; } = new BindingList<itemmodel>();
    }
}
