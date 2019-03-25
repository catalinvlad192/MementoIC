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
    [Activity(Label = "ActivityTaskEdit")]
    public class ActivityTaskEdit : Activity
    {
        //declaration of all objects presented in TaskEdit.axml
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

            //find them by id
            name = FindViewById<EditText>(Resource.Id.editName);
            description = FindViewById<EditText>(Resource.Id.editDescription);
            date = FindViewById<DatePicker>(Resource.Id.datePicker);
            time = FindViewById<TimePicker>(Resource.Id.timePicker);
            low = FindViewById<RadioButton>(Resource.Id.radioLow);
            mediu = FindViewById<RadioButton>(Resource.Id.radioMedium);
            high = FindViewById<RadioButton>(Resource.Id.radioHigh);
            save = FindViewById<Button>(Resource.Id.buttonSave);
            delete = FindViewById<Button>(Resource.Id.buttonDelete);


            //disable delete button
            delete.Enabled = false;

            //Create your application here

            //initializing everything before showing
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

            //on delete button click you delete the current task
            delete.Click += (sender, e) =>
            {
                ////intent
                // Intent intentDeleteToMainA = new Intent(this, typeof(MainActivity));

                // //sending data to main activity
                // intentDeleteToMainA.PutExtra("Name", name.Text);
                // intentDeleteToMainA.PutExtra("Delete", 1);

                // //finnishing
                // SetResult(Result.Ok, intentDeleteToMainA);
                // Finish();
                Delete();
            };

            //on save button click something happens
            save.Click += (sender, e) =>
            {
                ////intent
                //Intent intentToMainA = new Intent(this, typeof(MainActivity));

                ////sending data to main activity
                //if (name != null)
                //{
                //    intentToMainA.PutExtra("Name", name.Text);
                //}
                //else
                //{
                //    intentToMainA.PutExtra("Name", "Name");
                //}

                //if (description != null)
                //{
                //    intentToMainA.PutExtra("Description", description.Text);
                //}
                //else
                //{
                //    intentToMainA.PutExtra("Description", "Description");
                //}

                //if (date != null && time != null)
                //{
                //    intentToMainA.PutExtra("DateYear", date.Year);
                //    intentToMainA.PutExtra("DateMonth", date.Month+1);
                //    intentToMainA.PutExtra("DateDay", date.DayOfMonth);
                //    intentToMainA.PutExtra("DateHour", time.CurrentHour.IntValue());
                //    intentToMainA.PutExtra("DateMinute",time.CurrentMinute.IntValue());
                //}
                //else
                //{
                //    intentToMainA.PutExtra("Date",
                //        new DateTime(1998,
                //        1,
                //        2,
                //        12,
                //        00,
                //        00).ToString());
                //}


                //    if (low.Checked == true)
                //    {
                //        intentToMainA.PutExtra("Priority", 1);
                //    }
                //    else if (mediu.Checked == true)
                //    {
                //        intentToMainA.PutExtra("Priority", 2);
                //    }
                //    else
                //    {
                //        intentToMainA.PutExtra("Priority", 3);
                //    }

                //SetResult(Result.Ok, intentToMainA);
                //Finish();

                Save();
            };


        }

        //method which modifies both the DataBase table and the list of tasks
        protected void Save()
        {
            //creating the current displayed task for saving
            int priority;
            if (low.Checked == true)
                priority = 1;
            else if (mediu.Checked == true)
                priority = 2;
            else
                priority = 3;
            Task newTask = new Task(name.Text, description.Text, check, new DateTime(date.Year, date.Month + 1, date.DayOfMonth, time.CurrentHour.IntValue(), time.CurrentMinute.IntValue(), 00), priority);

            //checking whether the task has to be saved or it has to update another task
            if (MainActivity.items.Exists(t => t.name.Equals(name.Text)))
            {
                //delete the task from the database
                TaskRepository.UpdateTask(newTask);

                //finding the index in the list of tasks
                int position = 0;
                for (int i = 0; i < MainActivity.items.Count; i++)
                {
                    if (MainActivity.items[i].name.Equals(name.Text))
                    {
                        position = i;
                        break;
                    }
                }

                //update the task from the list of tasks
                MainActivity.items[position].description = description.Text;
                MainActivity.items[position].deadline = new DateTime(date.Year, date.Month + 1, date.DayOfMonth, time.CurrentHour.IntValue(), time.CurrentMinute.IntValue(), 00);
                MainActivity.items[position].priority = priority;
                MainActivity.items[position].status = check;
            }
            else
            {
                //save the task to DataBase
                TaskRepository.SaveTask(newTask);

                //save the task to list of tasks
                MainActivity.items.Add(newTask);
            }
            Finish();
        }

        //method which modifies both the DataBase table and the list of tasks
        protected void Delete()
        {
            //creating the current displayed task for delete
            int priority;
            if (low.Checked == true)
                priority = 1;
            else if (mediu.Checked == true)
                priority = 2;
            else
                priority = 3;
            Task newTask = new Task(name.Text, description.Text, check, new DateTime(date.Year, date.Month + 1, date.DayOfMonth, time.CurrentHour.IntValue(), time.CurrentMinute.IntValue(), 00), priority);

            //delete it from DataBase
            TaskRepository.DeleteStock(newTask);

            //find index of task in list of tasks
            int position = 0;
            for (int i = 0; i < MainActivity.items.Count; i++)
            {
                if (MainActivity.items[i].name.Equals(name.Text))
                {
                    position = i;
                    break;
                }
            }

            //remove the item from the list of tasks
            MainActivity.items.RemoveAt(position);

            Finish();
        }
    }
}