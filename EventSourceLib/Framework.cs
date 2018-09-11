namespace EventSourceLib
{
    public static class Framework
    {
        private static IControlGateway controlGateway=ControlGateway.GetInstance();

        private static IEventProcessor eventProcessor = EventProcessor.GetInstance();

        private static IEventSaver eventSaver;

        private static bool isEventSaverInjected = false;

        public static IControlGateway GetControlGateway()
        {
            return controlGateway;
        }

        public static IEventProcessor GetEventProcessor()
        {
            return eventProcessor;
        }

        public static IEventSaver GetEventSaver()
        {
            if(!isEventSaverInjected)
            {
                eventSaver = new SerializeEventSaver();
            }

            return eventSaver;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlGateway">Non null object</param>
        public static void InjectControlGateway(IControlGateway controlGateway)
        {
            Framework.controlGateway = controlGateway;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventProcessor">Non null object</param>
        public static void InjectEventProcessor(IEventProcessor eventProcessor)
        {
            Framework.eventProcessor = eventProcessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventSaver">Non null object</param>
        public static void InjectEventSaver(IEventSaver eventSaver)
        {
            Framework.eventSaver = eventSaver;
        }
    }
}
