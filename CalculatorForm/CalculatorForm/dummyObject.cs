using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorForm
{
    class dummyObject
    {
        public double dummyValue;
        public string name;

        public dummyObject(double v, string name)
        {
            this.dummyValue = v;
            this.name = name;
        }

        public override string ToString()
        {
            return this.name + "    " + this.dummyValue;
        }

    }
}
