using EventSourceLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUsage
{
    public partial class Form1 : Form, IEventProcessable
    {
        private IEventSaver eventSaver;

        private IEventProcessor eventProcessor;

        private RevertManager revertManager;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            eventProcessor = Framework.GetEventProcessor();
            eventProcessor.Subscribe(this);

            eventSaver = Framework.GetEventSaver();

            revertManager = new RevertManager();

            CalculatorFactory.GetInstance().CreateCalculator();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int value = int.Parse(txtValue.Text);

            eventProcessor.Process(new AddValueEvent(value),false);
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            revertManager.Revert();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            eventSaver.SaveEvents();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ReplayManager.Replay();
        }

        public void Process(IEvent eventData, bool isReplayEvent)
        {
            if (eventData is NewValueEvent)
            {
                label1.Text = ((NewValueEvent)eventData).Value.ToString();
            }
        }

        public void Revert(IEvent eventData)
        {
            //no-op for now
        }
    }
}
