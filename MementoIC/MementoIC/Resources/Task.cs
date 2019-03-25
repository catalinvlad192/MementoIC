using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace TaskManager
{
    public class Task
    {
        //private string name;
        // private string description;
        //private Boolean status = false;         //this way <HARD> OR
        //private DateTime deadline;
        //private int priority;


        //constructor with arguments
        public Task(string name, string description, Boolean status, DateTime deadline, int priority)
        {
            this.name = name;
            this.description = description;
            this.status = status;
            this.deadline = deadline;
            this.priority = priority;
        }

        //constructor without arguments
        public Task()
        {
            this.name = "";
            this.description = "";
            this.status = false;
            this.deadline = new DateTime(2000, 1, 2, 1, 0, 0);
            this.priority = 1;
        }

        //or THIS WAY <SIMPLER>

        //our primary key
        [PrimaryKey]
        public string name { get; set; }

        public string description { get; set; }

        public Boolean status { get; set; }

        public DateTime deadline { get; set; }

        public int priority { get; set; }
    }
}