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
    [Activity(Label = "Memento", MainLauncher = true, Icon = "@drawable/wall")]
    class MainActivity : Activity
    {
        //List of tasks
        public static List<Task> items = TaskRepository.GetTasks().ToList();

        //Adapter for the listView
        private ListAdapter adapter;

        //OnCreate
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //NotificationService
            Intent serviceToStart = new Intent(this, typeof(NotificationService));
            StartService(serviceToStart);

            //MainActivity layout
            SetContentView(Resource.Layout.Main);

            //Find by id
            ListView listView = FindViewById<ListView>(Resource.Id.listView1);
            Button buttonNew = FindViewById<Button>(Resource.Id.buttonNew);
            Button buttonSortPriority = FindViewById<Button>(Resource.Id.sortPriority);
            Button buttonSortDate = FindViewById<Button>(Resource.Id.sortDate);

            //Adapt list
            adapter = new ListAdapter(this, items);
            //set the adapter to build the list
            listView.Adapter = adapter;

            //On button Click starting the new activity
            buttonNew.Click += (sender, e) =>
            {
                Toast.MakeText(this, "Adding new task", ToastLength.Short).Show();
                //Make a list from the DataBase
                var aux = TaskRepository.GetTasks().ToList();

                //intent for the next activity
                Intent intentButtonClick = new Intent(this, typeof(ActivityTaskEdit));

                //start activity via intent waiting for a result
                StartActivityForResult(intentButtonClick, 0);
            };

            //On button click sort list by priority
            buttonSortPriority.Click += (sender, e) =>
            {
                Toast.MakeText(this, "List sorted by Priority", ToastLength.Short).Show();

                List<Task> sortedList = new List<Task>();

                for(int i=0; i<items.Count;)
                {
                    if (items[i].priority == 3)
                    {
                        sortedList.Insert(0, items[i]);
                        items.RemoveAt(i);
                    }
                    else if (items[i].priority == 2)
                    {
                        sortedList.Insert(sortedList.Count, items[i]);
                        items.RemoveAt(i);
                    }
                    else i++;
                }

                for (int i = 0; i < items.Count;)
                {
                        sortedList.Insert(sortedList.Count, items[i]);
                        items.RemoveAt(i);
                }

                items = sortedList;

                //Adapt list
                adapter = new ListAdapter(this, items);
                //set the adapter to build the list
                listView.Adapter = adapter;
            };
            //On button click sort list by date
            buttonSortDate.Click += (sender, e) =>
            {
                Toast.MakeText(this, "List sorted by Date", ToastLength.Short).Show();

                for(int i = 0; i<items.Count - 1; i++)
                {
                    for(int j=i+1; j<items.Count; j++)
                    {
                        if( DateTime.Compare(items[i].deadline, items[j].deadline) > 0 )
                        {
                            Task aux;
                            aux = items[i];
                            items[i] = items[j];
                            items[j] = aux;
                        }
                    }
                }

                adapter.NotifyDataSetChanged();
            };

            //On listView item click
            listView.ItemClick += (sender, e) =>
            {
                string s = "Editing task named " + items[e.Position].name;
                Toast.MakeText(this, s, ToastLength.Short).Show();
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

                //Start next activity via intent waiting for a result
                StartActivityForResult(intentOnItemClick, 0);
            };
        }

        //Method which wait for a result to update the listview
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            //Just update the adapter for the listView for refresh
            adapter.NotifyDataSetChanged();
        }
    }
}
