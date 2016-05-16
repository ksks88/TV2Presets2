
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/27/2015 14:45:18
-- Generated from EDMX file: C:\Users\Milan\documents\visual studio 2013\Projects\TV2Presets2\TV2Presets2\Models\TV2PresetsModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TV2Presets2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SatellitePositionSatellite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Satellites] DROP CONSTRAINT [FK_SatellitePositionSatellite];
GO
IF OBJECT_ID(N'[dbo].[FK_SatellitePositionFixedAntenna]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FixedAntennas] DROP CONSTRAINT [FK_SatellitePositionFixedAntenna];
GO
IF OBJECT_ID(N'[dbo].[FK_DownlinkChannelSatellite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DownlinkChannels] DROP CONSTRAINT [FK_DownlinkChannelSatellite];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DownlinkChannels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DownlinkChannels];
GO
IF OBJECT_ID(N'[dbo].[Satellites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Satellites];
GO
IF OBJECT_ID(N'[dbo].[SatellitePositions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SatellitePositions];
GO
IF OBJECT_ID(N'[dbo].[FixedAntennas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FixedAntennas];
GO
IF OBJECT_ID(N'[dbo].[SteerableAntennas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SteerableAntennas];
GO
IF OBJECT_ID(N'[dbo].[IRDs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IRDs];
GO
IF OBJECT_ID(N'[dbo].[EXTCards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EXTCards];
GO
IF OBJECT_ID(N'[dbo].[BISSCodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BISSCodes];
GO
IF OBJECT_ID(N'[dbo].[UplinkChannels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UplinkChannels];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DownlinkChannels'
CREATE TABLE [dbo].[DownlinkChannels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Frequency] float  NOT NULL,
    [SymbolRate] float  NOT NULL,
    [Polarisation] int  NOT NULL,
    [FEC] int  NOT NULL,
    [RollOff] int  NOT NULL,
    [SDHD] int  NOT NULL,
    [EBUChannel] bit  NOT NULL,
    [Modulation] int  NOT NULL,
    [SatelliteId] int  NOT NULL
);
GO

-- Creating table 'Satellites'
CREATE TABLE [dbo].[Satellites] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SatellitePositionId] int  NOT NULL
);
GO

-- Creating table 'SatellitePositions'
CREATE TABLE [dbo].[SatellitePositions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FixedAntennas'
CREATE TABLE [dbo].[FixedAntennas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Size] nvarchar(max)  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [XHighInput] int  NULL,
    [XLowInput] int  NULL,
    [YHighInput] int  NULL,
    [YLowInput] int  NULL,
    [XHighFreq] float  NULL,
    [XLowFreq] float  NULL,
    [YHighFreq] float  NULL,
    [YLowFreq] float  NULL,
    [SatellitePositionId] int  NOT NULL
);
GO

-- Creating table 'SteerableAntennas'
CREATE TABLE [dbo].[SteerableAntennas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Azimuth] float  NOT NULL,
    [Elevation] float  NOT NULL,
    [Polarization] float  NOT NULL,
    [Size] nvarchar(max)  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [XHighInput] int  NULL,
    [XLowInput] int  NULL,
    [YHighInput] int  NULL,
    [YLowInput] int  NULL,
    [XHighFreq] float  NULL,
    [XLowFreq] float  NULL,
    [YHighFreq] float  NULL,
    [YLowFreq] float  NULL
);
GO

-- Creating table 'IRDs'
CREATE TABLE [dbo].[IRDs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [MatrixOutput] int  NOT NULL,
    [MatrixInput] int  NOT NULL,
    [IRDType] int  NOT NULL,
    [SerialNumber] nvarchar(max)  NULL,
    [RegisterNumber] nvarchar(max)  NULL,
    [FWVersion] nvarchar(max)  NULL,
    [Comments] nvarchar(max)  NULL,
    [IPAddress] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EXTCards'
CREATE TABLE [dbo].[EXTCards] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [MatrixOutput] int  NOT NULL,
    [Comments] nvarchar(max)  NULL
);
GO

-- Creating table 'BISSCodes'
CREATE TABLE [dbo].[BISSCodes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [BISSType] int  NOT NULL,
    [BISSKey] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UplinkChannels'
CREATE TABLE [dbo].[UplinkChannels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Satellite] nvarchar(max)  NOT NULL,
    [Frequency] float  NOT NULL,
    [SymbolRate] float  NOT NULL,
    [Polarisation] int  NOT NULL,
    [DataInterfaceRate] float  NOT NULL,
    [FEC] int  NOT NULL,
    [RollOff] int  NOT NULL,
    [Modulation] int  NOT NULL,
    [LO_Frequency] int  NOT NULL,
    [Power] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'DownlinkChannels'
ALTER TABLE [dbo].[DownlinkChannels]
ADD CONSTRAINT [PK_DownlinkChannels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Satellites'
ALTER TABLE [dbo].[Satellites]
ADD CONSTRAINT [PK_Satellites]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SatellitePositions'
ALTER TABLE [dbo].[SatellitePositions]
ADD CONSTRAINT [PK_SatellitePositions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FixedAntennas'
ALTER TABLE [dbo].[FixedAntennas]
ADD CONSTRAINT [PK_FixedAntennas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SteerableAntennas'
ALTER TABLE [dbo].[SteerableAntennas]
ADD CONSTRAINT [PK_SteerableAntennas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IRDs'
ALTER TABLE [dbo].[IRDs]
ADD CONSTRAINT [PK_IRDs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EXTCards'
ALTER TABLE [dbo].[EXTCards]
ADD CONSTRAINT [PK_EXTCards]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BISSCodes'
ALTER TABLE [dbo].[BISSCodes]
ADD CONSTRAINT [PK_BISSCodes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UplinkChannels'
ALTER TABLE [dbo].[UplinkChannels]
ADD CONSTRAINT [PK_UplinkChannels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SatellitePositionId] in table 'Satellites'
ALTER TABLE [dbo].[Satellites]
ADD CONSTRAINT [FK_SatellitePositionSatellite]
    FOREIGN KEY ([SatellitePositionId])
    REFERENCES [dbo].[SatellitePositions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SatellitePositionSatellite'
CREATE INDEX [IX_FK_SatellitePositionSatellite]
ON [dbo].[Satellites]
    ([SatellitePositionId]);
GO

-- Creating foreign key on [SatellitePositionId] in table 'FixedAntennas'
ALTER TABLE [dbo].[FixedAntennas]
ADD CONSTRAINT [FK_SatellitePositionFixedAntenna]
    FOREIGN KEY ([SatellitePositionId])
    REFERENCES [dbo].[SatellitePositions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SatellitePositionFixedAntenna'
CREATE INDEX [IX_FK_SatellitePositionFixedAntenna]
ON [dbo].[FixedAntennas]
    ([SatellitePositionId]);
GO

-- Creating foreign key on [SatelliteId] in table 'DownlinkChannels'
ALTER TABLE [dbo].[DownlinkChannels]
ADD CONSTRAINT [FK_DownlinkChannelSatellite]
    FOREIGN KEY ([SatelliteId])
    REFERENCES [dbo].[Satellites]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DownlinkChannelSatellite'
CREATE INDEX [IX_FK_DownlinkChannelSatellite]
ON [dbo].[DownlinkChannels]
    ([SatelliteId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------