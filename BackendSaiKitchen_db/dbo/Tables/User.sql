CREATE TABLE [dbo].[User] (
    [UserId]              INT            IDENTITY (1, 1) NOT NULL,
    [UserName]            NVARCHAR (50)  NULL,
    [UserEmail]           NVARCHAR (50)  NULL,
    [UserPassword]        NVARCHAR (50)  NULL,
    [UserMobile]          NVARCHAR (50)  NULL,
    [UserToken]           NVARCHAR (500) NULL,
    [UserProfileImageURL] NVARCHAR (500) NULL,
    [UserFCMToken]        NVARCHAR (500) NULL,
    [IsActive]            BIT            NULL,
    [IsDeleted]           BIT            NULL,
    [IsOnline]            BIT            NULL,
    [CreatedBy]           INT            NULL,
    [CreatedDate]         NVARCHAR (50)  NULL,
    [UpdatedBy]           INT            NULL,
    [UpdatedDate]         NVARCHAR (50)  NULL,
    CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

