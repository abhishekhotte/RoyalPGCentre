using BAL.Interfaces;
using DAL;
using DAL.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BAL.Implementation
{
    public class NewsBAL :INews
    {
        NewsDAL news = new NewsDAL();

        public object AddUpdate(DAL.News obj, ref int newsId)
        {
            return news.AddUpdate(obj, out newsId);
        }

        public List<DAL.Entities.NewsDetails> GetNewsList()
        {
            return news.GetNewsList();
        }
    }
}
