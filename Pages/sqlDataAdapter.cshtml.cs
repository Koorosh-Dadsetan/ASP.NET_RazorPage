using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClosedXML.Excel;

namespace ASP.NET_RazorPage.Pages
{
    public class sqlDataAdapter : PageModel
    {
        private DataTable dataTable = new DataTable("Employees Sheet");

        private string connString =
            "Data Source=DESKTOP-90OC7A4\\SQLEXPRESS;Initial Catalog=Test_db;Integrated Security=true";
        private string query =
            "SELECT id, FullName, Mobile, Age, Address FROM [Test_db].[dbo].[Employees]";

        public IEnumerable<DataRow>? Cultures { get; set; }

        public int TotalRecords { get; set; }

        public int PageNo { get; set; }

        public int PageSize { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchBox { get; set; }

        public void OnGet(int p = 1, int s = 5)
        {
            if (string.IsNullOrEmpty(SearchBox))
            {
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

                var results = from rows in dataTable.AsEnumerable()
                              select rows;

                Cultures = results
                    .Skip((p - 1) * s).Take(s);

                TotalRecords = results.Count();

                PageNo = p;

                PageSize = s;
            }
            else if (SearchBox != null)
            {
                query =
            "SELECT id, FullName, Mobile, Age, Address FROM [Test_db].[dbo].[Employees] WHERE FullName LIKE N'%" + SearchBox + "%'";

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

                var results = from myRow in dataTable.AsEnumerable()
                              select myRow;

                Cultures = results
                    .Skip((p - 1) * s).Take(s);

                TotalRecords = results.Count();

                PageNo = p;

                PageSize = s;
            }
        }

        public FileResult OnPostExportExcel()
        {
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

            using (XLWorkbook wb = new XLWorkbook { RightToLeft = true })
            {
                var ws = wb.Worksheets.Add(dataTable);

                var myCustomStyle = XLWorkbook.DefaultStyle;
                myCustomStyle.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                myCustomStyle.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                ws.RangeUsed(XLCellsUsedOptions.AllContents).Style = myCustomStyle;

                ws.Column(1).AdjustToContents();
                ws.Column(2).AdjustToContents();
                ws.Column(3).AdjustToContents();
                ws.Column(4).AdjustToContents();
                ws.Column(5).AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
                }
            }
        }

        public FileResult OnPostExportPDF()
        {
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

            //string htmlString = "<html><body><h1>Koorosh Dadsetan</h1></body></html>";

            using (MemoryStream stream = new MemoryStream())
            {
                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

                converter.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
                converter.Options.PdfPageOrientation = SelectPdf.PdfPageOrientation.Landscape;
                converter.Options.AutoFitWidth = SelectPdf.HtmlToPdfPageFitMode.AutoFit;
                converter.Options.AutoFitHeight = SelectPdf.HtmlToPdfPageFitMode.AutoFit;

                //SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlString);

                SelectPdf.PdfDocument doc = converter.ConvertUrl("https://localhost:7093/sqldataadapter");

                doc.Save(stream);
                doc.Close();

                return File(stream.ToArray(), "application/pdf", "Employees.pdf");
            }
        }
    }
}
