using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NOGAsteWebApp.Models;

namespace NOGAsteWebApp.Controllers
{
    public class EventsDBController : Controller
    {
        //                       you-are-here
        //IEventsDBRepository> EventsDBControler> EventsDBRepository
        //EventsDBController Selects correct model, binds to view
        //EventsDBRepository.cs using the "repo." 


        //Field
        public static string gblVar1DefConLvl     = "2";
        public static string gblVar2NumHighEvents = "7";
        public static string gblVar3UserList      = "bsimpson,rsmith,strangelove";


        private readonly IEventsDBRepository repo;


        //Constructor
        //"this" refers to the current instance of
        //the "EventsDBController" class.   It is used to
        //disintguish between the class's
        //instance variable   "this.repo"
        //and the constructor parameter (IEventsDBRepository repo)
        //with the same name.

        //used in the constructor of the EventsDBController class to
        //assign the repo parameter passed to the constructor to the
        //private repo field of the class. This is known as
        //dependency injection

        //repo is the parameter passed to the constructor of the
        //EventsDBController class. It represents an instance of
        //a class that implements the IEventsDBRepository interface.

        //this.repo is the instance variable of the EventsDBController
        //class. By using this, you're explicitly referring to the
        //instance variable instead of the constructor parameter.
        //This is often done to disambiguate between the local
        //parameter and the instance variable when they have the
        //same name.

        //So, this.repo = repo; is assigning the value of the repo
        //parameter to the repo instance variable within the
        //constructor, making the repo parameter accessible
        //within other methods of the class.
        public EventsDBController(IEventsDBRepository repo)
        {
            this.repo = repo;
        }//EventsDBControlle





        //---------------------------------
        //There is no View "GetAllEvents" this is
        //called by EventsDB.Index.cshtml
        public IActionResult Index()
        {            //this.repo
            var eventList = repo.GetAllEvents();
            return View(eventList);
        }//GetAllEvents



        //-------------------------------------ZZZ
        public IActionResult GetHighCount()
        {                        //this.repo
            int highThreatEventCount = repo.CountHighThreatEvents();
            // Use the highThreatEventCount as needed.
            return View();
        }//GetHighCount




        //---------------------------------
        public IActionResult ViewEvent(int id) //creates the view
        {
            EventsDBModel eventsDBModelObj = repo.GetEvent(id); //returns an "EventsDBModel" object

            //if ( eventsDBModelObj == null ) 
            //{
            //return View($"id:{id}, EventsDBModelObj:{eventsDBModelObj} Event Not Found");
            //}

            return View(eventsDBModelObj); 
        }//ViewEvent


        //--------EventsDBController-------
        public IActionResult UpdateEventToDatabase(EventsDBModel updEventToDB)
        {
            /*--------Begin New Code For Drop Down Free Text Other Field---*/
            if (updEventToDB.ActionReqd == "Other")
            {
                // If "Other" is selected, set ActionReqd to the value of ActionReqdOther
                updEventToDB.ActionReqd = updEventToDB.ActionReqdOther;
            }
            /*--------End New Code------------*/



            repo.UpdateEvent(updEventToDB);

            return RedirectToAction("ViewEvent", new { id = updEventToDB.KeyID });
        }//UpdateEventToDatabase



        //-------EventsDBController--------------
        public IActionResult UpdateEvent(int id) //creates the view
        {
            EventsDBModel updEvent = repo.GetEvent(id); //This must adhere to app.MapControllerRoute in program.cs

            //if (updEvent == null)
            //{
            //    return View("EventNotFound"); //creates error response view
            //}
            return View(updEvent);
         }//UpdateEvent


        //---------------------------------
        //InsertProduct() NOT USED



        //---------------------------------
        //InsertProductToDatabase() NOT USED

        //Restricts to POST only similar to .htaccess file in apache
        [HttpPost]
        public IActionResult DeleteEvent(int id)
        {
            repo.DeleteEvent(id);
            return RedirectToAction("Index");
        }

        //---------------------------------
        public IActionResult GetMaliciousEvents()  //creates the view
        {
            var malEvents = repo.GetMaliciousEvents(); //gets the data
            return View(malEvents);
        }//GetMaliciousEvents


        //----------------------------------ZZZ
        public IActionResult ConfidentialFileAccess() //Action method
        {
            
            //int tmpHighCnt = repo.CountHighThreatEvents();   //Blows up here
            //EventsDBController.gblVar1DefConLvl = tmpHighCnt.ToString();

            //creates a new **INSTANCE** NOT a **collection**
            //that is the problem.   We need it to be a collection
            //of the EventsDBModel class and
            //initializes its properties Var1DefConLvl, Var2NumHighEvents,
            //Var3UserLis. This model object is created to pass data to the view.
            var model = new EventsDBModel 
            {
                Var1DefConLvl     = gblVar1DefConLvl,
                Var2NumHighEvents = gblVar2NumHighEvents,
                Var3UserList      = gblVar3UserList
            };



            //This line stores the "model" (EventsDBModel object) in the "ViewData" dictionary
            //with the key "Model". ViewData is a dictionary-like container
            //for passing data from the controller to the view. In this case,
            //it's making the model object available to the view so that
            //you can access its properties and display them in the view.
            ViewData["Model"] = model; // Store the model in ViewData

            //This line calls a method repo.ConfidentialFileAccess() to
            //retrieve data (presumably a collection of confidential events)
            //from your repository (or data source). The returned data is
            //stored in the confEvents variable. This data is typically
            //used to display a list of confidential events in your view.
            //using the key "Model" to associate the "model" with the key
            //in the ViewData dictionary
            var confEvents = repo.ConfidentialFileAccess();

            //The ViewData["Model"] line doesn't directly pass variables to
            //confEvents. Instead, it makes the model object available to the
            //view, and the confEvents variable contains a different set of
            //data obtained from the repo.ConfidentialFileAccess() method.

            //Both model and confEvents are available to the view, and you
            //can access and display data from both in your view. For example,
            //you can use the model data to display information like Var1DefConLvl,
            //Var2NumHighEvents, and Var3UserList, and confEvents data to
            //display a list of confidential events in your view.
            //When you return View(confEvents); at the end of the action, you
            ////are telling ASP.NET MVC to render the view associated with this
            //action, and the data stored in ViewData (in this case, "Model")
            //will be available for use in that view. In your view, you can
            //access this data using *** @ViewData["Model"] ***.
            //The data stored in ViewData can be any object or data that you
            //want to pass from the controller to the view. It's a way to share
            //data between the controller and the view, and it can be useful
            //for passing information or data that is needed for rendering the view.
            return View(confEvents);
        }//ConfidentialFileAccess()



        //--------List of all Events Marked ThreatEval HIGH---------------
        //This started out as "DefCon1" then I changed to "HighRiskThreats"
        //bc detecting unknown user seemed more urgent 
        public IActionResult HighRiskThreats()
        {
            var highRiskThreats = repo.HighRiskThreats();
            return View(highRiskThreats);
        }//HighRiskThreats()


        //-------List Accounts With Logon Failures-------------
        public IActionResult LogonFail()
        {
            var logonFailEvents = repo.LogonFail();
            return View(logonFailEvents);
        }//LogonFail()


        //--------Dangerous Logon Types---4=Batch, 8=ClearText, 9=PrivEsc/RunAS, 10 RemoteInteractive-----------
        //Impersonation Level Anonymous, Default, Delegate(RISK), Identify, Impersonate
        public IActionResult RiskyAccess()
        {
            var RiskyAccessEvents = repo.RiskyAccess();
            return View(RiskyAccessEvents);
        }//RiskyAccess()


        //-------Plain List Of All Users----------------
        public IActionResult UserIDList()
        {
            var UserIDList = repo.UserIDList();
            return View(UserIDList);
        }//unkUserEvent()


        //--------List of all Unknown Users-----------
        public IActionResult UnkUserRedAlert()
        {
            var unkUserEvent = repo.UnkUserRedAlert();
            return View(unkUserEvent);
        }//unkUserEvent()


        //--------List of all Dangerous ImpersonateLvlEvent-----------
        public IActionResult ImpersonateLvlEvent()
        {
            var impersonateLvlEvent = repo.ImpersonateLvlEvent();
            return View(impersonateLvlEvent);
        }//ImpersonateLvlEvent()



    }//class
}//namespace
