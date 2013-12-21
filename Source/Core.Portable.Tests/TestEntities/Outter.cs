using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoC.Tests.TestEntities
{
    class Outter
    {
        public Inner InnerProperty { get; set; }
        public Inner InnerField;

        public Outter()
        {
            InnerProperty = new Inner();
        }
    }
}
