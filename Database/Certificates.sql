/*
    Certificates from İpek Sertel's CV
    Run in SSMS against PortfolioDb AFTER running the AddCertificates migration (update-database).
    Replaces all rows in the Certificates table.
*/
SET XACT_ABORT ON;
BEGIN TRANSACTION;

DELETE FROM Certificates;
INSERT INTO Certificates (Title, Issuer, DateText, CertificateUrl, IsActive) VALUES
(N'Learn C# by Building Applications (A–Z)',               N'Udemy',                                  N'Sep 2026',               NULL, 1),
(N'25 Hands-on C# Projects in 25 Lessons',                 N'Udemy',                                  N'Oct 2026 (in progress)', NULL, 1),
(N'CANIAS Development Management Expert Training',         N'IAS – Industrial Application Software',  N'May 2026',               NULL, 1),
(N'Front-End Development Certificate',                     N'Coderspace',                             N'2025',                   NULL, 1),
(N'Logo Netsis 3 Standard System Specialist',              N'Logo Yazılım',                           N'2025',                   NULL, 1),
(N'Applied IT Business Analysis & Real Recruitment Cases', N'Udemy',                                  N'2024',                   NULL, 1);

COMMIT TRANSACTION;
PRINT 'Certificates added.';
