using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;  // Step: 1 - 0
using Android.Database.Sqlite; // Step: 1 - 1
using Android.Database;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ZoomCar
{
    [Activity(Label = "DBHelper")]
    public class DBHelper : SQLiteOpenHelper
    {
        private static string _DatabaseName = "mydatabase.db";
        private const string TableName = "user";
        private const string ColumnID = "id";
        private const string ColumnfName = "fname";
        private const string ColumnlName = "lname";
        private const string ColumnEmail = "emails";
        private const string columnAge = "age";
        private const string columnPassword = "password";

        public const string createTable = "create table " +
      TableName + "( " + ColumnID + " INTEGER PRIMARY KEY AUTOINCREMENT,"
          + ColumnfName + " text,"
          + ColumnlName + " TEXT,"
          + ColumnEmail + " TEXT,"
          + columnAge + " INT,"
          + columnPassword + " TEXT);";

        SQLiteDatabase myDBObj; // Step: 1 - 5
        Context myContext; // Step: 1 - 6

        public DBHelper(Context context) : base(context, name: _DatabaseName, factory: null, version: 1) //Step 2;
        {
            myContext = context;
            myDBObj = WritableDatabase; // Step:3 create a DB objects
        }


        public override void OnCreate(SQLiteDatabase db)
        {
            System.Console.WriteLine("---------------\n\n\n\n\ncreate table query is: " + createTable);

            db.ExecSQL(createTable);  //Step: 4

        }

        public user selectMyValues(string id)
        {

            String sqlQuery = "Select * from " + TableName + " where " + ColumnID + " = " + id;

            ICursor result = myDBObj.RawQuery(sqlQuery, null);

            user userInfo = null;

            while (result.MoveToNext())
            {

                var IDfromDB = result.GetInt(result.GetColumnIndexOrThrow(ColumnID));
                var fNameFromDb = result.GetString(result.GetColumnIndexOrThrow(ColumnfName));
                var lNameFromDb = result.GetString(result.GetColumnIndexOrThrow(ColumnlName));
                var emailFromDb = result.GetString(result.GetColumnIndexOrThrow(ColumnEmail));
                var ageFromDb = result.GetString(result.GetColumnIndexOrThrow(columnAge));
                var passwordFromDb = result.GetString(result.GetColumnIndexOrThrow(columnPassword));

                userInfo = new user(IDfromDB.ToString(),fNameFromDb, lNameFromDb, emailFromDb, passwordFromDb, ageFromDb);

                System.Console.WriteLine(" Value fROM DB --> " + IDfromDB + "  " + fNameFromDb + "  " + lNameFromDb + "  " + emailFromDb + "  " + ageFromDb + "  " + passwordFromDb);
            }
            return userInfo;
        }

        public ICursor showAllData()
        {
            String selectAllSQL = "select * from " + TableName;
            return myDBObj.RawQuery(selectAllSQL, null);
        }


        public int validateLogin(string email, string password)
        {
            String loginStmt = "Select " + ColumnID + " from " + TableName + " where " + ColumnEmail + "=" + "'" + email + "' and " + columnPassword + "= '" + password + "'";

            ICursor result = myDBObj.RawQuery(loginStmt, null);

            int IDfromDB = 0;

            if (result.Count > 0)
            {
                while (result.MoveToNext())
                {
                    IDfromDB = result.GetInt(result.GetColumnIndexOrThrow(ColumnID));
                }
                System.Console.WriteLine("Email found");
                return IDfromDB;
            }
            else
            {
                System.Console.WriteLine("Not Email found");
                return IDfromDB;
            }
        }

        public void insertMyValue(string vfname, string vlname, string vemail, string vage, string vpassword)
        {

            String insertSQL = "insert into " + TableName + "(" + ColumnfName + "," + ColumnlName + "," + ColumnEmail + "," + columnAge + "," + columnPassword + ") values ('" + vfname + "'" + "," + "'" + vlname + "'," + "'" + vemail + "'," + vage + ",'" + vpassword + "'" + ");";

            System.Console.WriteLine("Insert SQL " + insertSQL);
            myDBObj.ExecSQL(insertSQL);


        }

        public void updateData(string id, string vfname, string vlname, string vage, string vpassword, string vemail)
        {
            String updateSQL = "update  " + TableName + " set " + ColumnfName + " = '" + vfname + "' , " + ColumnlName + " ='" + vlname + "' ," + ColumnEmail + " ='" + vemail + "' , " + columnAge + " ='" + vage + "' , " + columnPassword + " ='" + vpassword + "'  where " + ColumnID + "=" + Convert.ToInt32(id);

            System.Console.WriteLine("Insert SQL " + updateSQL);
            myDBObj.ExecSQL(updateSQL);
        }

        public void deleteData(string id)
        {
            String deleteSQL = "delete from " + TableName + " where " + ColumnID + " = " + Convert.ToInt32(id);

            System.Console.WriteLine("Delete SQL " + deleteSQL);
            myDBObj.ExecSQL(deleteSQL);

        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new NotImplementedException();
        }
    }
}