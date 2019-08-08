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

namespace ZoomCar
{
    public class EditInfo : Fragment
    {
        DBHelper myDbInstace;
        TextView welcomeMessageText;
        EditText fnameEdit;
        EditText lnameEdit;
        EditText emailEdit;
        EditText ageEdit;
        EditText passwordEdit;
        Button updateBtn, deleteBtn;

        string id;

        public EditInfo(string idIntent)
        {
            this.id = idIntent;
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
            View myView = inflater.Inflate(Resource.Layout.EditInfo, container, false);
            
            welcomeMessageText = myView.FindViewById<TextView>(Resource.Id.welcomMessage);
            fnameEdit = myView.FindViewById<EditText>(Resource.Id.fNameEdit);
            lnameEdit = myView.FindViewById<EditText>(Resource.Id.lNameEdit);
            emailEdit = myView.FindViewById<EditText>(Resource.Id.emailEdit);
            ageEdit = myView.FindViewById<EditText>(Resource.Id.ageEdit);
            passwordEdit = myView.FindViewById<EditText>(Resource.Id.passwordEdit);

            emailEdit.Enabled = false;
            EnabledisableEditBox(false);

            myDbInstace = new DBHelper(Activity);
            user userInfo = myDbInstace.selectMyValues(id);

            welcomeMessageText.Text = "Welcome " + userInfo.fname;
            fnameEdit.Text = userInfo.fname;
            lnameEdit.Text = userInfo.lname;
            emailEdit.Text = userInfo.email;
            ageEdit.Text = userInfo.age;
            passwordEdit.Text = userInfo.password;

            updateBtn = myView.FindViewById<Button>(Resource.Id.btnUpdate);
            updateBtn.Click += updateInfo;

            deleteBtn = myView.FindViewById<Button>(Resource.Id.btnDelete);
            deleteBtn.Click += deleteRecord;
            return myView;

//            return base.OnCreateView(inflater, container, savedInstanceState);
        }
        public override void OnResume()
        {
            base.OnResume();
            System.Console.WriteLine("OnResume");

        }

        private void updateInfo(object sender, EventArgs e)
        {
            if (!fnameEdit.Enabled)
            {
                EnabledisableEditBox(true);
                updateBtn.Text = "Update";
            }
            else
            {
                var vfname = fnameEdit.Text;
                var vlname = lnameEdit.Text;
                var vage = ageEdit.Text;
                var vpassword = passwordEdit.Text;
                var vemail = emailEdit.Text;
                myDbInstace.updateData(id, vfname, vlname, vage, vpassword, vemail);
                updateBtn.Text = "Edit";
                EnabledisableEditBox(false);
                Toast.MakeText(Activity, "Update successfull", ToastLength.Long).Show();
            }
        }

        private void deleteRecord(object sender, EventArgs e)
        {
            myDbInstace.deleteData(id);
            Toast.MakeText(Activity, "Deletion successful", ToastLength.Long).Show();

            Intent i = new Intent(Activity, typeof(Login));
            StartActivity(i);
        }

        private void EnabledisableEditBox(bool flag)
        {
            fnameEdit.Enabled = flag;
            lnameEdit.Enabled = flag;
            ageEdit.Enabled = flag;
            passwordEdit.Enabled = flag;
        }
    }
}
