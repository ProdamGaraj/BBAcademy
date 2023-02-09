export ASPNETCORE_ConnectionStrings__Bilim='Host=localhost;Port=5432;Username=postgres;Password=Garbage1;Database=Bilim'
dotnet ef database update -s '../WebApi'
read -p "Press enter to continue"