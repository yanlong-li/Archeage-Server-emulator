using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LocalCommons.Native.Logging
{
    /// <summary>
    /// Usual Logger
    /// Author: Raphail
    /// </summary>
    public class Logger
    {
        private static StreamWriter writer;

        /// <summary>
        /// Initialized StreamWriter Which Writing All data into .log File.
        /// </summary>
        public static void Init()
        {
            if (!Directory.Exists(@"log"))
                Directory.CreateDirectory(@"log");
            writer = new StreamWriter(@"log/" + "logging" + ".log");
            writer.AutoFlush = true;
        }

        /// <summary>
        /// Trace With Parameters(objects)
        /// </summary>
        /// <param name="data">Information String</param>
        /// <param name="prms">Parameters</param>
        public static void Trace(string data, params object[] prms)
        {
            Console.WriteLine(DateTime.Now.ToString("g") + " - " + data, prms);
            writer.WriteLine(DateTime.Now.ToString("g") + " - " + data, prms);
        }

        /// <summary>
        /// Trace Usual String
        /// </summary>
        /// <param name="data">Information String</param>
        public static void Trace(string data)
        {
            Console.WriteLine(DateTime.Now.ToString("g") + " - " + data);
            writer.WriteLine(DateTime.Now.ToString("g") + " - " + data);
        }

        /// <summary>
        /// Writing Section
        /// </summary>
        /// <param name="data">Section Information</param>
        public static void Section(string data)
        {
            data = "[ " + data + " ]";
            while (data.Length < 79) data = "-" + data;
            Console.WriteLine(data);
            writer.WriteLine(data);
        }
    }
}
