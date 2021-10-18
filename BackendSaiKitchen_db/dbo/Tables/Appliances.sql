CREATE TABLE [dbo].[Appliances] (
    [AppliancesId]        INT           IDENTITY (1, 1) NOT NULL,
    [KitchenDesignInfoId] INT           NULL,
    [AppliancesName]      NVARCHAR (50) NULL,
    [AppliancesValue]     BIT           NULL,
    [CreatedBy]           INT           NULL,
    [CreatedDate]         NVARCHAR (50) NULL,
    [UpdatedBy]           INT           NULL,
    [UpdatedDate]         NVARCHAR (50) NULL,
    [IsActive]            BIT           NULL,
    [IsDeleted]           BIT           NULL,
    CONSTRAINT [PK_Appliances] PRIMARY KEY CLUSTERED ([AppliancesId] ASC),
    CONSTRAINT [FK_Appliances_KitchenDesignInfo] FOREIGN KEY ([KitchenDesignInfoId]) REFERENCES [dbo].[KitchenDesignInfo] ([KDIId])
);

