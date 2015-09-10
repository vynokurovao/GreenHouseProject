using System;
using System.Collections.Generic;
using System.Linq;
using GreenHouse.Models;

namespace GreenHouse.ContexManager
{
    public class DBInitialization
    {

        public void Initialization(Entities context)
        {
            
            var roles = new List<Role>
            {
                new Role { RoleId =1, RoleName= "admin"},
                new Role { RoleId =2, RoleName= "Client"}
            };
            roles.ForEach(r => context.Role.Add(r));
            context.SaveChanges();


            var users = new List<User>
            {
                new User { Role = 1, Email = "levvania@mail.ru", Surname = "Левицкий", FirstName = "Иван", Password = "pass" },
                new User { Role = 2, Email = "rahuba@ukr.net", Surname = "Рахуба", FirstName = "Олександр", Password = "pass" },
            };
            users.ForEach(u => context.User.Add(u));
            context.SaveChanges();


            var additionalEquipments = new List<AdditionalEquipment>
            {
                new AdditionalEquipment {AdditionalEquipmentName = "Wifi"},
                new AdditionalEquipment {AdditionalEquipmentName = "Проектор"},
                new AdditionalEquipment {AdditionalEquipmentName = "Монитор"},
                new AdditionalEquipment {AdditionalEquipmentName = "Микрофон"}
            };
            additionalEquipments.ForEach(adde => context.AdditionalEquipment.Add(adde));
            context.SaveChanges();

    
            var auditoriums = new List<Auditorium>
            {
                new Auditorium {AuditoriumName = "301", Capacity = 35},
                new Auditorium {AuditoriumName = "302", Capacity = 25 },
                new Auditorium {AuditoriumName = "303", Capacity = 45 },
                new Auditorium {AuditoriumName = "304", Capacity = 55 },
                new Auditorium {AuditoriumName = "305", Capacity = 65 },
                new Auditorium {AuditoriumName = "306", Capacity = 15 },
                new Auditorium {AuditoriumName = "307", Capacity = 85 },
                new Auditorium {AuditoriumName = "308", Capacity = 30 },
                new Auditorium {AuditoriumName = "309", Capacity = 40 },
                new Auditorium {AuditoriumName = "310", Capacity = 53 },
                new Auditorium {AuditoriumName = "311", Capacity = 25 },
                new Auditorium {AuditoriumName = "312", Capacity = 100 },
                new Auditorium {AuditoriumName = "313", Capacity = 32 },
                new Auditorium {AuditoriumName = "314", Capacity = 65 },
                new Auditorium {AuditoriumName = "315", Capacity = 19 }
            };
            auditoriums.ForEach(a => context.Auditorium.Add(a));
            context.SaveChanges();


            var reservation = new List<Reservation>
            {
                new Reservation {TargetAuditorium = context.Auditorium.Where(a => a.AuditoriumName.Equals("301")).First().AuditoriumId,
                                CreatedBy = context.User.Where(u => u.Email.Equals("levvania@mail.ru")).First().UserId,
                                Purpose = "Бизнес-митинг", Type = true, StartDate = new DateTime(2015, 09 ,09 ,09, 0, 0), FinishDate = new DateTime(2015, 09, 09, 10, 0, 0)
                                },
                new Reservation {TargetAuditorium = context.Auditorium.Where(a => a.AuditoriumName.Equals("303")).First().AuditoriumId,
                                CreatedBy = context.User.Where(u => u.Email.Equals("levvania@mail.ru")).First().UserId,
                                Purpose = "Семинар", Type = true, StartDate = new DateTime(2015, 09 ,09 ,11, 0, 0), FinishDate = new DateTime(2015, 09, 09, 12, 0, 0)
                                },
                new Reservation {TargetAuditorium = context.Auditorium.Where(a => a.AuditoriumName.Equals("306")).First().AuditoriumId,
                                CreatedBy = context.User.Where(u => u.Email.Equals("rahuba@ukr.net")).First().UserId,
                                Purpose = "Лекция", Type = true, StartDate = new DateTime(2015, 09 ,09 ,14, 0, 0), FinishDate = new DateTime(2015, 09, 09, 15, 0, 0)
                                }
            };
            reservation.ForEach(r => context.Reservation.Add(r));
            context.SaveChanges();
        }

    }
}