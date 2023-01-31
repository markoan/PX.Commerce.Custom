using PX.Commerce.Core;
using PX.Commerce.Objects;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PX.Commerce.Custom.CCConnector;

namespace PX.Commerce.Custom
{
	[Serializable]
	[PXCacheName("Custom Store Settings")]
	public class BCBindingCustom : IBqlTable
	{
		public class PK : PrimaryKeyOf<BCBindingCustom>.By<BCBindingCustom.bindingID>
		{
			public static BCBindingCustom Find(PXGraph graph, int? binding) => FindBy(graph, binding);
		}

		#region BindingID
		[PXDBInt(IsKey = true)]
		[PXDBDefault(typeof(BCBinding.bindingID))]
		[PXUIField(DisplayName = "Store", Visible = false)]
		[PXParent(typeof(Select<BCBinding, Where<BCBinding.bindingID, Equal<Current<BCBindingCustom.bindingID>>>>))]
		public int? BindingID { get; set; }
		public abstract class bindingID : IBqlField { }
		#endregion

		//Connection

		#region StoreBaseUrl
		[PXDBString(100, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Store Admin URL")]
		[PXDefault()]
		public virtual string ApiBaseUrl { get; set; }
		public abstract class apiBaseUrl : IBqlField { }
		#endregion

		#region ApiKey
		[PXRSACryptString(IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "API Key")]
		[PXDefault()]
		public virtual string ApiKey { get; set; }
		public abstract class apiKey : IBqlField { }
		#endregion

		#region ApiPassword
		[PXRSACryptString(IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "API Password")]
		[PXDefault()]
		public virtual string ApiPassword { get; set; }
		public abstract class apiPassword : IBqlField { }
        #endregion

        #region ApiToken
        [PXRSACryptString(IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "API Token")]
        [PXDefault()]
        public virtual string ApiToken { get; set; }
        public abstract class apiToken : IBqlField { }
        #endregion

        #region ApiTokenSecret
        [PXRSACryptString(IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "API Token Secret")]
        [PXDefault()]
        public virtual string ApiTokenSecret { get; set; }
        public abstract class apiTokenSecret : IBqlField { }
        #endregion

        #region StoreUrl
        [PXDBString(200, IsUnicode = true)]
		[PXUIField(DisplayName = "Store URL", IsReadOnly = true)]
		public virtual string StoreUrl { get; set; }
		public abstract class storeUrl : IBqlField { }
		#endregion

		#region DefaultCurrency
		[PXDBString(12, IsUnicode = true)]
		[PXUIField(DisplayName = "Default Currency", IsReadOnly = true)]
		public virtual string DefaultCurrency { get; set; }
		public abstract class defaultCurrency : IBqlField { }
		#endregion

		#region StoreTimeZone 
		[PXDBString(100, IsUnicode = true)]
		[PXUIField(DisplayName = "Store Time Zone", IsReadOnly = true)]
		public virtual string StoreTimeZone { get; set; }
		public abstract class storeTimeZone : IBqlField { }
        #endregion

        #region StoreName
        [PXDBString(250, IsUnicode = true)]
        [PXUIField(DisplayName = "Store Name", IsReadOnly = true)]
        public virtual string StoreName { get; set; }
        public abstract class storeName : IBqlField { }
        #endregion

        //Audit fields
        #region Audit

        #region NoteID

        public abstract class noteID : PX.Data.IBqlField
        {
        }

        /// <summary>
        /// Identifier of the <see cref="PX.Data.Note">Note</see> object, associated with the document.
        /// </summary>
        /// <value>
        /// Corresponds to the <see cref="PX.Data.Note.NoteID">Note.NoteID</see> field.
        /// </value>
        [PXNote(ShowInReferenceSelector = true)]
        public Guid? NoteID { get; set; }

        #endregion NoteID

        #region tstamp

        public abstract class Tstamp : PX.Data.IBqlField
        {
        }

        protected byte[] _tstamp;

        [PXDBTimestamp()]
        public virtual byte[] tstamp
        {
            get
            {
                return this._tstamp;
            }
            set
            {
                this._tstamp = value;
            }
        }

        #endregion tstamp

        #region CreatedByID

        public abstract class createdByID : PX.Data.IBqlField
        {
        }

        protected Guid? _CreatedByID;

        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID
        {
            get
            {
                return this._CreatedByID;
            }
            set
            {
                this._CreatedByID = value;
            }
        }

        #endregion CreatedByID

        #region CreatedByScreenID

        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }

        protected string _CreatedByScreenID;

        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID
        {
            get
            {
                return this._CreatedByScreenID;
            }
            set
            {
                this._CreatedByScreenID = value;
            }
        }

        #endregion CreatedByScreenID

        #region CreatedDateTime

        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }

        protected DateTime? _CreatedDateTime;

        [PXDBCreatedDateTime()]
        public virtual DateTime? CreatedDateTime
        {
            get
            {
                return this._CreatedDateTime;
            }
            set
            {
                this._CreatedDateTime = value;
            }
        }

        #endregion CreatedDateTime

        #region LastModifiedByID

        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }

        protected Guid? _LastModifiedByID;

        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID
        {
            get
            {
                return this._LastModifiedByID;
            }
            set
            {
                this._LastModifiedByID = value;
            }
        }

        #endregion LastModifiedByID

        #region LastModifiedByScreenID

        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }

        protected string _LastModifiedByScreenID;

        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID
        {
            get
            {
                return this._LastModifiedByScreenID;
            }
            set
            {
                this._LastModifiedByScreenID = value;
            }
        }

        #endregion LastModifiedByScreenID

        #region LastModifiedDateTime

        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        protected DateTime? _LastModifiedDateTime;

        [PXDBLastModifiedDateTime()]
        public virtual DateTime? LastModifiedDateTime
        {
            get
            {
                return this._LastModifiedDateTime;
            }
            set
            {
                this._LastModifiedDateTime = value;
            }
        }

        #endregion LastModifiedDateTime

        #endregion Audit

    }

    [PXPrimaryGraph(new Type[] { typeof(BCStoreMaint) },
					new Type[] { typeof(Where<BCBinding.connectorType, Equal<ccConnectorType>>) })]

	public class BCBindingCustomExtension : PXCacheExtension<BCBinding>
	{
        public static bool IsActive() { return true; }
    }
}
