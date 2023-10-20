using Dapper;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NOGAsteWebApp.Models;
using MySql.Data.MySqlClient;
using System;

namespace NOGAsteWebApp
{
    //                                          you-are-here
    //IEventsDBRepository> EventsDBController> EventsDBRepository

    //The EventsDBRepository is where the stubbed out methods
    //in IEventsDBRepository are implemented

    public class EventsDBRepository : IEventsDBRepository
    {
        //field
        private  readonly IDbConnection _conn;

        //_conn is used to store an instance of the
        //IDbConnection interface. This connection is
        //typically used for interacting with a database.

        //conn: Is a parameter of the constructor for the
        //EventsDBRepository class. It represents an instance
        //of the IDbConnection interface

        //Constructor
        public EventsDBRepository(IDbConnection conn)
        {
           _conn = conn;
        }



        //-----------------------
        public void DeleteEvent(int id)
        {
           _conn.Execute("DELETE FROM securityLogs.events " +
                " WHERE KeyId = @id;", new { id = id });
        }

        //-----------------------
        public IEnumerable<EventsDBModel> GetAllEvents()
        {
           return _conn.Query<EventsDBModel>("SELECT * from securityLogs.events;");
        }

        public EventsDBModel GetEvent(int tgtKeyID)
        {
            int foobar = tgtKeyID;
            return _conn.QuerySingle<EventsDBModel>("SELECT " +
                " KeyID,EventID,UserID,ThreatEval,ActionReqd FROM " +
                " securityLogs.events  WHERE KeyID = @id", new { id = foobar });  
        }




        //--------------------------------ZZZ
        public int CountHighThreatEvents()
        {
            using (var conn = new MySqlConnection("DefaultConnection"))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT COUNT(ThreatEval) " +
                    " FROM events WHERE ThreatEval = 'HIGH'", conn))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count;
                }
            }
        }//CountHighThreatEvents





        //-------EventsDBRepository----------------
        public void UpdateEvent(EventsDBModel keyIdOfEventToBeUpdated)
        {
            _conn.Execute("UPDATE securityLogs.events SET " +
                " ThreatEval = @threatEval, ActionReqd = @actionReqd " +
                " WHERE KeyID = @id" ,
            new { threatEval = keyIdOfEventToBeUpdated.ThreatEval,
                  actionReqd = keyIdOfEventToBeUpdated.ActionReqd,
                  id = keyIdOfEventToBeUpdated.KeyID  });
            }




        //-----------------------
        public IEnumerable<EventsDBModel> GetMaliciousEvents()
        {
            return _conn.Query<EventsDBModel>("SELECT " +
                " KeyID,EventID,UserID,CommandRun,ProcessInfo,ObjName,AppPath,ThreatEval,ActionReqd " +
                " FROM securityLogs.events WHERE " +
                " CommandRun  LIKE '%powershell%' OR " +
                " CommandRun  LIKE '%wmic%'       OR " +
                " CommandRun  LIKE '%psget%'      OR " +

                " ProcessInfo LIKE '%powershell%' OR " +
                " ProcessInfo LIKE '%wmic%'       OR " +
                " ProcessInfo LIKE '%psget%'      OR " +

                " ObjName     LIKE '%powershell%' OR " +
                " ObjName     LIKE '%wmic%'       OR " +
                " ObjName     LIKE '%psget%'      OR " +

                " AppPath     LIKE '%powershell%' OR " +
                " ObjName     LIKE '%wmic%'       OR " +
                " ObjName     LIKE '%psget%'      " 
                 );
        }//GetMaliciousEvents




        //------------------------
        public IEnumerable<EventsDBModel> ConfidentialFileAccess()
        {
            return _conn.Query<EventsDBModel>("SELECT " +
                " KeyID,EventID,UserID,CommandRun,ProcessInfo,ObjName,AppPath,ThreatEval,ActionReqd " +
                " FROM securityLogs.events WHERE " +
                " CommandRun  LIKE '%personell%' OR " +
                " CommandRun  LIKE '%Rocket%'    OR " +
                " CommandRun  LIKE '%crypto%'    OR " +
                " CommandRun  LIKE '%Merger%'    OR " +
                " ProcessInfo LIKE '%personell%' OR " +
                " ProcessInfo LIKE '%Rocket%'    OR " +
                " ProcessInfo LIKE '%crypto%'    OR " +
                " ProcessInfo LIKE '%Merger%'    OR " +
                " ObjName     LIKE '%personell%' OR " +
                " ObjName     LIKE '%Rocket%'    OR " +
                " ObjName     LIKE '%crypto%'    OR " +
                " ObjName     LIKE '%psget%'     OR " +
                " AppPath     LIKE '%personell%' OR " +
                " AppPath     LIKE '%Rocket%'    OR " +
                " AppPath     LIKE '%crypto%'    OR " +
                " AppPath     LIKE '%Merger%'      ;"
                );
        }//ConfidentialFileAccess



        //------------------------
        public IEnumerable<EventsDBModel> HighRiskThreats()
        {
            return _conn.Query<EventsDBModel>("SELECT " +
            " KeyID, EventID, TimeCreated, UserID, " +
            " MachineName, ThreatEval, ActionReqd  " +
            " FROM securityLogs.events WHERE       " +
            " ThreatEval = 'HIGH' ORDER BY UserID ;"
                );
        }//HighRiskThreats



        //------------------------
        public IEnumerable<EventsDBModel> LogonFail()
        {
            return _conn.Query<EventsDBModel>("SELECT         " +
            " KeyID, EventID, TimeCreated, UserID, LogonType, " +
            " LogonFail, ThreatEval, ActionReqd FROM           " +
            " securityLogs.events WHERE LogonFail <> '';      "
                );
        }//LogonFail


        //------------------------
        public IEnumerable<EventsDBModel> RiskyAccess()
        {
            return _conn.Query<EventsDBModel>("SELECT         " +
            " KeyID, EventID, TimeCreated, UserID, LogonType, " +
            " ThreatEval, ActionReqd FROM                     " +
            " securityLogs.events WHERE LogonType <> ''       " +
            " AND LogonType <> 3;      "
                );
        }//RiskyAccess


        //------------------------
        public IEnumerable<EventsDBModel> UserIDList()
        {
            return _conn.Query<EventsDBModel>("SELECT DISTINCT   " +
            " UserID " +
            " FROM                     " +
            " securityLogs.events WHERE UserID <> '' ORDER BY UserID;      "
                );
        }//UserIDList

        //public IEnumerable<EventsDBModel> UnkUserRedAlert()
        //{
        //    return _conn.Query<EventsDBModel>("SELECT DISTINCT  " +
        //    " UserID " +
        //    " FROM securityLogs.events WHERE KnownUser <> 'KNOWN'  " +
        //    " AND UserID <> '' and UserID <> 'S-1-5-18'                    " +
        //    " AND UserID <> 'S-1-5-21-110707328-881830710-1281915939-1001' " +
        //    " ORDER BY UserID;      "
        //        );
        //}//UserIDList


        //------------------------
        public IEnumerable<EventsDBModel> UnkUserRedAlert()
        {
            return _conn.Query<EventsDBModel>("SELECT   " +
            " KeyID, EventID, TimeCreated, UserID, MachineName, " + 
            " KnownUser, ThreatEval, ActionReqd                 " +
            " FROM securityLogs.events WHERE KnownUser <> 'KNOWN'  " +
            " AND UserID <> '' and UserID <> 'S-1-5-18'                    " +
            " AND UserID <> 'S-1-5-21-110707328-881830710-1281915939-1001' " +
            " ORDER BY UserID;      "
                );
        }//UnkUserRedAler


        //------------------------
        public IEnumerable<EventsDBModel> ImpersonateLvlEvent()
        {
            return _conn.Query<EventsDBModel>("SELECT   " +
            " KeyID, EventID, TimeCreated, ImpersonateLvl, " +
            " UserID, ThreatEval, ActionReqd " +
            " FROM securityLogs.events WHERE impersonateLvl <> ''  " +
            " ORDER BY UserID;      "
                );
        }//ImpersonateLvlEvent

    }//class
}//namespace
