using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace EventSourceLib
{
    public class SerializeEventSaver : IEventSaver
    {
        private readonly string fileName;

        private readonly IControlGateway controlGateway;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">file name to save the serialize event</param>
        /// <param name="controlGateway"></param>
        public SerializeEventSaver(string fileName,IControlGateway controlGateway)
        {
            this.fileName = fileName;

            this.controlGateway = controlGateway;
        }

        public SerializeEventSaver(string fileName = "events.txt"):this(fileName,
            Framework.GetControlGateway())
        {

        }

        public SerializeEventSaver():this("events.txt", 
            Framework.GetControlGateway())
        {

        }

    
        public IEvent[] LoadEvents()
        {
            IEvent[] results = new IEvent[] { };

            if (!System.IO.File.Exists(fileName))
            {
                return results;
            }

            IList<IEvent> eventList = null;

            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                eventList = binaryFormatter.Deserialize(stream) as IList<IEvent>;
            }

            if(eventList!=null)
            {
                results = eventList.ToArray();
            }

            return results;
        }

        /// <summary>
        /// Save the event to the file.
        /// If the file name already exist, overwrite the file
        /// </summary>
        public bool SaveEvents()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, GetEvents());
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        private IList<IEvent> GetEvents()
        {
            IList<IEvent> eventList = new List<IEvent>();

            for (int i = 0; i < controlGateway.GetEventCount(); i++)
            {
                eventList.Add(controlGateway.GetEvent(i));
            }

            return eventList;
        }
    }
}
