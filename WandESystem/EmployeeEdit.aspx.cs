using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;
using System.Text;

public partial class EmployeeEdit : BasePageHome
{
    #region Variable/Property Declaration

    /// <summary>
    /// EmployeeId
    /// </summary>
    public int EmployeeId
    {
        get
        {
            string id = GetQueryString("ID");

            if (!string.IsNullOrEmpty(id))
                return Convert.ToInt32(id);
            else
                return 0;
        }
    }

    /// <summary>
    /// Employee View
    /// </summary>
    private DataTable EmployeeView
    {
        get
        {
            return (DataTable)ViewState["EmployeeView"];
        }
        set
        {
            ViewState["EmployeeView"] = value;
        }

    }

    #endregion

    #region Page Events

    /// <summary>
    /// Page Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkAdmin');");

        if (!Page.IsPostBack)
        {
            SetRights(PageRole.SystemPages.EmployeeEdit.GetHashCode());

            General.BindEmployeeType(ref rdEmpType);
            rdEmpType.SelectedValue = Employee.EmployeeTypeName.Package.GetHashCode().ToString();

            if (EmployeeId > 0)
            {
                btnSave.Text = "Update";
                FillControls();
            }
            else
            {
                GetEmployeeView();
                ddlEmpNo.DataSource = EmployeeView;
                ddlEmpNo.DataBind();
                ddlEmpNo.Items.Insert(0, new ListItem(String.Empty, "0"));
                ddlEmpNo.SelectedIndex = 0;
            }
        }
    }
    #endregion

    #region Control Events

    /// <summary>
    /// Save Page
    /// </summary>
    /// <param name=sender></param>
    /// <param name=e></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (ValidatePage())
        //{
        int actionType = ActionType.Save.GetHashCode();

        Employee objEmployee;

        if (EmployeeId > 0)
        {
            actionType = ActionType.Update.GetHashCode();
            objEmployee = new Employee(EmployeeId);
        }
        else
        {
            objEmployee = new Employee();
        }

        FillObject(ref objEmployee);
        objEmployee.Save();

        //if (EmployeeId == 0)
        //    SendPassword(objEmployee.EmployeeEmail , objEmployee.EmployeePassword );

        Response.Redirect("EmployeeList.aspx?at=" + actionType);
        //}
    }

    /// <summary>
    /// handles Cancel Click.Redirect to EmployeeList page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeList.aspx");
    }

    /// <summary>
    /// Get Name And Surname of the Empoyee
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEmpNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpNo.SelectedIndex != -1)
        {
            DataRow[] drEmployee = EmployeeView.Select("IDENTIFIER='" + ddlEmpNo.SelectedValue.ToString() + "'");
            if (drEmployee.Length > 0)
            {
                txtEmpName.Text = Convert.ToString(drEmployee[0]["NAME"]);
                txtEmpSurname.Text = Convert.ToString(drEmployee[0]["SURNAME"]);
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Fill Object
    /// </summary>
    private void FillObject(ref Employee objEmployee)
    {
        objEmployee.EmployeeId = this.EmployeeId;
        objEmployee.EmployeeNo = ddlEmpNo.Visible == true ? ddlEmpNo.SelectedValue : lblEmpNo.Text;
        objEmployee.EmployeeName = Server.HtmlEncode(txtEmpName.Text.Trim());
        objEmployee.EmployeeSurname = Server.HtmlEncode(txtEmpSurname.Text.Trim());
        objEmployee.EmployeeEmail = Server.HtmlEncode(txtEmail.Text.Trim());
        objEmployee.EmployeeType = Convert.ToInt16(rdEmpType.SelectedValue);

        if (EmployeeId > 0)
        {
            objEmployee.Updated_UserId = ProjectSession.UserID;
            objEmployee.Updated_TS = DateTime.Now;
        }
        else
        {
            objEmployee.EmployeePassword = System.Web.Security.Membership.GeneratePassword(12, 1);
            objEmployee.Created_UserId = ProjectSession.UserID;
            objEmployee.Created_TS = DateTime.Now;
        }
    }

    /// <summary>
    /// Fill Controls
    /// </summary>
    private void FillControls()
    {
        Employee objEmployee = new Employee(this.EmployeeId);
        lblEmpNo.Text = objEmployee.EmployeeNo;
        ddlEmpNo.SelectedValue = objEmployee.EmployeeNo;
        txtEmpName.Text = objEmployee.EmployeeName;
        txtEmpSurname.Text = objEmployee.EmployeeSurname;
        txtEmail.Text = objEmployee.EmployeeEmail;

        rdEmpType.SelectedValue = objEmployee.EmployeeType.ToString();

        ddlEmpNo.Visible = false;
        rfvEmpNo.Enabled = false;
        lblEmpNo.Visible = true;
    }

    /// <summary>
    /// Get Employee View
    /// </summary>
    private void GetEmployeeView()
    {
        EmployeeView = Employee.GetEmployeeView();
    }

    /// <summary>
    /// Sned password - to new employee
    /// </summary>
    private void SendPassword(string strEmail , string strPassword)
    {
        try
        {
            string strBody = ReadTextFile(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ForgotPasswordTemplatePath"]));
            strBody = strBody.Replace("$$UserName$$", strEmail);
            strBody = strBody.Replace("$$Password$$", strPassword);

            Common.SendMail(System.Configuration.ConfigurationManager.AppSettings["FromAddress"].ToString(), System.Configuration.ConfigurationManager.AppSettings["FromDisplay"].ToString(), strEmail, "", "", "Password Details", strBody, true);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, lblMessage, MessageBoxType.Error);
        }
    }

    /// <summary>
    /// Return string from reading text file
    /// </summary>
    /// <param name="sFile"></param>
    /// <returns></returns>
    public static string ReadTextFile(string sFile)
    {
        System.IO.FileStream fs = new System.IO.FileStream(sFile, System.IO.FileMode.Open);
        System.IO.StreamReader sr = new System.IO.StreamReader(fs);
        string sText = sr.ReadToEnd();
        sr.Close();
        fs.Close();
        return sText;
    }
    #endregion

}