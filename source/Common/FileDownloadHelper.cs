using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    public class FileDownloadHelper : LogBase, IDisposable
    {
        private EventHandler<ProgressChangeEventArgs> _progressChangedHandler = null;

        public FileDownloadHelper(EventHandler<LogEventArgs> handler = null, EventHandler<ProgressChangeEventArgs> progressChangedHandler = null)
            : base(handler)
        {
            this._progressChangedHandler = progressChangedHandler;
        }

        public void DownloadFileFromUrl(string downloadUrl, string zipFileName)
        {
            // Open a connection to the URL where the report is available.
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(downloadUrl);
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Stream httpStream = response.GetResponseStream();

            // Open the report file.
            FileInfo zipFileInfo = new FileInfo(zipFileName);
            if (!zipFileInfo.Directory.Exists)
            {
                zipFileInfo.Directory.Create();
            }

            FileStream fileStream = new FileStream(zipFileInfo.FullName, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            BinaryReader binaryReader = new BinaryReader(httpStream);

            // Read the report and save it to the file.
            int bufferSize = 10240;
            long writtenBytes = 0L;
            while (true)
            {
                // Read report data from API.
                byte[] buffer = binaryReader.ReadBytes(bufferSize);

                // Write report data to file.
                binaryWriter.Write(buffer);

                writtenBytes += buffer.Length;

                if (this._progressChangedHandler != null)
                {
                    var _percent = response.ContentLength == 0 ? 0.5 : writtenBytes * 0.5 / response.ContentLength;
                    this._progressChangedHandler(this, new ProgressChangeEventArgs(0.5 + _percent, zipFileName));
                }

                // If the end of the report is reached, break out of the 
                // loop.
                if (buffer.Length != bufferSize)
                {
                    break;
                }
            }

            // Clean up.
            binaryWriter.Close();
            binaryReader.Close();
            fileStream.Close();
            httpStream.Close();
        }

        public void TryDownloadFileFromUrl(string downloadUrl, string zipFileName)
        {
            MethodHelper.TryGetVoid(DownloadFileFromUrl, this, downloadUrl, zipFileName);
        }

        public async Task DownloadFileFromUrlAsync(string downloadUrl, string zipFileName)
        {
            // Open a connection to the URL where the report is available.
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(downloadUrl);
            HttpWebResponse response = (HttpWebResponse)await webRequest.GetResponseAsync();
            Stream httpStream = response.GetResponseStream();

            // Open the report file.
            FileInfo zipFileInfo = new FileInfo(zipFileName);
            if (!zipFileInfo.Directory.Exists)
            {
                zipFileInfo.Directory.Create();
            }

            FileStream fileStream = new FileStream(zipFileInfo.FullName, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            BinaryReader binaryReader = new BinaryReader(httpStream);

            // Read the report and save it to the file.
            int bufferSize = 10240;
            long writtenBytes = 0L;
            while (true)
            {
                // Read report data from API.
                byte[] buffer = binaryReader.ReadBytes(bufferSize);

                // Write report data to file.
                binaryWriter.Write(buffer);

                writtenBytes += buffer.Length;

                if (this._progressChangedHandler != null)
                {
                    var _percent = response.ContentLength == 0 ? 0.5 : writtenBytes * 0.5 / response.ContentLength;
                    this._progressChangedHandler(this, new ProgressChangeEventArgs(0.5 + _percent, zipFileName));
                }

                // If the end of the report is reached, break out of the 
                // loop.
                if (buffer.Length != bufferSize)
                {
                    break;
                }
            }

            // Clean up.
            binaryWriter.Close();
            binaryReader.Close();
            fileStream.Close();
            httpStream.Close();
        }

        public void Dispose() { }
    }
}
