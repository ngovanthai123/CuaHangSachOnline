using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Services
{
    public interface IProduct
    {
        IEnumerable<Sach> getProductAll();
        int totalProduct();
        int numberPage(int totalProduct, int limit);
        IEnumerable<Sach> paginationProduct(int start, int limit);

    }
}