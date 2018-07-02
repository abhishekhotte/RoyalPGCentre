using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    interface INews
    {
        List<NewsDetails> GetNewsList();
        object AddUpdate(DAL.News obj, ref int newsId);
    }
}
