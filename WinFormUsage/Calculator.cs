using EventSourceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormUsage
{
    public class Calculator : Aggregate
    {
        private double currentValue;

        public Calculator(long version = 0) : base(version)
        {
            currentValue = 0;

            Handle<CalcualtorCreatedEvent>(HandleCreatedEvent);
            Handle<AddValueEvent>(HandleEvent);
            HandleRevertEvent<AddValueEvent>(HandleRevertEvent);
        }

        public void Add(double value)
        {
            currentValue += value;

            TriggerValueChangeEvent();
        }

        public void Decrease(double value)
        {
            currentValue -= value;

            TriggerValueChangeEvent();
        }

        public override void Reset()
        {
            currentValue = 0;
        }

        private void HandleCreatedEvent(CalcualtorCreatedEvent eventData)
        {

        }

        private void HandleEvent(AddValueEvent eventData)
        {
            Add(eventData.Value);
        }

        private void HandleRevertEvent(AddValueEvent eventData)
        {
            Decrease(eventData.Value);
        }

        private void TriggerValueChangeEvent()
        {
            SendEvent(new NewValueEvent(currentValue));

        }
    }
}
