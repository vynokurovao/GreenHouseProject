﻿using GreenHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenHouse.ContexManager
{
    public class ReservationManager
    {
        static Entities db;

        private List<List<TD>> table;

        public List<List<TD>> Table
        {
            get
            {
                return table;
            }
            set
            {
                table = value;
            }
        }
        

        public ReservationManager(DateTime date)
        {
            db = new Entities();

            Table = GetDateReservation(date);
        }

        private Reservation GetAuditoriumReservation(int AuditoriumId, DateTime date)
        {
            Reservation reservation = null;

            db = new Entities();

            foreach (Reservation reserv in db.Reservation)
            {
                if (reserv.TargetAuditorium == AuditoriumId && ( reserv.StartDate <= date && reserv.FinishDate > date))
                {
                    reservation = reserv;

                    return reservation;
                }
            }

            return reservation;
        }

        private TD ConvertToTD(Reservation reservation)
        {
            TD td = new TD();

            td.User = reservation.User;

            td.ReservationId = reservation.ReservationId;

            td.Auditorium = reservation.Auditorium;

            td.CreatedBy = reservation.CreatedBy;

            td.FinishDate = reservation.FinishDate;

            td.StartDate = reservation.StartDate;

            td.TargetAuditorium = reservation.TargetAuditorium;

            td.Type = reservation.Type;

            td.Purpose = reservation.Purpose;

            return td;
        }


        public List<List<TD>> GetDateReservation(DateTime date)
        {
            List<List<TD>> table = new List<List<TD>>();

            db = new Entities();

            for (int i = 9; i <= 21; i++)
            {
                List<TD> row = new List<TD>();

                TD first = new TD();

                first.ReservationId = -2;

                first.Purpose = i.ToString();

                row.Add(first);

                int roomNumber = 0;

                foreach (Auditorium auditory in db.Auditorium)
                {
                    Reservation reserv = GetAuditoriumReservation(auditory.AuditoriumId, new DateTime(date.Year, date.Month, date.Day, i, 0, 0));

                    TD td = new TD();

                    if (reserv == null)
                    {
                        td.ReservationId = -1;

                        td.StartDate = new DateTime(date.Year, date.Month, date.Day, i, 0, 0);

                        td.TargetAuditorium = auditory.AuditoriumId;
                    }
                    else
                    {
                        td = ConvertToTD(reserv);
                    }

                    td.Hour = i;

                    td.Placement = "right";

                    if (roomNumber >= 8)
                    {
                        td.Placement = "left";
                    }

                    if (i == 9)
                    {
                        td.Placement = "bottom";
                    }
                    else if (i == 20 || i == 21)
                    {
                        td.Placement = "top";
                    }
                    
                    roomNumber++;

                    row.Add(td);
                }

                table.Add(row);
            }

            return table;
        }

        public List<List<TD>> GetWeekReservation(DateTime date, string auditoriumName)
        {
            List<List<TD>> table = new List<List<TD>>();

            DayOfWeek dayOfWeek = date.DayOfWeek;

            DateTime startdate = date;

            IQueryable<Auditorium> auditorium = db.Auditorium
                       .Where(auditor => auditor.AuditoriumName.Equals(auditoriumName));

            for (int i = 9; i <= 21; i++)
            {
                startdate = date;

                startdate = startdate.Subtract(new TimeSpan((int)dayOfWeek - 1, 0, 0, 0));

                List<TD> row = new List<TD>();

                TD first = new TD();

                first.ReservationId = -2;

                first.Purpose = i.ToString();

                row.Add(first);

                int roomNumber = 0;

                for (int day = 0; day < 7; day++)
                {
                    foreach (Auditorium auditory in auditorium)
                    {
                        Reservation reserv = GetAuditoriumReservation(auditory.AuditoriumId, new DateTime(startdate.Year, startdate.Month, startdate.Day, i, 0, 0));

                        TD td = new TD();

                        if (reserv == null)
                        {
                            td.ReservationId = -1;

                            td.StartDate = new DateTime(startdate.Year, startdate.Month, startdate.Day, i, 0, 0);

                            td.TargetAuditorium = auditory.AuditoriumId;
                        }
                        else
                        {
                            td = ConvertToTD(reserv);
                        }

                        td.Hour = i;

                        td.Placement = "right";

                        if (roomNumber >= 5)
                        {
                            td.Placement = "left";
                        }

                        if (i == 9)
                        {
                            td.Placement = "bottom";
                        }
                        else if (i == 20 || i == 21)
                        {
                            td.Placement = "top";
                        }

                        row.Add(td);
                    }

                    roomNumber++;

                    startdate = startdate.AddDays(1);
                }

                table.Add(row);
            }
            return table;
        }

        public List<List<TD>> GetDayRoomReservation(DateTime date, string auditoriumName)
        {
            IQueryable < Auditorium > auditorium = db.Auditorium
                        .Where(auditor => auditor.AuditoriumName.Equals(auditoriumName));
            
            for (int i = 9; i <= 21; i++)
            {
                List<TD> row = new List<TD>();

                TD first = new TD();

                first.ReservationId = -2;

                first.Purpose = i.ToString();

                row.Add(first);

                int roomNumber = 0;

                foreach (Auditorium auditory in auditorium)
                {
                    Reservation reserv = GetAuditoriumReservation(auditory.AuditoriumId, new DateTime(date.Year, date.Month, date.Day, i, 0, 0));

                    TD td = new TD();

                    if (reserv == null)
                    {
                        td.ReservationId = -1;

                        td.StartDate = new DateTime(date.Year, date.Month, date.Day, i, 0, 0);

                        td.TargetAuditorium = auditory.AuditoriumId;
                    }
                    else
                    {
                        td = ConvertToTD(reserv);
                    }

                    td.Hour = i;

                    td.Placement = "bottom";

                    if (i == 17 || i == 18 || i == 19 || i == 20 || i == 21)
                    {
                        td.Placement = "top";
                    }

                    roomNumber++;

                    row.Add(td);
                }
                table.Add(row);
            }

            return table;
        }

        public List<string> GetDays(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;

            DateTime startdate = date;

            startdate = startdate.Subtract(new TimeSpan((int)dayOfWeek - 1, 0, 0, 0));

            List<string> list = new List<string>();

            for (int i = 0; i < 7; i++)
            {
                string dayName = "";

                switch (startdate.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        dayName = "Понедельник";
                        break;
                    case DayOfWeek.Tuesday:
                        dayName = "Вторник";
                        break;
                    case DayOfWeek.Wednesday:
                        dayName = "Среда";
                        break;
                    case DayOfWeek.Thursday:
                        dayName = "Четверг";
                        break;
                    case DayOfWeek.Friday:
                        dayName = "Пятница";
                        break;
                    case DayOfWeek.Saturday:
                        dayName = "Суббота";
                        break;
                    case DayOfWeek.Sunday:
                        dayName = "Воскресенье";
                        break;
                }

                list.Add(dayName + " " + startdate.Day + "." + startdate.Month + "." + startdate.Year);

                startdate = startdate.AddDays(1);
            }

            return list;
        }
    }
}