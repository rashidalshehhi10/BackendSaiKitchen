CREATE TABLE [dbo].[InquiryStatus] (
    [InquiryStatusId]          INT            NOT NULL,
    [InquiryStatusName]        NVARCHAR (500) NULL,
    [InquiryStatusDescription] NVARCHAR (500) NULL,
    [IsActive]                 BIT            NULL,
    [IsDeleted]                BIT            NULL,
    [CreatedBy]                INT            NULL,
    [CreatedDate]              NVARCHAR (50)  NULL,
    [UpdatedBy]                INT            NULL,
    [UpdatedDate]              NVARCHAR (50)  NULL,
    CONSTRAINT [PK_InquiryStatus] PRIMARY KEY CLUSTERED ([InquiryStatusId] ASC)
);

