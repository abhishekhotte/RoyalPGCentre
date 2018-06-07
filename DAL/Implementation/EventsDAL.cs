using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    public class EventsDAL
    {
        RoyalPGCentreEntities dbEntities;
        public EventsDAL()
        {
            dbEntities = new DAL.RoyalPGCentreEntities();
        }

        public object GetEventsList()
        {
            var obj = dbEntities.Events.Select(e => new 
            {
                Id = e.Id,
                Name = e.Name,
                Date= e.Date
            }).ToList();
            return obj;
        }

        public object Add(Event obj, ref int eventId)
        {
            try
            {
                dbEntities.Events.Add(obj);
                dbEntities.SaveChanges();
                eventId = obj.Id;
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }

        }
    }
}
