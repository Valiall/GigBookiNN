using GigBookin.Data;
using GigBookin.Models.Entities;
using GigBookin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace GigBookin.Controllers
{
    public class PerformerController : Controller
    {
        public readonly ApplicationDbContext context;
        public PerformerController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var performer = context.Performers.Include(x=>x.Genre).ToList();
            return View(performer);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string singername)
        {

            try
            {
                if (string.IsNullOrEmpty(singername))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var searchedItems = await this.context.Performers.Where(x => x.Name == singername)
                                                                     .Include(x => x.Genre).ToListAsync();

                    if (searchedItems != null)
                    {
                        return View(searchedItems);
                    }
                    throw new ArgumentException("There is no performer with this name! Choose something else!");
                }
            }
            catch(Exception ex) 
            { 
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> FilterByStars(int stars)
        {
            try
            {
                if (stars < 1 || stars > 5)
                {
                    throw new ArgumentException();
                }
                else
                {
                    var searchedItems = await this.context.Performers.Where(x => x.Rating == stars)
                                                                     .Include(x => x.Genre).ToListAsync();

                    if (searchedItems != null)
                    {
                        return View("Index", searchedItems);
                    }
                    throw new ArgumentException("There is no performer with this rating! Choose something else!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> FilterByPrice(int price)
        {
            try
            {
                if (price < 1)
                {
                    throw new ArgumentException();
                }
                else
                {
                    var searchedItems = await this.context.Performers.Where(x => x.Price == price)
                                                                     .Include(x => x.Genre).ToListAsync();

                    if (searchedItems != null)
                    {
                        return View("Index", searchedItems);
                    }
                    throw new ArgumentException("There is no performer with this value! Choose something else!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> FilterByGenre(string genre)
        {
            try
            {
                if (string.IsNullOrEmpty(genre))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var searchedItems = await this.context.Performers.Include(x => x.Genre)
                                                                      .Where(x => x.Genre.Name == genre).ToListAsync();

                    if (searchedItems != null)
                    {
                        return View("Index", searchedItems);
                    }
                    throw new ArgumentException("There is no performer from this genre! Choose something else!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Genres = context.Genres.ToList(); 
            return View();
        }

        [HttpPost]
     //   [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Add(AddPerformerViewModel model)
        {
            var performer = new Performer()
            {
                Id = new Guid(),
                Name = model.Name,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                Email = model.Email,
                Phone = model.Phone,
                Type = model.Type,
                Rating = model.Rating,
                Experience = model.Experience,
                Price = model.Price
            };
            ViewBag.Genres = await context.Genres.ToListAsync();

            await context.Performers.AddAsync(performer);
            context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> ShowMoreInfo(Guid id)
        {
            var performer = await context.Performers
                                           .Include(p => p.Genre) 
                                           .FirstOrDefaultAsync(x => x.Id == id);

            if (performer != null)
            {
                var viewModel = new ShowMoreInfoPerformerViewModel()
                {
                    Id = performer.Id,
                    Name = performer.Name,
                    GenreId = performer.GenreId,
                    Genre = performer.Genre, 
                    ImageUrl = performer.ImageUrl,
                    Description = performer.Description,
                    Email = performer.Email,
                    Phone = performer.Phone,
                    Type = performer.Type,
                    Rating = performer.Rating,
                    Experience = performer.Experience,
                    Price = performer.Price
                };

               
                ViewBag.Genres = await context.Genres.ToListAsync();

                return View(viewModel);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var performer = await context.Performers.FirstOrDefaultAsync(x => x.Id == id);
            if (performer != null)
            {
                var viewModel = new EditPerformerViewModel()
                {
                    Id = performer.Id,
                    Name = performer.Name,
                    GenreId = performer.GenreId,
                    ImageUrl = performer.ImageUrl,
                    Description = performer.Description,
                    Email = performer.Email,
                    Phone = performer.Phone,
                    Type = performer.Type,
                    Rating = performer.Rating,
                    Experience = performer.Experience,
                    Price = performer.Price
                };

                ViewBag.Genres = await context.Genres.ToListAsync(); 

                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(EditPerformerViewModel model)
        {
            var performer = await context.Performers.FindAsync(model.Id);
            {
                if (performer != null)
                {
                    performer.Id = model.Id;
                    performer.Name = model.Name;
                    performer.GenreId = model.GenreId;
                    performer.ImageUrl = model.ImageUrl;
                    performer.Description = model.Description;
                    performer.Email = model.Email;
                    performer.Phone = model.Phone;
                    performer.Type = model.Type;
                    performer.Rating = model.Rating;
                    performer.Experience = model.Experience;
                    performer.Price = model.Price;



                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

        }
       
        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var performer = await context.Performers.FirstOrDefaultAsync(x => x.Id == id);

            if (performer != null)

            {
               var viewModel = new RemovePerformerViewModel()
                {
                  Id=performer.Id,
                  Name = performer.Name,
                 GenreId = performer.GenreId,
                  ImageUrl = performer.ImageUrl,
                  Description = performer.Description,
                  Email = performer.Email,
                  Phone = performer.Phone,
                  Type = performer.Type,
                  Rating = performer.Rating,
                  Experience=performer.Experience,
                  Price = performer.Price

                };

                return await Task.Run(() => View("Remove", viewModel));

               }
            return RedirectToAction("Index");

        }

        [HttpPost]

        public async Task<IActionResult> Remove(RemovePerformerViewModel model)
        {
            var performer = await context.Performers.FindAsync(model.Id);
            if (performer != null)
            {

                context.Performers.Remove(performer);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");

        }




        [HttpPost]
       //[Authorize(Roles = "EventOrganiser")]
        
        public async Task<IActionResult> Hire(Guid performerId)
        {
            try
            {
                
                var eventOrganiserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                
                var eventOrganiser = await context.Users
                    .Include(eo => eo.EventPerformers)
                    .FirstOrDefaultAsync(eo => eo.Id == eventOrganiserId);

                if (eventOrganiser == null)
                {
                    return NotFound("Event organizer not found.");
                }

                
                var performer = await context.Performers.FirstOrDefaultAsync(p => p.Id == performerId);

                if (performer == null)
                {
                    return NotFound("Performer not found.");
                }

                
                if (eventOrganiser.EventPerformers.Any(ep => ep.PerformerId == performerId))
                {
                    return BadRequest("Performer already exists in the collection.");
                }

              
                eventOrganiser.EventPerformers.Add(new EventPerformer
                {
                    PerformerId = performerId,
                    EventOrganiserId = eventOrganiserId,
                    Performer = performer,
                    EventOrganiser = eventOrganiser
                });

              
                await context.SaveChangesAsync();

               
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                
                return StatusCode(500, "An error occurred while adding performer to collection.");
            }
        }
        
      
        
        
        
        
        
        
        [HttpGet]
      //  [Authorize(Roles = "Administrator")]
        //[Authorize(Roles = "EventOrganiser")]
        public async Task<IActionResult> HiredPerformers()
        {
            
            var eventOrganiserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

           
            var eventOrganiser = await context.Users
                .Where(pe => pe.Id == eventOrganiserId)
                .Include(a => a.EventPerformers)
                    .ThenInclude(p => p.Performer)
                        .ThenInclude(p => p.Genre)
                .FirstOrDefaultAsync();

            if (eventOrganiser == null)
            {
                throw new ArgumentException("Invalid EventOrganiser Id");
            }

            var performers = eventOrganiser.EventPerformers.Select(ep => ep.Performer);

            
            var model = performers.Select(ep => new PerformerViewModel()
            {
                Id = ep.Id,
                Name = ep.Name,
                GenreId = ep.GenreId,
                ImageUrl = ep.ImageUrl,
                Description = ep.Description,
                Email = ep.Email,
                Phone = ep.Phone,
                Type = ep.Type,
                Rating = ep.Rating,
                Experience = ep.Experience,
                Price = ep.Price,
           
            });

            
            return View(model);
        }
    }



    //[HttpPost]
    //public async Task<IActionResult> RemoveFromCollection(Guid performerId)
    //{
    //    var eventOrganiserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    //    await performerservice.RemovePerformerFromCollectionAsync(performerId, eventOrganiserId);
    //    return RedirectToAction("PerformerHired");
    //}


}

