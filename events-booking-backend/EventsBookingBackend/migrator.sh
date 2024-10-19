export Database__Postgresql__ConnectionString="Host=localhost;Port=5433;Username=postgres;Password=123123;Database=events_booking;"
dotnet ef migrations add MigrateAll --context AuthDbContext --output-dir ./Migrations/Auth
dotnet ef migrations add MigrateAll --context UserDbContext --output-dir ./Migrations/User
dotnet ef migrations add MigrateAll --context BookingDbContext --output-dir ./Migrations/Booking
dotnet ef migrations add MigrateAll --context CategoryDbContext --output-dir ./Migrations/Category
dotnet ef migrations add MigrateAll --context EventDbContext --output-dir ./Migrations/Event
dotnet ef migrations add MigrateAll --context ReviewDbContext --output-dir ./Migrations/Review
dotnet ef migrations add MigrateAll --context FileDbContext --output-dir ./Migrations/File
dotnet ef migrations add MigrateAll --context PaymeDbContext --output-dir ./Migrations/Payme