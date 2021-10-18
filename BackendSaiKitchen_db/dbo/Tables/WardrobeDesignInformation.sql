CREATE TABLE [dbo].[WardrobeDesignInformation] (
    [WDIId]            INT            IDENTITY (1, 1) NOT NULL,
    [WDIClosetType]    NVARCHAR (50)  NULL,
    [WDIBoardModel]    NVARCHAR (50)  NULL,
    [WDISelectedColor] NVARCHAR (50)  NULL,
    [WDICeilingHeight] NVARCHAR (50)  NULL,
    [WDIClosetHeight]  NVARCHAR (50)  NULL,
    [WDIStorageDoor]   BIT            NULL,
    [WDIDoorDesign]    NVARCHAR (50)  NULL,
    [WDIHandleDesign]  NVARCHAR (50)  NULL,
    [WDIDoorMaterial]  NVARCHAR (50)  NULL,
    [WDINotes]         NVARCHAR (MAX) NULL,
    [CreatedBy]        INT            NULL,
    [CreatedDate]      NVARCHAR (50)  NULL,
    [UpdatedBy]        INT            NULL,
    [UpdatedDate]      NVARCHAR (50)  NULL,
    [IsActive]         BIT            NULL,
    [IsDeleted]        BIT            NULL,
    CONSTRAINT [PK_WardrobeDesignInformation] PRIMARY KEY CLUSTERED ([WDIId] ASC)
);

