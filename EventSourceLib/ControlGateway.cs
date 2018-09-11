using System.Collections.Generic;
using System.Linq;

namespace EventSourceLib
{
    public sealed class ControlGateway : IControlGateway
    {
        private ICollection<IEvent> eventList;

        private bool isLogEnabled;

        private static ControlGateway instance;

        private ControlGateway()
        {
            eventList = new List<IEvent>();

            this.isLogEnabled = true;

        }

        public static ControlGateway GetInstance()
        {
            if (instance == null)
            {
                instance = new ControlGateway();
            }

            return instance;
        }

        public void Log(IEvent eventData)
        {
            //we should not log when in reply mode

            if (IsLogEnabled())
            {
                eventList.Add(eventData);
            }
        }

        public IEvent GetEvent(int index)
        {
            return eventList.ElementAtOrDefault(index);
        }

        public int GetEventCount()
        {
            return eventList.Count;
        }

        public void ReplaceEvents(IEvent[] events)
        {
            eventList.Clear();
            
            foreach(IEvent currentEvent in events)
            {
                eventList.Add(currentEvent);
            }
        }

        public void SetIsLogEnabled(bool isLogEnabled)
        {
            this.isLogEnabled = isLogEnabled;
        }

        public bool IsLogEnabled()
        {
            return isLogEnabled;
        }

    }
}
