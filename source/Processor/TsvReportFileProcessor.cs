using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    public class TsvReportFileProcessor : LogBase, IDisposable
    {
        private EventHandler<BulkProcessEventArgs> _processHandler = null;
        private EventHandler<ProgressChangeEventArgs> _progressChangedHandler = null;

        private bool _completed = false;

        /// <summary>
        /// Whether the process is done
        /// </summary>
        public bool Completed { get { return this._completed; } }

        /// <summary>
        /// Whether delete zip file after whole process was done
        /// </summary>
        public bool DeleteZipFileAfterProcessCompleted { get; set; }

        /// <summary>
        /// Whether delete tsv file after whole process was done
        /// </summary>
        public bool DeleteTsvFileWhenProcessCompleted { get; set; }

        public string[] Columns { get; private set; }

        /// <summary>
        /// Process a ZIP file
        /// UnZip and Parse the tsv file
        /// </summary>
        /// <param name="logHandler">define a LogHandler if you need process the Log</param>
        /// <param name="processHandler">define a handler to handle the object[] row values</param>
        /// <param name="progressChangeHandler">define a handler to handle the progress changed event</param>
        /// <param name="zipSourceFile">zip source file</param>
        /// <param name="deleteTsvFileWhenProcessCompleted">default is TRUE</param>
        /// <param name="deleteZipFileWhenProcessCompleted">default is TRUE</param>
        public TsvReportFileProcessor(
            EventHandler<LogEventArgs> logHandler,
            EventHandler<BulkProcessEventArgs> processHandler,
            EventHandler<ProgressChangeEventArgs> progressChangeHandler,
            string zipSourceFile,
            bool deleteTsvFileWhenProcessCompleted = true,
            bool deleteZipFileWhenProcessCompleted = true)
            : base(logHandler)
        {
            this._processHandler = processHandler;
            this._progressChangedHandler = progressChangeHandler;
            this.ZipFileName = zipSourceFile;

            if (string.IsNullOrEmpty(this.ZipFileName) || !File.Exists(this.ZipFileName))
            {
                Log(new LogEventArgs(ServiceType.FileProcessor, "FileProcessor.FileProcessor()", "ArgumentNullException(\"zipSourceFile\")"));
                throw new ArgumentNullException("zipSourceFile");
            }

            BuildTsvPath();

            this.DeleteZipFileAfterProcessCompleted = deleteZipFileWhenProcessCompleted;
            this.DeleteTsvFileWhenProcessCompleted = deleteTsvFileWhenProcessCompleted;
        }

        public void Dispose()
        {

        }

        public string ZipFileName { get; private set; }

        public string TsvFileOutputPath { get; private set; }

        private bool _zipFileProcessed = false;

        public string TsvFileName { get; private set; }

        private void ProcessRow(Dictionary<string, string> row, bool _completed)
        {
            if (_completed && !this._completed)
                this._completed = true;

            if (this._processHandler != null)
                this._processHandler(this, new BulkProcessEventArgs(row, _completed));
        }

        public bool Process()
        {
            bool result = false;

            try
            {
                _zipFileProcessed = false;

                UnZip();
                Parse();

                result = true;
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.FileProcessor, "Process", "Error occured while processing!", new
                {
                    ZipFileName = this.ZipFileName,
                    TsvFileOutputPath = this.TsvFileOutputPath,
                    TsvFileName = this.TsvFileName
                }, ex));

                result = false;
            }

            if (this.DeleteTsvFileWhenProcessCompleted)
            {
                try
                {
                    File.Delete(this.TsvFileName);
                }
                catch (Exception ex)
                {
                    Log(new LogEventArgs(ServiceType.FileProcessor, "Process", "Error occured while deleting tsv files!", new
                    {
                        ZipFileName = this.ZipFileName,
                        TsvFileOutputPath = TsvFileOutputPath,
                        TsvFileName = this.TsvFileName
                    }, ex));
                }
            }

            if (this.DeleteZipFileAfterProcessCompleted && result)
            {
                try
                {
                    File.Delete(this.ZipFileName);
                }
                catch (Exception ex)
                {
                    Log(new LogEventArgs(ServiceType.FileProcessor, "Process", "Error occured while deleting zip file!", new
                    {
                        ZipFileName = this.ZipFileName,
                        TsvFileOutputPath = TsvFileOutputPath,
                        TsvFileName = this.TsvFileName
                    }, ex));
                }
            }

            return result;
        }

        private void BuildTsvPath()
        {
            FileInfo f = new FileInfo(this.ZipFileName);
            this.TsvFileOutputPath = Path.Combine(f.Directory.FullName, "UnZip");

            if (!Directory.Exists(this.TsvFileOutputPath))
                Directory.CreateDirectory(this.TsvFileOutputPath);
        }

        private string BuildTsvOutputFile(string filename)
        {
            return Path.Combine(this.TsvFileOutputPath, string.Format("{0}{1}", Guid.NewGuid().ToString(), new FileInfo(filename).Extension));
        }

        private void UnZip()
        {
            if (_zipFileProcessed)
                return;

            using (ZipArchive archive = ZipFile.OpenRead(this.ZipFileName))
            {
                if (archive.Entries != null && archive.Entries.Count > 1)
                {
                    //UnSupported
                    throw new Exception("Only support one Entry in each Zip File!");
                }

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".tsv", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var desFile = BuildTsvOutputFile(entry.FullName);
                        entry.ExtractToFile(desFile, true);
                        this.TsvFileName = desFile;
                    }
                }
            }

            _zipFileProcessed = true;
        }

        private long _totalRows = 0L;

        private void Parse()
        {
            if (this._progressChangedHandler != null || this._processHandler != null)
                ScanSingleFile();

            long proceedRows = 0L;

            var rowValueStartIndex = ValueStartRowIndex();
            if (rowValueStartIndex == -1)
                return;

            var index = -1L;
            using (StreamReader sr = new StreamReader(this.TsvFileName, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    index++;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var array = line.Split('\t');

                    if (array.Length == this.Columns.Length && index >= rowValueStartIndex)
                    {
                        Dictionary<string, string> rowValues = new Dictionary<string, string>();
                        for (int i = 0; i < this.Columns.Length; i++)
                        {
                            rowValues.Add(this.Columns[i], string.IsNullOrWhiteSpace(array[i]) ? null : array[i].Trim(new char[] { '"' }));
                        }

                        proceedRows++;

                        ProcessRow(rowValues, proceedRows >= _totalRows);

                        if (this._progressChangedHandler != null)
                            this._progressChangedHandler(
                                this,
                                new ProgressChangeEventArgs(_totalRows == 0 ? 0 : proceedRows * 1.0 / _totalRows, this.TsvFileName));
                    }
                }
            }

            this._completed = true;
        }

        private void ScanSingleFile()
        {
            long columnRowPosition = 0;
            long rowPosition = 0;

            using (StreamReader sr = new StreamReader(this.TsvFileName, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    
                    rowPosition++;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    if (line.StartsWith("\"Rows: "))
                    {
                        columnRowPosition = rowPosition + 2;

                        long L;
                        var b = long.TryParse(line.Trim('"').Remove(0, 6), out L);
                        if (b)
                            _totalRows = L;
                    }

                    if(rowPosition == columnRowPosition)
                    {
                        this.Columns = (from n in line.Split(new char[] { '\t' }) select n.Trim(new char[] { '"' })).ToArray();
                        break;
                    }
                }
            }
        }

        private int ValueStartRowIndex()
        {
            if (_totalRows == 0)
                return -1;

            using (StreamReader sr = new StreamReader(this.TsvFileName, Encoding.UTF8))
            {
                var index = 0;
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        return index + 2;
                    else
                        index++;
                }
            }

            return -1;
        }
    }
}