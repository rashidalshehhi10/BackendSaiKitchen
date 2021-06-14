CREATE TABLE [dbo].[File] (
    [FileId]        INT            IDENTITY (1, 1) NOT NULL,
    [FileName]      NVARCHAR (50)  NULL,
    [FileImage]     IMAGE          NULL,
    [FileURL]       NVARCHAR (MAX) NULL,
    [MeasurementId] INT            NULL,
    [DesignId]      INT            NULL,
    [IsActive]      BIT            NULL,
    [IsDeleted]     BIT            NULL,
    [CreatedBy]     INT            NULL,
    [CreatedDate]   NVARCHAR (50)  NULL,
    [UpdatedBy]     INT            NULL,
    [UpdatedDate]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_MeasurementFile] PRIMARY KEY CLUSTERED ([FileId] ASC),
    CONSTRAINT [FK_File_Design] FOREIGN KEY ([DesignId]) REFERENCES [dbo].[Design] ([DesignId]),
    CONSTRAINT [FK_File_Measurement] FOREIGN KEY ([MeasurementId]) REFERENCES [dbo].[Measurement] ([MeasurementId])
);

