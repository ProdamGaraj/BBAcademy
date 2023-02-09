export ASPNETCORE_ConnectionStrings__Crystal='Host=localhost;Port=5432;Username=postgres;Password=root;Database=Crystal'
dotnet ef database update -s '../CoE.Web'
read -p "Press enter to continue"