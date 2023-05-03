using FA_DB.Models;


namespace FA_DB.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder) 
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                if(!context.users.Any()) 
                {
                    context.users.AddRange(new User()
                    {
                        Email = "Jonas@mail.dk",
                        //Password = "1234",
                        FirstName = "Jonas",
                        LastName = "Jedig",
                        PasswordHash = new byte[4] { 1, 2, 3, 4 },
                        Salt = new byte[4] { 1, 2, 3, 4 }

                    },
                    new User()
                    {
                        Email = "Jeppe@mail.dk",
                        //Password = "1234",
                        FirstName = "Jeppe",
                        LastName = "Pape",
                        PasswordHash = new byte[4] { 1, 2, 3, 4 },
                        Salt = new byte[4] { 1, 2, 3, 4 }
                    },
                    new User()
                    {
                        Email = "Mohamed@mail.dk",
                        //Password = "1234",
                        FirstName = "Mohamed",
                        LastName = "Abdou",
                        PasswordHash = new byte[4] { 1, 2, 3, 4 },
                        Salt = new byte[4] { 1, 2, 3, 4 }
                    },
                    new User()
                    {
                        Email = "Mads@mail.dk",
                        FirstName = "Mads",
                        LastName = "Stavnsbo",
                        PasswordHash = new byte[4] { 1, 2, 3, 4 },
                        Salt = new byte[4] { 1, 2, 3, 4 }
                    },
                    new User()
                    {
                        Email = "Alan@mail.dk",
                        //Password = "1234",
                        FirstName = "Alan",
                        LastName = "Khamo",
                        PasswordHash = new byte[4] { 1, 2, 3, 4 },
                        Salt = new byte[4] { 1, 2, 3, 4 }
                    },
                    new User()
                    {
                        Email = "Sean@mail.dk",
                        //Password = "1234",
                        FirstName = "Sean",
                        LastName = "Bateman",
                        PasswordHash = new byte[4] { 1, 2, 3, 4 },
                        Salt = new byte[4] { 1, 2, 3, 4 }
                    },
                    new User()
                    {
                        Email = "Kasper@mail.dk",
                        //Password = "1234",
                        FirstName = "Kasper",
                        LastName = "Martensen",
                        PasswordHash = new byte[4] { 1, 2, 3, 4 },
                        Salt = new byte[4] { 1, 2, 3, 4 }
                    });                    
                    context.SaveChanges();
                }

                if (!context.traningPrograms.Any())
                {
                    context.traningPrograms.AddRange(new TraningProgram()
                    {                        
                        TraningProgramID = 1,
                        Name = "Program 1"
                    });
                    context.SaveChanges();
                }

            }
        }
    }
}
