CREATE TABLE [dbo].[PermissionRole] (
    [PermissionRoleId]  INT           IDENTITY (1, 1) NOT NULL,
    [PermissionId]      INT           NULL,
    [BranchRoleId]      INT           NULL,
    [PermissionLevelId] INT           NULL,
    [CreatedBy]         INT           NULL,
    [CreatedDate]       NVARCHAR (50) NULL,
    [UpdatedBy]         INT           NULL,
    [UpdatedDate]       NVARCHAR (50) NULL,
    [IsActive]          BIT           CONSTRAINT [DF_PermissionRole_IsActive] DEFAULT ('true') NULL,
    [IsDeleted]         BIT           CONSTRAINT [DF_PermissionRole_IsDeleted] DEFAULT ('false') NULL,
    CONSTRAINT [PK_PermissionRole] PRIMARY KEY CLUSTERED ([PermissionRoleId] ASC),
    CONSTRAINT [FK_PermissionRole_BranchRole] FOREIGN KEY ([BranchRoleId]) REFERENCES [dbo].[BranchRole] ([BranchRoleId]),
    CONSTRAINT [FK_PermissionRole_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([PermissionId]),
    CONSTRAINT [FK_PermissionRole_PermissionLevel] FOREIGN KEY ([PermissionLevelId]) REFERENCES [dbo].[PermissionLevel] ([PermissionLevelId])
);

