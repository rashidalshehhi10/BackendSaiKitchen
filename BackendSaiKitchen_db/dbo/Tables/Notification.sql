CREATE TABLE [dbo].[Notification] (
    [NotificationId]            INT            IDENTITY (1, 1) NOT NULL,
    [NotificationContent]       NVARCHAR (500) NULL,
    [NotificationCategoryId]    INT            NULL,
    [IsActionable]              BIT            NULL,
    [NotificationAcceptAction]  NVARCHAR (500) NULL,
    [NotificationDeclineAction] NVARCHAR (500) NULL,
    [UserId]                    INT            NULL,
    [BranchId]                  INT            NULL,
    [UserRoleId]                INT            NULL,
    [IsRead]                    BIT            NULL,
    [IsActive]                  BIT            NULL,
    [IsDeleted]                 BIT            NULL,
    [CreatedBy]                 INT            NULL,
    [CreatedDate]               NVARCHAR (50)  NULL,
    [UpdatedBy]                 INT            NULL,
    [UpdatedDate]               NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED ([NotificationId] ASC),
    CONSTRAINT [FK_Notification_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch] ([BranchId]),
    CONSTRAINT [FK_Notification_NotificationCategory] FOREIGN KEY ([NotificationCategoryId]) REFERENCES [dbo].[NotificationCategory] ([NotificationCategoryId]),
    CONSTRAINT [FK_Notification_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_Notification_UserRole] FOREIGN KEY ([UserRoleId]) REFERENCES [dbo].[UserRole] ([UserRoleId])
);

