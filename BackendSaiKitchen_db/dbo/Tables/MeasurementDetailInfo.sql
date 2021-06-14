CREATE TABLE [dbo].[MeasurementDetailInfo] (
    [MeasurementDetailInfoId]             INT           IDENTITY (1, 1) NOT NULL,
    [MeasurementDetailInfoName]           NVARCHAR (50) NULL,
    [MeasurementDetailInfoDistanceHeight] NVARCHAR (50) NULL,
    [MeasurementDetailInfoDistanceLL]     NVARCHAR (50) NULL,
    [MeasurementDetailInfoDistanceRR]     NVARCHAR (50) NULL,
    [MeasurementDetailInfoDistanceHFF]    NVARCHAR (50) NULL,
    [MeasurementDetailId]                 INT           NULL,
    [IsActive]                            BIT           NULL,
    [IsDeleted]                           BIT           NULL,
    [CreatedBy]                           INT           NULL,
    [CreatedDate]                         NVARCHAR (50) NULL,
    [UpdatedBy]                           INT           NULL,
    [UpdatedDate]                         NVARCHAR (50) NULL,
    CONSTRAINT [PK_MeasurementDetailInfo] PRIMARY KEY CLUSTERED ([MeasurementDetailInfoId] ASC),
    CONSTRAINT [FK_MeasurementDetailInfo_MeasurementDetail] FOREIGN KEY ([MeasurementDetailId]) REFERENCES [dbo].[MeasurementDetail] ([MeasurementDetailId])
);

