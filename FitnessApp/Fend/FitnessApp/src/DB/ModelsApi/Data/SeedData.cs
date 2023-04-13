using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ModelsApi.Models.Entities;
using static BCrypt.Net.BCrypt;

namespace ModelsApi.Data
{
    public static class DbUtilities
    {
        internal static void SeedData(ApplicationDbContext context, int bcryptWorkfactor)
        {
            context.Database.EnsureCreated();
            if (!context.Accounts.Any())
                SeedAccounts(context, bcryptWorkfactor);
            if (!context.Managers.Any())
                SeedManagers(context);
            if (!context.Models.Any())
                SeedModels(context);
            if (!context.Jobs.Any())
                SeedJobs(context);
            if (!context.JobModels.Any())
                SeedJobModels(context);
            if (!context.Expenses.Any())
                SeedExpenses(context);
        }

        static void SeedAccounts(ApplicationDbContext context, int bcryptWorkfactor)
        {
            context.Accounts.AddRange(
                // Seed manager
                new EfAccount
                {
                    Email = "boss@m.dk",
                    PwHash = HashPassword("asdfQWER", bcryptWorkfactor),
                    IsManager = true
                },
                // Seed some models
                new EfAccount
                {
                    Email = "nc@m.dk",
                    PwHash = HashPassword("Pas123", bcryptWorkfactor),
                    IsManager = false
                },
                new EfAccount
                {
                    Email = "hc@m.dk",
                    PwHash = HashPassword("Pas123", bcryptWorkfactor),
                    IsManager = false
                },
                new EfAccount
                {
                    Email = "al@m.dk",
                    PwHash = HashPassword("Pas123", bcryptWorkfactor),
                    IsManager = false
                },
                new EfAccount
                {
                    Email = "jk@m.dk",
                    PwHash = HashPassword("Pas123", bcryptWorkfactor),
                    IsManager = false
                }
                );
            context.SaveChanges();
        }

        static void SeedManagers(ApplicationDbContext context)
        {
            context.Managers.Add(
                new EfManager
                {
                    EfAccountId = 1,
                    Email = "boss@m.dk",
                    FirstName = "The",
                    LastName = "Boss",
                    
                });
                context.SaveChanges();
        }

            static void SeedModels(ApplicationDbContext context)
        {
            context.Models.AddRange(
                new EfModel
                {
                    EfAccountId = 2,
                    FirstName = "Naomi",
                    LastName = "Campbell",
                    Email = "nc@m.dk",
                    PhoneNo = "+44123654789",
                    AddresLine1 = "Abbey Road 68",
                    AddresLine2 = "",
                    Zip = "XYZ789",
                    City = "London",
                    BirthDate = new DateTime(1970, 5, 22),
                    Nationality = "United Kingdom",
                    Height = 1.78,
                    ShoeSize = 9,
                    HairColor = "Black",
                    EyeColor = "Hazel",
                    Comments = "One of the five original supermodels, Naomi Campbell was born in London and caught her break when she was 15 years old. She has graced the covers of more than 500 magazines during her career"
                },
                new EfModel
                {
                    EfAccountId = 3,
                    FirstName = "Helena",
                    LastName = "Christensen",
                    Email = "hc@m.dk",
                    PhoneNo = "+112345678",
                    AddresLine1 = "9th Avenue 678",
                    AddresLine2 = "Manhattan",
                    Zip = "12345",
                    City = "New York",
                    Country = "USA",
                    BirthDate = new DateTime(1968, 12, 25),
                    Nationality = "Denmark",
                    Height = 1.78,
                    ShoeSize = 9,
                    HairColor = "Brown",
                    EyeColor = "Green",
                    Comments = "She is a former Victoria's Secret Angel, clothing designer and beauty queen. Christensen was also the co-founder and original creative director for Nylon magazine, and she is a supporter of funding for breast cancer organizations and other philanthropic charities."
                },
                new EfModel
                {
                    EfAccountId = 4,
                    FirstName = "Alex",
                    LastName = "Lundqvist",
                    Email = "al@m.dk",
                    PhoneNo = "+44321654987",
                    AddresLine1 = "Queen Rd 129A",
                    AddresLine2 = "",
                    Zip = "ABZ123",
                    City = "London",
                    Country = "England",
                    BirthDate = new DateTime(1972, 4, 14),
                    Nationality = "Sweden",
                    Height = 1.83,
                    ShoeSize = 10,
                    HairColor = "Brown",
                    EyeColor = "Blue",
                    Comments = "Alex has worked with almost every top men's designer and brand in the world, definitely marking him as one of the original male supermodels"
                },
                new EfModel
                {
                    EfAccountId = 5,
                    FirstName = "Jon",
                    LastName = "Kortajarena",
                    Email = "jk@m.dk",
                    PhoneNo = "+3412345678",
                    AddresLine1 = "Avinguda del Paral·lel 127",
                    AddresLine2 = "",
                    Zip = "12345",
                    City = "Barcelona",
                    Country = "Spain",
                    BirthDate = new DateTime(1985, 5, 19),
                    Nationality = "Spain",
                    Height = 1.88,
                    ShoeSize = 11,
                    HairColor = "Chestnut",
                    EyeColor = "Brown",
                    Comments = "From Bilbao, Spain. Interests include photography and indie films"
                }
                );
            context.SaveChanges();
        }

        private static void SeedJobs(ApplicationDbContext context)
        {
            context.Jobs.AddRange(
                new EfJob
                {
                    Customer = "Vogue",
                    StartDate = new DateTimeOffset(2020, 05, 03, 9, 0, 0, TimeSpan.Zero),
                    Days = 2,
                    Location = "Tower Brigde London",
                    Comments = "Outdoor location!"
                },
                new EfJob
                {
                    Customer = "Vogue",
                    StartDate = new DateTimeOffset(2020, 06, 2, 10, 0, 0, TimeSpan.Zero),
                    Days = 1,
                    Location = "Hyde Park London",
                    Comments = "Only if sunshine."
                },
                new EfJob
                {
                    Customer = "Elle",
                    StartDate = new DateTimeOffset(2020, 05, 28, 8, 0, 0, TimeSpan.Zero),
                    Days = 1,
                    Location = "Eiffel Tower Paris",
                    Comments = ""
                },
                new EfJob
                {
                    Customer = "Versace",
                    StartDate = new DateTimeOffset(2020, 05, 29, 9, 0, 0, TimeSpan.FromHours(2)),
                    Days = 2,
                    Location = "Milan, Italy",
                    Comments = ""
                },
                new EfJob
                {
                    Customer = "Hugo Boss",
                    StartDate = new DateTimeOffset(2020, 05, 30, 8, 30, 0, TimeSpan.Zero),
                    Days = 2,
                    Location = "Alexanderplatz, Berlin, Tyskland",
                    Comments = ""
                }
                );
            context.SaveChanges();
        }

        private static void SeedJobModels(ApplicationDbContext context)
        {
            context.JobModels.AddRange(
                new EfJobModel
                {
                    EfJobId = 1,
                    EfModelId = 1
                },
                new EfJobModel
                {
                    EfJobId = 3,
                    EfModelId = 4
                }
                );
            context.SaveChanges();
        }

        private static void SeedExpenses(ApplicationDbContext context)
        {
            context.Expenses.Add(
                new EfExpense
                {
                    ModelId = 1,
                    JobId = 1,
                    Date = new DateTime(2020, 05, 03),
                    Text = "Taxi",
                    amount = 88.5M
                }
                );
            context.SaveChanges();
        }
    }
}
