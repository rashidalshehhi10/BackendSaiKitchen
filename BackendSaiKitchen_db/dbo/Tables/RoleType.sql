CREATE TABLE [dbo].[RoleType] (
    [RoleTypeId]   INT            NOT NULL,
    [RoleTypeName] NVARCHAR (500) NULL,
    [IsActive]     BIT            NULL,
    [IsDeleted]    BIT            NULL,
    [CreatedBy]    INT            NULL,
    [CreatedDate]  NVARCHAR (50)  NULL,
    [UpdatedBy]    INT            NULL,
    [UpdatedDate]  NVARCHAR (50)  NULL,
    CONSTRAINT [PK_RoleType] PRIMARY KEY CLUSTERED ([RoleTypeId] ASC)
);

