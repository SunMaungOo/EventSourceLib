using System;
using System.Collections.Generic;

namespace EventSourceLib
{
    public abstract class Aggregate : IEventProcessable, IState
    {
        public long Version { get; private set; }

        private IDictionary<Type, Delegate> eventHandlers;

        private IDictionary<Type, Delegate> revertEventHandlers;

        private bool isReplayEvent;

        private IEventProcessor eventProcessor;

        private IControlGateway controlGateway;

        protected Aggregate(long version = 0):this(version,
            Framework.GetEventProcessor(),
            Framework.GetControlGateway())
        {

        }

        protected Aggregate(long version,IEventProcessor eventProcessor,
            IControlGateway controlGateway)
        {
            Version = version;

            eventHandlers = new Dictionary<Type, Delegate>();

            revertEventHandlers = new Dictionary<Type, Delegate>();

            this.eventProcessor = eventProcessor;
            this.eventProcessor.Subscribe(this);

            this.controlGateway = controlGateway;
        }

        protected void Handle<Event>(Action<Event> action)
            where Event : IEvent
        {
            this.eventHandlers[typeof(Event)] = action;
        }

        protected void HandleRevertEvent<Event>(Action<Event> action)
         where Event : IEvent
        {
            this.revertEventHandlers[typeof(Event)] = action;
        }

        public void Save(IEvent eventData)
        {
            controlGateway.Log(eventData);
        }

        public void Process(IEvent eventData, bool isReplayEvent)
        {
            SetIsReplayEvent(isReplayEvent);

            HandleEvent(eventData, false);

            //we don't need to set log for reply
            //event
            if (!IsReplayEvent())
            {
                Save(eventData);
            }
        }

        public void Revert(IEvent eventData)
        {
            //revert event is "Always not" replay event

            SetIsReplayEvent(false);

            //if we are reverting , we don't need to 
            //save it to event log

            HandleEvent(eventData, true);
        }

        private void HandleEvent(IEvent eventData, bool isRevertEvent = false)
        {
            Delegate handler;

            if (isRevertEvent)
            {
                revertEventHandlers.TryGetValue(eventData.GetType(), out handler);
            }
            else
            {
                eventHandlers.TryGetValue(eventData.GetType(), out handler);
            }

            if (handler == null)
            {
                return;
            }

            handler.DynamicInvoke(eventData);

        }

        private void SetIsReplayEvent(bool isReplayEvent)
        {
            this.isReplayEvent = isReplayEvent;
        }

        /// <summary>
        /// Return whether the current event is the replay event
        /// </summary>
        /// <returns></returns>
        protected bool IsReplayEvent()
        {
            return isReplayEvent;
        }

        public abstract void Reset();


    }
}
