using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.SessionState;
using System.Web.Script.Serialization;
public partial class DOE_yes : System.Web.UI.Page
{
    [System.Web.Services.WebMethod]
    protected void Page_Load(object sender, EventArgs e)
    {
        rec_categorydata();


    }

    [System.Web.Services.WebMethod]
    public static string[] GetCustomer()
    {
        List<string> Customer = new List<string>();
        
        string strSQL = "select EP_Cate_KP from npi_ep_category where EP_Cate_Stage='PI1' and  EP_Cate_SpeChar='PI Thickness(ASI)' and EP_Cate_Iiitems='Wafer PSV type / Thickness'";


        clsMySQL db = new clsMySQL();
        foreach (DataRow dr in db.QueryDataSet(strSQL).Tables[0].Rows)
        {
            //customers.Add(string.Format("{0},{1}", dr["new_customer"], dr["new_device"]));
            Customer.Add(string.Format("{0}", dr["EP_Cate_KP"]));
        }

        return Customer.ToArray();

    }


    public static string GetCount(string count)
    {
        return count;
    }

    protected void count_categorydata()
    {
        string doe_str = "select count(*) from npi_ep_category where EP_Cate_Stage='PI1' and EP_Cate_Iiitems = 'PI Thickness (um)' and EP_Cate_SpeChar='PI CD' ";
        string count = "";

        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();

        MySqlCommand MySqlCmd = new MySqlCommand(doe_str, MySqlConn);
        MySqlDataReader mydr = MySqlCmd.ExecuteReader();

        while(mydr.Read())
        {
           count = mydr["count(*)"].ToString();
        }

        string strScript = string.Format("<script language='javascript'>createtable("+count+");</script>");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);


    }

    public void rec_categorydata()
    {
        string doe_str = "select * from npi_ep_category where EP_Cate_Stage='PI1' and  EP_Cate_SpeChar='PI Thickness(ASI)' and EP_Cate_Iiitems='Wafer PSV type / Thickness' ";
        string count = "";
        int num = 0;
        string[] category_kp = new string[50];
        List<string> str_stage = new List<string>();
        List<string> str_kp = new List<string>();
        List<string> str_md = new List<string>();
        List<string> str_cate = new List<string>();
        List<string> str_SpeChar = new List<string>();
        string kp = "";


        MySqlConnection MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        MySqlConn.Open();

        MySqlCommand MySqlCmd = new MySqlCommand(doe_str, MySqlConn);
        MySqlDataReader mydr = MySqlCmd.ExecuteReader();

        
        while (mydr.Read())
        {
            str_stage.Add("'" + (String)mydr["EP_Cate_Stage"] + "'");
            str_SpeChar.Add("'" + (String)mydr["EP_Cate_SpeChar"] + "'");
            str_md.Add("'" + (String)mydr["EP_Cate_Md"] + "'");
            str_cate.Add("'" + (String)mydr["EP_Cate_Cate"] + "'");
            str_kp.Add("'" + (String)mydr["EP_Cate_KP"] + "'");            
            
            
            num++;                            
        }

        StringBuilder sb = new StringBuilder(); //using System.Text;
        sb.Append("<script>");
        sb.Append(" var array_stage = new Array; var array_SpeChar = new Array;var array_md = new Array;var array_cate = new Array;var array_kp = new Array; var row_count = 0;");

       
                      
        foreach (string str in str_stage)
        {
            sb.Append("array_stage.push(" + str + ");");
        }
        foreach (string str in str_SpeChar)
        {
            sb.Append("array_SpeChar.push(" + str + ");");
        }
        foreach (string str in str_md)
        {
            sb.Append("array_md.push(" + str + ");");
        }
        foreach (string str in str_cate)
        {
            sb.Append("array_cate.push(" + str + ");");
        } foreach (string str in str_kp)
        {
            sb.Append("array_kp.push(" + str + ");");
        }
        sb.Append("row_count=" + num + ";");
        //sb.Append("createtable(" + num + ")");
        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "TestArrayScript", sb.ToString());

       

    }




    protected void Button1_Click(object sender, EventArgs e)
    {
        string gg = "'tt'";
        string strScript = string.Format("<script language='javascript'>createtable(20,"+gg+");</script>");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", strScript);
        //createtable
        //Page.RegisterClientScriptBlock("Pass", "<script>createtable(20" +  ",'" + gg + "');</script>");

    }
}