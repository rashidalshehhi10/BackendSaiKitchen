CREATE TABLE [dbo].[Building] (
    [BuildingId]             INT            IDENTITY (1, 1) NOT NULL,
    [BuildingAddress]        NVARCHAR (500) NULL,
    [BuildingTypeOfUnit]     NVARCHAR (500) NULL,
    [BuildingCondition]      NVARCHAR (500) NULL,
    [BuildingFloor]          NVARCHAR (500) NULL,
    [BuildingReconstruction] BIT            NULL,
    [IsActive]               BIT            NULL,
    [IsDeleted]              BIT            NULL,
    [CreatedBy]              INT            NULL,
    [CreatedDate]            NVARCHAR (50)  NULL,
    [UpdatedBy]              INT            NULL,
    [UpdatedDate]            NVARCHAR (50)  NULL,
    CONSTRAINT [PK_bUILDING] PRIMARY KEY CLUSTERED ([BuildingId] ASC)
);

