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

public class Task
{
    private string name;
    private string description;
    private Boolean status;
    private DateTime deadline;
    private int priority;

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

//GETTERS
    public string getName()
    {
        return this.name;
    }
    public string getDescription()
    {
        return this.description;
    }
    public Boolean getStatus()
    {
        return this.status;
    }
    public DateTime getDeadline()
    {
        return this.deadline;
    }
    public int getPriority()
    {
        return this.priority;
    }

//SETTERS
    public void setName(string name)
    {
        this.name = name;
    }
    public void setDescription(string descr)
    {
        this.description = descr; ;
    }
    public void setStatus(Boolean status)
    {
        this.status = status;
    }
    public void setDeadline(DateTime dt)
    {
        this.deadline = dt;
    }
    public void setPriority(int pr)
    {
        this.priority = pr;
    }
}