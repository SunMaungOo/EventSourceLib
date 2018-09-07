namespace EventSourceLib
{
    public interface IControlGateway
    {
        /// <summary>
        /// Log/Save the event.
        /// We should not log/save the event
        /// if IsLogEnabled() return false
        /// </summary>
        /// <param name="eventData"></param>
        void Log(IEvent eventData);

        /// <summary>
        /// Get the event at the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Event at the index or null on error</returns>
        IEvent GetEvent(int index);

        /// <summary>
        /// How many event is stored
        /// </summary>
        /// <returns></returns>
        int GetEventCount();

        /// <summary>
        /// Replace all the event with 
        /// the event in the paramater.
        /// </summary>
        /// <param name="events">Should not be null array.
        /// Use empty array to represent just
        /// deleting array.
        /// The events should not be null</param>
        void ReplaceEvents(IEvent[] events);

        /// <summary>
        /// Set the logging feature
        /// </summary>
        /// <param name="isLogEnabled"></param>
        void SetIsLogEnabled(bool isLogEnabled);

        /// <summary>
        /// Whether the logging is enabled
        /// </summary>
        /// <returns></returns>
        bool IsLogEnabled();
    }
}
