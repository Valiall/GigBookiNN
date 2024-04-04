using GigBookin.Data;
using GigBookin.Models;
using GigBookin.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GigBookin.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext context;
        public GenreController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var genre = context.Genres.ToList();
            return View(genre);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGenreViewModel model)
        {
            var genre = new Genre()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description=model.Description
            };
            await context.Genres.AddAsync(genre);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Remove(Guid id)
        {

            var genre = await context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre != null)

            {
                var viewModel = new RemoveGenreViewModel()
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Description = genre.Description,
                };

                return await Task.Run(() => View("Remove", viewModel));

            }
            return RedirectToAction("Index");



        }

        [HttpPost]

        public async Task<IActionResult> Remove(RemoveGenreViewModel model)
        {
            var genre = await context.Genres.FindAsync(model.Id);
            if (genre != null)
            {
                context.Genres.Remove(genre);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }


    }
}

