using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace RegisterUser.Pages.Users
{
    public class EditModel : PageModel
    {
        //Create empty instance 'clientInfo'
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {

            // id is obtained when Edit button is clicked
            String id = Request.Query["userid"];
            try
            {

                // hook up with connectionString to SQL server
                string connectionString = "Data Source=DESKTOP-HF7C6UH;Initial Catalog=registerdata;Integrated Security=True";
                // fetch data with the id from `users` table
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users WHERE UserID=@userid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@userid", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                userInfo.userid = "" + reader.GetInt64(0);
                                userInfo.username = reader.GetString(1);
                                userInfo.userno = "" + reader.GetInt32(2);
                               
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

        public void OnPost()
        {
            // get data from the form by Request.Form
            userInfo.userid = Request.Form["userid"]; // require id to update 'users' table
            userInfo.username = Request.Form["username"];
            userInfo.userno = Request.Form["userno"];
           
            // check empty string and output error message
            if (userInfo.userid.Length == 0 || userInfo.username.Length == 0 || userInfo.userno.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            // update user into database
            try
            {
                // hook up with connectionString to SQL server
                string connectionString = "Data Source=DESKTOP-HF7C6UH;Initial Catalog=registerdata;Integrated Security=True";
                // fetch all data from `users` table
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
 //                   String sql = "UPDATE users " +
 //                                 "SET UserName=@username, UserNo=@userno " +
 //                                 "WHERE UserID=@userid;";
                      String sql = "EXEC updateuser @userid, @username, @userno;"; //use updateuser stored procedure
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // replace every @ with data from Request.Form
                        command.Parameters.AddWithValue("@username", userInfo.username);
                        command.Parameters.AddWithValue("@userno", userInfo.userno);            
                        command.Parameters.AddWithValue("@userid", userInfo.userid);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            //redirect the page to `Users Index` page
            Response.Redirect("/Users/Index");
        }
    }
}

