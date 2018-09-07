namespace EventSourceLib
{
    /// <summary>
    /// The main class which the application
    /// will used to pass event
    /// </summary>
    public interface IEventProcessor
    {
        /// <summary>
        /// Subscribe the event listener
        /// </summary>
        /// <param name="subscriber"></param>
        void Subscribe(IEventProcessable subscriber);

        /// <summary>
        /// Process the event
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="isReplay">whether we are processing the event
        /// because we replaying</param>
        void Process(IEvent eventData, bool isReplay);

        /// <summary>
        /// Revert the event it the parameter
        /// </summary>
        /// <param name="eventData"></param>
        void Revert(IEvent eventData);

        /// <summary>
        /// Reset all the state of the subsribers
        /// </summary>
        void ResetState();
    }
}
