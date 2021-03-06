﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    public class NewsDAL
    {
        RoyalPGCentreEntities dbEntities;
        public NewsDAL()
        {
            dbEntities = new DAL.RoyalPGCentreEntities();
        }

        ~NewsDAL()
        {
            dbEntities.Dispose();
        }

        public List<Entities.NewsDetails> GetNewsList()
        {
            var tmp= dbEntities.GetNewsDetails().Select(s => new Entities.NewsDetails()
            {
                Id = s.Id,
                Name=s.Name,
                Description=s.Description,
                Date= Convert.ToDateTime(s.Date),
                CreatedDate = Convert.ToDateTime(s.CreatedDate),
            }).Where(d=>d.Date >= DateTime.Now)
            .ToList();
            return tmp;
        }

        public object AddUpdate(News obj, out int newsId)
        {
            newsId = 0;
            try
            {
                ObjectParameter outparameter = new ObjectParameter("newsId", typeof(int));
                dbEntities.AddUpdateNews(obj.Id, obj.Name, obj.Description, obj.Date, obj.ExpiresDate, outparameter);
                newsId = Convert.ToInt32(outparameter.Value);
                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
