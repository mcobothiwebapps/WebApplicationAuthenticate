using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationAuthenticate.Models;

namespace WebApplicationAuthenticate.Controllers
{
    public class RegController : Controller
    {
        private DBContextClassReg _DbReg = new DBContextClassReg();

        // GET: Reg
        public ActionResult users()
        {
            var UsersList = _DbReg.Regs;
            return View(UsersList);
        }

        public ActionResult index()
        {
            return View();
        }
        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult register(Reg model)
        {
            if (ModelState.IsValid)
            {
                var NewUser = new Reg
                {
                    UserId = model.UserId,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    //UserType = model.UserType
                };

                try
                {
                    _DbReg.Regs.Add(NewUser);
                    _DbReg.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            // Output or log the error details
                            Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                    throw;  // Rethrow the exception after logging if needed
                }

                return RedirectToAction("login");
            }

            return View(model);
        }


        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Email == "admin@gmail.com" && model.Password == "admin@0123")
                {
                    return RedirectToAction("dashboard", "Reg");
                }
                var user = _DbReg.Regs.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Check the user's role and redirect accordingly
                    //if (user.UserType == Reg.UserTypeList.User)
                    //{
                        // Redirect to the admin dashboard
                        return RedirectToAction("profile", "Reg");
                    //}
                    
                }

                // If user is not found, you can show an error message
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            // If we got this far, something failed, so re-display the form
            return View(model);
        }



        public ActionResult dashboard()
        {
            return View();
        }
        
        public ActionResult profile()
        {
            return View();
        }

    }
}