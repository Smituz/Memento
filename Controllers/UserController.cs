using Microsoft.AspNetCore.Mvc;
using MeriDiaryv2.Data; // Adjust the namespace as needed
using MeriDiaryv2.Models; // Adjust the namespace as needed
using System.Linq;
using MeriDiaryv2.ViewModels;

namespace MeriDiaryv2.Controllers
{
    public class UserController : Controller
    {
        private readonly MeriDiaryContext _context;

        public UserController(MeriDiaryContext context)
        {
            _context = context;
        }

        // GET: /User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Set user information in session
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    return RedirectToAction("Index", "DiaryEntries");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return RedirectToAction("Index", "DiaryEntries");
        }

        // GET: /User/Signup
        public IActionResult Signup()
        {
            return View();
        }

        // POST: /User/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(SingupViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if user already exists
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", "User already exists.");
                    return View(model);
                }

                // Create new user
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNo = model.PhoneNo,
                    Email = model.Email,
                    Password = model.Password,
                    BirthDate = model.Birthdate,// Consider hashing the password in a real application
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                // Redirect to login after successful signup
                return RedirectToAction("Login");
            }
            return View(model);
        }

        // GET: /User/Logout
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            // Check if user is logged in
            if (!IsUserLoggedIn())
                return RedirectToAction("Landing", "Home");

            var userId = HttpContext.Session.GetString("UserId");
            var userProfile = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

            if (userProfile == null)
                return NotFound();

            // Get the total number of diary entries for this user
            var totalDiaryEntries = _context.DiaryEntries.Count(de => de.UserId == userProfile.Id);

            ViewBag.TotalDiaryEntries = totalDiaryEntries; // Pass the count to the view

            return View(userProfile); // Pass the user profile model to the view
        }
        public IActionResult EditProfile()
        {
            // Check if user is logged in
            if (!IsUserLoggedIn())
                return RedirectToAction("Landing", "Home");

            var userId = HttpContext.Session.GetString("UserId");
            var userProfile = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

            if (userProfile == null)
                return NotFound();

            // Map the User entity to EditProfileViewModel
            var viewModel = new ProfileViewModel
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Email = userProfile.Email,
                PhoneNumber = userProfile.PhoneNo,
                Birthdate = userProfile.BirthDate
            };

            return View(viewModel); // Pass the ViewModel to the view for editing
        }

        // Action to save changes made to the user's profile
        [HttpPost]
        public IActionResult SaveProfile(ProfileViewModel viewModel)
        {
            // Check if user is logged in
            if (!IsUserLoggedIn())
                return RedirectToAction("Landing", "Home");

            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("UserId");
                var userProfile = _context.Users.FirstOrDefault(u => u.Id.ToString() == userId);

                if (userProfile == null)
                    return NotFound();

                // Update the user profile with the data from the ViewModel
                userProfile.FirstName = viewModel.FirstName;
                userProfile.LastName = viewModel.LastName;
                userProfile.Email = viewModel.Email;
                userProfile.PhoneNo = viewModel.PhoneNumber;
                userProfile.BirthDate = viewModel.Birthdate;

                _context.Users.Update(userProfile);
                _context.SaveChanges();

                return RedirectToAction("Profile"); // Redirect back to profile after saving
            }

            return View("EditProfile", viewModel); // Return to EditProfile view with current data if validation fails
        }


        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetString("UserId") != null;
        }

    }

}

