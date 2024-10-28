using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSnacks.Services
{
    public class ServiceFactory
    {
        public static FavouriteService CreateFavouriteService()
        {
            return new FavouriteService(); 
        }
    }
}
