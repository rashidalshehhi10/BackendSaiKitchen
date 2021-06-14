CREATE TABLE [dbo].[WayOfContact] (
    [WayOfContactId]   INT           NOT NULL,
    [WayOfContactName] NVARCHAR (50) NULL,
    [IsActive]         BIT           NULL,
    [IsDeleted]        BIT           NULL,
    [CreatedBy]        INT           NULL,
    [CreatedDate]      NVARCHAR (50) NULL,
    [UpdatedBy]        INT           NULL,
    [UpdatedDate]      NVARCHAR (50) NULL,
    CONSTRAINT [PK_WayOfContact] PRIMARY KEY CLUSTERED ([WayOfContactId] ASC)
);

