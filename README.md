# AmarOvijog

Create new Application for web api or mvc 6
install sqlserver,tools and asp.net core
go to Package manager console and run

Scaffold-DbContext "Server=AMINUL\SQLEXPRESS;Database=BdGeoService;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

Scaffold-Identity -Project AmarOvijog -DbContext BdGeoServiceContext -Files "Account.Login;Account.Register;Account.Manage" -OutputDir Areas/Identity
