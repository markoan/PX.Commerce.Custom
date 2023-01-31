<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormTab.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="BC201030.aspx.cs" Inherits="Page_BC201030" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormTab.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%" TypeName="PX.Commerce.Custom.BCStoreMaint" PrimaryView="Bindings">
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" Style="z-index: 100" Width="100%" DataMember="Bindings" TabIndex="6500">
		<Template>
			<px:PXLayoutRule runat="server" StartRow="True" ControlSize="L" LabelsWidth="SM"></px:PXLayoutRule>
			<px:PXDropDown AllowEdit="False" CommitChanges="True" runat="server" ID="edDropdownConnectorType" DataField="ConnectorType"></px:PXDropDown>
			<px:PXSelector AutoRefresh="True" AllowEdit="False" CommitChanges="True" runat="server" ID="edSelectorBindingName" DataField="BindingName"></px:PXSelector>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule70" StartColumn="True" />
			<px:PXCheckBox CommitChanges="True" AlignLeft="True" runat="server" ID="edBooleanIsActive" DataField="IsActive"></px:PXCheckBox>
			<px:PXCheckBox AlignLeft="True" runat="server" ID="edBooleanIsDefault" DataField="IsDefault"></px:PXCheckBox>
		</Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" runat="Server">
	<px:PXTab ID="tab" runat="server" Width="100%" Height="150px" DataSourceID="ds" DataMember="CurrentStore">
		<Items>
			<px:PXTabItem Text="Connection Settings" Visible="True">
				<Template>
					<px:PXLayoutRule runat="server" StartColumn="True" ID="edColumn1"></px:PXLayoutRule>
					<px:PXLayoutRule runat="server" ControlSize="XL" LabelsWidth="SM"  StartGroup="True" GroupCaption="Store Settings"></px:PXLayoutRule>
					<px:PXFormView RenderStyle="Simple" DataMember="CurrentBinding" runat="server" ID="frmCurrentBinding1" DataSourceID="ds" TabIndex="18700">
						<Template>
							<px:PXLayoutRule StartColumn="True" ControlSize="XL" LabelsWidth="SM" runat="server" StartRow="True"></px:PXLayoutRule>
							<px:PXTextEdit ID="edStoreAdminUrl" runat="server" AlreadyLocalized="False" DataField="ApiBaseUrl" CommitChanges="True" IsClientControl="True"></px:PXTextEdit>
							<px:PXTextEdit ID="edApiKey" runat="server" AlreadyLocalized="False" DataField="ApiKey" CommitChanges="True" IsClientControl="True"></px:PXTextEdit>
							<px:PXTextEdit ID="edApiPassword" runat="server" AlreadyLocalized="False" DataField="ApiPassword" CommitChanges="True" IsClientControl="True"></px:PXTextEdit>
							<px:PXTextEdit ID="edApiTpken" runat="server" AlreadyLocalized="False" DataField="ApiToken" CommitChanges="True" IsClientControl="True"></px:PXTextEdit>
							<px:PXTextEdit ID="edApiTokenSecret" runat="server" AlreadyLocalized="False" DataField="ApiTokenSecret" CommitChanges="True" IsClientControl="True"></px:PXTextEdit>
						</Template>
					</px:PXFormView>
					<px:PXFormView RenderStyle="Simple" DataMember="CurrentBinding" runat="server" ID="PXFormView1" DataSourceID="ds" TabIndex="18900">
						<Template>
							<px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="System Settings" LabelsWidth="SM" ControlSize="XL"></px:PXLayoutRule>
							<px:PXSelector runat="server" ID="CstPXSelectorLocaleName" DataField="LocaleName"></px:PXSelector>
						</Template>
					</px:PXFormView>
					<px:PXLayoutRule runat="server" StartColumn="True"></px:PXLayoutRule>
					<px:PXLayoutRule runat="server" ControlSize="L" LabelsWidth="SM" StartGroup="True" GroupCaption="Store Properties"></px:PXLayoutRule>
					<px:PXFormView RenderStyle="Simple" DataMember="CurrentBinding" runat="server" ID="PXFormView2" DataSourceID="ds" TabIndex="19100">
						<Template>
							<px:PXLayoutRule StartColumn="True" ControlSize="L" LabelsWidth="SM" runat="server" StartRow="True"></px:PXLayoutRule>
							<px:PXTextEdit runat="server" ID="edCurrency" DataField="DefaultCurrency" AlreadyLocalized="False" IsClientControl="True"></px:PXTextEdit>
							<px:PXTextEdit runat="server" ID="edStoreTimeZone" DataField="StoreTimeZone" AlreadyLocalized="False" IsClientControl="True"></px:PXTextEdit>
							<px:PXTextEdit runat="server" ID="edStoreName" DataField="StoreName" AlreadyLocalized="False" IsClientControl="True"></px:PXTextEdit>
						</Template>
					</px:PXFormView>
					<px:PXLayoutRule runat="server" ControlSize="L" LabelsWidth="SM" StartGroup="True" GroupCaption="Store Administrator Details"></px:PXLayoutRule>
					<px:PXFormView RenderStyle="Simple" DataMember="CurrentBinding" runat="server" ID="PXFormView3">
						<Template>
							<px:PXLayoutRule runat="server"  ID="PXLayoutRule12" LabelsWidth="SM" ControlSize="L"></px:PXLayoutRule>
							<px:PXSelector runat="server" ID="edBindingAdministrator" DataField="BindingAdministrator"></px:PXSelector>
							<px:PXLayoutRule runat="server" ControlSize="L" LabelsWidth="SM" StartGroup="True" GroupCaption="License Restrictions"></px:PXLayoutRule>
							<px:PXNumberEdit runat="server" ID="edAllowedStores" DataField="AllowedStores"></px:PXNumberEdit>
						</Template>
					</px:PXFormView>
				</Template>
				<ContentLayout SpacingSize="Medium" ControlSize="XM" LabelsWidth="M"></ContentLayout>
			</px:PXTabItem>
			<px:PXTabItem Text="Entity Settings">
				<Template>
					<px:PXGrid MatrixMode="True" runat="server" SkinID="Details" Width="100%" ID="CstPXGrid60" DataSourceID="ds">
						<AutoSize Enabled="True" Container="Window"></AutoSize>
						<ActionBar DefaultAction="navigate">
							<Actions>
								<AddNew ToolBarVisible="False"></AddNew>
								<Delete ToolBarVisible="False"></Delete>
								<ExportExcel ToolBarVisible="False"></ExportExcel>
							</Actions>
						</ActionBar>
						<Levels>
							<px:PXGridLevel DataMember="Entities">
								<Columns>
									<px:PXGridColumn Type="CheckBox" TextAlign="Center" DataField="IsActive" Width="60px" CommitChanges="True"></px:PXGridColumn>
									<px:PXGridColumn LinkCommand="Navigate" DataField="EntityType"></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="Direction"></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="PrimarySystem" Width="120px"></px:PXGridColumn>
									<px:PXGridColumn DataField="ImportRealTimeStatus" Width="120px"></px:PXGridColumn>
									<px:PXGridColumn DataField="ExportRealTimeStatus" Width="120px"></px:PXGridColumn>
									<px:PXGridColumn DataField="RealTimeMode" Width="130"></px:PXGridColumn>
									<px:PXGridColumn DataField="MaxAttemptCount" Width="120"></px:PXGridColumn>
								</Columns>
								<RowTemplate>
									<px:PXNumberEdit runat="server" ID="CstPXNumberEdit70" DataField="MaxAttemptCount" AlreadyLocalized="False"></px:PXNumberEdit>
								</RowTemplate>
							</px:PXGridLevel>
						</Levels>
						<Mode AllowAddNew="False" AllowDelete="False" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Customer Settings">
				<Template>
					<px:PXLayoutRule runat="server" ControlSize="M" LabelsWidth="M" StartColumn="True" StartGroup="false">
					</px:PXLayoutRule>
					<px:PXLayoutRule GroupCaption="Customer" runat="server" StartGroup="True"></px:PXLayoutRule>
					<px:PXSelector AllowEdit="True" ID="edCustomerClassID" runat="server" DataField="CustomerClassID" CommitChanges="True" edit="1">
					</px:PXSelector>
					<px:PXSegmentMask runat="server" DataField="CustomerTemplate" ID="CstPXSegmentMask28"></px:PXSegmentMask>
					<px:PXSelector runat="server" ID="CstPXSelector27" DataField="CustomerNumberingID" AllowEdit="True" edit="1"></px:PXSelector>
					<px:PXSegmentMask runat="server" DataField="LocationTemplate" ID="CstPXSegmentMask32"></px:PXSegmentMask>
					<px:PXSelector runat="server" ID="CstPXSelector31" DataField="LocationNumberingID" AllowEdit="True" edit="1"></px:PXSelector>
					<px:PXSelector runat="server" ID="CstPXSelector29" DataField="InventoryNumberingID" AllowEdit="True" edit="1"></px:PXSelector>
					<px:PXSegmentMask runat="server" DataField="InventoryTemplate" ID="CstPXSegmentMask30"></px:PXSegmentMask>
					<px:PXSegmentMask AllowEdit="True" runat="server" ID="CstPXSegmentMask49" DataField="GuestCustomerID"></px:PXSegmentMask>

				</Template>
				<ContentLayout ControlSize="XM" LabelsWidth="M"></ContentLayout>
			</px:PXTabItem>
			<px:PXTabItem Text="Inventory Settings">
				<Template>
					<px:PXLayoutRule ControlSize="L" LabelsWidth="M" runat="server" StartGroup="True" GroupCaption="Inventory Settings"></px:PXLayoutRule>
					<px:PXSelector AllowEdit="True" CommitChanges="True" runat="server" ID="CstPXSelector10" DataField="StockItemClassID" edit="1"></px:PXSelector>
					<px:PXSelector AllowEdit="True" CommitChanges="True" runat="server" ID="CstPXSelector9" DataField="NonStockItemClassID" edit="1"></px:PXSelector>
					<px:PXDropDown CommitChanges="True" runat="server" ID="edVisibility" DataField="Visibility" IsClientControl="True"></px:PXDropDown>
					<px:PXDropDown CommitChanges="True" runat="server" ID="CstPXDropDown57" DataField="Availability" IsClientControl="True"></px:PXDropDown>
					<px:PXDropDown CommitChanges="True" runat="server" ID="CstPXDropDown58" DataField="NotAvailMode" IsClientControl="True"></px:PXDropDown>
					<px:PXDropDown CommitChanges="True" runat="server" DataField="AvailabilityCalcRule" ID="CstPXDropDown45" IsClientControl="True"></px:PXDropDown>
					<px:PXDropDown CommitChanges="True" runat="server" ID="CstPXDropDown71" DataField="WarehouseMode" IsClientControl="True"></px:PXDropDown>
					<px:PXLayoutRule ControlSize="L" LabelsWidth="M" GroupCaption="Warehouse Mapping for Inventory Export" runat="server" ID="CstPXLayoutRule762" StartGroup="True"></px:PXLayoutRule>
					<px:PXGrid Height="200px" Width="510px" SyncPosition="True" SkinID="Inquire" MatrixMode="True" runat="server" ID="gridExportLocations" DataSourceID="ds">
						<Levels>
							<px:PXGridLevel DataMember="ExportLocations">
								<RowTemplate>
									<px:PXSelector AutoRefresh="True" CommitChanges="True" runat="server" DataField="LocationID" ID="pxsLocationId"></px:PXSelector>
									<px:PXTextEdit runat="server" DataField="Description" AlreadyLocalized="False" ID="pxtLocationDescription" IsClientControl="True"></px:PXTextEdit>
									<px:PXSelector runat="server" ID="pxsSiteId" DataField="SiteID" CommitChanges="True"></px:PXSelector>
									<px:PXDropDown runat="server" CommitChanges="True" ID="pxddExtLocationId" DataField="ExternalLocationID"></px:PXDropDown>
								</RowTemplate>
								<Columns>
									<px:PXGridColumn CommitChanges="True" DataField="SiteID" Width="140px"></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="LocationID" Width="140px"></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="ExternalLocationID" Width="200px"></px:PXGridColumn>
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<Mode InitNewRow="True" />
						<ActionBar>
							<Actions>
								<AddNew Enabled="True" /></Actions></ActionBar>
						<ActionBar>
							<Actions>
								<Delete Enabled="True" /></Actions></ActionBar>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Order Settings">
				<Template>
					<px:PXLayoutRule LabelsWidth="SM" ControlSize="M" runat="server" StartColumn="True"></px:PXLayoutRule>
					<px:PXLayoutRule LabelsWidth="SM" ControlSize="M" runat="server" StartGroup="True" GroupCaption="General"></px:PXLayoutRule>
					<px:PXFormView RenderStyle="Simple" DataMember="CurrentBinding" runat="server" ID="frmCurrentBinding1" DataSourceID="ds">
						<Template>
							<px:PXLayoutRule StartColumn="True" LabelsWidth="SM" ControlSize="M" runat="server" StartRow="True"></px:PXLayoutRule>
							<px:PXSegmentMask runat="server" ID="edBranchID" DataField="BranchID" AllowEdit="True"></px:PXSegmentMask>
						</Template>
					</px:PXFormView>
					<px:PXLayoutRule LabelsWidth="SM" ControlSize="M" GroupCaption="Order" runat="server" StartGroup="True"></px:PXLayoutRule>
					<px:PXSelector CommitChanges="True" AllowEdit="True" runat="server" ID="edOrderTpe" DataField="OrderType" edit="1"></px:PXSelector>
					<px:PXDropDown runat="server" ID="CstPXDropDown1" DataField="OtherSalesOrderTypes" IsClientControl="True"></px:PXDropDown>
					<px:PXSelector CommitChanges="True" AllowEdit="True" runat="server" ID="edReturnOrderType" DataField="ReturnOrderType" edit="1"></px:PXSelector>
					<px:PXSelector AllowEdit="True" runat="server" ID="edRefundItem" DataField="RefundAmountItemID" edit="1"></px:PXSelector>
					<px:PXSelector AllowEdit="True" runat="server" ID="edReasonCode" DataField="ReasonCode" edit="1"></px:PXSelector>
					<px:PXDropDown ID="edTimeZone" runat="server" DataField="OrderTimeZone" IsClientControl="True" />
					<px:PXDropDown runat="server" ID="CstPXDropDown80" DataField="PostDiscounts" CommitChanges="True" IsClientControl="True"></px:PXDropDown>
					<px:PXSelector AllowEdit="True" runat="server" ID="CstPXSelector81" DataField="GiftCertificateItemID" edit="1"></px:PXSelector>
					<px:PXDateTimeEdit runat="server" ID="CstPXDateTimeSyncFrom" DataField="SyncOrdersFrom" AlreadyLocalized="False" IsClientControl="True" />
					<px:PXLayoutRule LabelsWidth="SM"  ControlSize="M" runat="server" StartGroup="True" GroupCaption="Taxes"></px:PXLayoutRule>
					<px:PXCheckBox runat="server" ID="CstPXCheckBox2" DataField="TaxSynchronization" CommitChanges="True" AlreadyLocalized="False" IsClientControl="True"></px:PXCheckBox>
					<px:PXSelector AutoRefresh="True" runat="server" ID="CstPXSelector118" DataField="DefaultTaxZoneID" CommitChanges="True"></px:PXSelector>
					<px:PXCheckBox runat="server" ID="CstPXCheckBox1" DataField="UseAsPrimaryTaxZone" AlreadyLocalized="False" IsClientControl="True"/>
					<px:PXLayoutRule LabelsWidth="SM"  ControlSize="M" runat="server" StartGroup="True" GroupCaption="Substitution Lists"></px:PXLayoutRule>
					<px:PXSelector CommitChanges="True" runat="server" ID="PXSelector2" DataField="TaxSubstitutionListID"></px:PXSelector>
					<px:PXSelector CommitChanges="True" runat="server" ID="PXSelector3" DataField="TaxCategorySubstitutionListID"></px:PXSelector>
					<px:PXLayoutRule ControlSize="L" LabelsWidth="M" runat="server" StartColumn="True"></px:PXLayoutRule>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Payment Settings">
				<Template>
					<px:PXLayoutRule ControlSize="L" LabelsWidth="M" runat="server" StartColumn="True"></px:PXLayoutRule>
					<px:PXLayoutRule GroupCaption="Payment Method Mapping" runat="server" StartGroup="True"></px:PXLayoutRule>
					<px:PXGrid SyncPosition="True" TabIndex="30400" Height="160px" runat="server" ID="PaymentsMethods" Caption="Base Currency Payment Methods" MatrixMode="True" SkinID="Inquire" Width="100%" DataSourceID="ds">
						<Levels>
							<px:PXGridLevel DataMember="PaymentMethods">
								<Columns>
									<px:PXGridColumn CommitChanges="True" TextAlign="Center" Type="CheckBox" DataField="Active" Width="80px"></px:PXGridColumn>
									<px:PXGridColumn DataField="StorePaymentMethod" Width="200px"></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="PaymentMethodID" Width="140px"></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="CashAccountID" Width="140px"></px:PXGridColumn>
									<px:PXGridColumn CommitChanges="True" DataField="ProcessingCenterID" Width="120px" />
									<px:PXGridColumn DataField="CuryID" Width="80px"></px:PXGridColumn>
									<px:PXGridColumn TextAlign="Center" Type="CheckBox" DataField="ReleasePayments" Width="80px"></px:PXGridColumn>
									<px:PXGridColumn DataField="StoreOrderPaymentMethod" Width="200px" ></px:PXGridColumn>
									<px:PXGridColumn  TextAlign="Center" Type="CheckBox" DataField="ProcessRefunds" Width="100px"></px:PXGridColumn>
								 </Columns>
								
								<RowTemplate>
									<px:PXSelector AutoRefresh="True" runat="server" ID="CstPXSelector112" DataField="CashAccountID"></px:PXSelector>
								</RowTemplate>
							</px:PXGridLevel>
						</Levels>
						<Mode AllowDelete="True" AllowAddNew="True"></Mode>
						<ActionBar ActionsVisible="True" DefaultAction="">
							<Actions>
								<AddNew Enabled="True" ToolBarVisible="Top" MenuVisible="True"></AddNew>
								<Delete Enabled="True" ToolBarVisible="Top"></Delete>
							</Actions>
						</ActionBar>
					</px:PXGrid>
				</Template>
			</px:PXTabItem>
			<px:PXTabItem Text="Shipping Settings">
				<Template>
					<px:PXGrid Width="100%" SyncPosition="True" AllowPaging="False" SkinID="Inquire" AutoAdjustColumns="True" MatrixMode="True" runat="server" ID="PXGrid1">
						<Levels>
							<px:PXGridLevel DataMember="ShippingMappings">
								<Columns>
									<px:PXGridColumn CommitChanges="True" TextAlign="Center" Type="CheckBox" DataField="Active" Width="60"></px:PXGridColumn>
									<px:PXGridColumn DataField="ShippingZone" Width="150"></px:PXGridColumn>
									<px:PXGridColumn DataField="ShippingMethod" Width="180"></px:PXGridColumn>
									<px:PXGridColumn DataField="CarrierID" Width="140"></px:PXGridColumn>
									<px:PXGridColumn DataField="ZoneID" Width="120"></px:PXGridColumn>
									<px:PXGridColumn DataField="ShipTermsID" Width="120"></px:PXGridColumn>
								</Columns>
							</px:PXGridLevel>
						</Levels>
						<ActionBar>
							<Actions>
								<AddNew Enabled="True" />
								<Delete Enabled="True" />
							</Actions>
						</ActionBar>
						<AutoSize Enabled="true" />
					</px:PXGrid>
				</Template>
			</px:PXTabItem>

		</Items>
		<AutoSize Container="Window" Enabled="True" MinHeight="150"></AutoSize>
	</px:PXTab>
</asp:Content>
