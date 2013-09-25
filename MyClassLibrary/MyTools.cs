using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace MyClassLibrary
{
    public static class MyTools
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



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.
