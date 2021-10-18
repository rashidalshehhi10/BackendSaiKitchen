CREATE TABLE [dbo].[MeasurementDetail] (
    [MeasurementDetailId]                INT            IDENTITY (1, 1) NOT NULL,
    [MeasurementDetailName]              NVARCHAR (500) NULL,
    [MeasurementDetailDescription]       NVARCHAR (500) NULL,
    [MeasurementDetailCeilingHeight]     NVARCHAR (50)  NULL,
    [MeasurementDetailCielingDiameter]   NVARCHAR (50)  NULL,
    [MeasurementDetailCornishHeight]     NVARCHAR (50)  NULL,
    [MeasurementDetailCornishDiameter]   NVARCHAR (50)  NULL,
    [MeasurementDetailSkirtingHeight]    NVARCHAR (50)  NULL,
    [MeasurementDetailSkirtingDiameter]  NVARCHAR (50)  NULL,
    [MeasurementDetailPlinthHeight]      NVARCHAR (50)  NULL,
    [MeasurementDetailPlinthDiameter]    NVARCHAR (50)  NULL,
    [MeasurementDetailDoorHeight]        NVARCHAR (50)  NULL,
    [MeasurementDetailSpotLightFromWall] NVARCHAR (50)  NULL,
    [IsActive]                           BIT            NULL,
    [IsDeleted]                          BIT            NULL,
    [CreatedBy]                          INT            NULL,
    [CreatedDate]                        NVARCHAR (50)  NULL,
    [UpdatedBy]                          INT            NULL,
    [UpdatedDate]                        NVARCHAR (50)  NULL,
    CONSTRAINT [PK_MeasurementDetail] PRIMARY KEY CLUSTERED ([MeasurementDetailId] ASC)
);

