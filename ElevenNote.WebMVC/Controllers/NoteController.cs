using ElevenNote.Services;
using ElevenNoteModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize] // This annotation makes it so that the views are accessible only to logged in users. 
    public class NoteController : Controller // The first word in the controller name will be the first part of our path.
                                             // Our path will be localhost:xxxxx/Note
    {
        // GET: Note
        public ActionResult Index() // The ActionResult is a return type.  Allows us to return a View() method.  
                                    // That View() method will return a view that corresponds to the NoteController.  
                                    // When running the app, we can go to localhost:xxxxx/Note/Index.  Path starts
                                    // with the name of the controller (without the word controller), then the name of the 
                                    // action, which is Index.  Right click on index to add view in module 4.04.  
                                    // The Index() method displays all the notes for the current user.  If click on Index, then
                                    // ctrl M G, you will be taken to the Index file.
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();
            //var model = new NoteListItem[0]; // Initializing a new instance of the NoteListItem as an IEnumerable with [0] syntax.  Will satisfy some requirements in the Index View.  Module 4.04.
            return View(model);  // When we go to that path, it will return a view for that path.  
        }

        // Add method here VVVV
        // GET
        public ActionResult Create() // Need a view and a model for the app to work.  
        {
            return View();
        }

        [HttpPost] // The code in this section adds success/failure messages for validation of note creation.  Module 7.02
        [ValidateAntiForgeryToken] 
        public ActionResult Create(NoteCreate model)  // The Create(NoteCreate model) method makes sure the model is valid, grabs the current userId, calls
                                                      // on CreateNote, and returns the user back to the index view.  
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index"); // TempData removes information after it's accessed.
            };

            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        private NoteService CreateNoteService() // Tip: Do not try to instantiate a Service inside of the constructor.  The MVC
                                                // framework does not have the data available that may be needed.
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }

        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch"); // Add some validation similar to the ActionResult Create to make sure the NoteId matches.
                return View(model);
            }

            var service = CreateNoteService(); // Build out the method, displaying a message to the user with the result of their actions.

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id) //ActionResult created to remove the note from the database.
        {
            // Add some validation to the code below.
            var service = CreateNoteService();
            service.DeleteNote(id); // Add code to service to get rid of the error.
            TempData["SaveResult"] = "Your note was deleted.";
            return RedirectToAction("Index");
        }
    }
}

// Slowly walkthrough the app and hover over the various points where the app is broken.
//With Breakpoints and Quickwatch, You can explore the data coming from the view, and the best .NET developers use these tools often.
//Persist a Note
//When we save something to the database, we persist it.

//Remove the breakpoint and rerun the app.
//Create a note. When you hit submit, the page won't change.
//Go to SQL Server Object Explorer.
//Find the database and expand the tables.
//Right click on  and click View Data.
//The data is not going into the database, we'll take care of that in the next chapter by bringing in our ElevenNote.Service.
//
//
//
// Ctrl k c (comments out many lines)