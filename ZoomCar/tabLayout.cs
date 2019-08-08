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
    public class tabLayout : Activity
    {
        String id;
        Fragment[] _fragmentsArray;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //set our view from the "main layout" resource
            RequestWindowFeature(Android.Views.WindowFeatures.ActionBar);
            //Enable navigation mode to support tab layout
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tabLayout);

            id = Intent.GetStringExtra("id");

            _fragmentsArray = new Fragment[]
            {
                new EditInfo(id),
                new ListOfItems(id),
                new Favourites(id),
                new PostAdd(id),
            };

            AddTabToActionBar("Edit Info");
            AddTabToActionBar("Cars List");
            AddTabToActionBar("Favorites");
            AddTabToActionBar("My Adds");
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // set the menu layout on Main Activity  
            MenuInflater.Inflate(Resource.Layout.mainMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem1:
                    {
                        Intent i = new Intent(this, typeof(MainActivity));
                        StartActivity(i);
                        return true;
                    }
            }

            return base.OnOptionsItemSelected(item);
        }

        void AddTabToActionBar(string tabTitle)
        {
            Android.App.ActionBar.Tab tab = ActionBar.NewTab();
            tab.SetText(tabTitle);

            tab.SetIcon(Android.Resource.Drawable.IcMediaPlay);
            tab.TabSelected += TabOnTabSelected;

            ActionBar.AddTab(tab);
        }

        private void TabOnTabSelected(object sender, Android.App.ActionBar.TabEventArgs tabEventArgs)
        {
            Android.App.ActionBar.Tab tab = (Android.App.ActionBar.Tab)sender;

            //Log.Debug(Tag, "The tab {0} has been selected ", tab.Text);

            Fragment frag = _fragmentsArray[tab.Position];

            tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
        }

    }
}
