using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Spherification.src.model.system.persistance
{
    class PersistanceService
    {
        public static ApplicationState state { get; set; }
        private static String PERSIST_FILENAME = @"Application-state.txt";

        public static void loadApplicationState()
        {
            if (!File.Exists(PERSIST_FILENAME))
            {
                // init settings
                state = new ApplicationState();
                Console.Write("State created", state);
            }
            else
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(PERSIST_FILENAME, FileMode.Open, FileAccess.Read);
                state = (ApplicationState)formatter.Deserialize(stream);
                // load settings
               

            }

  
        }

        public static void persistApplicationState()
        {
            // save state
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(PERSIST_FILENAME, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, state);
            stream.Close();
        }

        public static ApplicationState getApplicationState()
        {
            return state;
        }

        public static void setApplicationState(ApplicationState s)
        {
            state = s;
        }
    }
        
}
