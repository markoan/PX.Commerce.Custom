/****** Object:  Table [dbo].[BCBindingCustom] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME='BCBINDINGCUSTOM' AND XTYPE='U')
BEGIN
CREATE TABLE [dbo].[BCBindingCustom]
(
	-- multi-tenancy support
	[CompanyID]					int NOT NULL DEFAULT ((0)),
	-- key
	[BindingID]					int NOT NULL,

	-- fields
	[ApiBaseUrl]				nvarchar(MAX) NULL,
	[ApiKey]					nvarchar(MAX) NULL,
	[ApiPassword]				nvarchar(MAX) NULL,
	[ApiToken]					nvarchar(MAX) NULL,
	[ApiTokenSecret]			nvarchar(MAX) NULL,
	[StoreUrl]					nvarchar(MAX) NULL,

	[StoreTimeZone]				nvarchar(100) NULL,
	[DefaultCurrency]			nvarchar(12) NULL,
	[StoreName]					nvarchar(250) NULL,

	[BranchID]					int NULL,
	
	-- Notes support
	[NoteID]					uniqueidentifier NULL,
	-- handle concurrency
	[tstamp]					timestamp NOT NULL,

	-- basic audit fields
	[CreatedByID]				uniqueidentifier NOT NULL,
	[CreatedByScreenID]			char(8) NOT NULL,
	[CreatedDateTime]			smalldatetime NOT NULL,
	[LastModifiedByID]			uniqueidentifier NOT NULL,
	[LastModifiedByScreenID]	char(8) NOT NULL,
	[LastModifiedDateTime]		smalldatetime NOT NULL,

	CONSTRAINT [PK_BCBindingCustom] PRIMARY KEY CLUSTERED 
	(
		[CompanyID] ASC,
		[BindingID] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[BCBindingCustom] ADD DEFAULT ((0)) FOR [CompanyID]
END