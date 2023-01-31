using PX.Commerce.Core;
using PX.Commerce.Core.API;
using PX.Commerce.Custom.API.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Objects.SO;

namespace PX.Commerce.Custom
{
	#region MappedEntity
	public abstract class CCMappedEntity<ExternType, LocalType> : MappedEntity<ExternType, LocalType>
		where ExternType : BCAPIEntity, IExternEntity
		where LocalType : CBAPIEntity, ILocalEntity
	{
		public CCMappedEntity(String entType)
			: base(CCConnector.TYPE, entType)
		{ }
		public CCMappedEntity(BCSyncStatus status)
			: base(status)
		{
		}
		public CCMappedEntity(String entType, LocalType entity, Guid? id, DateTime? timestamp)
			: base(CCConnector.TYPE, entType, entity, id, timestamp)
		{
		}
		public CCMappedEntity(String entType, ExternType entity, String id, DateTime? timestamp)
			: base(CCConnector.TYPE, entType, entity, id, timestamp)
		{
		}
		public CCMappedEntity(String entType, ExternType entity, String id, String hash)
			: base(CCConnector.TYPE, entType, entity, id, hash)
		{
		}
	}
	#endregion

	#region MappedContact
	public class MappedContact : CCMappedEntity<ContactData, Contact>
	{
		public const String TYPE = CCEntitiesAttributes.Contact;

		public MappedContact()
			: base(TYPE)
		{ }
		public MappedContact(Contact entity, Guid? id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
		public MappedContact(ContactData entity, String id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
	}
	#endregion

	#region MappedLocation
	public class MappedLocation : CCMappedEntity<LocationData, CustomerLocation>
	{
		public const String TYPE = BCEntitiesAttribute.Address;

		public MappedLocation()
			: base(TYPE)
		{ }
		public MappedLocation(BCSyncStatus status)
			: base(status) { }

		public MappedLocation(CustomerLocation entity, Guid? id, DateTime? timestamp, Int32? parent)
			: base(TYPE, entity, id, timestamp)
		{
			ParentID = parent;
		}
		public MappedLocation(LocationData entity, String id, String hash, Int32? parent)
			: base(TYPE, entity, id, hash)
		{
			ParentID = parent;
		}
	}

	public class MappedAddress : CCMappedEntity<LocationData, Address>
	{
		public const String TYPE = BCEntitiesAttribute.Address;

		public MappedAddress()
			: base(TYPE)
		{ }
		public MappedAddress(BCSyncStatus status)
			: base(status) { }

		public MappedAddress(Address entity, Guid? id, DateTime? timestamp, Int32? parent)
			: base(TYPE, entity, id, timestamp)
		{
			ParentID = parent;
		}
		public MappedAddress(LocationData entity, String id, String hash, Int32? parent)
			: base(TYPE, entity, id, hash)
		{
			ParentID = parent;
		}
	}

	public class MappedContactAddress : CCMappedEntity<API.REST.CAddressDataSingle, Address>
	{
		public const String TYPE = BCEntitiesAttribute.Address;

		public MappedContactAddress()
			: base(TYPE)
		{ }
		public MappedContactAddress(BCSyncStatus status)
			: base(status) { }

		public MappedContactAddress(Address entity, Guid? id, DateTime? timestamp, Int32? parent)
			: base(TYPE, entity, id, timestamp)
		{
			ParentID = parent;
		}
		public MappedContactAddress(API.REST.CAddressDataSingle entity, String id, String hash, Int32? parent)
			: base(TYPE, entity, id, hash)
		{
			ParentID = parent;
		}
	}


	#endregion


	#region MappedCustomer
	public class MappedCustomer : CCMappedEntity<AccountData, Core.API.Customer>
	{
		public const String TYPE = BCEntitiesAttribute.Customer;

		public MappedCustomer()
			: base(TYPE)
		{ }
		public MappedCustomer(Core.API.Customer entity, Guid? id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
		public MappedCustomer(AccountData entity, String id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
	}
	#endregion


	#region MappedStockItem
	public class MappedStockItem : CCMappedEntity<ProductData, StockItem>
    {
		public const String TYPE = BCEntitiesAttribute.StockItem;

		public MappedStockItem() : base (TYPE) { }
		
		public MappedStockItem(StockItem entity, Guid? id, DateTime? timestamp) : base (TYPE, entity, id, timestamp) { }

		public MappedStockItem(ProductData entity, String id, DateTime? timestamp) : base (TYPE, entity, id, timestamp) { }
    }
	#endregion

	#region MappedAvailability
	public class MappedAvailability : CCMappedEntity<ProductQtyData, StorageDetailsResult>
	{
		public const String TYPE = BCEntitiesAttribute.ProductAvailability;

		public MappedAvailability()
			: base(TYPE)
		{ }
		public MappedAvailability(StorageDetailsResult entity, Guid? id, DateTime? timestamp, Int32? parent)
			: base(TYPE, entity, id, timestamp)
		{
			ParentID = parent;
			UpdateParentExternTS = true;
		}
		public MappedAvailability(ProductQtyData entity, String id, DateTime? timestamp, Int32? parent)
			: base(TYPE, entity, id, timestamp)
		{
			ParentID = parent;
			UpdateParentExternTS = true;
		}
	}
	#endregion

	#region MappedNonStockItem
	public class MappedNonStockItem : CCMappedEntity<ProductData, NonStockItem>
	{
		public const String TYPE = BCEntitiesAttribute.NonStockItem;

		public MappedNonStockItem()
			: base(TYPE)
		{ }
		public MappedNonStockItem(NonStockItem entity, Guid? id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
		public MappedNonStockItem(ProductData entity, String id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
	}
	#endregion

	#region MappedTemplateItem
	public class MappedTemplateItem : CCMappedEntity<ProductData, TemplateItems>
	{
		public const String TYPE = BCEntitiesAttribute.ProductWithVariant;

		public MappedTemplateItem()
			: base(TYPE)
		{ }
		public MappedTemplateItem(TemplateItems entity, Guid? id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
		public MappedTemplateItem(ProductData entity, String id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
	}
	#endregion

	#region MappedProductImage
	public class MappedProductImage : CCMappedEntity<ProductImageData, ItemImageDetails>
    {
		public const String TYPE = BCEntitiesAttribute.ProductImage;

		public MappedProductImage() : base (TYPE) { }

		public MappedProductImage(ItemImageDetails entity, Guid? id, DateTime? timestamp, Int32? parent) : base(TYPE, entity, id, timestamp) 
		{
			ParentID = parent;
		}

		public MappedProductImage(ProductImageData entity, String id, DateTime? timestamp, Int32? parent) : base(TYPE, entity, id, timestamp) 
		{
			ParentID = parent;
		}
    }
	#endregion

	#region MappedProductCategory
	public class MappedCategory : CCMappedEntity<ProductCategoryData, BCItemSalesCategory>
	{
		public const String TYPE = BCEntitiesAttribute.SalesCategory;

		public MappedCategory() : base(TYPE) { }
		public MappedCategory(BCItemSalesCategory entity, Guid? id, DateTime? timestamp)
				: base(TYPE, entity, id, timestamp) { }
		public MappedCategory(ProductCategoryData entity, String id, DateTime? timestamp)
				: base(TYPE, entity, id, timestamp) { }
		public MappedCategory(ProductCategoryData entity, String id, String hash)
				: base(TYPE, entity, id, hash) { }
	}
	#endregion

	#region MappedOrder
	public class MappedOrder : CCMappedEntity<OrderData, PX.Commerce.Core.API.SalesOrder>
	{
		public const String TYPE = BCEntitiesAttribute.Order;

		public MappedOrder()
			: base(TYPE)
		{ }
		public MappedOrder(SalesOrder entity, Guid? id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
		public MappedOrder(OrderData entity, String id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
	}
	#endregion

	#region MappedPayment
	public class MappedPayment : CCMappedEntity<InvoiceData, PX.Commerce.Core.API.Payment>
	{
		public const String TYPE = BCEntitiesAttribute.Payment;

		public MappedPayment()
			: base(TYPE)
		{ }
		public MappedPayment(Payment entity, Guid? id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
		public MappedPayment(InvoiceData entity, String id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
	}
	#endregion

	#region MappedShipment
	public class MappedShipment : CCMappedEntity<ShipmentData, PX.Commerce.Core.API.BCShipments>
	{
		public const String TYPE = BCEntitiesAttribute.Shipment;

		public MappedShipment()
			: base(TYPE)
		{ }
		public MappedShipment(BCShipments entity, Guid? id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
		public MappedShipment(ShipmentData entity, String id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
	}
	#endregion

	#region MappedInvoice
	public class MappedInvoice : CCMappedEntity<InvoicePostData, Invoice>
	{
		public const String TYPE = "IV";

		public MappedInvoice()
			: base(TYPE)
		{ }
		public MappedInvoice(Invoice entity, Guid? id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
		public MappedInvoice(InvoicePostData entity, String id, DateTime? timestamp)
			: base(TYPE, entity, id, timestamp) { }
	}
	#endregion

	#region MappedPriceList
	public class MappedPriceList : CCMappedEntity<PriceList, PriceListSalesPrice>
	{
		public const String TYPE = BCEntitiesAttribute.PriceList;

		public MappedPriceList()
			: base(TYPE)
		{ }
		public MappedPriceList(PriceListSalesPrice entity, Guid? id, DateTime? timestamp, Int32? parent)
			: base(TYPE, entity, id, timestamp)
		{
			ParentID = parent;
		}
		public MappedPriceList(PriceList entity, String id, DateTime? timestamp, Int32? parent)
			: base(TYPE, entity, id, timestamp)
		{
			ParentID = parent;
		}
	}
	#endregion
}
