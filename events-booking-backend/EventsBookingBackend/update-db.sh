export Database__Postgresql__ConnectionString="Host=localhost;Port=5433;Username=postgres;Password=123123;Database=events_booking;"
dotnet ef database update --context AuthDbContext
dotnet ef database update --context UserDbContext
dotnet ef database update --context BookingDbContext
dotnet ef database update --context CategoryDbContext
dotnet ef database update --context EventDbContext
dotnet ef database update --context ReviewDbContext
dotnet ef database update --context FileDbContext