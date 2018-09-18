using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace EventSourceLib
{
    /// <summary>
    /// Class which store the serialize event object
    /// </summary>
    public sealed class SerializeEventSaver : IEventSaver
    {
        private readonly string fileName;

        private readonly IControlGateway controlGateway;

        private readonly bool isOverwrite;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">file name to save the serialize event</param>
        /// <param name="controlGateway"></param>
        /// <param name="isOverwrite">Ovewrite the file if exist</param>
        public SerializeEventSaver(string fileName,IControlGateway controlGateway,
            bool isOverwrite=true)
        {
            this.fileName = fileName;

            this.controlGateway = controlGateway;

            this.isOverwrite = isOverwrite;
        }

        public SerializeEventSaver(string fileName = "events.txt"):this(fileName,
            Framework.GetControlGateway())
        {

        }

        public SerializeEventSaver(string fileName,bool isOverwrite):this(fileName,
            Framework.GetControlGateway(),
            isOverwrite)
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
                DeleteFile(fileName);

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

        /// <summary>
        /// Delete the file if exist
        /// </summary>
        /// <param name="fileName"></param>
        private void DeleteFile(string fileName)
        {
            if(isOverwrite)
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
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
