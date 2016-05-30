using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    public class BillingProcessor : LogBase, IDisposable
    {
        public void Dispose()
        {
            if (this._base64String != null)
                _base64String = null;
            if (this.bytes != null)
                this.bytes = null;
        }

        public BillingProcessor(string base64String, EventHandler<LogEventArgs> handler = null)
            : base(handler)
        {
            if (string.IsNullOrWhiteSpace(base64String) || string.IsNullOrEmpty(base64String))
                throw new ArgumentNullException("base64String", "base64String can not be null");

            this._base64String = base64String;
            this.bytes = Convert.FromBase64String(_base64String);
            this.BillDocType = IsConsolidatedBill() ? BillType.ConsolidatedFinancialDocument : BillType.FinancialDocument;
        }

        public BillingProcessor(byte[] _bytes, EventHandler<LogEventArgs> hander = null)
            : base(hander)
        {
            this._base64String = Encoding.UTF8.GetString(_bytes);
            this.bytes = Convert.FromBase64String(_base64String);
            this.BillDocType = IsConsolidatedBill() ? BillType.ConsolidatedFinancialDocument : BillType.FinancialDocument;
        }

        public BillingProcessor(FileInfo xmlFile, EventHandler<LogEventArgs> handler = null)
            : base(handler)
        {
            if (xmlFile == null || !xmlFile.Exists)
                throw new FileNotFoundException("xmlFile not found");

            this._base64String = Convert.ToBase64String(File.ReadAllBytes(xmlFile.FullName));
            this.bytes = Convert.FromBase64String(_base64String);
            this.BillDocType = IsConsolidatedBill() ? BillType.ConsolidatedFinancialDocument : BillType.FinancialDocument;
        }

        public BillType BillDocType
        {
            get;
            private set;
        }

        private string _base64String = null;
        private byte[] bytes = null;

        public bool SaveBase64StringBillToLocalFile(string localFile)
        {
            try
            {
                FileInfo f = new FileInfo(localFile);
                if (!f.Directory.Exists)
                    f.Directory.Create();

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(this._base64String)))
                {
                    using (FileStream fs = new FileStream(localFile, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "SaveBase64StringBillToLocalFile",
                    ex.Message,
                    new
                    {
                        Base64String = _base64String,
                        LocalFile = localFile
                    }, ex));
            }

            return false;
        }

        public object ParseBill()
        {
            try
            {
                if (this.BillDocType == BillType.ConsolidatedFinancialDocument)
                {
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        var serializer = new XmlSerializer(typeof(ConsolidatedFinancialDocument));
                        return serializer.Deserialize(ms) as ConsolidatedFinancialDocument;
                    }
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        var serializer = new XmlSerializer(typeof(FinancialDocument));
                        return serializer.Deserialize(ms) as FinancialDocument;
                    }
                }
            }
            catch (Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "ParseBill", ex.Message, new { Base64String = _base64String }, ex));
            }

            return null;
        }

        private bool IsConsolidatedBill()
        {
            try
            {
                var xmlString = Encoding.UTF8.GetString(Convert.FromBase64String(this._base64String));

                return xmlString.TrimStart('\r').TrimStart('\n').TrimStart('\t').TrimStart().StartsWith("<ConsolidatedFinancialDocument");
            }
            catch(Exception ex)
            {
                Log(new LogEventArgs(ServiceType.CustomerBilling, "IsConsolidatedBill", ex.Message, new { Base64String = _base64String }, ex));
            }

            return false;
        }
    }

    public enum BillType
    {
        ConsolidatedFinancialDocument,
        FinancialDocument
    }
}
