export ASPNETCORE_ConnectionStrings__Bilim='Host=localhost\;Username=postgres\;Password=Garbage1\;Database=Bilim'
dotnet ef migrations add PaymentEntities -o Data/Migrations -s '../WebApi'
read -p "Press enter to continue"