namespace EventSourceLib
{
    public sealed class RevertManager
    {
        private readonly IControlGateway controlGateway;

        private readonly IEventProcessor eventProcessor;

        private int revertIndex;

        public RevertManager(IControlGateway controlGateway,IEventProcessor eventProcessor)
        {
            this.controlGateway = controlGateway;

            this.eventProcessor = eventProcessor;

            revertIndex = 0;

        }

        public RevertManager():this(Framework.GetControlGateway(),Framework.GetEventProcessor())
        {

        }

        /// <summary>
        /// Revert one event
        /// </summary>
        public void Revert()
        {
            ++revertIndex;

            int eventCount = controlGateway.GetEventCount();

            int currentIndex = CalculateIndex(eventCount);

            if (!IsRevertable(currentIndex))
            {
                return;
            }

            IEvent revertEvent = controlGateway.GetEvent(currentIndex);

            if (revertEvent == null)
            {
                --revertIndex;

                return;
            }

            //when we are reverting , we don't need to log that event
            //therefore we disable it
            controlGateway.SetIsLogEnabled(false);

            eventProcessor.Revert(revertEvent);

            //after reverting the event
            //we should able the event logging feature againg
            controlGateway.SetIsLogEnabled(true);
        }

        /// <summary>
        /// Calculate the current event index
        /// </summary>
        /// <param name="eventCount">Total event count</param>
        /// <returns></returns>
        private int CalculateIndex(int eventCount)
        {
            return eventCount - revertIndex;
        }

        /// <summary>
        /// Check whether there is still event which we can revert
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool IsRevertable(int index)
        {
            return (index >= 0) && (controlGateway.GetEventCount() > index);
        }
    }
}
