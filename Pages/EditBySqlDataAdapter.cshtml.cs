using ASP.NET_RazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_RazorPage.Pages
{
    public class EditBySqlDataAdapterModel : PageModel
    {
        public DataTable dataTable = new DataTable();

        private string connString =
            "Data Source=DESKTOP-90OC7A4\\SQLEXPRESS;Initial Catalog=Test_db;Integrated Security=true";

        private string? query { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامی می‌باشد")]
        [MaxLength(30, ErrorMessage = "حداکثر 30 کاراکتر مجاز می‌باشد")]
        [BindProperty]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامی می‌باشد")]
        [MinLength(11, ErrorMessage = "لطفا شماره تماس را به صورت صحیح وارد نمائید")]
        [MaxLength(11, ErrorMessage = "لطفا شماره تماس را به صورت صحیح وارد نمائید")]
        [BindProperty]
        public string? Mobile { get; set; }

        [Range(0, 120, ErrorMessage = "لطفا سن را به صورت صحیح وارد نمائید")]
        [BindProperty]
        public int? Age { get; set; }

        [MaxLength(50, ErrorMessage = "حداکثر 50 کاراکتر مجاز می‌باشد")]
        [BindProperty]
        public string? Address { get; set; }

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

            FullName = (string?)dataTable.Rows[0]["FullName"];
            Mobile = (string?)dataTable.Rows[0]["Mobile"];
            Age = (int?)dataTable.Rows[0]["Age"];
            Address = (string?)dataTable.Rows[0]["Address"];
        }

        public IActionResult OnPost(int? id)
        {
            query = "UPDATE [Test_db].[dbo].[Employees] SET FullName=N'" + FullName + "' ,Mobile='" + Mobile + "' ,Age=" + Age + ", Address=N'" + Address + "' WHERE id=" + id;

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
