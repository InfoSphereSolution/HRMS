using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BAL;
using System.Data;
namespace SphereInfoSolutionHRMS.Admin
{
    public partial class Designation : System.Web.UI.Page
    {
        Models.DesignationAttribute desigantionmodel = new Models.DesignationAttribute();
        BAL.DesignationMaster designation = new BAL.DesignationMaster();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((NestedMasterHome)this.Master).PageName = "Designation";
                DisplayDepartment();
                DisplayRole();
                DisplayDesigantion();
                DisplayTempDesigantion();
            }
        }

        //Display Department List
        public void DisplayDepartment()
        {

            dt = designation.DepartmentList();
            ddlDepartmentName.DataTextField = "Department_Name";
            ddlDepartmentName.DataValueField = "Dept_Id";
            ddlDepartmentName.DataSource = dt;
            ddlDepartmentName.DataBind();
            ddlDepartmentName.Items.Insert(0, "-Select Department-");
        }

        //Display Role List
        public void DisplayRole()
        {

            dt = designation.RoleList();
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleId";
            ddlRole.DataSource = dt;
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, "-Select Role-");
        }

        //Display Mst Desigantion
        public void DisplayDesigantion()
        {

            dt = designation.GetDesigantion();
           if (dt.Rows.Count > 0)
           {
               gvDesignation.DataSource = dt;
         
                gvDesignation.DataBind();
                gvDesignation.Visible = true;
                lblMessageDesignation.Text = "";
            }
            else
            {
                gvDesignation.Visible = false;
                lblMessageDesignation.Text = "No Roles Found";
                lblMessageDesignation.ForeColor = System.Drawing.Color.Red;
            }
        }

        //Display Void TempDesigantion
        public void DisplayTempDesigantion()
        {
            dt = designation.TempDesigantion();
            if (dt.Rows.Count > 0)
            {
                gvTempDesignation.DataSource = dt;
                gvTempDesignation.DataBind();
                gvTempDesignation.Visible = true;
                lblMessageTempDesignation.Text = "";
                btnapprove.Visible = true;
                btnreject.Visible = true;
            }
            else
            {

                gvTempDesignation.Visible = false;
                lblMessageTempDesignation.Text = "No Record Found";
                btnapprove.Visible = false;
                btnreject.Visible = false;  
            }
        }

        protected void btnAddDesignation_Click(object sender, EventArgs e)
        {
            desigantionmodel.DesignationName = txtDesignationname.Text;
            if (!string.IsNullOrEmpty(ddlRole.SelectedValue.ToString()))
            {

                desigantionmodel.RoleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            }
            if (!string.IsNullOrEmpty(ddlDepartmentName.SelectedValue.ToString()))
            {

                desigantionmodel.DeptId = Convert.ToInt32(ddlDepartmentName.SelectedValue.ToString());
            }


            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                desigantionmodel.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                // Do stuf...
            }
            int i = designation.AddDesignation(desigantionmodel);
            if (i == -1)
            {
                lblMessage.Text = "Designation Already Added";

                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else if (i == 1)
            {
                lblMessage.Text = "Designation Added Succesfully";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else if (i == 3)
            {

                lblMessage.Text = "Designation Added Succesfully.. Wating for approval.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else if (i == -3)
            {

                lblMessage.Text = "Designation Already Exist.. Wating for approval.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblMessage.Text = "Error";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            DisplayTempDesigantion();
            DisplayDesigantion();
        }


        //Method For TempTable
        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
            }
        }

        protected void gvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int DesignationId = Convert.ToInt32(e.CommandArgument);
                int UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                desigantionmodel.DesigId = Convert.ToInt32(DesignationId);
                desigantionmodel.UpdatedBy = Convert.ToInt32(UserId);
                int i = designation.RemoveDesignation(desigantionmodel);
                if (i == 2)
                {
                    lblMessageDesignation.Text = "Role Removed Succesfully";
                    lblMessageDesignation.ForeColor = System.Drawing.Color.Green;
                }
                else if (i == 4)
                {

                    lblMessageDesignation.Text = "Role Removed Succesfully.. Wating for approval.";
                    lblMessageDesignation.ForeColor = System.Drawing.Color.Green;
                }
                else if (i == -5)
                {

                    lblMessageDesignation.Text = "Role  Already Removed.. Wating for approval.";
                    lblMessageDesignation.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblMessageDesignation.Text = "Error";
                    lblMessageDesignation.ForeColor = System.Drawing.Color.Red;
                }
                DisplayTempDesigantion();
                DisplayDesigantion();

            }
        }

        protected void gvDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int IsActive = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IsActive"));
                Button btnRemove = (Button)e.Row.FindControl("btnRemove");

                if (IsActive == 0)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5D8DC"); ;
                    btnRemove.Enabled = false;
                    btnRemove.CssClass = "btn btn-danger btn-xs disabled";

                }
                else if (IsActive == 1)
                {
                    //e.Row.BackColor = System.Drawing.Color.Honeydew;
                    btnRemove.Visible = true;
                    btnRemove.Enabled = true;

                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Blue;
                }
            }
        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            UpdateTempDesignation(1);
            DisplayTempDesigantion();
            DisplayDesigantion();
            lblMessageTempDesignation.Text = "";
        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            UpdateTempDesignation(2);
            DisplayTempDesigantion();
            DisplayDesigantion();
            lblMessageTempDesignation.Text = "";
  
        }

        protected void UpdateTempDesignation(int Operation)
        {

            foreach (GridViewRow gvrow in gvTempDesignation.Rows)
            {
                CheckBox chkdApprove = (CheckBox)gvrow.FindControl("chkboxSelectDesignation");
                desigantionmodel.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                desigantionmodel.Operation = Operation;

                if (chkdApprove.Checked)
                {
                    desigantionmodel.TempDesigId = Convert.ToInt32(gvTempDesignation.DataKeys[gvrow.RowIndex].Value);

                    int i = designation.UpdateTempDesignation(desigantionmodel);

                    if (Operation == 1)
                    {
                        if (i == 1)
                        {
                            lblMessageTempDesignation.Text = "Selected Designation Approved Succesfully";
                            lblMessageTempDesignation.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMessageTempDesignation.Text = "Error";
                            lblMessageTempDesignation.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        if (i == 2)
                        {
                            lblMessageTempDesignation.Text = "Selected Designation Removed Succesfully";
                            lblMessageTempDesignation.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMessageTempDesignation.Text = "Error";
                            lblMessageTempDesignation.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            String Designation = txtSearchDesignation.Text;
            txtSearchDesignation.Text = "";
            DataTable dt = designation.FilterDesignation(Designation);
            if (dt.Rows.Count > 0)
            {
                gvDesignation.DataSource = dt;
                gvDesignation.DataBind();
                gvDesignation.Visible = true;
                lblMessageDesignation.Text = "";
            }
            else
            {
                gvDesignation.Visible = false;
                lblMessageDesignation.Text = "No Roles Found";
                lblMessageDesignation.ForeColor = System.Drawing.Color.Red;

            }
        }

        protected void btnShowAllDesignation_Click(object sender, EventArgs e)
        {
            DisplayDesigantion();
        }
    }
}