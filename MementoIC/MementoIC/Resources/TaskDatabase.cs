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
using System.IO;


namespace MementoIC
{

    //DataBase
    public class TaskDatabase : SQLiteConnection
    {
        static object locker = new object();

        public static string DatabaseFilePath
        {
            get
            {
                var sqliteFilename = "StockDB.db3";
                string libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var path = Path.Combine(libraryPath, sqliteFilename);
                return path;
            }
        }

        public TaskDatabase(string path) : base(path)
        {
            // create the tables
            CreateTable<Task>();
        }

        public IEnumerable<Task> GetTasks()
        {
            lock (locker)
            {
                return (from i in Table<Task>() select i).ToList();
            }
        }

        public Task GetTask(string name)
        {
            lock (locker)
            {
                return Table<Task>().FirstOrDefault(x => x.getName() == name);
            }
        }

        public int UpdateTask(Task item)
        {
            lock (locker)
            {
                return Update(item);
            }
        }
        public int SaveTask(Task item)
        {
            lock (locker)
            {

                return Insert(item);
            }
        }

        public int DeleteTask(Task tk)
        {
            lock (locker)
            {
                return Delete<Task>(tk.getName());
            }
        }
    }

}