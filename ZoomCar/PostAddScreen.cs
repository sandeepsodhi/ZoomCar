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
    [Activity(Label = "Zoom Drive")]
    public class PostAddScreen : Activity
    {
        DBHelper myDbInstace;
        EditText carName, carModel, carMake, carDesc;
        Button postAddBtn;
        Android.App.AlertDialog.Builder myAlert;
        string uId;
        Intent i;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PostAddScreen);

            uId = Intent.GetStringExtra("Uid");

            myDbInstace = new DBHelper(this);

            carName = FindViewById<EditText>(Resource.Id.carName);
            carModel = FindViewById<EditText>(Resource.Id.model);
            carMake = FindViewById<EditText>(Resource.Id.make);
            carDesc = FindViewById<EditText>(Resource.Id.desc);
            postAddBtn = FindViewById<Button>(Resource.Id.post);

            postAddBtn.Click += PostAddBtn_Click;

        }
        
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Layout.GoBack, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem2:
                    {
                        Intent i = new Intent(this, typeof(MainActivity));
                        StartActivity(i);
                        return true;
                    }
                case Resource.Id.menuItem1:
                    {
                        Intent i = new Intent(this, typeof(tabLayout));
                        i.PutExtra("id", uId);

                        StartActivity(i);
                        return true;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        private void PostAddBtn_Click(object sender, EventArgs e)
        {
            var vcarName = carName.Text;
              var vcarModel = carModel.Text;
            var vcarMake = carMake.Text;
            var vcarDesc = carDesc.Text;
            var vuserId = uId;

            Random r = new Random();

            var vcarImage = "car" + r.Next(1, 9);


            myAlert = new Android.App.AlertDialog.Builder(this);

            if (vcarName == " " || vcarName.Equals(""))
            {
                errorMessageDialog(" vehicle name");

            }
            else if (vcarModel == " " || vcarModel.Equals(""))
            {
                errorMessageDialog(" vehicle model");
            }
            else if (vcarMake == " " || vcarMake.Equals(""))
            {
                errorMessageDialog(" vehicle make");
            }
            else if (vcarDesc == " " || vcarDesc.Equals(""))
            {
                errorMessageDialog(" vehical description");
            }
            else
            {
                myDbInstace.insertValueVehicleInfo(vcarName, vcarModel, vcarMake, vcarDesc, vcarImage ,vuserId);

                Toast.MakeText(this, "Insertion Succesfull!!", ToastLength.Long).Show();

                i = new Intent(this, typeof(tabLayout));
                i.PutExtra("id", uId);
                StartActivity(i);

            }
        }

        private void errorMessageDialog(string msg)
        {
            myAlert.SetTitle("Error");
            myAlert.SetMessage("Please enter a " + msg);
            myAlert.SetPositiveButton("OK", OkAction);
            Dialog myDialog = myAlert.Create();
            myDialog.Show();
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            System.Console.WriteLine("Ok button is clicked!!!");
        }
    }
}