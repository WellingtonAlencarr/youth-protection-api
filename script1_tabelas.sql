IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TB_USER] (
    [UserId] bigint NOT NULL IDENTITY,
    [FictionalName] Varchar(200) NULL,
    [Email] Varchar(200) NULL,
    [PasswordHash] Varchar(200) NULL,
    [CellPhone] Varchar(200) NULL,
    [BirthDate] Varchar(200) NULL,
    [Uf] Varchar(200) NULL,
    [City] Varchar(200) NULL,
    [Role] nvarchar(max) NOT NULL,
    [UserStatus] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_TB_USER] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [TB_PUBLICATION] (
    [PublicationId] bigint NOT NULL IDENTITY,
    [UserId] bigint NOT NULL,
    [PublicationContent] Varchar(200) NULL,
    [PublicationsRole] nvarchar(max) NOT NULL,
    [PublicationStatus] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [ModificationDate] datetime2 NOT NULL,
    CONSTRAINT [PK_TB_PUBLICATION] PRIMARY KEY ([PublicationId]),
    CONSTRAINT [FK_TB_PUBLICATION_TB_USER_UserId] FOREIGN KEY ([UserId]) REFERENCES [TB_USER] ([UserId])
);
GO

CREATE TABLE [TB_ATTENDANCES] (
    [Id] bigint NOT NULL IDENTITY,
    [VolunteerId] bigint NOT NULL,
    [PublicationId] bigint NOT NULL,
    [StartedAt] datetime2 NOT NULL,
    [IsCompleted] bit NOT NULL,
    CONSTRAINT [PK_TB_ATTENDANCES] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TB_ATTENDANCES_TB_PUBLICATION_PublicationId] FOREIGN KEY ([PublicationId]) REFERENCES [TB_PUBLICATION] ([PublicationId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TB_ATTENDANCES_TB_USER_VolunteerId] FOREIGN KEY ([VolunteerId]) REFERENCES [TB_USER] ([UserId]) ON DELETE CASCADE
);
GO

CREATE TABLE [TB_CHAT] (
    [Id] bigint NOT NULL IDENTITY,
    [AttendanceId] bigint NOT NULL,
    CONSTRAINT [PK_TB_CHAT] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TB_CHAT_TB_ATTENDANCES_AttendanceId] FOREIGN KEY ([AttendanceId]) REFERENCES [TB_ATTENDANCES] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TB_MESSAGES] (
    [Id] bigint NOT NULL IDENTITY,
    [ChatId] bigint NOT NULL,
    [SenderId] bigint NOT NULL,
    [Content] Varchar(200) NULL,
    [SentAt] datetime2 NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT [PK_TB_MESSAGES] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TB_MESSAGES_TB_CHAT_ChatId] FOREIGN KEY ([ChatId]) REFERENCES [TB_CHAT] ([Id]),
    CONSTRAINT [FK_TB_MESSAGES_TB_USER_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [TB_USER] ([UserId]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_TB_ATTENDANCES_PublicationId] ON [TB_ATTENDANCES] ([PublicationId]);
GO

CREATE INDEX [IX_TB_ATTENDANCES_VolunteerId] ON [TB_ATTENDANCES] ([VolunteerId]);
GO

CREATE INDEX [IX_TB_CHAT_AttendanceId] ON [TB_CHAT] ([AttendanceId]);
GO

CREATE INDEX [IX_TB_MESSAGES_ChatId] ON [TB_MESSAGES] ([ChatId]);
GO

CREATE INDEX [IX_TB_MESSAGES_SenderId] ON [TB_MESSAGES] ([SenderId]);
GO

CREATE INDEX [IX_TB_PUBLICATION_UserId] ON [TB_PUBLICATION] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241106111836_InitalCreate', N'8.0.10');
GO

COMMIT;
GO

