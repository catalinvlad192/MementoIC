using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MementoIC
{

    //Adapter for displaying the list
    class ListAdapter : BaseAdapter<Task>
    {
        //A list of tasks to be displayed
        List<Task> items;

        //The context where it should be displayed
        Activity context;

        //Constructor
        public ListAdapter(Activity context, List<Task> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        //Method for position of the item
        public override long GetItemId(int position)
        {
            return position;
        }

        //
        public override Task this[int position]
        {
            get { return items[position]; }
        }

        //method which returns the number of items in the list of tasks
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

            view.FindViewById<TextView>(Resource.Id.textName).Text = items[position].getName();

            //set texts
            TextView textName = view.FindViewById<TextView>(Resource.Id.textName);
            TextView date = view.FindViewById<TextView>(Resource.Id.textDate);
            CheckBox check = view.FindViewById<CheckBox>(Resource.Id.checkBoxItem);

            textName.Text = items[position].getName();
            date.Text = items[position].getDeadline().ToString();
            check.Checked = items[position].getStatus();

            if (items[position].getPriority() == 1)
            {
                textName.SetBackgroundColor(Android.Graphics.Color.ParseColor("#13b60d"));
                date.SetBackgroundColor(Android.Graphics.Color.ParseColor("#13b60d"));
            }
            else if (items[position].getPriority() == 2)
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
            items[position].setStatus(check.Checked);
                Task aux = new Task(items[position].getName(), items[position].getDescription(), items[position].getStatus(),
                    items[position].getDeadline(), items[position].getPriority());
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