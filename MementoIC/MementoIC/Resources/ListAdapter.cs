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
    //adapter for displaying the list
    class ListAdapter : BaseAdapter<Task>
    {
        //a list of tasks to be displayed
        List<Task> items;

        //the context where it should be displayed
        Activity context;

        //constructor
        public ListAdapter(Activity context, List<Task> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        //method for position of the item
        public override long GetItemId(int position)
        {
            return position;
        }

        //
        public override Task this[int position]
        {
            get { return items[position]; }
        }

        //method which return the number of items in the list of tasks
        public override int Count
        {
            get { return this.items.Count; }
        }

        //
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.ListView, null);

            view.FindViewById<TextView>(Resource.Id.textName).Text = items[position].name;

            //set texts
            TextView textName = view.FindViewById<TextView>(Resource.Id.textName);
            TextView date = view.FindViewById<TextView>(Resource.Id.textDate);
            CheckBox check = view.FindViewById<CheckBox>(Resource.Id.checkBoxItem);

            textName.Text = items[position].name;
            date.Text = items[position].deadline.ToString();
            check.Checked = items[position].status;



            if (items[position].priority == 1)
            {
                textName.SetBackgroundColor(Android.Graphics.Color.ParseColor("#13b60d"));
                date.SetBackgroundColor(Android.Graphics.Color.ParseColor("#13b60d"));
            }
            else if (items[position].priority == 2)
            {
                textName.SetBackgroundColor(Android.Graphics.Color.ParseColor("#d45611"));
                date.SetBackgroundColor(Android.Graphics.Color.ParseColor("#d45611"));
            }
            else
            {
                textName.SetBackgroundColor(Android.Graphics.Color.ParseColor("#e11f1f"));
                date.SetBackgroundColor(Android.Graphics.Color.ParseColor("#e11f1f"));
            }

            check.CheckedChange += (sender, e) =>
            {
                //update
                items[position].status = check.Checked;
                Task aux = new Task(items[position].name, items[position].description, items[position].status, items[position].deadline, items[position].priority);
                TaskRepository.UpdateTask(aux);
            };

            return view;
        }

        private void Check_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}