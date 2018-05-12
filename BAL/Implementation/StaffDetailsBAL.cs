using BAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Implementation;
using DAL.Entities;

namespace BAL.Implementation
{
    class StaffDetailsBAL : IStaffDetails
    {
        StaffDetailsDAL staffDetails;
        public StaffDetailsBAL()
        {
            staffDetails = new StaffDetailsDAL();
        }

        public object AddUpdateStaffDetails(StaffDetails obj, ref int staffId)
        {
            return staffDetails.AddUpdate(obj,ref staffId);
        }

        public List<StaffDetails> GetStaffList()
        {
            return staffDetails.GetStaffList();
        }
    }
}
