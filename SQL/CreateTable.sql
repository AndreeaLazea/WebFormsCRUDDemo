-- Demo Items Table Creation Script
-- This table stores basic CRUD items for demonstration

USE [CRUDDemoDB]
GO

-- Drop table if exists (for clean re-runs)
IF OBJECT_ID('dbo.DemoItems', 'U') IS NOT NULL
    DROP TABLE dbo.DemoItems
GO

-- Create the DemoItems table
CREATE TABLE DemoItems (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    CreatedOn DATETIME DEFAULT GETDATE() NOT NULL
)
GO

-- Insert sample data for testing
INSERT INTO DemoItems (Title, Description)
VALUES 
    ('Sample Item 1', 'This is a sample description for item 1'),
    ('Sample Item 2', 'This is a sample description for item 2'),
    ('Sample Item 3', 'This is a sample description for item 3')
GO

-- Verify the data
SELECT * FROM DemoItems
GO

