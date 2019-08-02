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
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        EditText email;
        EditText password;
        Button loginBtn;
        Button signUpbtn;
        DBHelper myDbInstace;

        Intent i;

        Android.App.AlertDialog.Builder myAlert;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loginScreen);
            // Create your application here
            myDbInstace = new DBHelper(this);


            email = FindViewById<EditText>(Resource.Id.userName);
            password = FindViewById<EditText>(Resource.Id.password);
            loginBtn = FindViewById<Button>(Resource.Id.login);
            signUpbtn = FindViewById<Button>(Resource.Id.signUp);

            loginBtn.Click += myButtonClick;
            signUpbtn.Click += SignUpbtnClick;
        }

        private void SignUpbtnClick(object sender, EventArgs e)
        {
            i = new Intent(this, typeof(SignUp));
            StartActivity(i);
        }

        private void myButtonClick(object sender, EventArgs e)
        {
            var vName = email.Text;
            var vPassword = password.Text;

            myAlert = new Android.App.AlertDialog.Builder(this);


            if (vName == " " || vName.Equals(""))
            {
                errorMessageDialog("username");

            }
            else if (vPassword == " " || vPassword.Equals(""))
            {
                errorMessageDialog("password");
            }
            else
            {
                int IDfromDB = myDbInstace.validateLogin(vName, vPassword);
                if (IDfromDB != 0)
                {

                    i = new Intent(this, typeof(SignUp));
                    i.PutExtra("id", IDfromDB.ToString());
                    StartActivity(i);
                }
                else
                {
                    myAlert.SetTitle("Error!!!");
                    myAlert.SetMessage("Invalid Username or password");
                    myAlert.SetPositiveButton("OK", OkAction);
                    Dialog myDialog = myAlert.Create();
                    myDialog.Show();
                }
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
