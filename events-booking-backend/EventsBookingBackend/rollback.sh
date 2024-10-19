export Database__Postgresql__ConnectionString="Host=localhost;Port=5433;Username=postgres;Password=123123;Database=events_booking;"
dotnet ef database update 0 --context AuthDbContext
dotnet ef database update 0 --context UserDbContext
dotnet ef database update 0 --context BookingDbContext
dotnet ef database update 0 --context CategoryDbContext
dotnet ef database update 0 --context EventDbContext
dotnet ef database update 0 --context ReviewDbContext
dotnet ef database update 0 --context FileDbContext