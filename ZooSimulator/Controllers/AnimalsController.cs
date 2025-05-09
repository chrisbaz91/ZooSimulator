using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZooSimulator.DataAccess;
using ZooSimulator.Handlers;
using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Controllers
{
    public class AnimalsController(IAnimalRepository animalRepo, IEnclosureRepository enclosureRepo, IValidator<FieldsModel> validator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var handler = new IndexQueryHandler(enclosureRepo, animalRepo);
            var model = await handler.Handle();
            return View(model);
        }

        public async Task<IActionResult> List()
        {
            var handler = new ListQueryHandler(enclosureRepo, animalRepo);
            var model = await handler.Handle();
            return PartialView("_List", model);
        }

        public async Task<IActionResult> Feed(FeedQuery query)
        {
            var handler = new FeedQueryHandler(query, animalRepo, enclosureRepo);
            await handler.Handle();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> PassTime()
        {
            var handler = new PassTimeQueryHandler(animalRepo, enclosureRepo);
            await handler.Handle();
            return RedirectToAction(nameof(List));
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(DetailsQuery query)
        {
            if (!await animalRepo.AnimalExists(query.Id))
            {
                return NotFound();
            }

            var handler = new DetailsQueryHandler(animalRepo);
            var model = await handler.Handle(query);
            return View(model);
        }

        // GET: Animals/Create
        public IActionResult Create(CreateQuery query)
        {
            var handler = new CreateQueryHandler();
            var model = handler.Handle(query);
            return View(model);
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateModel model)
        {
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(model);

            if (ModelState.IsValid && validationResult.IsValid)
            {
                var handler = new CreateModelHandler(animalRepo);
                await handler.Handle(model);
                return RedirectToAction(nameof(Index));
            }

            validationResult.AddToModelState(ModelState);

            return View(model);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(EditQuery query)
        {
            if (!await animalRepo.AnimalExists(query.Id))
            {
                return NotFound();
            }

            var handler = new EditQueryHandler(animalRepo);
            var model = await handler.Handle(query);
            return View(model);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditModel model)
        {
            FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(model);

            if (ModelState.IsValid && validationResult.IsValid)
            {
                var handler = new EditModelHandler(animalRepo);
                var result = await handler.Handle(model);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            validationResult.AddToModelState(ModelState);

            return View(model);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(DeleteQuery query)
        {
            if (!await animalRepo.AnimalExists(query.Id))
            {
                return NotFound();
            }

            var handler = new DeleteQueryHandler(animalRepo);
            var result = await handler.Handle(query);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
