using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace AALauncher
{
    public static class Serializer
    {
        private static XmlDeserializationEvents _xmlDeserializeEvents;
        private static XmlWriterSettings _xmlSerializeSettings;
        private static XmlSerializerNamespaces _namespaces;

        static Serializer()
        {
            _xmlDeserializeEvents = new XmlDeserializationEvents();
            _xmlDeserializeEvents.OnUnknownElement += OnUnknownElement;
            _xmlDeserializeEvents.OnUnknownNode += OnUnknownNode;
          //  _xmlDeserializeEvents.OnUnreferencedObject += OnUnreferencedObject;
            _xmlDeserializeEvents.OnUnknownAttribute += OnUnknownAttribute; 

            _xmlSerializeSettings = new XmlWriterSettings();
#if !DEBUG
            _xmlSerializeSettings.Indent = false;
#endif
            _xmlSerializeSettings.OmitXmlDeclaration = false;

            _namespaces = new XmlSerializerNamespaces();
            //_namespaces.Add(string.Empty, null);
        }


        public const string BackupFilePostfix = "_backup";


        public enum Format
        {
            Binary, Xml
        }
     

        private static void Write(string destinationFileName, MemoryStream stream, bool safe = false)
        {
            if (destinationFileName == null) throw new ArgumentNullException("destinationFileName");
            if (stream == null) throw new ArgumentNullException("stream");

            var bytes = stream.GetBuffer();

            if (safe)
            {
                string tempPath = destinationFileName + "_writeProgress";

                using (var file = OpenWrite(tempPath))
                {
                    file.Write(bytes, 0, (int)stream.Length);
                }

                string saveDataPath = destinationFileName + BackupFilePostfix;

                if (File.Exists(destinationFileName))
                    File.Replace(tempPath, destinationFileName, saveDataPath, true);
                else
                    File.Move(tempPath, destinationFileName);
            }
            else
            {
                using (var file = OpenWrite(destinationFileName))
                {
                    file.Write(bytes, 0, (int)stream.Length);
                }
            }
        }

        private static void SaveBin(string path, object data, bool safe = false)
        {
            using (var stream = new MemoryStream(0x100000))
            {
                var formatter = new BinaryFormatter();
                //formatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;
                //formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
                //formatter.FilterLevel = TypeFilterLevel.Low;

                formatter.Serialize(stream, data);

                Write(path, stream, safe);
            }
        }

        private static void SaveXml(string path, object data, bool safe = false)
        {
            Type type = data.GetType();

            var formatter = new XmlSerializer(type);

            using (var stream = new MemoryStream(0x100000))
            {
                using (XmlWriter wr = XmlWriter.Create(stream, _xmlSerializeSettings))
                {
                    formatter.Serialize(wr, data, _namespaces);
                }

                Write(path, stream, safe);
            }
        }

        public static void Save(string path, object data, Format stype = Format.Xml, bool isSafe = false)
        {
            //Stopwatch st = Stopwatch.StartNew();

            switch (stype)
            {
                case Format.Xml:
                    SaveXml(path, data, isSafe);
                    break;

                case Format.Binary:
                    SaveBin(path, data, isSafe);
                    break;
            }

            //Trace.Write("--" + st.ElapsedMilliseconds + " ms--");
        }


        public static T Load<T>(string path, bool throwexc = false, Format stype = Format.Xml) where T : class
        {
            T data;
            Exception error;

            if (!TryRead(path, stype, out data, out error))
            {
                var backup = path + BackupFilePostfix;
                if (File.Exists(backup))
                {
                    if (!TryRead(backup, stype, out data, out error)) //something wrong here...what about previous error??
                    {
                        if (throwexc)
                        {
                            throw error;
                        }
                    }
                }
                else
                {
                    if (throwexc)
                    {
                        throw error;
                    }
                }
            }

            return data;
        }

        private static Stream OpenRead(string path)
        {
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        private static Stream OpenWrite(string path)
        {
            return new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
        }

        private static bool TryRead<T>(string path, Format stype, out T retVal, out Exception error) where T : class
        {
            retVal = default(T);

            error = null;
            try
            {
                if (stype != Format.Xml)
                {
                    var formatter = new BinaryFormatter();

                    using (var stream = OpenRead(path))
                    {
                        var obj = formatter.Deserialize(stream);
                        retVal = obj as T;
                    }
                }
                else
                {
                    Type type = typeof(T);

                     
                    var formatter = new XmlSerializer(type);

                    using (var stream = OpenRead(path))
                    {
                        //retVal = (T)formatter.Deserialize(stream);

                        using (var reader = XmlReader.Create(stream))
                        {
                            retVal = (T)formatter.Deserialize(reader, _xmlDeserializeEvents);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                error = e;
            }

            return retVal != default(T);
        }

        private static void OnUnreferencedObject(object sender, UnreferencedObjectEventArgs unreferencedObjectEventArgs)
        {
            //Trace.WriteLine("unknown element in xml file" + xmlElementEventArgs.Element.Name);
        }

        private static void OnUnknownNode(object sender, XmlNodeEventArgs xmlNodeEventArgs)
        {
            Trace.WriteLine("unknown node in xml file" + xmlNodeEventArgs.Name);
        }

        private static void OnUnknownElement(object sender, XmlElementEventArgs xmlElementEventArgs)
        {
            //if (xmlElementEventArgs.Element.Name == "ExtraData" || xmlElementEventArgs.Element.Name == "ExtraDataBug")
            //{
            //}
            //else
            //{
            //    if (xmlElementEventArgs.Element.Name == "Trackers")
            //    {
            //        using (var reader = XmlReader.Create(new StringReader(xmlElementEventArgs.Element.OuterXml)))
            //        {
            //            SerializableDictionary<string, InfoTracker> dict = new SerializableDictionary<string, InfoTracker>();
            //            reader.Read();
            //            dict.ReadXml(reader);

            //            ((ItemBase)xmlElementEventArgs.ObjectBeingDeserialized).Trackers = new SerializableStringDictionary<InfoTracker>(dict);
            //        }
            //    }
            //    //else if (xmlElementEventArgs.Element.Name == "Topics")
            //    //{
            //    //    using (var reader = XmlReader.Create(new StringReader(xmlElementEventArgs.Element.OuterXml)))
            //    //    {
            //    //        var dict = new SerializableDictionary<string, InfoTopic>();
            //    //        reader.Read();
            //    //        dict.ReadXml(reader);

            //    //        ((ItemBase)xmlElementEventArgs.ObjectBeingDeserialized).Topics = 
            //    //            new SerializableStringDictionary<InfoTopic>(dict);
            //    //    }

            //    //}
              
            Trace.WriteLine("unknown element in xml file" + xmlElementEventArgs.Element.Name);

            //}

        }

        private static void OnUnknownAttribute(object sender, XmlAttributeEventArgs xmlAttributeEventArgs)
        {
            Trace.WriteLine("unknown attribute in xml file" + xmlAttributeEventArgs.Attr.Name);
        }

    }
}