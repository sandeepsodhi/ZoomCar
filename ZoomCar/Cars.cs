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
    public class Cars
    {
        public string cId;
        public string cName;
        public string cModel;
        public string cMake;
        public string cDesc;
        public string cImage;
        public string cPostedBy;

        public Cars(string cId, string cName, string cModel, string cMake, string cDesc, string cImage, string cPostedBy)
        {
            this.cId = cId;
            this.cName = cName;
            this.cModel = cModel;
            this.cMake = cMake;
            this.cDesc = cDesc;
            this.cImage = cImage;
            this.cPostedBy = cPostedBy;
        }
    }
}