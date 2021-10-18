CREATE TABLE [dbo].[Accesories] (
    [AccesoriesId]         INT            IDENTITY (1, 1) NOT NULL,
    [WardrobeDesignInfoId] INT            NULL,
    [AccesoriesName]       NVARCHAR (500) NULL,
    [AccesoriesValue]      BIT            NULL,
    [CreatedBy]            INT            NULL,
    [CreatedDate]          NVARCHAR (50)  NULL,
    [UpdatedBy]            INT            NULL,
    [UpdatedDate]          NVARCHAR (50)  NULL,
    [IsActive]             BIT            NULL,
    [IsDeleted]            BIT            NULL,
    CONSTRAINT [PK_Accesories] PRIMARY KEY CLUSTERED ([AccesoriesId] ASC),
    CONSTRAINT [FK_Accesories_WardrobeDesignInformation] FOREIGN KEY ([WardrobeDesignInfoId]) REFERENCES [dbo].[WardrobeDesignInformation] ([WDIId])
);

