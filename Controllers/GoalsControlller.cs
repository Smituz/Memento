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

    // List all goals for the logged-in user with the earliest deadline first
    public IActionResult Index()
    {
        // Retrieve the UserId from the session
        if (!int.TryParse(HttpContext.Session.GetString("UserId"), out int userId))
        {
            return RedirectToAction("Login", "Account"); // Redirect if not logged in
        }

        var goals = _context.Goals
            .Where(g => g.UserId == userId && g.Deadline >= DateTime.Today) // Filter by user and deadline
            .OrderBy(g => g.Deadline)
            .Select(g => new AddGoalViewModel
            {
                Id = g.Id,
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

    // Save a new goal for the logged-in user
    [HttpPost]
    public IActionResult AddGoal(AddGoalViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (!int.TryParse(HttpContext.Session.GetString("UserId"), out int userId))
            {
                return RedirectToAction("Login", "Account"); // Redirect if not logged in
            }

            var goal = new Goal
            {
                Title = model.Title,
                Deadline = model.Deadline,
                Completed = false, // Default to not completed
                UserId = userId
            };

            _context.Goals.Add(goal);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(model);
    }

    // Update the status of a goal
    [HttpPost]
    public IActionResult UpdateGoal(int id, bool completed)
    {
        var goal = _context.Goals.Find(id);
        if (goal != null && goal.UserId == int.Parse(HttpContext.Session.GetString("UserId")))
        {
            goal.Completed = completed;
            _context.SaveChanges();  // Save the changes to the database
        }

        return RedirectToAction("Index");  // Redirect back to the Goals list
    }
}
