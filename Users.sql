﻿CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL
);
