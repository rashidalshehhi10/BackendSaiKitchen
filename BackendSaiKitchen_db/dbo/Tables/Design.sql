CREATE TABLE [dbo].[Design] (
    [DesignId]           INT            IDENTITY (1, 1) NOT NULL,
    [DesignName]         NVARCHAR (50)  NULL,
    [DesignDescription]  NVARCHAR (500) NULL,
    [DesignComment]      NVARCHAR (MAX) NULL,
    [StatusId]           INT            NULL,
    [InquiryWorkscopeId] INT            NULL,
    [DesignTakenBy]      INT            NULL,
    [IsDesignApproved]   BIT            NULL,
    [DesignApprovedBy]   INT            NULL,
    [DesignApprovedon]   NVARCHAR (50)  NULL,
    [IsActive]           BIT            NULL,
    [IsDeleted]          BIT            NULL,
    [CreatedBy]          INT            NULL,
    [CreatedDate]        NVARCHAR (50)  NULL,
    [UpdatedBy]          INT            NULL,
    [UpdatedDate]        NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Design] PRIMARY KEY CLUSTERED ([DesignId] ASC),
    CONSTRAINT [FK_Design_InquiryWorkscope] FOREIGN KEY ([InquiryWorkscopeId]) REFERENCES [dbo].[InquiryWorkscope] ([InquiryWorkscopeId]),
    CONSTRAINT [FK_Design_User] FOREIGN KEY ([DesignTakenBy]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_Design_User1] FOREIGN KEY ([DesignApprovedBy]) REFERENCES [dbo].[User] ([UserId])
);

