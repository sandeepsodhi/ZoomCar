using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Uri = Android.Net.Uri;

namespace ZoomCar
{
    [Activity(Label = "ViewAdd")]
    public class ViewAdd : Activity
    {
        int count;
        String idC, idU;
        Intent i;
        Button addFavBtn, makeCall;
        DBHelper myDbInstance;
        TextView carName, carMake, carModel, carDesc;

        Android.App.AlertDialog.Builder myAlert;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ViewAdd);

            myDbInstance = new DBHelper(this);

            idC = Intent.GetStringExtra("cid");
            idU = Intent.GetStringExtra("uid");

            Cars result = myDbInstance.selectAddData(idC);

            var cNamefromDB = result.cName; //.GetString(result.GetColumnIndexOrThrow("cName"));
            var cMakefromDB = result.cMake; //.GetString(result.GetColumnIndexOrThrow("cMake"));
            var cModelfromDB = result.cModel; ///.GetString(result.GetColumnIndexOrThrow("cModel"));
            var cDescfromDB = result.cDesc; //.GetString(result.GetColumnIndexOrThrow("cDesc"));

            carName = FindViewById<TextView>(Resource.Id.carName);
            carMake = FindViewById<TextView>(Resource.Id.carMake);
            carModel = FindViewById<TextView>(Resource.Id.carModel);
            carDesc = FindViewById<TextView>(Resource.Id.carDesc);
            addFavBtn = FindViewById<Button>(Resource.Id.addfavBtn);
            makeCall = FindViewById<Button>(Resource.Id.makeCall);

            carName.Text = cNamefromDB;
            carMake.Text = cMakefromDB;
            carModel.Text = "Model: "+ cModelfromDB;
            carDesc.Text = "Description: " + cDescfromDB;

            count = myDbInstance.checkFav(idU, idC);
            if (count == 0)
            {
                addFavBtn.Text = "Add to favourites";
            }
            else
            {
                addFavBtn.Text = "Remove from favourites";
            }

            addFavBtn.Click += AddFavBtn_Click;
            makeCall.Click += MakeCall_Click;

        }

        private void MakeCall_Click(object sender, EventArgs e)
        {

            Intent callIntent = new Intent(Intent.ActionDial);
            callIntent.SetData(Uri.Parse($"tel:123456789"));
            StartActivity(callIntent);

        }

        private void AddFavBtn_Click(object sender, EventArgs e)
        {
            myAlert = new Android.App.AlertDialog.Builder(this);
            myAlert.SetTitle("Favourties");

            count = myDbInstance.checkFav(idU, idC);

            if(count == 0)
            {
                myAlert.SetMessage("Do you want to add this item to favorites!!!");
                myAlert.SetPositiveButton("Add", delegate {
                    myDbInstance.insertIntoFav(idU, idC);
                    addFavBtn.Text = "Remove from favourites";
                });
            }
            else
            {
                myAlert.SetMessage("Do you want to remove this item from favorites!!!");
                myAlert.SetPositiveButton("Remove", delegate {
                    myDbInstance.deleteFromFav(idU, idC);
                    addFavBtn.Text = "Add to favourites";
                });

            }

            myAlert.SetNegativeButton("Cancel", delegate {
                Console.Write("Cancelled");
            });
            Dialog myDialog = myAlert.Create();
            myDialog.Show();
        }
    }
}