CREATE TABLE [dbo].[ContactStatus] (
    [ContactStatusId]   INT           NOT NULL,
    [ContactStatusName] NVARCHAR (50) NULL,
    [IsActive]          BIT           NULL,
    [IsDeleted]         BIT           NULL,
    [CreatedBy]         INT           NULL,
    [CreatedDate]       NVARCHAR (50) NULL,
    [UpdatedBy]         INT           NULL,
    [UpdatedDate]       NVARCHAR (50) NULL,
    CONSTRAINT [PK_ContactStatus] PRIMARY KEY CLUSTERED ([ContactStatusId] ASC)
);

