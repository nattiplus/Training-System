
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/31/2021 17:05:53
-- Generated from EDMX file: D:\GCS190156\Application Development\ASP.NET MVC\ASP.NET System\Training System\Training System\Training System\Models\DBModels.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-Training System-20211123033135];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryCourse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [FK_CategoryCourse];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseEnrollment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Enrollments] DROP CONSTRAINT [FK_CourseEnrollment];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseScore]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Scores] DROP CONSTRAINT [FK_CourseScore];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseOfficeAssign]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OfficeAssigns] DROP CONSTRAINT [FK_CourseOfficeAssign];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserTrainer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trainers] DROP CONSTRAINT [FK_AspNetUserTrainer];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserTrainingStaff]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrainingStaffs] DROP CONSTRAINT [FK_AspNetUserTrainingStaff];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserTrainee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trainees] DROP CONSTRAINT [FK_AspNetUserTrainee];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserAdmin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Admins] DROP CONSTRAINT [FK_AspNetUserAdmin];
GO
IF OBJECT_ID(N'[dbo].[FK_TraineeScore]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Scores] DROP CONSTRAINT [FK_TraineeScore];
GO
IF OBJECT_ID(N'[dbo].[FK_TraineeEnrollment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Enrollments] DROP CONSTRAINT [FK_TraineeEnrollment];
GO
IF OBJECT_ID(N'[dbo].[FK_TrainerOfficeAssign]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OfficeAssigns] DROP CONSTRAINT [FK_TrainerOfficeAssign];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[Trainees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trainees];
GO
IF OBJECT_ID(N'[dbo].[Trainers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trainers];
GO
IF OBJECT_ID(N'[dbo].[Admins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admins];
GO
IF OBJECT_ID(N'[dbo].[TrainingStaffs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrainingStaffs];
GO
IF OBJECT_ID(N'[dbo].[Enrollments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Enrollments];
GO
IF OBJECT_ID(N'[dbo].[Scores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Scores];
GO
IF OBJECT_ID(N'[dbo].[OfficeAssigns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OfficeAssigns];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [Discriminator] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [Image] nvarchar(max)  NULL,
    [Create_Date] datetime  NULL,
    [Update_Date] datetime  NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [Descriptions] nvarchar(max)  NULL,
    [Requirements] nvarchar(max)  NULL,
    [Image] nvarchar(max)  NULL,
    [Publisher] nvarchar(128)  NULL,
    [About] nvarchar(max)  NULL,
    [TotalTime] nvarchar(128)  NULL,
    [Create_Date] datetime  NULL,
    [Update_Date] datetime  NULL,
    [CategoryId] int  NOT NULL
);
GO

-- Creating table 'Trainees'
CREATE TABLE [dbo].[Trainees] (
    [Id] nvarchar(128)  NOT NULL,
    [FullName] nvarchar(256)  NOT NULL,
    [Age] nvarchar(128)  NULL,
    [DoB] datetime  NULL,
    [Email] nvarchar(128)  NULL,
    [Telephone] nvarchar(128)  NULL,
    [Location] nvarchar(max)  NULL,
    [Department] nvarchar(max)  NULL,
    [ExperienceDetails] nvarchar(max)  NULL,
    [ProgrammingLanguage] nvarchar(max)  NULL,
    [TOEICscore] float  NULL,
    [Education] nvarchar(max)  NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Trainers'
CREATE TABLE [dbo].[Trainers] (
    [Id] nvarchar(128)  NOT NULL,
    [FullName] nvarchar(256)  NOT NULL,
    [Age] nvarchar(128)  NULL,
    [DoB] datetime  NULL,
    [Email] nvarchar(128)  NULL,
    [Telephone] nvarchar(128)  NULL,
    [Location] nvarchar(max)  NULL,
    [Department] nvarchar(max)  NULL,
    [ExperienceDetails] nvarchar(max)  NULL,
    [Type] nvarchar(128)  NULL,
    [WorkingPlace] nvarchar(max)  NULL,
    [Education] nvarchar(max)  NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [Id] nvarchar(128)  NOT NULL,
    [FullName] nvarchar(256)  NOT NULL,
    [Age] nvarchar(128)  NULL,
    [DoB] datetime  NULL,
    [Email] nvarchar(128)  NULL,
    [Telephone] nvarchar(128)  NULL,
    [Location] nvarchar(max)  NULL,
    [Department] nvarchar(max)  NULL,
    [ExperienceDetails] nvarchar(max)  NULL,
    [Type] nvarchar(128)  NULL,
    [WorkingPlace] nvarchar(max)  NULL,
    [Education] nvarchar(max)  NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'TrainingStaffs'
CREATE TABLE [dbo].[TrainingStaffs] (
    [Id] nvarchar(128)  NOT NULL,
    [FullName] nvarchar(256)  NOT NULL,
    [Age] nvarchar(128)  NULL,
    [DoB] datetime  NULL,
    [Email] nvarchar(128)  NULL,
    [Telephone] nvarchar(128)  NULL,
    [Location] nvarchar(max)  NULL,
    [Department] nvarchar(max)  NULL,
    [ExperienceDetails] nvarchar(max)  NULL,
    [Type] nvarchar(128)  NULL,
    [WorkingPlace] nvarchar(max)  NULL,
    [Education] nvarchar(max)  NULL,
    [AspNetUserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Enrollments'
CREATE TABLE [dbo].[Enrollments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Enroll_Date] datetime  NOT NULL,
    [Descriptions] nvarchar(max)  NULL,
    [Status] bit  NOT NULL,
    [CourseId] int  NOT NULL,
    [Update_Date] datetime  NULL,
    [TraineeId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Scores'
CREATE TABLE [dbo].[Scores] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Mark_Date] datetime  NOT NULL,
    [ToTal] float  NOT NULL,
    [Status] bit  NOT NULL,
    [CourseId] int  NOT NULL,
    [Update_Date] datetime  NULL,
    [TraineeId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'OfficeAssigns'
CREATE TABLE [dbo].[OfficeAssigns] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Assign_Date] datetime  NOT NULL,
    [Update_Date] datetime  NULL,
    [CourseId] int  NOT NULL,
    [Status] bit  NOT NULL,
    [TrainerId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] nvarchar(128)  NOT NULL,
    [RoleId] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Trainees'
ALTER TABLE [dbo].[Trainees]
ADD CONSTRAINT [PK_Trainees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Trainers'
ALTER TABLE [dbo].[Trainers]
ADD CONSTRAINT [PK_Trainers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TrainingStaffs'
ALTER TABLE [dbo].[TrainingStaffs]
ADD CONSTRAINT [PK_TrainingStaffs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Enrollments'
ALTER TABLE [dbo].[Enrollments]
ADD CONSTRAINT [PK_Enrollments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Scores'
ALTER TABLE [dbo].[Scores]
ADD CONSTRAINT [PK_Scores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OfficeAssigns'
ALTER TABLE [dbo].[OfficeAssigns]
ADD CONSTRAINT [PK_OfficeAssigns]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([RoleId], [UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([UserId]);
GO

-- Creating foreign key on [CategoryId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK_CategoryCourse]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCourse'
CREATE INDEX [IX_FK_CategoryCourse]
ON [dbo].[Courses]
    ([CategoryId]);
GO

-- Creating foreign key on [CourseId] in table 'Enrollments'
ALTER TABLE [dbo].[Enrollments]
ADD CONSTRAINT [FK_CourseEnrollment]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseEnrollment'
CREATE INDEX [IX_FK_CourseEnrollment]
ON [dbo].[Enrollments]
    ([CourseId]);
GO

-- Creating foreign key on [CourseId] in table 'Scores'
ALTER TABLE [dbo].[Scores]
ADD CONSTRAINT [FK_CourseScore]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseScore'
CREATE INDEX [IX_FK_CourseScore]
ON [dbo].[Scores]
    ([CourseId]);
GO

-- Creating foreign key on [CourseId] in table 'OfficeAssigns'
ALTER TABLE [dbo].[OfficeAssigns]
ADD CONSTRAINT [FK_CourseOfficeAssign]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseOfficeAssign'
CREATE INDEX [IX_FK_CourseOfficeAssign]
ON [dbo].[OfficeAssigns]
    ([CourseId]);
GO

-- Creating foreign key on [AspNetUserId] in table 'Trainers'
ALTER TABLE [dbo].[Trainers]
ADD CONSTRAINT [FK_AspNetUserTrainer]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserTrainer'
CREATE INDEX [IX_FK_AspNetUserTrainer]
ON [dbo].[Trainers]
    ([AspNetUserId]);
GO

-- Creating foreign key on [AspNetUserId] in table 'TrainingStaffs'
ALTER TABLE [dbo].[TrainingStaffs]
ADD CONSTRAINT [FK_AspNetUserTrainingStaff]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserTrainingStaff'
CREATE INDEX [IX_FK_AspNetUserTrainingStaff]
ON [dbo].[TrainingStaffs]
    ([AspNetUserId]);
GO

-- Creating foreign key on [AspNetUserId] in table 'Trainees'
ALTER TABLE [dbo].[Trainees]
ADD CONSTRAINT [FK_AspNetUserTrainee]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserTrainee'
CREATE INDEX [IX_FK_AspNetUserTrainee]
ON [dbo].[Trainees]
    ([AspNetUserId]);
GO

-- Creating foreign key on [AspNetUserId] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [FK_AspNetUserAdmin]
    FOREIGN KEY ([AspNetUserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserAdmin'
CREATE INDEX [IX_FK_AspNetUserAdmin]
ON [dbo].[Admins]
    ([AspNetUserId]);
GO

-- Creating foreign key on [TraineeId] in table 'Scores'
ALTER TABLE [dbo].[Scores]
ADD CONSTRAINT [FK_TraineeScore]
    FOREIGN KEY ([TraineeId])
    REFERENCES [dbo].[Trainees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TraineeScore'
CREATE INDEX [IX_FK_TraineeScore]
ON [dbo].[Scores]
    ([TraineeId]);
GO

-- Creating foreign key on [TraineeId] in table 'Enrollments'
ALTER TABLE [dbo].[Enrollments]
ADD CONSTRAINT [FK_TraineeEnrollment]
    FOREIGN KEY ([TraineeId])
    REFERENCES [dbo].[Trainees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TraineeEnrollment'
CREATE INDEX [IX_FK_TraineeEnrollment]
ON [dbo].[Enrollments]
    ([TraineeId]);
GO

-- Creating foreign key on [TrainerId] in table 'OfficeAssigns'
ALTER TABLE [dbo].[OfficeAssigns]
ADD CONSTRAINT [FK_TrainerOfficeAssign]
    FOREIGN KEY ([TrainerId])
    REFERENCES [dbo].[Trainers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrainerOfficeAssign'
CREATE INDEX [IX_FK_TrainerOfficeAssign]
ON [dbo].[OfficeAssigns]
    ([TrainerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------