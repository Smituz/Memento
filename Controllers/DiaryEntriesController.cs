using Microsoft.AspNetCore.Mvc;
using MeriDiaryv2.Data;
using MeriDiaryv2.Models;
using System.Linq;
using MeriDiaryv2.ViewModels;
public class DiaryEntriesController : Controller
{
    private readonly MeriDiaryContext _context;

    public DiaryEntriesController(MeriDiaryContext context)
    {
        _context = context;
    }

    // Helper: Check if user is logged in
    private bool IsUserLoggedIn()
    {
        return HttpContext.Session.GetString("UserId") != null;
    }

    // 1. Index: Load all diary entries for logged-in user
    public IActionResult Index()
    {
        if (!IsUserLoggedIn())
            return RedirectToAction("Landing", "Home");

        var userId = HttpContext.Session.GetString("UserId");
        var entries = _context.DiaryEntries
            .Where(e => e.UserId.ToString() == userId)
            .OrderByDescending(e => e.CreatedAt)
            .ToList();

        return View(entries);
    }

    // 2. NewEntry: Display the form to create a new diary entry
    public IActionResult NewEntry()
    {
        if (!IsUserLoggedIn())
            return RedirectToAction("Landing", "Home");

        return View();
    }

    // 3. CreateEntry: Save the new diary entry (POST)
    // 3. CreateEntry: Save the new diary entry (POST)
    [HttpPost]
    public IActionResult CreateEntry(NewDiaryEntryViewModel entryViewModel)
    {
        if (!IsUserLoggedIn())
            return RedirectToAction("Landing", "Home");

        if (ModelState.IsValid) // Validate the ViewModel
        {
            var entry = new DiaryEntry // Create a DiaryEntry model from the ViewModel
            {
                Title = entryViewModel.Title,
                Content = entryViewModel.Content,
                UserId = int.Parse(HttpContext.Session.GetString("UserId")), // Assuming UserId is an int
                CreatedAt = DateTime.Now // Assuming you want to set the current date/time
            };

            _context.DiaryEntries.Add(entry);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // If model state is not valid, return the same view with the ViewModel to show validation errors
        return View(entryViewModel);
    }


    // 4. ViewEntry: Display and edit a specific diary entry
    public IActionResult ViewEntry(int id)
    {
        if (!IsUserLoggedIn())
            return RedirectToAction("Landing", "Home");

        var entry = _context.DiaryEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null)
            return NotFound();

        return View(entry);
    }

    // 5. UpdateEntry: Save the edited entry (POST)
    [HttpPost]
    public IActionResult UpdateEntry(DiaryEntry entry)
    {
        if (!IsUserLoggedIn())
            return RedirectToAction("Landing", "Home");

        _context.DiaryEntries.Update(entry);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
    // 6. EditEntry: Display the form to edit a specific diary entry
    public IActionResult EditEntry(int id)
    {
        if (!IsUserLoggedIn())
            return RedirectToAction("Landing", "Home");

        var entry = _context.DiaryEntries.FirstOrDefault(e => e.Id == id);
        if (entry == null)
            return NotFound();

        var entryViewModel = new NewDiaryEntryViewModel
        {
            Title = entry.Title,
            Content = entry.Content,
            Mood = entry.Mood
        };

        return View(entryViewModel); // Pass the ViewModel to the view
    }

    // 7. SaveEntry: Save the edited diary entry (POST)
    [HttpPost]
    public IActionResult SaveEntry(NewDiaryEntryViewModel entryViewModel)
    {
        if (!IsUserLoggedIn())
            return RedirectToAction("Landing", "Home");

        if (ModelState.IsValid)
        {
            var entry = _context.DiaryEntries.FirstOrDefault(e => e.Id == entryViewModel.Id);
            if (entry == null)
                return NotFound();

            // Update entry properties
            entry.Title = entryViewModel.Title;
            entry.Content = entryViewModel.Content;
            entry.Mood = entryViewModel.Mood;

            _context.DiaryEntries.Update(entry);
            _context.SaveChanges();

            // Return the same view with updated values
            return RedirectToAction("Index");
        }

        // If model state is not valid, return the same view with the ViewModel to show validation errors
        return RedirectToAction("Index");
    }
}
