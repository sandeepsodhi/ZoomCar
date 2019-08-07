using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Database;

namespace ZoomCar
{
    public class PostAdd : Fragment
    {
        String idU;
        Button postBtn;
        Intent i;
        ListView myListView;
        SearchView mySearchView;
        DBHelper myDbInstance;
        List<Cars> myUserList = new List<Cars>();
        Android.App.AlertDialog.Builder myAlert;

        public PostAdd(String uId)
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
            View myView = inflater.Inflate(Resource.Layout.PostAdd, container, false);

            postBtn = myView.FindViewById<Button>(Resource.Id.postAdd);
            myDbInstance = new DBHelper(Activity);

            myListView = myView.FindViewById<ListView>(Resource.Id.listView1);
            mySearchView = myView.FindViewById<SearchView>(Resource.Id.searchView1);

            myUserList.Clear();
            ICursor result = myDbInstance.selectMyVehicleValue(idU);

            while (result.MoveToNext())
            {

                var cIDfromDB = result.GetInt(result.GetColumnIndexOrThrow("cId"));
                var cNamefromDB = result.GetString(result.GetColumnIndexOrThrow("cName"));
                var cMakefromDB = result.GetString(result.GetColumnIndexOrThrow("cMake"));
                var cModelfromDB = result.GetString(result.GetColumnIndexOrThrow("cModel"));
                var cDescfromDB = result.GetString(result.GetColumnIndexOrThrow("cDesc"));
                var cPostedByfromDB = result.GetString(result.GetColumnIndexOrThrow("cPostedById"));

                myUserList.Add(new Cars(cIDfromDB.ToString(), cNamefromDB, cMakefromDB, cModelfromDB, cDescfromDB, cPostedByfromDB));

            }

            //myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, stringArray);
            var myAdapter = new CustomAdapter(Activity, myUserList);

            myListView.Adapter = myAdapter; 
            //myListView.ItemClick += MyListView_ItemClick;
            myListView.ItemClick += MyListView_ItemClick;

            mySearchView.QueryTextChange += MySearchView_QueryTextChange;

            postBtn.Click += PostBtn_Click;

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

            foreach (Cars carsObj in myUserList)
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
            var index = e.Position;
            myAlert.SetTitle("Delete");
            myAlert.SetMessage("Do you want to delete this user?");

            myAlert.SetNegativeButton("Delete", delegate {
                var data = myUserList[e.Position];
                myDbInstance.deleteVehicleValue(idU, data.cId.ToString());

            });

            myAlert.SetPositiveButton("Edit", delegate {
                var data = myUserList[e.Position];
                i = new Intent(Activity, typeof(EditAdd));
                i.PutExtra("cid", data.cId.ToString());
                i.PutExtra("uid", idU);
                StartActivity(i);

            });
            Dialog myDialog = myAlert.Create();
            myDialog.Show();
            //    var myValue = stringArray[index];
        }

        private void PostBtn_Click(object sender, EventArgs e)
        {
            i = new Intent(Activity, typeof(PostAddScreen));
            i.PutExtra("Uid", idU);
            StartActivity(i);
        }
    }
}
