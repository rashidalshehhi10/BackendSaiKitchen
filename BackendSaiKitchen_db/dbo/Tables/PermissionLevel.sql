CREATE TABLE [dbo].[PermissionLevel] (
    [PermissionLevelId]   INT           NOT NULL,
    [PermissionLevelName] NVARCHAR (50) NULL,
    [IsActive]            BIT           NULL,
    [IsDeleted]           BIT           NULL,
    [CreatedBy]           INT           NULL,
    [CreatedDate]         NVARCHAR (50) NULL,
    [UpdatedBy]           INT           NULL,
    [UpdatedDate]         NVARCHAR (50) NULL,
    CONSTRAINT [PK_PermissionLevel] PRIMARY KEY CLUSTERED ([PermissionLevelId] ASC)
);

