using EventSourceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormUsage
{
    public class CalculatorFactory : AbstractFactory
    {
        private static CalculatorFactory instance;

        private CalculatorFactory() : base()
        {
        }

        public static CalculatorFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new CalculatorFactory();
            }

            return instance;
        }

        protected override void HandleEvent(IEvent eventData)
        {
            if (eventData is CalcualtorCreatedEvent)
            {
                InternalCreateCalculator();

                //CreateCalculator();
            }
        }

        public Calculator CreateCalculator()
        {
            Calculator calculator = new Calculator();
            calculator.Process(new CalcualtorCreatedEvent(), false);

            return calculator;
        }

        private void InternalCreateCalculator()
        {
            Calculator calculator = new Calculator();
        }



    }
}
