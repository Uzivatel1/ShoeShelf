using ShoesShelf.Models;
using System.Diagnostics;

namespace ShoesShelf.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any shoes.
            if (context.Shoe.Any())
            {
                return;   // DB has been seeded
            }

            var brunswickMale40 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Male,
                Size = 40,
                Price = 1990M,
                InclusionDate = DateTime.Parse("11.02.2023"),
                Rented = false
            };

            var brunswickMale41 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Male,
                Size = 41,
                Price = 1990M,
                InclusionDate = DateTime.Parse("05.09.2023"),
                Rented = true
            };

            var brunswickMale42 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Male,
                Size = 42,
                Price = 1990M,
                InclusionDate = DateTime.Parse("23.03.2023"),
                Rented = false
            };

            var brunswickMale43 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Male,
                Size = 43,
                Price = 1990M,
                InclusionDate = DateTime.Parse("16.09.2023"),
                Rented = false
            };

            var brunswickMale44 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Male,
                Size = 44,
                Price = 1990M,
                InclusionDate = DateTime.Parse("14.07.2023"),
                Rented = false
            };

            var brunswickMale45 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Male,
                Size = 45,
                Price = 1990M,
                InclusionDate = DateTime.Parse("02.02.2023"),
                Rented = true
            };

            var brunswickFemale37 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Female,
                Size = 37,
                Price = 2690M,
                InclusionDate = DateTime.Parse("24.06.2023"),
                Rented = true
            };

            var brunswickFemale38 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Female,
                Size = 38,
                Price = 2690M,
                InclusionDate = DateTime.Parse("18.07.2023"),
                Rented = false
            };

            var brunswickFemale39 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Female,
                Size = 39,
                Price = 2690M,
                InclusionDate = DateTime.Parse("18.09.2023"),
                Rented = true
            };

            var brunswickFemale40 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Female,
                Size = 40,
                Price = 2690M,
                InclusionDate = DateTime.Parse("29.09.2023"),
                Rented = false
            };

            var brunswickFemale41 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Female,
                Size = 41,
                Price = 2690M,
                InclusionDate = DateTime.Parse("30.01.2023"),
                Rented = false
            };

            var brunswickFemale42 = new Shoe
            {
                Brand = "BRUNSWICK",
                Category = Category.Female,
                Size = 42,
                Price = 2690M,
                InclusionDate = DateTime.Parse("18.10.2023"),
                Rented = true
            };

            var globalMale40 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Male,
                Size = 40,
                Price = 1790M,
                InclusionDate = DateTime.Parse("05.08.2023"),
                Rented = false
            };

            var globalMale41 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Male,
                Size = 41,
                Price = 1790M,
                InclusionDate = DateTime.Parse("24.08.2023"),
                Rented = true
            };

            var globalMale42 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Male,
                Size = 42,
                Price = 1790M,
                InclusionDate = DateTime.Parse("13.05.2023"),
                Rented = true
            };

            var globalMale4301 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Male,
                Size = 43,
                Price = 1790M,
                InclusionDate = DateTime.Parse("11.03.2023"),
                Rented = true
            };

            var globalMale4302 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Male,
                Size = 43,
                Price = 1790M,
                InclusionDate = DateTime.Parse("11.03.2023"),
                Rented = false
            };

            var globalMale4303 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Male,
                Size = 43,
                Price = 1790M,
                InclusionDate = DateTime.Parse("11.03.2023"),
                Rented = true
            };

            var globalMale44 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Male,
                Size = 44,
                Price = 1790M,
                InclusionDate = DateTime.Parse("17.10.2023"),
                Rented = false
            };

            var globalMale45 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Male,
                Size = 45,
                Price = 1790M,
                InclusionDate = DateTime.Parse("15.06.2023"),
                Rented = true
            };

            var globalFemale37 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Female,
                Size = 37,
                Price = 1690M,
                InclusionDate = DateTime.Parse("14.09.2023"),
                Rented = true
            };

            var globalFemale38 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Female,
                Size = 38,
                Price = 1690M,
                InclusionDate = DateTime.Parse("01.04.2023"),
                Rented = false
            };

            var globalFemale39 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Female,
                Size = 39,
                Price = 1690M,
                InclusionDate = DateTime.Parse("08.07.2023"),
                Rented = false
            };

            var globalFemale40 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Female,
                Size = 40,
                Price = 1690M,
                InclusionDate = DateTime.Parse("09.11.2023"),
                Rented = true
            };

            var globalFemale41 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Female,
                Size = 41,
                Price = 1690M,
                InclusionDate = DateTime.Parse("29.07.2023"),
                Rented = true
            };

            var globalFemale42 = new Shoe
            {
                Brand = "900 GLOBAL",
                Category = Category.Female,
                Size = 42,
                Price = 1690M,
                InclusionDate = DateTime.Parse("25.09.2023"),
                Rented = false
            };

            var dexterMale40 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Male,
                Size = 40,
                Price = 2290M,
                InclusionDate = DateTime.Parse("17.10.2023"),
                Rented = false
            };

            var dexterMale41 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Male,
                Size = 41,
                Price = 2290M,
                InclusionDate = DateTime.Parse("06.03.2023"),
                Rented = false
            };

            var dexterMale4201 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Male,
                Size = 42,
                Price = 2290M,
                InclusionDate = DateTime.Parse("16.04.2023"),
                Rented = true
            };

            var dexterMale4202 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Male,
                Size = 42,
                Price = 2290M,
                InclusionDate = DateTime.Parse("16.04.2023"),
                Rented = false
            };

            var dexterMale43 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Male,
                Size = 43,
                Price = 2290M,
                InclusionDate = DateTime.Parse("03.11.2023"),
                Rented = false
            };

            var dexterMale44 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Male,
                Size = 44,
                Price = 2290M,
                InclusionDate = DateTime.Parse("20.01.2023"),
                Rented = true
            };

            var dexterMale4501 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Male,
                Size = 45,
                Price = 2290M,
                InclusionDate = DateTime.Parse("12.10.2023"),
                Rented = true
            };

            var dexterMale4502 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Male,
                Size = 45,
                Price = 2290M,
                InclusionDate = DateTime.Parse("25.01.2023"),
                Rented = false
            };

            var dexterFemale37 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Female,
                Size = 37,
                Price = 1590M,
                InclusionDate = DateTime.Parse("17.04.2023"),
                Rented = false
            };

            var dexterFemale38 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Female,
                Size = 38,
                Price = 1590M,
                InclusionDate = DateTime.Parse("04.03.2023"),
                Rented = false
            };

            var dexterFemale39 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Female,
                Size = 39,
                Price = 1590M,
                InclusionDate = DateTime.Parse("15.08.2023"),
                Rented = true
            };

            var dexterFemale40 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Female,
                Size = 40,
                Price = 1590M,
                InclusionDate = DateTime.Parse("13.07.2023"),
                Rented = false
            };

            var dexterFemale41 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Female,
                Size = 41,
                Price = 1590M,
                InclusionDate = DateTime.Parse("16.11.2023"),
                Rented = true
            };

            var dexterFemale42 = new Shoe
            {
                Brand = "DEXTER",
                Category = Category.Female,
                Size = 42,
                Price = 1590M,
                InclusionDate = DateTime.Parse("02.03.2023"),
                Rented = false
            };

            var shoes = new Shoe[]
            {
                brunswickMale40,
                brunswickMale41,
                brunswickMale42,
                brunswickMale43,
                brunswickMale44,
                brunswickMale45,
                brunswickFemale37,
                brunswickFemale38,
                brunswickFemale39,
                brunswickFemale40,
                brunswickFemale41,
                brunswickFemale42,
                globalMale40,
                globalMale41,
                globalMale42,
                globalMale4301,
                globalMale4302,
                globalMale4303,
                globalMale44,
                globalMale45,
                globalFemale37,
                globalFemale38,
                globalFemale39,
                globalFemale40,
                globalFemale41,
                globalFemale42,
                dexterMale40,
                dexterMale41,
                dexterMale4201,
                dexterMale4202,
                dexterMale43,
                dexterMale44,
                dexterMale4501,
                dexterMale4502,
                dexterFemale37,
                dexterFemale38,
                dexterFemale39,
                dexterFemale40,
                dexterFemale41,
                dexterFemale42
            };

            context.AddRange(shoes);

            var defects = new Defect[]
            {
                new Defect {
                    Shoe = globalFemale40,
                    Severity = Severity.Critical,
                    Description = "Cracked sole"
                },
                new Defect {
                    Shoe = brunswickMale43,
                    Severity = Severity.Critical,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = dexterMale44,
                    Severity = Severity.Critical,
                    Description = "Cracked stitching"
                },
                new Defect {
                    Shoe = globalFemale37,
                    Severity = Severity.Minor,
                    Description = "Cracked upper material"
                },
                new Defect {
                    Shoe = dexterMale4201,
                    Severity = Severity.Critical,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = brunswickMale40,
                    Severity = Severity.Minor,
                    Description = "Cracked stitching"
                },
                new Defect {
                    Shoe = dexterMale43,
                    Severity = Severity.Critical,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = dexterFemale39,
                    Severity = Severity.Major,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = globalFemale42,
                    Severity = Severity.Minor,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = globalFemale38,
                    Severity = Severity.Critical,
                    Description = "Cracked sole"
                },
                new Defect {
                    Shoe = dexterFemale41,
                    Severity = Severity.Major,
                    Description = "Cracked upper material"
                },
                new Defect {
                    Shoe = dexterFemale40,
                    Severity = Severity.Critical,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = globalFemale40,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = dexterFemale41,
                    Severity = Severity.Minor,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = dexterMale44,
                    Severity = Severity.Minor,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = globalFemale42,
                    Severity = Severity.Minor,
                    Description = "Cracked upper material"
                },
                new Defect {
                    Shoe = dexterFemale39,
                    Severity = Severity.Major,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = dexterMale43,
                    Severity = Severity.Critical,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = globalFemale42,
                    Severity = Severity.Critical,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = dexterMale43,
                    Severity = Severity.Major,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = dexterFemale39,
                    Severity = Severity.Major,
                    Description = "Cracked stitching"
                },
                new Defect {
                    Shoe = dexterFemale39,
                    Severity = Severity.Critical,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = dexterFemale40,
                    Severity = Severity.Critical,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = dexterMale43,
                    Severity = Severity.Major,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = brunswickMale44,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = dexterMale43,
                    Severity = Severity.Minor,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = dexterFemale40,
                    Severity = Severity.Minor,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = globalMale4301,
                    Severity = Severity.Critical,
                    Description = "Cracked sole"
                },
                new Defect {
                    Shoe = dexterMale44,
                    Severity = Severity.Major,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = brunswickMale41,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = dexterFemale39,
                    Severity = Severity.Major,
                    Description = "Cracked upper material"
                },
                new Defect {
                    Shoe = dexterFemale39,
                    Severity = Severity.Critical,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = globalMale45,
                    Severity = Severity.Minor,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = dexterFemale39,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = globalMale40,
                    Severity = Severity.Minor,
                    Description = "Cracked upper material"
                },
                new Defect {
                    Shoe = brunswickFemale39,
                    Severity = Severity.Major,
                    Description = "Cracked upper material"
                },
                new Defect {
                    Shoe = brunswickFemale40,
                    Severity = Severity.Critical,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = globalMale41,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = dexterMale4201,
                    Severity = Severity.Major,
                    Description = "Cracked stitching"
                },
                new Defect {
                    Shoe = brunswickFemale38,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = dexterFemale41,
                    Severity = Severity.Critical,
                    Description = "Cracked stitching"
                },
                new Defect {
                    Shoe = dexterFemale41,
                    Severity = Severity.Major,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = dexterMale4201,
                    Severity = Severity.Major,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = brunswickFemale42,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = globalMale41,
                    Severity = Severity.Critical,
                    Description = "Cracked sole"
                },
                new Defect {
                    Shoe = dexterFemale40,
                    Severity = Severity.Major,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = dexterMale4202,
                    Severity = Severity.Major,
                    Description = "Cracked upper material"
                },
                new Defect {
                    Shoe = dexterMale4202,
                    Severity = Severity.Critical,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = globalFemale39,
                    Severity = Severity.Major,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = globalFemale37,
                    Severity = Severity.Critical,
                    Description = "Cracked sole"
                },
                new Defect {
                    Shoe = dexterMale44,
                    Severity = Severity.Major,
                    Description = "Cracked upper material"
                },
                new Defect {
                    Shoe = globalFemale38,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = globalMale42,
                    Severity = Severity.Major,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = brunswickFemale37,
                    Severity = Severity.Minor,
                    Description = "Cracked stitching"
                },
                new Defect {
                    Shoe = globalMale40,
                    Severity = Severity.Critical,
                    Description = "Cracked sole"
                },
                new Defect {
                    Shoe = dexterFemale40,
                    Severity = Severity.Major,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = brunswickFemale41,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = globalMale4302,
                    Severity = Severity.Critical,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = globalMale41,
                    Severity = Severity.Major,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = globalFemale38,
                    Severity = Severity.Major,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = globalFemale38,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = globalMale41,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = globalMale45,
                    Severity = Severity.Minor,
                    Description = "Cracked upper material"
                },
                new Defect {
                    Shoe = dexterMale4201,
                    Severity = Severity.Major,
                    Description = "Heel release"
                },
                new Defect {
                    Shoe = globalMale45,
                    Severity = Severity.Critical,
                    Description = "Ungluing"
                },
                new Defect {
                    Shoe = dexterMale4202,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = globalMale4303,
                    Severity = Severity.Critical,
                    Description = "Cracked upper material"
                },
                new Defect {
                    Shoe = brunswickMale45,
                    Severity = Severity.Minor,
                    Description = "Destruction of the surface"
                },
                new Defect {
                    Shoe = brunswickMale42,
                    Severity = Severity.Major,
                    Description = "Cracked upper material"
                }
            };

            context.AddRange(defects);

            var rentals = new Rental[]
            {
                new Rental {
                    Shoe = brunswickMale40,
                    RentalDate = DateTime.Parse("11.02.2023")
                },
                new Rental {
                    Shoe = brunswickMale40,
                    RentalDate = DateTime.Parse("11.03.2023")
                },
                new Rental {
                    Shoe = brunswickMale40,
                    RentalDate = DateTime.Parse("11.04.2023")
                },
                new Rental {
                    Shoe = brunswickMale41,
                    RentalDate = DateTime.Parse("05.09.2023")
                },
                new Rental {
                    Shoe = brunswickMale41,
                    RentalDate = DateTime.Parse("05.10.2023")
                },
                new Rental {
                    Shoe = brunswickMale42,
                    RentalDate = DateTime.Parse("23.03.2023")
                },
                new Rental {
                    Shoe = brunswickMale42,
                    RentalDate = DateTime.Parse("23.04.2023")
                },
                new Rental {
                    Shoe = brunswickMale43,
                    RentalDate = DateTime.Parse("16.09.2023")
                },
                new Rental {
                    Shoe = brunswickMale43,
                    RentalDate = DateTime.Parse("16.10.2023")
                },
                new Rental {
                    Shoe = brunswickMale43,
                    RentalDate = DateTime.Parse("16.11.2023")
                },
                new Rental {
                    Shoe = brunswickMale44,
                    RentalDate = DateTime.Parse("14.07.2023")
                },
                new Rental {
                    Shoe = brunswickMale44,
                    RentalDate = DateTime.Parse("14.08.2023")
                },
                new Rental {
                    Shoe = brunswickMale44,
                    RentalDate = DateTime.Parse("14.09.2023")
                },
                new Rental {
                    Shoe = brunswickMale44,
                    RentalDate = DateTime.Parse("14.10.2023")
                },
                new Rental {
                    Shoe = brunswickMale45,
                    RentalDate = DateTime.Parse("02.02.2023")
                },
                new Rental {
                    Shoe = brunswickMale45,
                    RentalDate = DateTime.Parse("02.03.2023")
                },
                new Rental {
                    Shoe = brunswickFemale37,
                    RentalDate = DateTime.Parse("24.06.2023")
                },
                new Rental {
                    Shoe = brunswickFemale37,
                    RentalDate = DateTime.Parse("24.07.2023")
                },
                new Rental {
                    Shoe = brunswickFemale38,
                    RentalDate = DateTime.Parse("18.07.2023")
                },
                new Rental {
                    Shoe = brunswickFemale38,
                    RentalDate = DateTime.Parse("18.08.2023")
                },
                new Rental {
                    Shoe = brunswickFemale39,
                    RentalDate = DateTime.Parse("18.09.2023")
                },
                new Rental {
                    Shoe = brunswickFemale39,
                    RentalDate = DateTime.Parse("18.10.2023")
                },
                new Rental {
                    Shoe = brunswickFemale39,
                    RentalDate = DateTime.Parse("18.11.2023")
                },
                new Rental {
                    Shoe = brunswickFemale40,
                    RentalDate = DateTime.Parse("29.09.2023")
                },
                new Rental {
                    Shoe = brunswickFemale40,
                    RentalDate = DateTime.Parse("29.10.2023")
                },
                new Rental {
                    Shoe = brunswickFemale40,
                    RentalDate = DateTime.Parse("29.11.2023")
                },
                new Rental {
                    Shoe = brunswickFemale41,
                    RentalDate = DateTime.Parse("30.01.2023")
                },
                new Rental {
                    Shoe = brunswickFemale41,
                    RentalDate = DateTime.Parse("30.03.2023")
                },
                new Rental {
                    Shoe = brunswickFemale41,
                    RentalDate = DateTime.Parse("30.04.2023")
                },
                new Rental {
                    Shoe = brunswickFemale41,
                    RentalDate = DateTime.Parse("30.05.2023")
                },
                new Rental {
                    Shoe = brunswickFemale42,
                    RentalDate = DateTime.Parse("18.10.2023")
                },
                new Rental {
                    Shoe = globalMale40,
                    RentalDate = DateTime.Parse("05.08.2023")
                },
                new Rental {
                    Shoe = globalMale41,
                    RentalDate = DateTime.Parse("24.08.2023")
                },
                new Rental {
                    Shoe = globalMale42,
                    RentalDate = DateTime.Parse("13.05.2023")
                },
                new Rental {
                    Shoe = globalMale4301,
                    RentalDate = DateTime.Parse("11.03.2023")
                },
                new Rental {
                    Shoe = globalMale4301,
                    RentalDate = DateTime.Parse("11.04.2023")
                },
                new Rental {
                    Shoe = globalMale4302,
                    RentalDate = DateTime.Parse("11.04.2023")
                },
                new Rental {
                    Shoe = globalMale4302,
                    RentalDate = DateTime.Parse("11.05.2023")
                },
                new Rental {
                    Shoe = globalMale4302,
                    RentalDate = DateTime.Parse("11.06.2023")
                },
                new Rental {
                    Shoe = globalMale4303,
                    RentalDate = DateTime.Parse("11.03.2023")
                },
                new Rental {
                    Shoe = globalMale4302,
                    RentalDate = DateTime.Parse("11.04.2023")
                },
                new Rental {
                    Shoe = globalMale4302,
                    RentalDate = DateTime.Parse("11.05.2023")
                },
                new Rental {
                    Shoe = globalMale4302,
                    RentalDate = DateTime.Parse("11.06.2023")
                },
                new Rental {
                    Shoe = globalMale44,
                    RentalDate = DateTime.Parse("17.10.2023")
                },
                new Rental {
                    Shoe = globalMale44,
                    RentalDate = DateTime.Parse("17.11.2023")
                },
                new Rental {
                    Shoe = globalMale44,
                    RentalDate = DateTime.Parse("11.12.2023")
                },
                new Rental {
                    Shoe = globalMale44,
                    RentalDate = DateTime.Parse("17.12.2023")
                },
                new Rental {
                    Shoe = globalMale45,
                    RentalDate = DateTime.Parse("15.06.2023")
                },
                new Rental {
                    Shoe = globalFemale37,
                    RentalDate = DateTime.Parse("14.09.2023")
                },
                new Rental {
                    Shoe = globalFemale38,
                    RentalDate = DateTime.Parse("01.04.2023")
                },
                new Rental {
                    Shoe = globalFemale38,
                    RentalDate = DateTime.Parse("01.05.2023")
                },
                new Rental {
                    Shoe = globalFemale39,
                    RentalDate = DateTime.Parse("08.07.2023")
                },
                new Rental {
                    Shoe = globalFemale39,
                    RentalDate = DateTime.Parse("08.08.2023")
                },
                new Rental {
                    Shoe = globalFemale40,
                    RentalDate = DateTime.Parse("09.11.2023")
                },
                new Rental {
                    Shoe = globalFemale40,
                    RentalDate = DateTime.Parse("09.12.2023")
                },
                new Rental {
                    Shoe = globalFemale41,
                    RentalDate = DateTime.Parse("29.07.2023")
                },
                new Rental {
                    Shoe = globalFemale41,
                    RentalDate = DateTime.Parse("29.08.2023")
                },
                new Rental {
                    Shoe = globalFemale41,
                    RentalDate = DateTime.Parse("29.09.2023")
                },
                new Rental {
                    Shoe = globalFemale41,
                    RentalDate = DateTime.Parse("29.10.2023")
                },
                new Rental {
                    Shoe = globalFemale42,
                    RentalDate = DateTime.Parse("25.09.2023")
                },
                new Rental {
                    Shoe = dexterMale40,
                    RentalDate = DateTime.Parse("17.10.2023")
                },
                new Rental {
                    Shoe = dexterMale41,
                    RentalDate = DateTime.Parse("06.03.2023")
                },
                new Rental {
                    Shoe = dexterMale4201,
                    RentalDate = DateTime.Parse("16.04.2023")
                },
                new Rental {
                    Shoe = dexterMale4201,
                    RentalDate = DateTime.Parse("16.05.2023")
                },
                new Rental {
                    Shoe = dexterMale4202,
                    RentalDate = DateTime.Parse("16.05.2023")
                },
                new Rental {
                    Shoe = dexterMale4202,
                    RentalDate = DateTime.Parse("16.06.2023")
                },
                new Rental {
                    Shoe = dexterMale43,
                    RentalDate = DateTime.Parse("03.11.2023")
                },
                new Rental {
                    Shoe = dexterMale43,
                    RentalDate = DateTime.Parse("13.11.2023")
                },
                new Rental {
                    Shoe = dexterMale43,
                    RentalDate = DateTime.Parse("03.12.2023")
                },
                new Rental {
                    Shoe = dexterMale44,
                    RentalDate = DateTime.Parse("20.01.2023")
                },
                new Rental {
                    Shoe = dexterMale44,
                    RentalDate = DateTime.Parse("20.02.2023")
                },
                new Rental {
                    Shoe = dexterMale4501,
                    RentalDate = DateTime.Parse("12.10.2023")
                },
                new Rental {
                    Shoe = dexterFemale37,
                    RentalDate = DateTime.Parse("17.04.2023")
                },
                new Rental {
                    Shoe = dexterFemale38,
                    RentalDate = DateTime.Parse("04.03.2023")
                },
                new Rental {
                    Shoe = dexterFemale39,
                    RentalDate = DateTime.Parse("15.08.2023")
                },
                new Rental {
                    Shoe = dexterFemale39,
                    RentalDate = DateTime.Parse("15.09.2023")
                },
                new Rental {
                    Shoe = dexterFemale40,
                    RentalDate = DateTime.Parse("13.07.2023")
                },
                new Rental {
                    Shoe = dexterFemale40,
                    RentalDate = DateTime.Parse("13.08.2023")
                },
                new Rental {
                    Shoe = dexterFemale40,
                    RentalDate = DateTime.Parse("13.09.2023")
                },
                new Rental {
                    Shoe = dexterFemale41,
                    RentalDate = DateTime.Parse("16.11.2023")
                },
                new Rental {
                    Shoe = dexterFemale41,
                    RentalDate = DateTime.Parse("16.12.2023")
                },
                new Rental {
                    Shoe = dexterFemale42,
                    RentalDate = DateTime.Parse("02.03.2023")
                }
            };

            context.AddRange(rentals);

            var disinfections = new Disinfection[]
            {
                new Disinfection {
                    Shoe = brunswickMale40,
                    DisinfectionDate = DateTime.Parse("11.02.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale40,
                    DisinfectionDate = DateTime.Parse("11.03.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale40,
                    DisinfectionDate = DateTime.Parse("11.04.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale41,
                    DisinfectionDate = DateTime.Parse("05.09.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale41,
                    DisinfectionDate = DateTime.Parse("05.10.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale42,
                    DisinfectionDate = DateTime.Parse("23.03.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale42,
                    DisinfectionDate = DateTime.Parse("23.04.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale43,
                    DisinfectionDate = DateTime.Parse("16.09.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale43,
                    DisinfectionDate = DateTime.Parse("16.10.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale43,
                    DisinfectionDate = DateTime.Parse("16.11.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale44,
                    DisinfectionDate = DateTime.Parse("14.07.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale44,
                    DisinfectionDate = DateTime.Parse("14.08.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale44,
                    DisinfectionDate = DateTime.Parse("14.09.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale44,
                    DisinfectionDate = DateTime.Parse("14.10.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale45,
                    DisinfectionDate = DateTime.Parse("02.02.2023")
                },
                new Disinfection {
                    Shoe = brunswickMale45,
                    DisinfectionDate = DateTime.Parse("02.03.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale37,
                    DisinfectionDate = DateTime.Parse("24.06.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale37,
                    DisinfectionDate = DateTime.Parse("24.07.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale38,
                    DisinfectionDate = DateTime.Parse("18.07.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale38,
                    DisinfectionDate = DateTime.Parse("18.08.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale39,
                    DisinfectionDate = DateTime.Parse("18.09.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale39,
                    DisinfectionDate = DateTime.Parse("18.10.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale39,
                    DisinfectionDate = DateTime.Parse("18.11.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale40,
                    DisinfectionDate = DateTime.Parse("29.09.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale40,
                    DisinfectionDate = DateTime.Parse("29.10.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale40,
                    DisinfectionDate = DateTime.Parse("29.11.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale41,
                    DisinfectionDate = DateTime.Parse("30.01.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale41,
                    DisinfectionDate = DateTime.Parse("30.03.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale41,
                    DisinfectionDate = DateTime.Parse("30.04.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale41,
                    DisinfectionDate = DateTime.Parse("30.05.2023")
                },
                new Disinfection {
                    Shoe = brunswickFemale42,
                    DisinfectionDate = DateTime.Parse("18.10.2023")
                },
                new Disinfection {
                    Shoe = globalMale40,
                    DisinfectionDate = DateTime.Parse("05.08.2023")
                },
                new Disinfection {
                    Shoe = globalMale41,
                    DisinfectionDate = DateTime.Parse("24.08.2023")
                },
                new Disinfection {
                    Shoe = globalMale42,
                    DisinfectionDate = DateTime.Parse("13.05.2023")
                },
                new Disinfection {
                    Shoe = globalMale4301,
                    DisinfectionDate = DateTime.Parse("11.03.2023")
                },
                new Disinfection {
                    Shoe = globalMale4301,
                    DisinfectionDate = DateTime.Parse("11.04.2023")
                },
                new Disinfection {
                    Shoe = globalMale4302,
                    DisinfectionDate = DateTime.Parse("11.04.2023")
                },
                new Disinfection {
                    Shoe = globalMale4302,
                    DisinfectionDate = DateTime.Parse("11.05.2023")
                },
                new Disinfection {
                    Shoe = globalMale4302,
                    DisinfectionDate = DateTime.Parse("11.06.2023")
                },
                new Disinfection {
                    Shoe = globalMale4303,
                    DisinfectionDate = DateTime.Parse("11.03.2023")
                },
                new Disinfection {
                    Shoe = globalMale4302,
                    DisinfectionDate = DateTime.Parse("11.04.2023")
                },
                new Disinfection {
                    Shoe = globalMale4302,
                    DisinfectionDate = DateTime.Parse("11.05.2023")
                },
                new Disinfection {
                    Shoe = globalMale4302,
                    DisinfectionDate = DateTime.Parse("11.06.2023")
                },
                new Disinfection {
                    Shoe = globalMale44,
                    DisinfectionDate = DateTime.Parse("17.10.2023")
                },
                new Disinfection {
                    Shoe = globalMale44,
                    DisinfectionDate = DateTime.Parse("17.11.2023")
                },
                new Disinfection {
                    Shoe = globalMale44,
                    DisinfectionDate = DateTime.Parse("11.12.2023")
                },
                new Disinfection {
                    Shoe = globalMale44,
                    DisinfectionDate = DateTime.Parse("17.12.2023")
                },
                new Disinfection {
                    Shoe = globalMale45,
                    DisinfectionDate = DateTime.Parse("15.06.2023")
                },
                new Disinfection {
                    Shoe = globalFemale37,
                    DisinfectionDate = DateTime.Parse("14.09.2023")
                },
                new Disinfection {
                    Shoe = globalFemale38,
                    DisinfectionDate = DateTime.Parse("01.04.2023")
                },
                new Disinfection {
                    Shoe = globalFemale38,
                    DisinfectionDate = DateTime.Parse("01.05.2023")
                },
                new Disinfection {
                    Shoe = globalFemale39,
                    DisinfectionDate = DateTime.Parse("08.07.2023")
                },
                new Disinfection {
                    Shoe = globalFemale39,
                    DisinfectionDate = DateTime.Parse("08.08.2023")
                },
                new Disinfection {
                    Shoe = globalFemale40,
                    DisinfectionDate = DateTime.Parse("09.11.2023")
                },
                new Disinfection {
                    Shoe = globalFemale40,
                    DisinfectionDate = DateTime.Parse("09.12.2023")
                },
                new Disinfection {
                    Shoe = globalFemale41,
                    DisinfectionDate = DateTime.Parse("29.07.2023")
                },
                new Disinfection {
                    Shoe = globalFemale41,
                    DisinfectionDate = DateTime.Parse("29.08.2023")
                },
                new Disinfection {
                    Shoe = globalFemale41,
                    DisinfectionDate = DateTime.Parse("29.09.2023")
                },
                new Disinfection {
                    Shoe = globalFemale41,
                    DisinfectionDate = DateTime.Parse("29.10.2023")
                },
                new Disinfection {
                    Shoe = globalFemale42,
                    DisinfectionDate = DateTime.Parse("25.09.2023")
                },
                new Disinfection {
                    Shoe = dexterMale40,
                    DisinfectionDate = DateTime.Parse("17.10.2023")
                },
                new Disinfection {
                    Shoe = dexterMale41,
                    DisinfectionDate = DateTime.Parse("06.03.2023")
                },
                new Disinfection {
                    Shoe = dexterMale4201,
                    DisinfectionDate = DateTime.Parse("16.04.2023")
                },
                new Disinfection {
                    Shoe = dexterMale4201,
                    DisinfectionDate = DateTime.Parse("16.05.2023")
                },
                new Disinfection {
                    Shoe = dexterMale4202,
                    DisinfectionDate = DateTime.Parse("16.05.2023")
                },
                new Disinfection {
                    Shoe = dexterMale4202,
                    DisinfectionDate = DateTime.Parse("16.06.2023")
                },
                new Disinfection {
                    Shoe = dexterMale43,
                    DisinfectionDate = DateTime.Parse("03.11.2023")
                },
                new Disinfection {
                    Shoe = dexterMale43,
                    DisinfectionDate = DateTime.Parse("13.11.2023")
                },
                new Disinfection {
                    Shoe = dexterMale43,
                    DisinfectionDate = DateTime.Parse("03.12.2023")
                },
                new Disinfection {
                    Shoe = dexterMale44,
                    DisinfectionDate = DateTime.Parse("20.01.2023")
                },
                new Disinfection {
                    Shoe = dexterMale44,
                    DisinfectionDate = DateTime.Parse("20.02.2023")
                },
                new Disinfection {
                    Shoe = dexterMale4501,
                    DisinfectionDate = DateTime.Parse("12.10.2023")
                },
                new Disinfection {
                    Shoe = dexterFemale37,
                    DisinfectionDate = DateTime.Parse("17.04.2023")
                },
                new Disinfection {
                    Shoe = dexterFemale38,
                    DisinfectionDate = DateTime.Parse("04.03.2023")
                },
                new Disinfection {
                    Shoe = dexterFemale39,
                    DisinfectionDate = DateTime.Parse("15.08.2023")
                },
                new Disinfection {
                    Shoe = dexterFemale39,
                    DisinfectionDate = DateTime.Parse("15.09.2023")
                },
                new Disinfection {
                    Shoe = dexterFemale40,
                    DisinfectionDate = DateTime.Parse("13.07.2023")
                },
                new Disinfection {
                    Shoe = dexterFemale40,
                    DisinfectionDate = DateTime.Parse("13.08.2023")
                },
                new Disinfection {
                    Shoe = dexterFemale40,
                    DisinfectionDate = DateTime.Parse("13.09.2023")
                },
                new Disinfection {
                    Shoe = dexterFemale41,
                    DisinfectionDate = DateTime.Parse("16.11.2023")
                },
                new Disinfection {
                    Shoe = dexterFemale41,
                    DisinfectionDate = DateTime.Parse("16.12.2023")
                },
                new Disinfection {
                    Shoe = dexterFemale42,
                    DisinfectionDate = DateTime.Parse("02.03.2023")
                }
            };

            context.AddRange(disinfections);

            context.SaveChanges();
        }
    }
}
