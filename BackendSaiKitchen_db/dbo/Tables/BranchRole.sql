CREATE TABLE [dbo].[BranchRole] (
    [BranchRoleId]          INT            IDENTITY (1, 1) NOT NULL,
    [BranchRoleName]        NVARCHAR (50)  NULL,
    [BranchRoleDescription] NVARCHAR (500) NULL,
    [RoleTypeId]            INT            NULL,
    [CreatedBy]             INT            NULL,
    [CreatedDate]           NVARCHAR (50)  NULL,
    [UpdatedBy]             INT            NULL,
    [UpdatedDate]           NVARCHAR (50)  NULL,
    [IsActive]              BIT            NULL,
    [IsDeleted]             BIT            NULL,
    CONSTRAINT [PK_BranchRole] PRIMARY KEY CLUSTERED ([BranchRoleId] ASC),
    CONSTRAINT [FK_BranchRole_RoleType] FOREIGN KEY ([RoleTypeId]) REFERENCES [dbo].[RoleType] ([RoleTypeId])
);

