USE [Maersk-Exercise-Database]
GO

/****** Object: Table [dbo].[SortJobs] Script Date: 3/2/2021 8:43:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SortJobs] (
    [id]       NVARCHAR (450) NOT NULL,
    [input]    NVARCHAR (MAX) NOT NULL,
    [output]   NVARCHAR (MAX) NOT NULL,
    [status]   NVARCHAR (MAX) NOT NULL,
    [duration] BIGINT         NOT NULL
);


