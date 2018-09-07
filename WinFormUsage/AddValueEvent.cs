using EventSourceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormUsage
{
    [Serializable]
    public class AddValueEvent : IEvent
    {
        public double Value { get; private set; }

        public AddValueEvent(double value)
        {
            Value = value;
        }
    }
}
