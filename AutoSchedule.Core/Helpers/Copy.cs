using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace AutoSchedule.Core.Helpers
{
    public class Copy
    {
        /// <summary>
        ///     Get the deep copy an object by serialization and deserialization.
        /// </summary>
        /// <remarks>
        ///     The object must have Serializable attribute.
        /// </remarks>
        /// <returns>
        ///     A deep copy of given object.
        /// </returns>
        public static T DeepCopySerialization<T>(T obj)
        {
            using MemoryStream memoryStream = new MemoryStream();

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            // Serialization.
            binaryFormatter.Serialize(memoryStream, obj);
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Deserialization.
            object copiedObj = binaryFormatter.Deserialize(memoryStream);

            return (T)copiedObj;
        }
    }
}
