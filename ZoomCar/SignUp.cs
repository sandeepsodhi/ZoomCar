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
    [Activity(Label = "SignUp")]
    public class SignUp : Activity
    {
        EditText fnameE, lnameE, ageE, passwordE, emailE;
        Button btnRegisterE;
        DBHelper myDbInstace;
        Android.App.AlertDialog.Builder myAlert;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.signUpScreen);

            myDbInstace = new DBHelper(this);

            fnameE = FindViewById<EditText>(Resource.Id.fName);
            lnameE = FindViewById<EditText>(Resource.Id.lName);
            ageE = FindViewById<EditText>(Resource.Id.age);
            passwordE = FindViewById<EditText>(Resource.Id.password);
            emailE = FindViewById<EditText>(Resource.Id.email);

            btnRegisterE = FindViewById<Button>(Resource.Id.btnRegister);

            btnRegisterE.Click += BtnRegisterE_Click;
        }
        private void BtnRegisterE_Click(object sender, EventArgs e)
        {
            var vfname = fnameE.Text;
            var vlname = lnameE.Text;
            var vage = ageE.Text;
            var vpassword = passwordE.Text;
            var vemail = emailE.Text;

            myAlert = new Android.App.AlertDialog.Builder(this);

            if (vfname == " " || vfname.Equals(""))
            {
                errorMessageDialog("first name");
            }
            else if (vlname == " " || vlname.Equals(""))
            {
                errorMessageDialog("last name");
            }
            else if (vemail == " " || vemail.Equals(""))
            {
                errorMessageDialog("email");
            }
            else if (vpassword == " " || vpassword.Equals(""))
            {
                errorMessageDialog("password");
            }
            else if (vage == " " || vage.Equals(""))
            {
                errorMessageDialog("phone number");
            }
            else
            {
                myDbInstace.insertMyValue(vfname, vlname, vemail, vage, vpassword);

                Toast.MakeText(this, "Registration Succesfull!!", ToastLength.Long).Show();

                Intent i = new Intent(this, typeof(MainActivity));
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
            System.Console.WriteLine("Ok button is clicked.");
        }
    }
}
