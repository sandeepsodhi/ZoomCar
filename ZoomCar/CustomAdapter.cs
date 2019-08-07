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

namespace ZoomCar
{
    class CustomAdapter : BaseAdapter<Cars>
    {
        Activity myContext;
        List<Cars> myListArray;

        public CustomAdapter(Activity context, List<Cars> myUserList)
        {
            this.myContext = context;
            this.myListArray = myUserList;
        }

        public override Cars this[int position]
        {
            get { return myListArray[position]; }
        }

        public override int Count
        {
            get { return myListArray.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View myView = convertView;

            Cars carsObj = myListArray[position];

            if (myView == null)
            {
                myView = myContext.LayoutInflater.Inflate(Resource.Layout.customListLayout, null);

                //int id = getResources().getIdentifier(lowerCountryCode, "drawable", getPackageName());
                //setImageResource(id);
                // myView.FindViewById<ImageView>(Resource.Id.image).SetImageResource(Resource.Drawable.logoText);

                var resourceId = (int)typeof(Resource.Drawable).GetField(carsObj.cImage).GetValue(null);
                myView.FindViewById<ImageView>(Resource.Id.image).SetImageResource(resourceId);


                myView.FindViewById<TextView>(Resource.Id.name).Text = carsObj.cName;
                myView.FindViewById<TextView>(Resource.Id.model).Text = "Model: " + carsObj.cModel;
                myView.FindViewById<TextView>(Resource.Id.make).Text = "Make: " + carsObj.cMake;
            }

            return myView;
        }
    }
}
