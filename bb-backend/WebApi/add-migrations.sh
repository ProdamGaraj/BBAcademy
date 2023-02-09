export ASPNETCORE_ConnectionStrings__Bilim='Host=localhost\;Username=postgres\;Password=postgres\;Database=Garbage1'
dotnet ef migrations add Initial -o Data/Migrations -s '../CoE.Web'
read -p "Press enter to continue"