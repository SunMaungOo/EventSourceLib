namespace EventSourceLib
{
    /// <summary>
    /// Class which replay all events
    /// 
    /// </summary>
    public static class ReplayManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventSaver">Where the event which will be replay are loaded</param>
        /// <param name="eventProcessor">The processor which trigger the event throughout
        /// the whole application</param>
        /// <param name="gateway"></param>
        public static void Replay(IEventSaver eventSaver,
            IEventProcessor eventProcessor,IControlGateway gateway)
        {
            IEvent[] eventList=eventSaver.LoadEvents();

            //replace with the event list from the file

            gateway.ReplaceEvents(eventList);

            //Reset all the state
            //We must reset all state before we reply the event because
            //we don't want them the state to be appended to each other
            eventProcessor.ResetState();

            foreach (IEvent eachEvent in eventList)
            {
                eventProcessor.Process(eachEvent, true);
            }
        }

        public static void Replay(IEventSaver eventSaver, IEventProcessor eventProcessor)
        {
            Replay(eventSaver, eventProcessor, Framework.GetControlGateway());
        }

        public static void Replay()
        {
            Replay(Framework.GetEventSaver(),
                Framework.GetEventProcessor());
        }

    }
}
