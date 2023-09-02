using DOAN.Entities;
using DOAN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOAN.Controllers.Components
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public MenuViewComponent( AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = from c in _dbContext.CategoryEnities
                        where c.IsDeleted == false
                        select new Category()
                        {
                            Id = c.Id,
                            Name = c.Name

                        };

            return View(query.ToList());

        }
    }
}
