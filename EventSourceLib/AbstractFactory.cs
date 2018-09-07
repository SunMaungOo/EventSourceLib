namespace EventSourceLib
{
    public abstract class AbstractFactory : IEventProcessable
    {
        protected AbstractFactory():this(Framework.GetEventProcessor())
        {

        }

        protected AbstractFactory(IEventProcessor eventProcessor)
        {
            eventProcessor.Subscribe(this);
        }

        public void Process(IEvent eventData, bool isReplayEvent)
        {
            HandleEvent(eventData);
        }

        public void Revert(IEvent eventData)
        {
            //no-op
        }

        protected abstract void HandleEvent(IEvent eventData);

    }
}
