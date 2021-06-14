CREATE TABLE [dbo].[Branch] (
    [BranchId]      INT            IDENTITY (1, 1) NOT NULL,
    [BranchName]    NVARCHAR (50)  NULL,
    [BranchAddress] NVARCHAR (500) NULL,
    [BranchContact] NVARCHAR (50)  NULL,
    [BranchTypeId]  INT            NULL,
    [IsActive]      BIT            NULL,
    [IsDeleted]     BIT            NULL,
    [CreatedBy]     INT            NULL,
    [CreatedDate]   NVARCHAR (50)  NULL,
    [UpdatedBy]     INT            NULL,
    [UpdatedDate]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_branch] PRIMARY KEY CLUSTERED ([BranchId] ASC),
    CONSTRAINT [FK_Branch_BranchType] FOREIGN KEY ([BranchTypeId]) REFERENCES [dbo].[BranchType] ([BranchTypeId])
);

