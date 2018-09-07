using EventSourceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormUsage
{
    [Serializable]
    public class NewValueEvent : IEvent
    {
        public double Value { get; private set; }

        public NewValueEvent(double value)
        {
            Value = value;
        }
    }
}
