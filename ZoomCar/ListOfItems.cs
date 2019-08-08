using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ZoomCar
{
    public class ListOfItems : Fragment
    {
        String idU;
        Intent i;
        ListView myListView;
        SearchView mySearchView;
        DBHelper myDbInstance;
        List<Cars> myVehicleList = new List<Cars>();
        Android.App.AlertDialog.Builder myAlert;

        public ListOfItems(String uId)
        {
            this.idU = uId;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View myView = inflater.Inflate(Resource.Layout.ListOfItems, container, false);

            myDbInstance = new DBHelper(Activity);

            myListView = myView.FindViewById<ListView>(Resource.Id.listView1);
            mySearchView = myView.FindViewById<SearchView>(Resource.Id.searchView1);

            myVehicleList.Clear();
            ICursor result = myDbInstance.selectVehicleList(idU);

            while (result.MoveToNext())
            {
                var cIDfromDB = result.GetInt(result.GetColumnIndexOrThrow("cId"));
                var cNamefromDB = result.GetString(result.GetColumnIndexOrThrow("cName"));
                var cMakefromDB = result.GetString(result.GetColumnIndexOrThrow("cMake"));
                var cModelfromDB = result.GetString(result.GetColumnIndexOrThrow("cModel"));
                var cDescfromDB = result.GetString(result.GetColumnIndexOrThrow("cDesc"));
                var cImagefromDB = result.GetString(result.GetColumnIndexOrThrow("cImage"));
                var cPostedByfromDB = result.GetString(result.GetColumnIndexOrThrow("cPostedById"));

                myVehicleList.Add(new Cars(cIDfromDB.ToString(), cNamefromDB, cMakefromDB, cModelfromDB, cDescfromDB, cImagefromDB, cPostedByfromDB));
            }

            //myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, stringArray);
            var myAdapter = new CustomAdapter(Activity, myVehicleList);

            myListView.Adapter = myAdapter;
            //myListView.ItemClick += MyListView_ItemClick;
            myListView.ItemClick += MyListView_ItemClick;

            mySearchView.QueryTextChange += MySearchView_QueryTextChange;

            return myView;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void MySearchView_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            string searchValue = e.NewText;
            System.Console.WriteLine("value is: " + searchValue);

            //List<string> newStringArray = new List<string>();
            List<Cars> newCars = new List<Cars>();

            //foreach (string str in stringArray)
            //{
            //    if (str.Contains(searchValue))
            //    {
            //        newStringArray.Add(str.ToString());
            //    }
            //}

            foreach (Cars carsObj in myVehicleList)
            {
                if (carsObj.cName.Contains(searchValue))
                {
                    newCars.Add(carsObj);
                }
            }

            //myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, newStringArray);

            var myAdapter = new CustomAdapter(Activity, newCars);
            myListView.Adapter = myAdapter;

        }

        private void MyListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            myAlert = new Android.App.AlertDialog.Builder(Activity);

            var data = myVehicleList[e.Position];
            var addId = data.cId;

            i = new Intent(Activity, typeof(ViewAdd));
            i.PutExtra("cid", addId.ToString());
            i.PutExtra("uid", idU);

            StartActivity(i);

            /*
            myAlert.SetTitle("Delete");
            myAlert.SetMessage("Do you want to delete this user?");
            myAlert.SetPositiveButton("Delete", delegate {
                var my = myUserList[e.Position];
                 var test = my.cDesc;


            });
            Dialog myDialog = myAlert.Create();
            myDialog.Show();*/
            //    var myValue = stringArray[index];
        }
    }
}