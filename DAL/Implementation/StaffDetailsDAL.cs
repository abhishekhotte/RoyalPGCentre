﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    public class StaffDetailsDAL
    {
        RoyalPGCentreEntities dbEntities;
        public StaffDetailsDAL()
        {
            dbEntities = new DAL.RoyalPGCentreEntities();
        }

        ~StaffDetailsDAL()
        {
            dbEntities.Dispose();
        }

        public List<StaffDetails> GetStaffList()
        {
            var obj = dbEntities.Staffs.Select(s => new StaffDetails
            {
                Id = s.Id,
                Name = s.Name,
                Designation = s.Designation,
                Email = s.Email,
                Mobile = s.Mobile,
                TeachingStaff = s.TeachingStaff
            }).ToList();
            return obj;
        }

        public object AddUpdate(StaffDetails obj)
        {
            try
            {
                Staff s = new Staff
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Designation = obj.Designation,
                    Email = obj.Email,
                    Mobile = obj.Mobile,
                    TeachingStaff = obj.TeachingStaff
                };
                if (s.Id == 0)
                    dbEntities.Staffs.Add(s);
                else
                {
                    var staff = dbEntities.Staffs.Where(sf => sf.Id == obj.Id).FirstOrDefault();
                    staff.Name = s.Name;
                    staff.Designation = s.Designation;
                    staff.Email = s.Email;
                    staff.Mobile = s.Mobile;
                    staff.TeachingStaff = s.TeachingStaff;
                }
                dbEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
