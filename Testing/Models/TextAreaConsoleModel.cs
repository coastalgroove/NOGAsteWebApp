using System.ComponentModel.DataAnnotations;

namespace NOGAsteWebApp.Models
{
    public class TextAreaConsoleModel
    {

        //Experiment UNDER CONSTRUCTION
        public string ConsoleText { get; set; }
        [Display(Name = "Console")]
        public string ConsoleOutput { get; set; }


        public int KeyID { get; set; }
        public string EventID { get; set; }
        public string TimeCreated { get; set; }
        public string EventMsg { get; set; }
        public string LogonType { get; set; }
        public string ElevToken { get; set; }
        public string ImpersonateLvl { get; set; }
        public string LogonFail { get; set; }
        public string KnownUser { get; set; }
        public string FailReason { get; set; }
        //public string   AfterHours     {  get; set; }
        //public string   LogonSuccess     {  get; set; }
        public string MachineName { get; set; }
        public string UserID { get; set; }
        public string ProgramRun { get; set; }
        public string CommandRun { get; set; }
        public string ProcessInfo { get; set; }
        public string ObjName { get; set; }
        public string AppPath { get; set; }
        public string LogLvl { get; set; }
        public string Status { get; set; }
        public string SubStatus { get; set; }
        public string ReasonEvnt { get; set; }
        public string ThreatEval { get; set; }
        public string ActionReqd { get; set; }

    }
}
