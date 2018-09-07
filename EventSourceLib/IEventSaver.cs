namespace EventSourceLib
{
    public interface IEventSaver
    {
        /// <summary>
        /// Save the events
        /// </summary>
        /// <returns></returns>
        bool SaveEvents();

        /// <summary>
        /// Load the events
        /// </summary>
        /// <returns>Empty array on error</returns>
        IEvent[] LoadEvents();
    }
}
