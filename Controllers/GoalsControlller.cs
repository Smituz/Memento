using Microsoft.AspNetCore.Mvc;
using MeriDiaryv2.Data;
using MeriDiaryv2.ViewModels;
using System.Linq;
using MeriDiaryv2.Models;

public class GoalsController : Controller
{
    private readonly MeriDiaryContext _context;

    public GoalsController(MeriDiaryContext context)
    {
        _context = context;
    }

    // List all goals with the earliest deadline first
    public IActionResult Index()
    {
        var goals = _context.Goals
            .Where(g => g.Deadline >= DateTime.Today) // Exclude past deadlines
            .OrderBy(g => g.Deadline)
            .Select(g => new AddGoalViewModel
            {
                Title = g.Title,
                Deadline = g.Deadline,
                Completed = g.Completed
            })
            .ToList();

        return View(goals);
    }

    // Display the AddGoal form
    public IActionResult AddGoal()
    {
        return View(new AddGoalViewModel());
    }

    // Save a new goal
    [HttpPost]
    public IActionResult AddGoal(AddGoalViewModel model)
    {
        if (ModelState.IsValid)
        {
            var goal = new Goal
            {
                Title = model.Title,
                Deadline = model.Deadline,
                Completed = false, // Default to not completed
                UserId = int.Parse(HttpContext.Session.GetString("UserId")), /* fetch user id from session */
            };

            _context.Goals.Add(goal);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(model);

       
    }

    [HttpPost]
    public IActionResult UpdateGoal(int id, bool completed)
    {
        var goal = _context.Goals.Find(id);
        if (goal != null)
        {
            goal.Completed = completed;
            _context.SaveChanges();  // Save the changes to the database
        }

        return RedirectToAction("Index");  // Redirect back to the Goals list
    }
}
