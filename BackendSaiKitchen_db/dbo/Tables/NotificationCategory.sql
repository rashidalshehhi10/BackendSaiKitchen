CREATE TABLE [dbo].[NotificationCategory] (
    [NotificationCategoryId]   INT           NOT NULL,
    [NotificationCategoryName] NVARCHAR (50) NULL,
    [IsActive]                 BIT           NULL,
    [IsDeleted]                BIT           NULL,
    [CreatedBy]                INT           NULL,
    [CreatedDate]              NVARCHAR (50) NULL,
    [UpdatedBy]                INT           NULL,
    [UpdatedDate]              NVARCHAR (50) NULL,
    CONSTRAINT [PK_NotificationCateory] PRIMARY KEY CLUSTERED ([NotificationCategoryId] ASC)
);

