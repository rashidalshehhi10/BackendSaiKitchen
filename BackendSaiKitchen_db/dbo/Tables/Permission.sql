CREATE TABLE [dbo].[Permission] (
    [PermissionId]   INT           NOT NULL,
    [PermissionName] NVARCHAR (50) NULL,
    [CreatedBy]      INT           NULL,
    [CreatedDate]    NVARCHAR (50) NULL,
    [UpdatedBy]      INT           NULL,
    [UpdatedDate]    NVARCHAR (50) NULL,
    [IsActive]       BIT           NULL,
    [IsDeleted]      BIT           NULL,
    CONSTRAINT [PK_BranchPermission] PRIMARY KEY CLUSTERED ([PermissionId] ASC)
);

