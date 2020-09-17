IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(30) NOT NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Facultes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(30) NOT NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Facultes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Publishers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Publishers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Saloons] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Saloons] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Students] (
    [Id] int NOT NULL IDENTITY,
    [RecordCreateDate] datetime2 NOT NULL,
    [RecruitmentDate] datetime2 NOT NULL,
    [TerminationDate] datetime2 NOT NULL,
    [Name] nvarchar(20) NOT NULL,
    [Surname] nvarchar(20) NOT NULL,
    [CitizenshipNo] nvarchar(11) NOT NULL,
    [Phone] nvarchar(11) NOT NULL,
    [Adress] nvarchar(100) NOT NULL,
    [StudentNo] int NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Writers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Surname] nvarchar(max) NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Writers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Departments] (
    [Id] int NOT NULL IDENTITY,
    [rtl_Faculty_Id] int NOT NULL,
    [Name] nvarchar(30) NOT NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Departments_Facultes_rtl_Faculty_Id] FOREIGN KEY ([rtl_Faculty_Id]) REFERENCES [Facultes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Bookshelfs] (
    [Id] int NOT NULL IDENTITY,
    [rtl_Saloon_Id] int NOT NULL,
    [Name] nvarchar(max) NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Bookshelfs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bookshelfs_Saloons_rtl_Saloon_Id] FOREIGN KEY ([rtl_Saloon_Id]) REFERENCES [Saloons] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Operations] (
    [Id] int NOT NULL IDENTITY,
    [rtl_Student_Id] int NOT NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    [GivenDate] datetime2 NOT NULL,
    [TakenDate] datetime2 NULL,
    [DateOfTaken] datetime2 NOT NULL,
    CONSTRAINT [PK_Operations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Operations_Students_rtl_Student_Id] FOREIGN KEY ([rtl_Student_Id]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Pricings] (
    [Id] int NOT NULL IDENTITY,
    [Rtl_Student_Id] int NOT NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    [LaggingDay] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [IsPaid] bit NOT NULL,
    CONSTRAINT [PK_Pricings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pricings_Students_Rtl_Student_Id] FOREIGN KEY ([Rtl_Student_Id]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [StudentDepartments] (
    [Id] int NOT NULL IDENTITY,
    [rtl_Department_Id] int NOT NULL,
    [rtl_Student_Id] int NOT NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_StudentDepartments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StudentDepartments_Departments_rtl_Department_Id] FOREIGN KEY ([rtl_Department_Id]) REFERENCES [Departments] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentDepartments_Students_rtl_Student_Id] FOREIGN KEY ([rtl_Student_Id]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Books] (
    [Id] int NOT NULL IDENTITY,
    [rlt_Bookshelf_Id] int NOT NULL,
    [rtl_Publisher_Id] int NOT NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [SerialNo] nvarchar(20) NOT NULL,
    [EditionNo] int NOT NULL,
    [ShelfNo] nvarchar(max) NULL,
    [DatePrinting] datetime2 NOT NULL,
    [DatePublish] datetime2 NOT NULL,
    [StockNumber] int NOT NULL,
    [OperationId] int NULL,
    [PricingId] int NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Books_Operations_OperationId] FOREIGN KEY ([OperationId]) REFERENCES [Operations] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Books_Pricings_PricingId] FOREIGN KEY ([PricingId]) REFERENCES [Pricings] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Books_Bookshelfs_rlt_Bookshelf_Id] FOREIGN KEY ([rlt_Bookshelf_Id]) REFERENCES [Bookshelfs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Books_Publishers_rtl_Publisher_Id] FOREIGN KEY ([rtl_Publisher_Id]) REFERENCES [Publishers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [BookCategories] (
    [Id] int NOT NULL IDENTITY,
    [rtl_Category_Id] int NOT NULL,
    [rtl_Book_Id] int NOT NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_BookCategories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BookCategories_Books_rtl_Book_Id] FOREIGN KEY ([rtl_Book_Id]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookCategories_Categories_rtl_Category_Id] FOREIGN KEY ([rtl_Category_Id]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [BookWriters] (
    [Id] int NOT NULL IDENTITY,
    [rtl_Book_Id] int NOT NULL,
    [rtl_Writer_Id] int NOT NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_BookWriters] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BookWriters_Books_rtl_Book_Id] FOREIGN KEY ([rtl_Book_Id]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookWriters_Writers_rtl_Writer_Id] FOREIGN KEY ([rtl_Writer_Id]) REFERENCES [Writers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_BookCategories_rtl_Book_Id] ON [BookCategories] ([rtl_Book_Id]);

GO

CREATE INDEX [IX_BookCategories_rtl_Category_Id] ON [BookCategories] ([rtl_Category_Id]);

GO

CREATE INDEX [IX_Books_OperationId] ON [Books] ([OperationId]);

GO

CREATE INDEX [IX_Books_PricingId] ON [Books] ([PricingId]);

GO

CREATE INDEX [IX_Books_rlt_Bookshelf_Id] ON [Books] ([rlt_Bookshelf_Id]);

GO

CREATE INDEX [IX_Books_rtl_Publisher_Id] ON [Books] ([rtl_Publisher_Id]);

GO

CREATE INDEX [IX_Bookshelfs_rtl_Saloon_Id] ON [Bookshelfs] ([rtl_Saloon_Id]);

GO

CREATE INDEX [IX_BookWriters_rtl_Book_Id] ON [BookWriters] ([rtl_Book_Id]);

GO

CREATE INDEX [IX_BookWriters_rtl_Writer_Id] ON [BookWriters] ([rtl_Writer_Id]);

GO

CREATE INDEX [IX_Departments_rtl_Faculty_Id] ON [Departments] ([rtl_Faculty_Id]);

GO

CREATE INDEX [IX_Operations_rtl_Student_Id] ON [Operations] ([rtl_Student_Id]);

GO

CREATE INDEX [IX_Pricings_Rtl_Student_Id] ON [Pricings] ([Rtl_Student_Id]);

GO

CREATE INDEX [IX_StudentDepartments_rtl_Department_Id] ON [StudentDepartments] ([rtl_Department_Id]);

GO

CREATE INDEX [IX_StudentDepartments_rtl_Student_Id] ON [StudentDepartments] ([rtl_Student_Id]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200902124822_d', N'3.1.7');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200902130806_1', N'3.1.7');

GO

ALTER TABLE [Pricings] DROP CONSTRAINT [FK_Pricings_Students_Rtl_Student_Id];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Pricings]') AND [c].[name] = N'LaggingDay');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Pricings] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Pricings] DROP COLUMN [LaggingDay];

GO

EXEC sp_rename N'[Pricings].[Rtl_Student_Id]', N'rtl_Student_Id', N'COLUMN';

GO

EXEC sp_rename N'[Pricings].[IX_Pricings_Rtl_Student_Id]', N'IX_Pricings_rtl_Student_Id', N'INDEX';

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'TerminationDate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Students] ALTER COLUMN [TerminationDate] datetime2 NULL;

GO

ALTER TABLE [Pricings] ADD CONSTRAINT [FK_Pricings_Students_rtl_Student_Id] FOREIGN KEY ([rtl_Student_Id]) REFERENCES [Students] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200902131735_2', N'3.1.7');

GO

ALTER TABLE [Books] DROP CONSTRAINT [FK_Books_Operations_OperationId];

GO

ALTER TABLE [Operations] DROP CONSTRAINT [FK_Operations_Students_rtl_Student_Id];

GO

DROP INDEX [IX_Operations_rtl_Student_Id] ON [Operations];

GO

DROP INDEX [IX_Books_OperationId] ON [Books];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'RecruitmentDate');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Students] DROP COLUMN [RecruitmentDate];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Students]') AND [c].[name] = N'TerminationDate');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Students] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Students] DROP COLUMN [TerminationDate];

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'OperationId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Books] DROP COLUMN [OperationId];

GO

ALTER TABLE [Operations] ADD [StudentId] int NULL;

GO

ALTER TABLE [Operations] ADD [rtl_Book_Id] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Departments] ADD [RecruitmentDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [Departments] ADD [TerminationDate] datetime2 NULL;

GO

ALTER TABLE [Books] ADD [Kod] nvarchar(10) NULL;

GO

ALTER TABLE [Books] ADD [rtl_Book_Id] int NULL;

GO

CREATE INDEX [IX_Operations_StudentId] ON [Operations] ([StudentId]);

GO

CREATE INDEX [IX_Books_rtl_Book_Id] ON [Books] ([rtl_Book_Id]);

GO

ALTER TABLE [Books] ADD CONSTRAINT [FK_Books_Operations_rtl_Book_Id] FOREIGN KEY ([rtl_Book_Id]) REFERENCES [Operations] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [Operations] ADD CONSTRAINT [FK_Operations_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200903053325_ss', N'3.1.7');

GO

ALTER TABLE [Operations] DROP CONSTRAINT [FK_Operations_Students_StudentId];

GO

DROP INDEX [IX_Operations_StudentId] ON [Operations];

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Operations]') AND [c].[name] = N'StudentId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Operations] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Operations] DROP COLUMN [StudentId];

GO

CREATE INDEX [IX_Operations_rtl_Student_Id] ON [Operations] ([rtl_Student_Id]);

GO

ALTER TABLE [Operations] ADD CONSTRAINT [FK_Operations_Students_rtl_Student_Id] FOREIGN KEY ([rtl_Student_Id]) REFERENCES [Students] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200903054547_s', N'3.1.7');

GO

ALTER TABLE [Books] DROP CONSTRAINT [FK_Books_Operations_rtl_Book_Id];

GO

DROP INDEX [IX_Books_rtl_Book_Id] ON [Books];

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Operations]') AND [c].[name] = N'rtl_Book_Id');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Operations] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Operations] DROP COLUMN [rtl_Book_Id];

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'rtl_Book_Id');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Books] DROP COLUMN [rtl_Book_Id];

GO

ALTER TABLE [Operations] ADD [BookId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Books] ADD [OperationId] int NULL;

GO

CREATE INDEX [IX_Books_OperationId] ON [Books] ([OperationId]);

GO

ALTER TABLE [Books] ADD CONSTRAINT [FK_Books_Operations_OperationId] FOREIGN KEY ([OperationId]) REFERENCES [Operations] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200903054647_p', N'3.1.7');

GO

ALTER TABLE [Books] DROP CONSTRAINT [FK_Books_Operations_OperationId];

GO

DROP INDEX [IX_Books_OperationId] ON [Books];

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Operations]') AND [c].[name] = N'BookId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Operations] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Operations] DROP COLUMN [BookId];

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'OperationId');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Books] DROP COLUMN [OperationId];

GO

ALTER TABLE [Operations] ADD [rtl_Book_Id] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Books] ADD [rtl_Book_Id] int NULL;

GO

CREATE INDEX [IX_Books_rtl_Book_Id] ON [Books] ([rtl_Book_Id]);

GO

ALTER TABLE [Books] ADD CONSTRAINT [FK_Books_Operations_rtl_Book_Id] FOREIGN KEY ([rtl_Book_Id]) REFERENCES [Operations] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200903055534_i', N'3.1.7');

GO

ALTER TABLE [Books] DROP CONSTRAINT [FK_Books_Operations_rtl_Book_Id];

GO

DROP INDEX [IX_Books_rtl_Book_Id] ON [Books];

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Operations]') AND [c].[name] = N'rtl_Book_Id');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Operations] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Operations] DROP COLUMN [rtl_Book_Id];

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Books]') AND [c].[name] = N'rtl_Book_Id');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Books] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Books] DROP COLUMN [rtl_Book_Id];

GO

CREATE TABLE [BookOperations] (
    [Id] int NOT NULL IDENTITY,
    [rtl_Operation_Id] int NOT NULL,
    [rtl_Book_Id] int NOT NULL,
    [RecordCreateDate] datetime2 NOT NULL,
    CONSTRAINT [PK_BookOperations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BookOperations_Books_rtl_Book_Id] FOREIGN KEY ([rtl_Book_Id]) REFERENCES [Books] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookOperations_Operations_rtl_Operation_Id] FOREIGN KEY ([rtl_Operation_Id]) REFERENCES [Operations] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_BookOperations_rtl_Book_Id] ON [BookOperations] ([rtl_Book_Id]);

GO

CREATE INDEX [IX_BookOperations_rtl_Operation_Id] ON [BookOperations] ([rtl_Operation_Id]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200903060439_12', N'3.1.7');

GO

DROP TABLE [BookOperations];

GO

ALTER TABLE [Operations] ADD [rtl_Book_Id] int NOT NULL DEFAULT 0;

GO

CREATE INDEX [IX_Operations_rtl_Book_Id] ON [Operations] ([rtl_Book_Id]);

GO

ALTER TABLE [Operations] ADD CONSTRAINT [FK_Operations_Books_rtl_Book_Id] FOREIGN KEY ([rtl_Book_Id]) REFERENCES [Books] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200903082549_12-Force', N'3.1.7');

GO

