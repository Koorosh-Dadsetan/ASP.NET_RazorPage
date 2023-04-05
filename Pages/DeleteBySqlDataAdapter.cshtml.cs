using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_RazorPage.Pages
{
    public class DeleteBySqlDataAdapterModel : PageModel
    {
        public DataTable dataTable = new DataTable();

        private string connString =
            "Data Source=DESKTOP-90OC7A4\\SQLEXPRESS;Initial Catalog=Test_db;Integrated Security=true";

        private string? query { get; set; }

        public void OnGet(int? id)
        {
            query =
            "SELECT id, FullName, Mobile, Age, Address FROM [Test_db].[dbo].[Employees] WHERE id=" + id;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);

                    conn.Close();
                    da.Dispose();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public IActionResult OnPost(int? id)
        {
            query = "DELETE [Test_db].[dbo].[Employees] WHERE id=" + id;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return Redirect("/sqlDataAdapter");
        }
    }
}
