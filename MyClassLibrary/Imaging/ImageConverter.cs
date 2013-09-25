using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace MyClassLibrary.Imaging
{
    public static class ImageConverter
    {
        public static Byte[] ConvertToByteArray(Image image, ImageFormat imageFormat)
        {
            MemoryStream memoryStream = new MemoryStream();

            image.Save(memoryStream, imageFormat);

            Byte[] byteArray = memoryStream.ToArray();

            memoryStream.Close();

            memoryStream.Dispose();

            return byteArray;
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.