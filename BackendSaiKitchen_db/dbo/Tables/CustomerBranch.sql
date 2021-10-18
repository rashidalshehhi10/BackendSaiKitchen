CREATE TABLE [dbo].[CustomerBranch] (
    [CustomerBranchId] INT           IDENTITY (1, 1) NOT NULL,
    [CustomerId]       INT           NULL,
    [BranchId]         INT           NULL,
    [CreatedBy]        INT           NULL,
    [CreatedDate]      NVARCHAR (50) NULL,
    [UpdatedBy]        INT           NULL,
    [UpdatedDate]      NVARCHAR (50) NULL,
    CONSTRAINT [PK_CustomerBranch] PRIMARY KEY CLUSTERED ([CustomerBranchId] ASC),
    CONSTRAINT [FK_CustomerBranch_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([BranchId]),
    CONSTRAINT [FK_CustomerBranch_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);

