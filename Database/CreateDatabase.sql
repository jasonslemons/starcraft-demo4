-- Create StarcraftDemo4 Database
-- This script creates the database and tables needed for the StarCraft Demo 4 application

USE master;
GO

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'StarcraftDemo4')
BEGIN
    CREATE DATABASE StarcraftDemo4;
END
GO

USE StarcraftDemo4;
GO

-- Create Games table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Games' AND xtype='U')
BEGIN
    CREATE TABLE Games (
        GameId int IDENTITY(1,1) PRIMARY KEY,
        StartTime datetime2 NOT NULL,
        EndTime datetime2 NULL,
        FinalMinerals int NOT NULL DEFAULT 0,
        FinalGas int NOT NULL DEFAULT 0,
        FinalUnitCount int NOT NULL DEFAULT 0,
        FinalUnitCap int NOT NULL DEFAULT 0,
        TotalGameTime int NOT NULL DEFAULT 0
    );
END
GO

-- Create GameSteps table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='GameSteps' AND xtype='U')
BEGIN
    CREATE TABLE GameSteps (
        StepId int IDENTITY(1,1) PRIMARY KEY,
        GameId int NOT NULL,
        StepNumber int NOT NULL,
        MoveType nvarchar(50) NOT NULL DEFAULT '',
        MoveDescription nvarchar(200) NOT NULL DEFAULT '',
        ObjectBuilt nvarchar(100) NOT NULL DEFAULT '',
        GameTimeAtStep int NOT NULL DEFAULT 0,
        MineralsAtStep int NOT NULL DEFAULT 0,
        GasAtStep int NOT NULL DEFAULT 0,
        UnitCountAtStep int NOT NULL DEFAULT 0,
        UnitCapAtStep int NOT NULL DEFAULT 0,
        StepTimestamp datetime2 NOT NULL,
        CONSTRAINT FK_GameSteps_Games FOREIGN KEY (GameId) REFERENCES Games(GameId) ON DELETE CASCADE
    );
END
GO

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name='IX_GameSteps_GameId' AND object_id = OBJECT_ID('GameSteps'))
BEGIN
    CREATE INDEX IX_GameSteps_GameId ON GameSteps(GameId);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name='IX_GameSteps_StepNumber' AND object_id = OBJECT_ID('GameSteps'))
BEGIN
    CREATE INDEX IX_GameSteps_StepNumber ON GameSteps(GameId, StepNumber);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name='IX_Games_StartTime' AND object_id = OBJECT_ID('Games'))
BEGIN
    CREATE INDEX IX_Games_StartTime ON Games(StartTime);
END
GO

PRINT 'Database schema created successfully!';
GO