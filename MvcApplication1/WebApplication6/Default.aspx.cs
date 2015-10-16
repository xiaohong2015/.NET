using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication6
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Name= Request.QueryString["Name"];
            if (Name != null && !Name.Equals(""))
            {
                string constr = "Data Source=XIAOHONG-PC;Initial Catalog=asp;Persist Security Info=True;User ID=sa;Password=xiaohong";
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string sql = "select price from T_Products where Name=@Name";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("Name", Name);

                SqlDataReader rd = cmd.ExecuteReader();
                double Price = 0;
                if (rd.Read())
                {
                    Price = Convert.ToDouble(rd["Price"]);
                }

                JavaScriptSerializer jss = new JavaScriptSerializer();
                Response.ContentType = "application/json";
                Response.Write(jss.Serialize(Price));
                Response.End();
            }
        }
    }
}