using LocalCommons.Native.Logging;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LocalCommons.Native.Bufferization
{
    /// <summary>
    /// Type For Reading / Writing Binary Files
    /// Author: Raphail
    /// </summary>
    public class BinaryOperations
    {
        /// <summary>
        /// Loading And Return File Specified in Path
        /// </summary>
        /// <typeparam name="T">Type Of Structure Which Must Be Deserialized.</typeparam>
        /// <param name="path">Full Path With File Name</param>
        /// <returns>List With Deserialized Data</returns>
        public static List<T> DeserializeBinaryFile<T>(string path)
        {
            int currentPos = 0;
            List<T> outlist = new List<T>();
            using (FileStream f = File.OpenRead(path))
            {
                while (f.Position < f.Length)
                {
                    Bar.OverwriteConsoleMessage("Loading Structures Of {" + typeof(T).Name + "} No. " + currentPos);
                    T deserialized = Serializer.DeserializeWithLengthPrefix<T>(f, PrefixStyle.Fixed32);
                    outlist.Add(deserialized);
                    currentPos++;
                }
            }
            return outlist;
        }

        /// <summary>
        /// Serializes - (Creating New or Updates) File Specified in Path.
        /// </summary>
        /// <typeparam name="T">Type Of Structure Which Must Be Serialized</typeparam>
        /// <param name="path">Full Path With File Name</param>
        public static void SerializeStructure<T>(string path, List<T> data)
        {
            if (File.Exists(path))
                File.Delete(path);
            using (FileStream fstream = File.Create(path))
            {
                for(int i = 0; i < data.Count; i++)
                {
                    T current = data[i];
                    Bar.OverwriteConsoleMessage("Saving Stuct {" + typeof(T).Name + "} No. " + i);
                    Serializer.SerializeWithLengthPrefix<T>(fstream, current, PrefixStyle.Fixed32);
                }
                fstream.SetLength(fstream.Position);
                Bar.OverwriteConsoleMessage("Structures Of {" + typeof(T).Name + "} Successfully Saved");
            }

        }
    }
}
