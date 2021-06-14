CREATE TABLE [dbo].[RoleHead] (
    [RoleHeadId]     INT           IDENTITY (1, 1) NOT NULL,
    [EmployeeRoleId] INT           NULL,
    [HeadRoleId]     INT           NULL,
    [IsActive]       BIT           NULL,
    [IsDeleted]      BIT           NULL,
    [CreatedDate]    NVARCHAR (50) NULL,
    [CreatedBy]      INT           NULL,
    [UpdatedDate]    NVARCHAR (50) NULL,
    [UpdatedBy]      INT           NULL,
    CONSTRAINT [PK_RoleHead] PRIMARY KEY CLUSTERED ([RoleHeadId] ASC),
    CONSTRAINT [FK_RoleHead_BranchRole] FOREIGN KEY ([EmployeeRoleId]) REFERENCES [dbo].[BranchRole] ([BranchRoleId])
);

