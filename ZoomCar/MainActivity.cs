using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace ZoomCar
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button loginBtn, signUpBtn;
        Intent i;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            loginBtn = FindViewById<Button>(Resource.Id.button1);
            signUpBtn = FindViewById<Button>(Resource.Id.button2);

            loginBtn.Click += LoginBtn_Click;
            signUpBtn.Click += SignUpBtn_Click;

        }

        private void SignUpBtn_Click(object sender, System.EventArgs e)
        {
            i = new Intent(this, typeof(SignUp));
            StartActivity(i);
        }

        private void LoginBtn_Click(object sender, System.EventArgs e)
        {
            i = new Intent(this, typeof(Login));
            StartActivity(i);
        }
    }
}
