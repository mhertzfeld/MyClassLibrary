﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace MyClassLibrary
{
    public static class MyUtilities
    {
        public static T Clone<T>(T obj)
        {
            T result;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, obj);

                memoryStream.Position = 0;

                result = (T)binaryFormatter.Deserialize(memoryStream);
            }

            return result;
        }

        public static void DisposeObject(IDisposable disposableObject)
        {
            if (disposableObject != null)
            {
                disposableObject.Dispose();
            }
        }

        public static Decimal SafeDivision(Decimal numerator, Decimal denominator)
        {
            return (denominator == 0 ? 0 : numerator / denominator);
        }
    }
}