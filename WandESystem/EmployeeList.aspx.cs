using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

public partial class EmployeeList : BasePage
{
    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        RunScript("ChangeMenuCSSOnClick('lnkAdmin');");
        if (!IsPostBack)
        {
            SetRights(PageRole.SystemPages.EmployeeList.GetHashCode());
            int actionType = 0;
            if (int.TryParse(GetPlainQueryString("at"), out actionType))
            {
                if (actionType == ActionType.Save.GetHashCode())
                    ShowMessage("Employee Created Successfully.", lblMessage, MessageBoxType.Success);
                else if (actionType == ActionType.Update.GetHashCode())
                    ShowMessage("Employee Updated Successfully.", lblMessage, MessageBoxType.Success);
            }

            SortBy = "EmployeeNo";
            BindEmployeeDetails();
        }
    }
    #endregion

    #region privateMethods

    private void BindEmployeeDetails()
    {
        string searchText = txtSearch.Text.Trim();

        DataSet ds = Employee.GetAllEmployees(searchText);

        if (ds != null)
        {
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            if (!string.IsNullOrEmpty(SortBy))
            {
                dv.Sort = SortBy + " " + OrderBy;
            }

            gvEmployee.DataSource = dv;
        }
        gvEmployee.DataBind();
        
                
    }
    #endregion

    #region Events
    /// <summary>
    /// Perform Search
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindEmployeeDetails();
        lblMessage.Visible = false;
    }
    #endregion

    #region GridEvents
    protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblEmployeeType = (Label)e.Row.FindControl("lblEmpType");
            Int16 EmpType = Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "EmployeeType"));

            if (EmpType == Employee.EmployeeTypeName.Package.GetHashCode())
                lblEmployeeType.Text = Employee.EmployeeTypeName.Package.ToString();
            else if (EmpType == Employee.EmployeeTypeName.Bargaining.GetHashCode())
                lblEmployeeType.Text = Employee.EmployeeTypeName.Bargaining.ToString();
            else if (EmpType == Employee.EmployeeTypeName.Exco.GetHashCode())
                lblEmployeeType.Text = Employee.EmployeeTypeName.Exco.ToString();
            //if (!ProjectSession.IsAdmin)
            //    gvEmployee.Columns[6].Visible = false; 
            ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
            bool IsActive = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive"));
            if (!IsActive)
                imgEdit.Visible = false;
            
        }
    }   

    /// <summary>
    /// Handle Paging Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployee.PageIndex = e.NewPageIndex;
        BindEmployeeDetails();
    }

    /// <summary>
    /// Handle Row Command Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditEmployee")
        {
            int employeeId = int.Parse(e.CommandArgument.ToString());

            if (employeeId > 0)
                Response.Redirect("EmployeeEdit.aspx?ID=" + EncryptionDecryption.GetEncrypt(employeeId.ToString()));
        }
        //else if (e.CommandName == "DeleteEmployee")
        //{
        //    string deleteMessage = Employee.Delete(ConvertTo.Integer(e.CommandArgument));
        //    if (string.IsNullOrEmpty(deleteMessage))
        //        ShowMessage("Employee has been deleted successfully.", lblMessage, MessageBoxType.Information);
        //    else
        //        ShowMessage(deleteMessage, lblMessage, MessageBoxType.Warning);
        //    lblMessage.Visible = true;
        //    BindEmployeeDetails();
        //}
        else if (e.CommandName == "DeActiveAccount")
        {
            string deleteMessage = Employee.DeActivate(ConvertTo.Integer(e.CommandArgument),false);
            if (string.IsNullOrEmpty(deleteMessage))
                ShowMessage("Employee has been de-activated successfully.", lblMessage, MessageBoxType.Success);
            else
                ShowMessage(deleteMessage, lblMessage, MessageBoxType.Warning);
            lblMessage.Visible = true;
            BindEmployeeDetails();
        }
        else if (e.CommandName == "ActiveAccount")
        {
            string deleteMessage = Employee.DeActivate(ConvertTo.Integer(e.CommandArgument),true);
            if (string.IsNullOrEmpty(deleteMessage))
                ShowMessage("Employee has been de-activated successfully.", lblMessage, MessageBoxType.Success);
            else
                ShowMessage(deleteMessage, lblMessage, MessageBoxType.Warning);
            lblMessage.Visible = true;
            BindEmployeeDetails();
        }
    }

    /// <summary>
    /// Handle Sorting Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (SortBy == e.SortExpression)
        {
            if (OrderBy == SystemEnum.SortDirection.asc.ToString())
                OrderBy = SystemEnum.SortDirection.desc.ToString();
            else
                OrderBy = SystemEnum.SortDirection.asc.ToString();
        }
        else
        {
            SortBy = e.SortExpression;
            OrderBy = SystemEnum.SortDirection.asc.ToString();
        }
        BindEmployeeDetails();
    }
    #endregion
}