using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using System;
using System.Linq;
using MementoIC;

namespace TaskManager
{
    [Activity(Label = "TaskManager", MainLauncher = true)]
    class MainActivity : Activity
    {
        //list of tasks <static>
        public static List<Task> items = TaskRepository.GetTasks().ToList();

        //the adapter for the listView
        ListAdapter adapter;

        //OnCreate
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Intent serviceToStart = new Intent(this, typeof(NotificationService));
            StartService(serviceToStart);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //find by id
            ListView listView = FindViewById<ListView>(Resource.Id.listView1);
            Button buttonNew = FindViewById<Button>(Resource.Id.buttonNew);

            //Dirty Init
            //Task a = new Task("ceva1", "altceva", false, new System.DateTime(2000,11,11,12,55,00), 1);
            //Task b = new Task("ceva2", "altceva", false, new System.DateTime(2000, 11, 11,12,55,00), 2);
            //Task c = new Task("ceva3", "altceva", false, new System.DateTime(2000, 11, 11,12,55,00), 3);
            //items.Add(a);
            //items.Add(b);
            //items.Add(c);

            //adapter for the list(see ListAdapter class)
            adapter = new ListAdapter(this, items);

            //set the adapter to build the list
            listView.Adapter = adapter;

            //on button Click starting the new activity
            buttonNew.Click += (sender, e) =>
            {
                //make a list from the DataBase
                var aux = TaskRepository.GetTasks().ToList();

                string s = "";

                //making a string with all the tasks available
                for (int i = 0; i < aux.Count; i++)
                {
                    s = s + aux[i].name + aux[i].description + aux[i].deadline.ToString() + aux[i].priority + aux[i].status + "\n";
                }

                //toast for showing the list
                Toast.MakeText(this, s, ToastLength.Short).Show();

                //intent for the next activity
                Intent intentButtonClick = new Intent(this, typeof(ActivityTaskEdit));

                //start activity via intent waiting for a result
                StartActivityForResult(intentButtonClick, 0);
            };

            //on listView item click
            listView.ItemClick += (sender, e) =>
            {

                //toast for showing which item was selected
                // Toast.MakeText(this, items[e.Position].name + "clicked", ToastLength.Short).Show();

                //intent for sending the data to the next activity and opening it
                Intent intentOnItemClick = new Intent(this, typeof(ActivityTaskEdit));
                intentOnItemClick.PutExtra("Name", items[e.Position].name);
                intentOnItemClick.PutExtra("Description", items[e.Position].description);
                intentOnItemClick.PutExtra("DateYear", items[e.Position].deadline.Year);
                intentOnItemClick.PutExtra("DateMonth", items[e.Position].deadline.Month);
                intentOnItemClick.PutExtra("DateDay", items[e.Position].deadline.Day);
                intentOnItemClick.PutExtra("DateHour", items[e.Position].deadline.Hour);
                intentOnItemClick.PutExtra("DateMinute", items[e.Position].deadline.Minute);
                intentOnItemClick.PutExtra("Priority", items[e.Position].priority);
                intentOnItemClick.PutExtra("Status", items[e.Position].status);

                if (items[e.Position].status == false)
                {

                }

                //start next activity via intent waiting for a result
                StartActivityForResult(intentOnItemClick, 0);
            };

        }


        //method which wait for a result to update the listview
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            //// list of tasks from onCreate
            //var taskToEdit = items;

            //if (data != null)
            //{
            //    var delete = data.GetIntExtra("Delete",2);
            //    if (delete == 2)
            //    {
            //        //getting data from activityTaskEdit
            //        var name = data.GetStringExtra("Name");

            //        if (items.Exists(t => t.name.Equals(name)))
            //        {
            //            //getting data from activityTaskEdit
            //            var description = data.GetStringExtra("Description");
            //            var dateYear = data.GetIntExtra("DateYear", 1999);
            //            var dateMonth = data.GetIntExtra("DateMonth", 1);
            //            var dateDay = data.GetIntExtra("DateDay", 2);
            //            var dateHour = data.GetIntExtra("DateHour", 0);
            //            var dateMinute = data.GetIntExtra("DateMinute", 0);
            //            var priority = data.GetIntExtra("Priority", 1);

            //            //finding the index
            //            int position = 0;
            //            for (int i = 0; i < taskToEdit.Count; i++)
            //            {
            //                if (taskToEdit[i].name.Equals(name))
            //                {
            //                    position = i;
            //                    break;
            //                }
            //            }

            //            taskToEdit[position].description=description;
            //            taskToEdit[position].deadline=new DateTime(dateYear, dateMonth, dateDay, dateHour, dateMinute, 0);
            //            taskToEdit[position].priority=priority;
            //            //notify changes
            //            adapter.NotifyDataSetChanged();
            //        }
            //        else
            //        {
            //            //getting data from activityTaskEdit
            //            var description = data.GetStringExtra("Description");
            //            var dateYear = data.GetIntExtra("DateYear", 1999);
            //            var dateMonth = data.GetIntExtra("DateMonth", 1);
            //            var dateDay = data.GetIntExtra("DateDay", 2);
            //            var dateHour = data.GetIntExtra("DateHour", 0);
            //            var dateMinute = data.GetIntExtra("DateMinute", 0);
            //            var priority = data.GetIntExtra("Priority", 1);

            //            // creating task
            //            Task newTask = new Task(name, description, false, new DateTime(dateYear, dateMonth, dateDay, dateHour, dateMinute, 0), priority);

            //            //adding it to the list and notifying the adapter for changes
            //            taskToEdit.Add(newTask);

            //            //notify changes
            //            adapter.NotifyDataSetChanged();
            //        }
            //    }
            //    else if (delete == 1)
            //    {
            //        var name = data.GetStringExtra("Name");
            //        int position = 0;
            //        for (int i = 0; i < taskToEdit.Count; i++)
            //        {
            //            if (taskToEdit[i].name.Equals(name))
            //            {
            //                position = i;
            //                break;
            //            }
            //        }
            //        taskToEdit.RemoveAt(position);
            //        adapter.NotifyDataSetChanged();
            //    }
            //}

            //just update the adapter for the listView for refresh
            adapter.NotifyDataSetChanged();
        }

    }
}