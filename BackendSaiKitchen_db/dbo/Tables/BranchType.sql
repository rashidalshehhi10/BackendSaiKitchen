CREATE TABLE [dbo].[BranchType] (
    [BranchTypeId]   INT           NOT NULL,
    [BranchTypeName] NVARCHAR (50) NULL,
    [IsActive]       BIT           NULL,
    [IsDeleted]      BIT           NULL,
    [CreatedBy]      INT           NULL,
    [CreatedDate]    NVARCHAR (50) NULL,
    [UpdatedBy]      INT           NULL,
    [UpdatedDate]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_BranchType] PRIMARY KEY CLUSTERED ([BranchTypeId] ASC)
);

