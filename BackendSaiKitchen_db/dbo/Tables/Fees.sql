CREATE TABLE [dbo].[Fees] (
    [FeesId]          INT            IDENTITY (1, 1) NOT NULL,
    [FeesName]        NVARCHAR (500) NULL,
    [FeesDescription] NVARCHAR (500) NULL,
    [FeesAmount]      NVARCHAR (500) NULL,
    [IsActive]        BIT            NULL,
    [IsDeleted]       BIT            NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDate]     NVARCHAR (50)  NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDate]     NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Fees] PRIMARY KEY CLUSTERED ([FeesId] ASC)
);

