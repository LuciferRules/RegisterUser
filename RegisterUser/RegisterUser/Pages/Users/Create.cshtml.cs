using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace RegisterUser.Pages.Users
{
    public class CreateModel : PageModel
    {
        //Create empty instance 'UserInfo'
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {   // get data from the form by Request.Form
            userInfo.username = Request.Form["username"];
            userInfo.userno = Request.Form["userno"];
         
            // check empty string and output error message
            if (userInfo.username.Length == 0 || userInfo.userno.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            // save new client into database
            try
            {
                // hook up with connectionString to SQL server
                string connectionString = "Data Source=DESKTOP-HF7C6UH;Initial Catalog=registerdata;Integrated Security=True";
                // fetch all data from `users` table
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
 //                   String sql = "INSERT INTO users " +
 //                                 "(username, userno) VALUES " +
 //                                 "(@username, @userno);";
                      String sql = "EXEC addnewuser @username, @userno;"; //use addnewuser stored procedure
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // replace every @ with data from Request.Form
                        command.Parameters.AddWithValue("@username", userInfo.username);
                        command.Parameters.AddWithValue("@userno", userInfo.userno);
               
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // upon success input, empty fill the Form and output success message
            userInfo.username = "";
            userInfo.userno = "";
            successMessage = "New Client Added Correctly";
            //redirect the page to `Users` page
            Response.Redirect("/Users/Index");
        }
    }
}
