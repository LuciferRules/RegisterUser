using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace RegisterUser.Pages.Users
{


    public class IndexModel : PageModel
    {

        //create an empty List named 'listUsers'
        public List<UserInfo> listUsers = new List<UserInfo>();
        public void OnGet()
        {
            try
            {
                // hook up with connectionString to SQL server
                String connectionString = "Data Source=DESKTOP-HF7C6UH;Initial Catalog=registerdata;Integrated Security=True";
                // fetch all data from `users` table
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //String sql = "SELECT * FROM users;";
                    String sql = "EXEC selectall;"; //use stored procedure selectall
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {   // create an instance 'userInfo' to contain all data
                                UserInfo userInfo = new UserInfo();
                                userInfo.userid = "" + reader.GetInt64(0);
                                userInfo.username = reader.GetString(1);
                                userInfo.userno = "" + reader.GetInt32(2);
                                userInfo.createdate = reader.GetDateTime(3).ToString();
                                //add 'userInfo' to List 'listUsers'
                                listUsers.Add(userInfo);

                            }


                            //TODO :debugging, delete later
                            foreach (UserInfo user in listUsers)
                            {
                                Console.WriteLine("UserName: " + user.username + ", UserNo: " + user.userno);
                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    // Create a UserInfo inner class to contain UserID, UserName, UserNo, CreateDate
    public class UserInfo
    {
        public String userid;
        public String username;
        public String userno;
        public String createdate;
    }

}
