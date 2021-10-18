CREATE TABLE [dbo].[UserRole] (
    [UserRoleId]   INT           IDENTITY (1, 1) NOT NULL,
    [UserId]       INT           NULL,
    [BranchId]     INT           NULL,
    [BranchRoleId] INT           NULL,
    [CreatedBy]    INT           NULL,
    [CreatedDate]  NVARCHAR (50) NULL,
    [UpdaredBy]    INT           NULL,
    [UpdatedDate]  NVARCHAR (50) NULL,
    [IsActive]     BIT           NULL,
    [IsDeleted]    BIT           NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([UserRoleId] ASC),
    CONSTRAINT [FK_Role_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([BranchId]),
    CONSTRAINT [FK_Role_BranchRole] FOREIGN KEY ([BranchRoleId]) REFERENCES [dbo].[BranchRole] ([BranchRoleId]),
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);

