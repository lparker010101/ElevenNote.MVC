using ElevenNoteModels;
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
        {
            var model = new NoteListItem[0]; // Initializing a new instance of the NoteListItem as an IEnumerable with [0] syntax.  Will satisfy some requirements in the Index View.  Module 4.04.
            return View(model);  // When we go to that path, it will return a view for that path.  
        }
    }
}