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
    public class user
    {
        public string id;
        public string fname;
        public string lname;
        public string email;
        public string password;
        public string age;

        public user(string idC, string fNameC, string lNameC, string emailC, string passwordC, string ageC)
        {
            this.id = idC;
            this.fname = fNameC;
            this.lname = lNameC;
            this.email = emailC;
            this.password = passwordC;
            this.age = ageC;
        }

    }
}