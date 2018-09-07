namespace EventSourceLib
{
    public static class Framework
    {
        public static IControlGateway GetControlGateway()
        {
            return ControlGateway.GetInstance();
        }

        public static IEventProcessor GetEventProcessor()
        {
            return EventProcessor.GetInstance();
        }

        public static IEventSaver GetEventSaver()
        {
            return new SerializeEventSaver();
        }
    }
}
