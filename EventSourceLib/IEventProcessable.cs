namespace EventSourceLib
{
    public interface IEventProcessable
    {
        /// <summary>
        /// Process the event.
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="isReplayEvent">whether it a reply event</param>
        void Process(IEvent eventData, bool isReplayEvent);

        /// <summary>
        /// Revert the event
        /// </summary>
        /// <param name="eventData"></param>
        void Revert(IEvent eventData);
    }
}
