/*
    Portfolio content from İpek Sertel's CV
    ------------------------------------------------------------
    Run in SSMS against the PortfolioDb database (local or live).
    - Updates Hero, About and Settings (keeps the profile image and the uploaded CV)
    - Replaces Education, Experience, Skills, Skill Categories, Projects and Project Categories
    - Does NOT touch Messages
    Everything runs in one transaction: if any statement fails, nothing is changed.
*/
SET XACT_ABORT ON;
BEGIN TRANSACTION;

/* ---------- Hero ---------- */
IF EXISTS (SELECT 1 FROM HeroSections)
    UPDATE HeroSections SET
        Title       = N'İpek Sertel',
        SubTitle    = N'Software Developer',
        Description = N'Software Developer working on ERP applications, SQL Server and web development. I build business logic and data-driven workflows in IAS Canias and develop web applications with C#, ASP.NET Core MVC and React.';
ELSE
    INSERT INTO HeroSections (Title, SubTitle, Description, ImageUrl)
    VALUES (N'İpek Sertel', N'Software Developer',
            N'Software Developer working on ERP applications, SQL Server and web development. I build business logic and data-driven workflows in IAS Canias and develop web applications with C#, ASP.NET Core MVC and React.',
            N'/images/ipek.png');

/* ---------- About ---------- */
DECLARE @bio NVARCHAR(MAX) = N'I am a Software Developer with experience in ERP application development, SQL Server database work, business-process customization and web development.
Currently I develop and maintain ERP screens, business logic and data-driven workflows in the IAS Canias environment using Troia and SQL. Before that, I worked with SQL Server and T-SQL to build and maintain views, stored procedures and scripts supporting finance, logistics and production processes.
I also build web applications with C#, ASP.NET Core MVC, React, JavaScript and Node.js, with a practical understanding of business requirements, data integrity, application maintenance and problem solving.';

IF EXISTS (SELECT 1 FROM Abouts)
    UPDATE Abouts SET
        NameSurname = N'İpek Sertel',
        JobTitle    = N'Software Developer',
        Location    = N'İzmir, Türkiye',
        Expert      = N'ERP Development, SQL Server & Web Applications',
        Email       = N'ipekgozlek@gmail.com',
        Description = @bio,
        OpenToWork  = N'Open to new opportunities and collaborations';
ELSE
    INSERT INTO Abouts (NameSurname, JobTitle, Location, Expert, Email, Description, OpenToWork)
    VALUES (N'İpek Sertel', N'Software Developer', N'İzmir, Türkiye', N'ERP Development, SQL Server & Web Applications',
            N'ipekgozlek@gmail.com', @bio, N'Open to new opportunities and collaborations');

/* ---------- Settings (contact + social links, CV file is kept) ---------- */
IF EXISTS (SELECT 1 FROM Contacts)
    UPDATE Contacts SET
        Email          = N'ipekgozlek@gmail.com',
        Location       = N'İzmir, Türkiye',
        Linkedin       = N'linkedin.com/in/ipek-sertel',
        Github         = N'github.com/ipekgozlek',
        Description    = N'Feel free to reach out about job opportunities, ERP and web projects, or collaborations. You can contact me directly or leave a message using the form.',
        SubDescription = N'I usually reply within 24 hours.';
ELSE
    INSERT INTO Contacts (Email, Location, Linkedin, Github, Description, SubDescription)
    VALUES (N'ipekgozlek@gmail.com', N'İzmir, Türkiye', N'linkedin.com/in/ipek-sertel', N'github.com/ipekgozlek',
            N'Feel free to reach out about job opportunities, ERP and web projects, or collaborations. You can contact me directly or leave a message using the form.',
            N'I usually reply within 24 hours.');

/* ---------- Education ---------- */
DELETE FROM Educations;
INSERT INTO Educations (Title, SubTitle, Description, Duration, Location, IsActive) VALUES
(N'Adnan Menderes University', N'B.Sc. in Computer Engineering (100% English program)',
 N'Bachelor''s degree in Computer Engineering, taught 100% in English.',
 N'Graduated 2022', N'Aydın, Türkiye', 1),
(N'St George International, London', N'Upper Intermediate English Course',
 N'Two-month Upper Intermediate (B2) English course in London.',
 N'2018 · 2 months', N'London, United Kingdom', 1);

/* ---------- Experience (each line of Description = one bullet point) ---------- */
DELETE FROM Experiences;
INSERT INTO Experiences (Title, CompanyName, Description, SideTitle, Duration, WorkType, IsActive) VALUES
(N'Software Developer', N'IAS Software Company',
 N'Develop and customize ERP applications in the IAS Canias environment using the Troia programming language.
Implement and maintain application screens, business logic and process-oriented functionality based on business requirements.
Use SQL to query, manipulate and report business data across ERP workflows.
Support ongoing application improvements by investigating requirements and refining existing ERP functionality.',
 N'IAS İzmir Plant, Aegean Free Zone (ESBAŞ)', N'Mar 2026 – Present', N'Full-time', 1),

(N'ERP Software Specialist', N'Planet Software Company',
 N'Managed and optimized Netsis ERP data in Microsoft SQL Server, with attention to data integrity and query performance.
Developed and maintained T-SQL views, stored procedures and scripts supporting finance, logistics and production modules.
Worked with business data and ERP workflows to investigate issues, support reporting needs and improve data handling.',
 N'İzmir, Türkiye', N'Jun 2024 – Feb 2026', N'Full-time', 1),

(N'CRM Consultant', N'Parge Software Consulting A.Ş.',
 N'Customized Microsoft Dynamics 365 CRM modules, forms and business process flows in test and live environments.
Supported client-specific business process requirements for organizations including Borusan Holding, Tatko and ÇYDD.',
 N'İstanbul, Türkiye', N'Dec 2022 – Nov 2023', N'Full-time', 1),

(N'Software Engineer Intern', N'S4IN Software Company',
 N'Supported internal web interface development using HTML, CSS and JavaScript.
Contributed to responsive layout improvements and user-interface refinements.',
 N'İzmir, Türkiye', N'Jul 2021 – Aug 2021', N'Internship', 1),

(N'Software Engineer Intern', N'Sisbim Software Company',
 N'Assisted in building customer-facing web pages based on design requirements.
Contributed to page layout adjustments and JavaScript form validation.',
 N'İzmir, Türkiye', N'Jul 2020 – Aug 2020', N'Internship', 1);

/* ---------- Skills ---------- */
DELETE FROM Skills;
DELETE FROM SkillCategories;
INSERT INTO SkillCategories (Title, Description, Icon) VALUES
(N'Programming & Web',         N'Building web applications from the database to the user interface.',            'bi-code-slash'),
(N'Databases',                 N'Microsoft SQL Server, T-SQL and reliable, well-structured business data.',        'bi-database'),
(N'Enterprise Applications',   N'ERP and CRM development, customization and business logic.',                     'bi-cpu'),
(N'Development Practices & Tools', N'Version control, debugging, maintenance and requirements analysis.',        'bi-tools'),
(N'Data Analysis',             N'Predictive modeling and segmentation with Python.',                               'bi-bar-chart');

INSERT INTO Skills (Title, Level, SkillCategoryId)
SELECT s.Title, s.Level, c.SkillCategoryId
FROM (VALUES
    (N'C#', 'Intermediate', N'Programming & Web'),
    (N'.NET', 'Intermediate', N'Programming & Web'),
    (N'ASP.NET Core MVC', 'Intermediate', N'Programming & Web'),
    (N'JavaScript', 'Intermediate', N'Programming & Web'),
    (N'React', 'Intermediate', N'Programming & Web'),
    (N'Node.js', 'Beginner', N'Programming & Web'),
    (N'HTML5', 'Advanced', N'Programming & Web'),
    (N'CSS3', 'Advanced', N'Programming & Web'),
    (N'Bootstrap', 'Advanced', N'Programming & Web'),
    (N'Microsoft SQL Server', 'Advanced', N'Databases'),
    (N'T-SQL', 'Advanced', N'Databases'),
    (N'Views & Stored Procedures', 'Advanced', N'Databases'),
    (N'Database Design', 'Intermediate', N'Databases'),
    (N'Data Integrity', 'Intermediate', N'Databases'),
    (N'IAS Canias ERP', 'Intermediate', N'Enterprise Applications'),
    (N'Troia', 'Intermediate', N'Enterprise Applications'),
    (N'Netsis ERP', 'Advanced', N'Enterprise Applications'),
    (N'Microsoft Dynamics 365 CRM', 'Intermediate', N'Enterprise Applications'),
    (N'ERP Customization', 'Advanced', N'Enterprise Applications'),
    (N'Git & GitHub', 'Intermediate', N'Development Practices & Tools'),
    (N'Responsive Design', 'Advanced', N'Development Practices & Tools'),
    (N'Debugging', 'Intermediate', N'Development Practices & Tools'),
    (N'Application Maintenance', 'Intermediate', N'Development Practices & Tools'),
    (N'Requirements Analysis', 'Intermediate', N'Development Practices & Tools'),
    (N'Python', 'Intermediate', N'Data Analysis'),
    (N'XGBoost', 'Beginner', N'Data Analysis'),
    (N'K-Means', 'Beginner', N'Data Analysis'),
    (N'Feature Engineering', 'Beginner', N'Data Analysis')
) AS s(Title, Level, CategoryName)
JOIN SkillCategories c ON c.Title = s.CategoryName;

/* ---------- Projects ---------- */
DELETE FROM Projects;
DELETE FROM Categories;
INSERT INTO Categories (CategoryName) VALUES
(N'Web Apps'), (N'Front-End'), (N'Data Science');

-- Inserted oldest-to-newest: the website lists the newest (last inserted) first
INSERT INTO Projects (Title, Description, GithubUrl, LiveUrl, CoverImageUrl, Technologies, IsActive, CategoryId)
SELECT p.Title, p.Description, p.GithubUrl, p.LiveUrl, NULL, p.Technologies, 1, c.CategoryId
FROM (VALUES
    (1, N'Promotion Impact & Cannibalization Analysis',
     N'A predictive model using product-level features (R² = 0.72, Adjusted MAPE = 87%); identifies within-group and cross-group cannibalization and segments stores with K-Means.',
     N'https://github.com/ipekgozlek/datascience98', NULL,
     N'Python, XGBoost, K-Means, Feature Engineering', N'Data Science'),
    (2, N'Responsive Product Carousel (Trendyol-inspired)',
     N'A responsive product carousel with sliding animations, local product-data persistence, add-to-cart, price rendering and DOM manipulation.',
     N'https://github.com/ipekgozlek/urunkart_Swiperjs', N'https://ipekgozlek.github.io/urunkart_Swiperjs/',
     N'HTML5, CSS3, JavaScript, Bootstrap 5, Local Storage API, Swiper.js', N'Front-End'),
    (3, N'Travel Trip Blog',
     N'A travel blog with destination posts, visitor comments and an admin panel for managing blog content.',
     N'https://github.com/ipekgozlek/TravelTripProje', NULL,
     N'C#, ASP.NET MVC 5, Entity Framework 6, SQL Server, Bootstrap', N'Web Apps'),
    (4, N'Migros-Inspired Online Grocery Web App',
     N'An online grocery application inspired by Migros, with product search, favorites and a live-updating shopping cart.',
     N'https://github.com/ipekgozlek/migros-backend', N'https://migros-shop.vercel.app/',
     N'React, Node.js', N'Web Apps'),
    (5, N'Personal Portfolio Website',
     N'An ASP.NET Core MVC portfolio with a management area for experiences, skills, messages, testimonials and a to-do list.',
     N'https://github.com/ipekgozlek/MyPortfolio', N'https://ipeksertel-portfolio.runasp.net/',
     N'C#, ASP.NET Core MVC, EF Core, SQL Server, View Components', N'Web Apps'),
    (6, N'Portfolio CMS with Admin Dashboard',
     N'This website: a database-backed portfolio with an admin panel and dashboard for managing content, projects, skills, certificates, messages and the downloadable CV.',
     N'https://github.com/ipekgozlek/MYAcademy_PortfolioProject', NULL,
     N'C#, .NET 10, ASP.NET Core MVC, EF Core, SQL Server, Bootstrap 5', N'Web Apps')
) AS p(SortOrder, Title, Description, GithubUrl, LiveUrl, Technologies, CategoryName)
JOIN Categories c ON c.CategoryName = p.CategoryName
ORDER BY p.SortOrder;

COMMIT TRANSACTION;
PRINT 'Portfolio content updated from CV.';
