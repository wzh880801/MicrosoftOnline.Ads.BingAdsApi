
namespace MicrosoftOnline.Ads.BingAdsApi
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class FinancialDocument
    {

        private FinancialDocumentDocumentHeader documentHeaderField;

        private FinancialDocumentDocumentItems documentItemsField;

        /// <remarks/>
        public FinancialDocumentDocumentHeader DocumentHeader
        {
            get
            {
                return this.documentHeaderField;
            }
            set
            {
                this.documentHeaderField = value;
            }
        }

        /// <remarks/>
        public FinancialDocumentDocumentItems DocumentItems
        {
            get
            {
                return this.documentItemsField;
            }
            set
            {
                this.documentItemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentHeader
    {

        private FinancialDocumentDocumentHeaderContactInformation[] customerHeaderDataField;

        private int datahashField;

        private string versionField;

        private bool isCompressedField;

        private bool isReRenderField;

        private bool isPendingField;

        private FinancialDocumentDocumentHeaderDocumentHeaderData documentHeaderDataField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ContactInformation", IsNullable = false)]
        public FinancialDocumentDocumentHeaderContactInformation[] CustomerHeaderData
        {
            get
            {
                return this.customerHeaderDataField;
            }
            set
            {
                this.customerHeaderDataField = value;
            }
        }

        /// <remarks/>
        public int Datahash
        {
            get
            {
                return this.datahashField;
            }
            set
            {
                this.datahashField = value;
            }
        }

        /// <remarks/>
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        public bool IsCompressed
        {
            get
            {
                return this.isCompressedField;
            }
            set
            {
                this.isCompressedField = value;
            }
        }

        /// <remarks/>
        public bool IsReRender
        {
            get
            {
                return this.isReRenderField;
            }
            set
            {
                this.isReRenderField = value;
            }
        }

        /// <remarks/>
        public bool IsPending
        {
            get
            {
                return this.isPendingField;
            }
            set
            {
                this.isPendingField = value;
            }
        }

        /// <remarks/>
        public FinancialDocumentDocumentHeaderDocumentHeaderData DocumentHeaderData
        {
            get
            {
                return this.documentHeaderDataField;
            }
            set
            {
                this.documentHeaderDataField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentHeaderContactInformation
    {

        private string contactInformationTypeField;

        private long clientIDField;

        private bool clientIDFieldSpecified;

        private FinancialDocumentDocumentHeaderContactInformationContact contactField;

        /// <remarks/>
        public string ContactInformationType
        {
            get
            {
                return this.contactInformationTypeField;
            }
            set
            {
                this.contactInformationTypeField = value;
            }
        }

        /// <remarks/>
        public long ClientID
        {
            get
            {
                return this.clientIDField;
            }
            set
            {
                this.clientIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ClientIDSpecified
        {
            get
            {
                return this.clientIDFieldSpecified;
            }
            set
            {
                this.clientIDFieldSpecified = value;
            }
        }

        /// <remarks/>
        public FinancialDocumentDocumentHeaderContactInformationContact Contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentHeaderContactInformationContact
    {

        private string customerNameField;

        private string attnNameField;

        private FinancialDocumentDocumentHeaderContactInformationContactAddress addressField;

        /// <remarks/>
        public string CustomerName
        {
            get
            {
                return this.customerNameField;
            }
            set
            {
                this.customerNameField = value;
            }
        }

        /// <remarks/>
        public string AttnName
        {
            get
            {
                return this.attnNameField;
            }
            set
            {
                this.attnNameField = value;
            }
        }

        /// <remarks/>
        public FinancialDocumentDocumentHeaderContactInformationContactAddress Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentHeaderContactInformationContactAddress
    {

        private string addressLine1Field;

        private string cityField;

        private string postalCodeField;

        private string countryCodeField;

        private string countryNameField;

        /// <remarks/>
        public string AddressLine1
        {
            get
            {
                return this.addressLine1Field;
            }
            set
            {
                this.addressLine1Field = value;
            }
        }

        /// <remarks/>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        public string CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentHeaderDocumentHeaderData
    {

        private long documentIDField;

        private string systemNameField;

        private string documentTypeField;

        private string countryCodeField;

        private string countryNameField;

        private string currencyCodeField;

        private decimal currencyExchangeRateToUSDField;

        private string languageCodeField;

        private long invoiceIDField;

        private long accountIDField;

        private string taxIDField;

        private string paymentInstrTypeNameField;

        private string paymentOptionNameField;

        private FinancialDocumentDocumentHeaderDocumentHeaderDataDocumentDates documentDatesField;

        private FinancialDocumentDocumentHeaderDocumentHeaderDataGrandTotals grandTotalsField;

        private ushort companyCodeField;

        private FinancialDocumentDocumentHeaderDocumentHeaderDataTaxEntrySet taxEntrySetField;

        private object commentsField;

        private object companyOrderNumberField;

        private object agencyOrderNumberField;

        private object campaignDescriptionField;

        private string billingTypeField;

        private string accountNumberField;

        private string accountNameField;

        private decimal agencyCommPctField;

        private long parentBillingDocumentIDField;

        /// <remarks/>
        public long DocumentID
        {
            get
            {
                return this.documentIDField;
            }
            set
            {
                this.documentIDField = value;
            }
        }

        /// <remarks/>
        public string SystemName
        {
            get
            {
                return this.systemNameField;
            }
            set
            {
                this.systemNameField = value;
            }
        }

        /// <remarks/>
        public string DocumentType
        {
            get
            {
                return this.documentTypeField;
            }
            set
            {
                this.documentTypeField = value;
            }
        }

        /// <remarks/>
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        public string CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }

        /// <remarks/>
        public string CurrencyCode
        {
            get
            {
                return this.currencyCodeField;
            }
            set
            {
                this.currencyCodeField = value;
            }
        }

        /// <remarks/>
        public decimal CurrencyExchangeRateToUSD
        {
            get
            {
                return this.currencyExchangeRateToUSDField;
            }
            set
            {
                this.currencyExchangeRateToUSDField = value;
            }
        }

        /// <remarks/>
        public string LanguageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        public long InvoiceID
        {
            get
            {
                return this.invoiceIDField;
            }
            set
            {
                this.invoiceIDField = value;
            }
        }

        /// <remarks/>
        public long AccountID
        {
            get
            {
                return this.accountIDField;
            }
            set
            {
                this.accountIDField = value;
            }
        }

        /// <remarks/>
        public string TaxID
        {
            get
            {
                return this.taxIDField;
            }
            set
            {
                this.taxIDField = value;
            }
        }

        /// <remarks/>
        public string PaymentInstrTypeName
        {
            get
            {
                return this.paymentInstrTypeNameField;
            }
            set
            {
                this.paymentInstrTypeNameField = value;
            }
        }

        /// <remarks/>
        public string PaymentOptionName
        {
            get
            {
                return this.paymentOptionNameField;
            }
            set
            {
                this.paymentOptionNameField = value;
            }
        }

        /// <remarks/>
        public FinancialDocumentDocumentHeaderDocumentHeaderDataDocumentDates DocumentDates
        {
            get
            {
                return this.documentDatesField;
            }
            set
            {
                this.documentDatesField = value;
            }
        }

        /// <remarks/>
        public FinancialDocumentDocumentHeaderDocumentHeaderDataGrandTotals GrandTotals
        {
            get
            {
                return this.grandTotalsField;
            }
            set
            {
                this.grandTotalsField = value;
            }
        }

        /// <remarks/>
        public ushort CompanyCode
        {
            get
            {
                return this.companyCodeField;
            }
            set
            {
                this.companyCodeField = value;
            }
        }

        /// <remarks/>
        public FinancialDocumentDocumentHeaderDocumentHeaderDataTaxEntrySet TaxEntrySet
        {
            get
            {
                return this.taxEntrySetField;
            }
            set
            {
                this.taxEntrySetField = value;
            }
        }

        /// <remarks/>
        public object Comments
        {
            get
            {
                return this.commentsField;
            }
            set
            {
                this.commentsField = value;
            }
        }

        /// <remarks/>
        public object CompanyOrderNumber
        {
            get
            {
                return this.companyOrderNumberField;
            }
            set
            {
                this.companyOrderNumberField = value;
            }
        }

        /// <remarks/>
        public object AgencyOrderNumber
        {
            get
            {
                return this.agencyOrderNumberField;
            }
            set
            {
                this.agencyOrderNumberField = value;
            }
        }

        /// <remarks/>
        public object CampaignDescription
        {
            get
            {
                return this.campaignDescriptionField;
            }
            set
            {
                this.campaignDescriptionField = value;
            }
        }

        /// <remarks/>
        public string BillingType
        {
            get
            {
                return this.billingTypeField;
            }
            set
            {
                this.billingTypeField = value;
            }
        }

        /// <remarks/>
        public string AccountNumber
        {
            get
            {
                return this.accountNumberField;
            }
            set
            {
                this.accountNumberField = value;
            }
        }

        /// <remarks/>
        public string AccountName
        {
            get
            {
                return this.accountNameField;
            }
            set
            {
                this.accountNameField = value;
            }
        }

        /// <remarks/>
        public decimal AgencyCommPct
        {
            get
            {
                return this.agencyCommPctField;
            }
            set
            {
                this.agencyCommPctField = value;
            }
        }

        /// <remarks/>
        public long ParentBillingDocumentID
        {
            get
            {
                return this.parentBillingDocumentIDField;
            }
            set
            {
                this.parentBillingDocumentIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentHeaderDocumentHeaderDataDocumentDates
    {

        private string endDateField;

        private string financialMonthField;

        private string documentDateField;

        private string dueDateField;

        /// <remarks/>
        public string EndDate
        {
            get
            {
                return this.endDateField;
            }
            set
            {
                this.endDateField = value;
            }
        }

        /// <remarks/>
        public string FinancialMonth
        {
            get
            {
                return this.financialMonthField;
            }
            set
            {
                this.financialMonthField = value;
            }
        }

        /// <remarks/>
        public string DocumentDate
        {
            get
            {
                return this.documentDateField;
            }
            set
            {
                this.documentDateField = value;
            }
        }

        /// <remarks/>
        public string DueDate
        {
            get
            {
                return this.dueDateField;
            }
            set
            {
                this.dueDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentHeaderDocumentHeaderDataGrandTotals
    {

        private decimal totalGrossAmountField;

        private decimal totalAgencyAmountField;

        private decimal totalSalesHouseCommAmtField;

        private decimal totalCouponAmtField;

        private decimal totalNetAmountField;

        private long totalUnitCountField;

        private decimal pastDueAmountField;

        private decimal totalAfterTaxField;

        private decimal currentActivityAfterTaxField;

        private decimal closingBalanceField;

        private decimal totalActivityField;

        private decimal totalCreditAndPromotionField;

        /// <remarks/>
        public decimal TotalGrossAmount
        {
            get
            {
                return this.totalGrossAmountField;
            }
            set
            {
                this.totalGrossAmountField = value;
            }
        }

        /// <remarks/>
        public decimal TotalAgencyAmount
        {
            get
            {
                return this.totalAgencyAmountField;
            }
            set
            {
                this.totalAgencyAmountField = value;
            }
        }

        /// <remarks/>
        public decimal TotalSalesHouseCommAmt
        {
            get
            {
                return this.totalSalesHouseCommAmtField;
            }
            set
            {
                this.totalSalesHouseCommAmtField = value;
            }
        }

        /// <remarks/>
        public decimal TotalCouponAmt
        {
            get
            {
                return this.totalCouponAmtField;
            }
            set
            {
                this.totalCouponAmtField = value;
            }
        }

        /// <remarks/>
        public decimal TotalNetAmount
        {
            get
            {
                return this.totalNetAmountField;
            }
            set
            {
                this.totalNetAmountField = value;
            }
        }

        /// <remarks/>
        public long TotalUnitCount
        {
            get
            {
                return this.totalUnitCountField;
            }
            set
            {
                this.totalUnitCountField = value;
            }
        }

        /// <remarks/>
        public decimal PastDueAmount
        {
            get
            {
                return this.pastDueAmountField;
            }
            set
            {
                this.pastDueAmountField = value;
            }
        }

        /// <remarks/>
        public decimal TotalAfterTax
        {
            get
            {
                return this.totalAfterTaxField;
            }
            set
            {
                this.totalAfterTaxField = value;
            }
        }

        /// <remarks/>
        public decimal CurrentActivityAfterTax
        {
            get
            {
                return this.currentActivityAfterTaxField;
            }
            set
            {
                this.currentActivityAfterTaxField = value;
            }
        }

        /// <remarks/>
        public decimal ClosingBalance
        {
            get
            {
                return this.closingBalanceField;
            }
            set
            {
                this.closingBalanceField = value;
            }
        }

        /// <remarks/>
        public decimal TotalActivity
        {
            get
            {
                return this.totalActivityField;
            }
            set
            {
                this.totalActivityField = value;
            }
        }

        /// <remarks/>
        public decimal TotalCreditAndPromotion
        {
            get
            {
                return this.totalCreditAndPromotionField;
            }
            set
            {
                this.totalCreditAndPromotionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentHeaderDocumentHeaderDataTaxEntrySet
    {

        private decimal totalTaxAmountField;

        private decimal totalTaxRateField;

        /// <remarks/>
        public decimal TotalTaxAmount
        {
            get
            {
                return this.totalTaxAmountField;
            }
            set
            {
                this.totalTaxAmountField = value;
            }
        }

        /// <remarks/>
        public decimal TotalTaxRate
        {
            get
            {
                return this.totalTaxRateField;
            }
            set
            {
                this.totalTaxRateField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentItems
    {

        private FinancialDocumentDocumentItemsCampaigns campaignsField;

        /// <remarks/>
        public FinancialDocumentDocumentItemsCampaigns Campaigns
        {
            get
            {
                return this.campaignsField;
            }
            set
            {
                this.campaignsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentItemsCampaigns
    {

        private FinancialDocumentDocumentItemsCampaignsCampaign campaignField;

        /// <remarks/>
        public FinancialDocumentDocumentItemsCampaignsCampaign Campaign
        {
            get
            {
                return this.campaignField;
            }
            set
            {
                this.campaignField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FinancialDocumentDocumentItemsCampaignsCampaign
    {

        private string productField;

        private string pricingModelField;

        private string activityMonthField;

        private string adjustmentTypeField;

        private string adjustmentDateField;

        private long referenceInvoiceNumberField;

        private long idField;

        private string nameField;

        private decimal totalField;

        private long campaignTotalUnitCountField;

        /// <remarks/>
        public string Product
        {
            get
            {
                return this.productField;
            }
            set
            {
                this.productField = value;
            }
        }

        /// <remarks/>
        public string PricingModel
        {
            get
            {
                return this.pricingModelField;
            }
            set
            {
                this.pricingModelField = value;
            }
        }

        /// <remarks/>
        public string ActivityMonth
        {
            get
            {
                return this.activityMonthField;
            }
            set
            {
                this.activityMonthField = value;
            }
        }

        /// <remarks/>
        public string AdjustmentType
        {
            get
            {
                return this.adjustmentTypeField;
            }
            set
            {
                this.adjustmentTypeField = value;
            }
        }

        /// <remarks/>
        public string AdjustmentDate
        {
            get
            {
                return this.adjustmentDateField;
            }
            set
            {
                this.adjustmentDateField = value;
            }
        }

        /// <remarks/>
        public long ReferenceInvoiceNumber
        {
            get
            {
                return this.referenceInvoiceNumberField;
            }
            set
            {
                this.referenceInvoiceNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long CampaignTotalUnitCount
        {
            get
            {
                return this.campaignTotalUnitCountField;
            }
            set
            {
                this.campaignTotalUnitCountField = value;
            }
        }
    }
}