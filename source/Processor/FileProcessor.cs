using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    public class FileProcessor : LogBase, IDisposable
    {
        private EventHandler<ProcessEventArgs> _processHandler = null;
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
        public FileProcessor(
            EventHandler<LogEventArgs> logHandler,
            EventHandler<ProcessEventArgs> processHandler,
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

        private List<string> _tsvFiles = new List<string>();
        private bool _zipFileProcessed = false;

        public List<string> TsvFiles
        {
            get
            {
                if (!_zipFileProcessed)
                    UnZip();

                return _tsvFiles == null ? new List<string>() : _tsvFiles;
            }
        }

        public int FilesCount
        {
            get
            {
                if (this.TsvFiles == null)
                    return 0;
                return this.TsvFiles.Count;
            }
        }

        public string CurrentProcessingTsvFileName { get; private set; }

        private void ProcessRow(object[] row, bool _completed)
        {
            if (_completed && this.FilesCount == 1 && !this._completed)
                this._completed = true;

            if (this._processHandler != null)
                this._processHandler(this, new ProcessEventArgs(row, _completed));
        }

        public bool Process()
        {
            bool result = false;

            try
            {
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
                    TsvFiles = this._tsvFiles
                }, ex));

                result = false;
            }

            if (this.DeleteTsvFileWhenProcessCompleted)
            {
                try
                {
                    foreach (var tsv in this.TsvFiles)
                        File.Delete(tsv);
                }
                catch (Exception ex)
                {
                    Log(new LogEventArgs(ServiceType.FileProcessor, "Process", "Error occured while deleting tsv files!", new
                    {
                        ZipFileName = this.ZipFileName,
                        TsvFileOutputPath = TsvFileOutputPath,
                        TsvFiles = _tsvFiles
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
                        TsvFiles = _tsvFiles
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
            return Path.Combine(this.TsvFileOutputPath, filename);
        }

        private void UnZip()
        {
            if (_zipFileProcessed)
                return;

            using (ZipArchive archive = ZipFile.OpenRead(this.ZipFileName))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".tsv", StringComparison.CurrentCultureIgnoreCase))
                    {
                        entry.ExtractToFile(BuildTsvOutputFile(entry.FullName), true);

                        if (this._tsvFiles == null)
                            this._tsvFiles = new List<string>();
                        this._tsvFiles.Add(BuildTsvOutputFile(entry.FullName));
                    }
                }
            }

            _zipFileProcessed = true;
        }

        private long _totalRows = 0L;
        private Dictionary<int, long> _filesAndRows = new Dictionary<int, long>();

        private void Parse()
        {
            if (this._progressChangedHandler != null || this._processHandler != null)
                ScanAllFiles();

            long proceedRows = 0L;

            foreach (var tsv in this.TsvFiles)
            {
                this.CurrentProcessingTsvFileName = tsv;

                var rowValueStartIndex = ValueStartRowIndex(tsv);
                if (rowValueStartIndex == -1)
                    continue;

                var index = -1L;
                using (StreamReader sr = new StreamReader(tsv, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();

                        index++;

                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        var array = line.Split('\t');

                        if (array.Length >= 3 && index >= rowValueStartIndex)
                        {
                            var lst = from n in array select n.TrimStart('"').TrimEnd('"');

                            proceedRows++;

                            ProcessRow(lst.ToArray(), proceedRows >= _totalRows);

                            if (this._progressChangedHandler != null)
                                this._progressChangedHandler(
                                    this,
                                    new ProgressChangeEventArgs(_totalRows == 0 ? 0 : proceedRows * 1.0 / _totalRows, tsv));
                        }
                    }
                }
            }

            this._completed = true;
        }

        private void ScanAllFiles()
        {
            _totalRows = 0L;

            foreach (var tsv in this.TsvFiles)
            {
                var hash = tsv.GetHashCode();
                var rows = ScanSingleFile(tsv);

                if (_filesAndRows == null)
                    _filesAndRows = new Dictionary<int, long>();

                if (_filesAndRows.Keys.Contains(hash))
                    _filesAndRows[hash] = rows;
                else
                    _filesAndRows.Add(hash, rows);

                _totalRows += rows;
            }
        }

        private long ScanSingleFile(string tsv)
        {
            using (StreamReader sr = new StreamReader(tsv, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    if (line.StartsWith("\"Rows: "))
                    {
                        long L;
                        var b = long.TryParse(line.TrimStart('"').TrimEnd('"').Remove(0, 6), out L);
                        if (b)
                            return L;
                    }
                }
            }

            return 0L;
        }

        private int ValueStartRowIndex(string tsv)
        {
            var hash = tsv.GetHashCode();

            var rows = _filesAndRows != null && _filesAndRows.Keys.Contains(hash) ? _filesAndRows[hash] : ScanSingleFile(tsv);

            if (rows == 0)
                return -1;

            using (StreamReader sr = new StreamReader(tsv, Encoding.UTF8))
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