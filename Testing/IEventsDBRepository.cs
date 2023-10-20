using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using NOGAsteWebApp.Models;


namespace NOGAsteWebApp
{
    //   you-are-here
    //IEventsDBRepository> EventsDBController> EventsDBRepository

    //The IEventsDBRepository is an interface that
    //defines a contract(s). It declares method signatures
    //that any class implementing this interface must provide.
    //These methods are part of the contract that any
    //implementing class must adhere to.
    public interface IEventsDBRepository
    {
        //-----------------------
        public IEnumerable<EventsDBModel> GetAllEvents();
        //-----------------------
        public EventsDBModel GetEvent(int id);
        //---------IEventsDBRepository--------------
        public void UpdateEvent(EventsDBModel updEvent);
        //-----------------------
        public IEnumerable<EventsDBModel> GetMaliciousEvents();
        //-----------------------
        public void DeleteEvent(int tgtKeyID);
        //-----------------------ZZZ
        public int CountHighThreatEvents();
        //------------------------
        public IEnumerable<EventsDBModel> ConfidentialFileAccess();
        //-------------------------
        public IEnumerable<EventsDBModel> HighRiskThreats();
        //------------------------
        public IEnumerable<EventsDBModel> LogonFail();
        //------------------------
        public IEnumerable<EventsDBModel> RiskyAccess();
        //------------------------
        public IEnumerable<EventsDBModel> UserIDList();
        //------------------------
        public IEnumerable<EventsDBModel> UnkUserRedAlert();
        //------------------------
        public IEnumerable<EventsDBModel> ImpersonateLvlEvent();

    }//Interface

}//namespace

