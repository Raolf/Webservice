IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [DataDim] (
        [M_ID] int NOT NULL IDENTITY,
        [DataID] int NOT NULL,
        [Humidity] int NOT NULL,
        [CO2] int NOT NULL,
        [Temperature] int NOT NULL,
        CONSTRAINT [PK_DataDim] PRIMARY KEY ([M_ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [DateDim] (
        [D_ID] int NOT NULL IDENTITY,
        [Year] int NOT NULL,
        [Month] int NOT NULL,
        [Day] int NOT NULL,
        [Hour] int NOT NULL,
        [Minute] int NOT NULL,
        [Seconds] int NOT NULL,
        [Weekday] nvarchar(max) NULL,
        [Monthname] nvarchar(max) NULL,
        [Holiday] bit NOT NULL,
        CONSTRAINT [PK_DateDim] PRIMARY KEY ([D_ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [FactTable] (
        [UniqueID] int NOT NULL IDENTITY,
        [D_ID] int NOT NULL,
        [R_ID] int NOT NULL,
        [S_ID] int NOT NULL,
        [U_ID] int NOT NULL,
        [Servosetting] int NOT NULL,
        CONSTRAINT [PK_FactTable] PRIMARY KEY ([UniqueID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [Room] (
        [roomID] int NOT NULL IDENTITY,
        [name] nvarchar(max) NULL,
        CONSTRAINT [PK_Room] PRIMARY KEY ([roomID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [RoomDim] (
        [R_ID] int NOT NULL IDENTITY,
        [RoomID] int NOT NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_RoomDim] PRIMARY KEY ([R_ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [ServoDim] (
        [S_ID] int NOT NULL IDENTITY,
        [SensorID] int NOT NULL,
        [PD_ID] int NOT NULL,
        [DaysSinceSet] int NOT NULL,
        [HoursSinceSet] int NOT NULL,
        [SecondsSinceSet] int NOT NULL,
        CONSTRAINT [PK_ServoDim] PRIMARY KEY ([S_ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [User] (
        [userID] int NOT NULL IDENTITY,
        [username] nvarchar(max) NULL,
        [password] int NOT NULL,
        [admin] bit NOT NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([userID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [UserDim] (
        [U_ID] int NOT NULL IDENTITY,
        [UserID] int NOT NULL,
        [DisplayName] nvarchar(max) NULL,
        [Admin] bit NOT NULL,
        CONSTRAINT [PK_UserDim] PRIMARY KEY ([U_ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [Sensor] (
        [sensorID] int NOT NULL IDENTITY,
        [roomID] int NOT NULL,
        [servoSetting] int NOT NULL,
        CONSTRAINT [PK_Sensor] PRIMARY KEY ([sensorID]),
        CONSTRAINT [FK_Sensor_Room_roomID] FOREIGN KEY ([roomID]) REFERENCES [Room] ([roomID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [RoomAccess] (
        [userID] int NOT NULL,
        [roomID] int NOT NULL,
        CONSTRAINT [PK_RoomAccess] PRIMARY KEY ([roomID], [userID]),
        CONSTRAINT [FK_RoomAccess_Room_roomID] FOREIGN KEY ([roomID]) REFERENCES [Room] ([roomID]) ON DELETE CASCADE,
        CONSTRAINT [FK_RoomAccess_User_userID] FOREIGN KEY ([userID]) REFERENCES [User] ([userID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [Data] (
        [sensorID] int NOT NULL,
        [timestamp] datetime2 NOT NULL,
        [humidity] int NOT NULL,
        [CO2] int NOT NULL,
        [temperature] int NOT NULL,
        CONSTRAINT [PK_Data] PRIMARY KEY ([sensorID], [timestamp]),
        CONSTRAINT [FK_Data_Sensor_sensorID] FOREIGN KEY ([sensorID]) REFERENCES [Sensor] ([sensorID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE TABLE [SensorLog] (
        [sensorID] int NOT NULL IDENTITY,
        [sensorID1] int NOT NULL,
        [timestamp] datetime2 NOT NULL,
        [servoSetting] bit NOT NULL,
        CONSTRAINT [PK_SensorLog] PRIMARY KEY ([sensorID]),
        CONSTRAINT [FK_SensorLog_Sensor_sensorID1] FOREIGN KEY ([sensorID1]) REFERENCES [Sensor] ([sensorID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE INDEX [IX_RoomAccess_userID] ON [RoomAccess] ([userID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE INDEX [IX_Sensor_roomID] ON [Sensor] ([roomID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    CREATE INDEX [IX_SensorLog_sensorID1] ON [SensorLog] ([sensorID1]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200713174558_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200713174558_initial', N'3.1.5');
END;

GO

