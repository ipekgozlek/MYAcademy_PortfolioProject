/*
    Projects and project categories
    Run in SSMS against PortfolioDb. Replaces all projects and categories
    (run it BEFORE uploading cover images from the admin panel).
*/
SET XACT_ABORT ON;
BEGIN TRANSACTION;

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
PRINT 'Projects updated.';
