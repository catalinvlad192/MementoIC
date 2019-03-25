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

namespace TaskManager
{
    public class TaskRepository
    {
        TaskDatabase db = null;
        protected static TaskRepository me;
        static TaskRepository()
        {
            me = new TaskRepository();
        }
        protected TaskRepository()
        {
            db = new TaskDatabase(TaskDatabase.DatabaseFilePath);
        }

        public static Task GetTask(string name)
        {
            return me.db.GetTask(name);
        }

        public static IEnumerable<Task> GetTasks()
        {
            return me.db.GetTasks();
        }

        public static int UpdateTask(Task item)
        {
            return me.db.UpdateTask(item);
        }
        public static int SaveTask(Task item)
        {
            return me.db.SaveTask(item);
        }

        public static int DeleteStock(Task item)
        {
            return me.db.DeleteTask(item);
        }
    }
}