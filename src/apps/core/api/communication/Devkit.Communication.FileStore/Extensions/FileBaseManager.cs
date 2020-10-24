// -----------------------------------------------------------------------
// <copyright file="FileBaseManager.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.FileStore.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// File Base Manager handler.
    /// </summary>
    public static class FileBaseManager
    {
        /// <summary>
        /// Saves file to the server.
        /// </summary>
        /// <param name="dataString">Posted 64 Bit data string.</param>
        /// <param name="size">Image Size.</param>
        /// <returns>bool.</returns>
        public static byte[] ToImage(this string dataString, int size = 0)
        {
            // copied from https://gist.github.com/vbfox/484643
            var base64Data = Regex.Match(dataString, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);
            var serverFileUrl = String.Empty;

            using (var stream = new MemoryStream(binData))
            {
                using (var image = Image.FromStream(stream))
                {
                    var newWidth = image.Width;
                    var newHeight = image.Height;

                    if (size != 0)
                    {
                        //Check if the Width is greater than its Height.
                        if (image.Width > image.Height)
                        {
                            newWidth = size;
                            newHeight = image.Height * size / image.Width;
                        }
                        else
                        {
                            newWidth = image.Width * size / image.Height;
                            newHeight = size;
                        }
                    }

                    //Start creating new bitmap with the new dimension.
                    using (var imageBitmap = new Bitmap(newWidth, newHeight))
                    {
                        //Start creating graphic effect for smoothing the file.
                        using (var imageGraph = Graphics.FromImage(imageBitmap))
                        {
                            imageGraph.CompositingQuality = CompositingQuality.HighQuality;
                            imageGraph.SmoothingMode = SmoothingMode.HighQuality;

                            //Start creating Rectangle to contain the image.
                            var imageRec = new Rectangle(0, 0, newWidth, newHeight);
                            imageGraph.DrawImage(image, imageRec);
                        }

                        using (var responseStream = new MemoryStream())
                        {
                            imageBitmap.Save(responseStream, image.RawFormat);
                            return responseStream.ToArray();
                        }
                    }
                }
            }
        }
    }
}