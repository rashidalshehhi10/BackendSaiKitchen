CREATE TABLE [dbo].[WorkScope] (
    [WorkScopeId]          INT            IDENTITY (1, 1) NOT NULL,
    [WorkScopeName]        NVARCHAR (500) NULL,
    [WorkScopeDescription] NVARCHAR (500) NULL,
    [QuestionaireType]     INT            NULL,
    [IsActive]             BIT            NULL,
    [IsDeleted]            BIT            NULL,
    [CreatedBy]            INT            NULL,
    [CreatedDate]          NVARCHAR (50)  NULL,
    [UpdatedBy]            INT            NULL,
    [UpdatedDate]          NVARCHAR (50)  NULL,
    CONSTRAINT [PK_WorkScope] PRIMARY KEY CLUSTERED ([WorkScopeId] ASC)
);

