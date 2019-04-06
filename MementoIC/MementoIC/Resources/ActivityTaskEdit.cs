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
using MementoIC;

namespace TaskManager
{
    [Activity(Label = "Task Edit/Add", Icon = "@drawable/wall")]
    public class ActivityTaskEdit : Activity
    {
        //Declaration of all objects presented in TaskEdit.axml
        EditText name;
        EditText description;
        DatePicker date;
        TimePicker time;
        RadioButton low;
        RadioButton mediu;
        RadioButton high;
        Button save;
        Button delete;
        Boolean check;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TaskEdit);

            //Find them by id
            name = FindViewById<EditText>(Resource.Id.editName);
            description = FindViewById<EditText>(Resource.Id.editDescription);
            date = FindViewById<DatePicker>(Resource.Id.datePicker);
            time = FindViewById<TimePicker>(Resource.Id.timePicker);
            low = FindViewById<RadioButton>(Resource.Id.radioLow);
            mediu = FindViewById<RadioButton>(Resource.Id.radioMedium);
            high = FindViewById<RadioButton>(Resource.Id.radioHigh);
            save = FindViewById<Button>(Resource.Id.buttonSave);
            delete = FindViewById<Button>(Resource.Id.buttonDelete);

            //Disable delete button
            delete.Enabled = false;

            //Initializing everything before showing
            if ((name.Text = Intent.GetStringExtra("Name")) != null)
            {
                name.Enabled = false;
                delete.Enabled = true;
                description.Text = Intent.GetStringExtra("Description");
                int auxPriority = Intent.GetIntExtra("Priority", 1);
                switch (auxPriority)
                {
                    case 1:
                        low.Checked = true;
                        break;
                    case 2:
                        mediu.Checked = true;
                        break;
                    case 3:
                        high.Checked = true;
                        break;
                }

                date.DateTime = new DateTime(Intent.GetIntExtra("DateYear", 2000), Intent.GetIntExtra("DateMonth", 0), Intent.GetIntExtra("DateDay", 2), Intent.GetIntExtra("DateHour", 1), Intent.GetIntExtra("DateMinute", 00), 00);
                time.CurrentHour = (Java.Lang.Integer)Intent.GetIntExtra("DateHour", 1);
                time.CurrentMinute = (Java.Lang.Integer)Intent.GetIntExtra("DateMinute", 1);
                check = Intent.GetBooleanExtra("Status", false);
            }

            //On delete button click you delete the current task
            delete.Click += (sender, e) =>
            {
                Toast.MakeText(this, "Item deleted", ToastLength.Short).Show();
                Delete();
            };

            //On save button click something happens
            save.Click += (sender, e) =>
            {
                Toast.MakeText(this, "Item saved", ToastLength.Short).Show();
                Save();
            };
        }

        //Method which modifies both the DataBase table and the list of tasks
        protected void Save()
        {
            //Creating the current displayed task for saving
            int priority;
            if (low.Checked == true)
                priority = 1;
            else if (mediu.Checked == true)
                priority = 2;
            else
                priority = 3;
            Task newTask = new Task(name.Text, description.Text, check, new DateTime(date.Year, date.Month + 1, date.DayOfMonth, time.CurrentHour.IntValue(), time.CurrentMinute.IntValue(), 00), priority);

            //Checking whether the task has to be saved or it has to update another task
            if (MainActivity.items.Exists(t => t.name.Equals(name.Text)))
            {
                //Delete the task from the database
                TaskRepository.UpdateTask(newTask);

                //Finding the index in the list of tasks
                int position = 0;
                for (int i = 0; i < MainActivity.items.Count; i++)
                {
                    if (MainActivity.items[i].name.Equals(name.Text))
                    {
                        position = i;
                        break;
                    }
                }

                //Update the task from the list of tasks
                MainActivity.items[position].description = description.Text;
                MainActivity.items[position].deadline = new DateTime(date.Year, date.Month + 1, date.DayOfMonth, time.CurrentHour.IntValue(), time.CurrentMinute.IntValue(), 00);
                MainActivity.items[position].priority = priority;
                MainActivity.items[position].status = check;
            }
            else
            {
                //Save the task to DataBase
                TaskRepository.SaveTask(newTask);

                //Save the task to list of tasks
                MainActivity.items.Add(newTask);
            }
            Finish();
        }

        //Method which modifies both the DataBase table and the list of tasks
        protected void Delete()
        {
            //Creating the current displayed task for delete
            int priority;
            if (low.Checked == true)
                priority = 1;
            else if (mediu.Checked == true)
                priority = 2;
            else
                priority = 3;
            Task newTask = new Task(name.Text, description.Text, check, new DateTime(date.Year, date.Month + 1, date.DayOfMonth, time.CurrentHour.IntValue(), time.CurrentMinute.IntValue(), 00), priority);

            //Delete it from DataBase
            TaskRepository.DeleteStock(newTask);

            //Find index of task in list of tasks
            int position = 0;
            for (int i = 0; i < MainActivity.items.Count; i++)
            {
                if (MainActivity.items[i].name.Equals(name.Text))
                {
                    position = i;
                    break;
                }
            }

            //Remove the item from the list of tasks
            MainActivity.items.RemoveAt(position);
            Finish();
        }
    }
}