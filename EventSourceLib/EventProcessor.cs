using System.Collections.Generic;
using System.Linq;

namespace EventSourceLib
{
    public class EventProcessor : IEventProcessor
    {
        private IList<IEventProcessable> subscriberList;

        private static EventProcessor instance;

        private EventProcessor()
        {
            subscriberList = new List<IEventProcessable>();
        }

        public static EventProcessor GetInstance()
        {
            if (instance == null)
            {
                instance = new EventProcessor();
            }

            return instance;
        }

        public void Subscribe(IEventProcessable subscriber)
        {
            subscriberList.Add(subscriber);
        }

        public void Process(IEvent eventData, bool isReplay = false)
        {
            foreach (IEventProcessable subscriber in subscriberList.ToList())
            {
                subscriber.Process(eventData, isReplay);

                //if replay , we don't need to save it to event log
            }

        }

        public void Revert(IEvent eventData)
        {
            foreach (IEventProcessable subscriber in subscriberList.ToList())
            {
                subscriber.Revert(eventData);
            }
        }

        /// <summary>
        /// Reset all the state
        /// </summary>
        public void ResetState()
        {
            foreach (IEventProcessable subscriber in subscriberList.ToList())
            {
                IState state = subscriber as IState;

                if (state != null)
                {
                    state.Reset();
                }
            }
        }
    }
}
