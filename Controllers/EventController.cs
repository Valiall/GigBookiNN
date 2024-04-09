﻿using GigBookin.Data;
using GigBookin.Models;
using GigBookin.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GigBookin.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext context;
        public EventController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var eventt = context.Events.ToList();
            return View(eventt);
        }

       
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model, string performerName)
        {
            var eventt = new Event()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Location = model.Location,
                WorkingHours = model.WorkingHours,
                Description = model.Description,
                Date = model.Date,
                Time = model.Time,
                PerformerId = model.PerformerId,
                //PerformerName = performerName // Set the performer's name here
            };

            await context.Events.AddAsync(eventt);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Remove(Guid id)
        {
            var eventt = await context.Events.FirstOrDefaultAsync(x => x.Id == id);
            if (eventt != null)
            {
                var viewModel = new RemoveEventViewModel()
                {
                    Id = eventt.Id,
                    Name = eventt.Name,
                    Location = eventt.Location,
                    WorkingHours=eventt.WorkingHours,
                    Description = eventt.Description,
                    Date=eventt.Date,
                    Time =eventt.Time,
                    PerformerId = eventt.PerformerId,
                   
                };
                return await Task.Run(() => View("Remove", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(RemoveEventViewModel model)
        {
            var eventt = await context.Events.FindAsync(model.Id);
            if (eventt != null)
            {
                context.Events.Remove(eventt);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
