CREATE TABLE [dbo].[KitchenDesignInfo] (
    [KDIId]                         INT            IDENTITY (1, 1) NOT NULL,
    [KDIKitchenType]                NVARCHAR (50)  NULL,
    [KDIBoardModelCarcass]          NVARCHAR (50)  NULL,
    [KDIBoardModelCarcassColor]     NVARCHAR (50)  NULL,
    [KDIBoradModelDoorShutter]      NVARCHAR (50)  NULL,
    [KDIBoardModelDoorShutterColor] NCHAR (10)     NULL,
    [KDIBaseUnitHeight]             NVARCHAR (50)  NULL,
    [KDIWallUnitHeight]             NVARCHAR (50)  NULL,
    [KDINotes]                      NVARCHAR (MAX) NULL,
    [CreatedBy]                     INT            NULL,
    [CreatedDate]                   NVARCHAR (50)  NULL,
    [UpdatedBy]                     INT            NULL,
    [UpdatedDate]                   NVARCHAR (50)  NULL,
    [IsActive]                      BIT            NULL,
    [IsDeleted]                     BIT            NULL,
    CONSTRAINT [PK_KitchenDesignInfo] PRIMARY KEY CLUSTERED ([KDIId] ASC)
);

