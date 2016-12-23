using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Models;
using BAL;
namespace SphereInfoSolutionHRMS.Admin
{
    public partial class role : System.Web.UI.Page
    {
        Models.RoleModel rolemodel = new RoleModel();
        BAL.Role rolelevel = new BAL.Role();
        DataSet ds = new DataSet();
        Int32 UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        Int32 PageId = 2;
        Access access = new Access();
        Boolean IsAdd = false;
        Boolean IsUpdate = false;
        Boolean IsDelete = false;
        Boolean IsApprove = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PageId != 0)
                {
                    checkGivenAccess();
                }                
                    Displaylevel();
                    bindgrid();
                    bindTempgrid();
                    ((NestedMasterHome)this.Master).PageName = "Role";
                

            }
        }

        protected void checkGivenAccess()
        {
            DataTable dt = access.CheckGivenAccess(UserId, PageId);

            if (dt.Rows.Count > 0)
            {
                pnListRole.Visible = true;
                IsAdd = Convert.ToBoolean(dt.Rows[0][0]);
                IsUpdate = Convert.ToBoolean(dt.Rows[0][1]);
                IsDelete = Convert.ToBoolean(dt.Rows[0][2]);
                IsApprove = Convert.ToBoolean(dt.Rows[0][3]);
                if (IsAdd)
                {
                    pnAddRole.Visible = true;
                }
                if (IsApprove)
                {
                    pnPendingRole.Visible = true;
                }
            }
        }
        public void Displaylevel()
        {
            DataTable dt = new DataTable();
            dt = rolelevel.DisplayLevel();
            ddllevel.DataTextField = "Role_Level";
            ddllevel.DataValueField = "Level_Id";
            ddllevel.DataSource = dt;
            ddllevel.DataBind();
            // Then add your first item
            ddllevel.Items.Insert(0, "Select Level");

        }
        public void bindgrid()
        {
            DataTable dt = new DataTable();
            dt = rolelevel.Displaydata();
            if (dt.Rows.Count > 0)
            {                
                gvRole.DataSource = dt;
                gvRole.DataBind();
                gvRole.Visible = true;
                lblMessageRole.Text = "";
            }
            else
            {
                gvRole.Visible = false;
                lblMessageRole.Text = "No Roles Found";
                lblMessageRole.ForeColor = System.Drawing.Color.Red;
            }
        }


        public void bindTempgrid()
        {
            DataTable dt = new DataTable();
            dt = rolelevel.DisplayTempdata();
            if (dt.Rows.Count > 0)
            {
                gvTempRole.DataSource = dt;
                gvTempRole.DataBind();
                gvTempRole.Visible = true;
                btnapprove.Visible = true;
                btnreject.Visible = true;
                lblMessageTempRole.Text = "";
            }
            else
            {
                gvTempRole.Visible = false;
                btnapprove.Visible = false;
                btnreject.Visible = false;
                lblMessageTempRole.Text = "No Pending Request Found";
                lblMessageTempRole.ForeColor = System.Drawing.Color.Red;
            }

        }
        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
            }
        }
        public void clearall()
        {
            txtrolename.Text = "";
            ddllevel.SelectedIndex = -1;
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                rolemodel.RoleName = txtrolename.Text;
                if (!String.IsNullOrEmpty(ddllevel.SelectedValue.ToString()))
                {
                    rolemodel.RoleLevel = Convert.ToInt32(ddllevel.SelectedValue.ToString());
                }
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    rolemodel.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    // Do stuf...
                }
                BAL.Role rolelevel = new BAL.Role();
                int i = rolelevel.AddRole(rolemodel);
                if (i == -1)
                {
                    lblMessage.Text = "Role Already Exist";

                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else if (i == 1)
                {
                    lblMessage.Text = "Role Added Succesfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else if (i == 3)
                {

                    lblMessage.Text = "Role Added Succesfully.. Wating for approval.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else if (i == -3)
                {

                    lblMessage.Text = "Role Already Exist.. Wating for approval.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblMessage.Text = "Error";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                bindgrid();
                bindTempgrid();
                clearall();
            }
            
        }

        protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int RoleId = Convert.ToInt32(e.CommandArgument);
                int UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                rolemodel.RoleId = Convert.ToInt32(RoleId);
                rolemodel.UpdatedBy = Convert.ToInt32(UserId);
                int i = rolelevel.RemoveRole(rolemodel);
                if (i == 2)
                {
                    lblMessageRole.Text = "Role Removed Succesfully";
                    lblMessageRole.ForeColor = System.Drawing.Color.Green;
                }
                else if (i == 4)
                {

                    lblMessageRole.Text = "Role Removed Succesfully.. Wating for approval.";
                    lblMessageRole.ForeColor = System.Drawing.Color.Green;
                }
                else if (i == -5)
                {

                    lblMessageRole.Text = "Role  Already Removed.. Wating for approval.";
                    lblMessageRole.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblMessageRole.Text = "Error";
                    lblMessageRole.ForeColor = System.Drawing.Color.Red;
                }
                bindgrid();
                bindTempgrid();
                lblMessage.Text = "";
            }
        }

        protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int IsActive = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IsActive"));
                Button btnRemove = (Button)e.Row.FindControl("btnRemove");
                btnRemove.Attributes["onclick"] = "if(!confirm('Do you want to delete Role?')){ return false; };";
                //if (IsDelete)
                //{
                //    btnRemove.Visible = true;
                //}
                //else
                //{
                //    btnRemove.Visible = false;
                //}

                if (IsActive == 0)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5D8DC"); ;
                    btnRemove.Enabled = false;
                    btnRemove.CssClass = "btn btn-danger btn-xs disabled";

                }
                else if (IsActive == 1)
                {
                    //e.Row.BackColor = System.Drawing.Color.Honeydew;                    
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
            UpdateTempRoles(1);
            bindgrid();
            bindTempgrid();
            lblMessage.Text = "";
        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            UpdateTempRoles(2);
            bindgrid();
            bindTempgrid();
            lblMessage.Text = "";
        }

        protected void UpdateTempRoles(int Operation)
        {

            foreach (GridViewRow gvrow in gvTempRole.Rows)
            {
                CheckBox chkdApprove = (CheckBox)gvrow.FindControl("chkboxSelectRole");
                rolemodel.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                rolemodel.Operation = Operation;

                if (chkdApprove.Checked)
                {
                    rolemodel.TempRoleId = Convert.ToInt32(gvTempRole.DataKeys[gvrow.RowIndex].Value);

                    int i = rolelevel.UpdateTempRole(rolemodel);

                    if (Operation == 1)
                    {
                        if (i == 1)
                        {
                            lblMessageTempRole.Text = "Selected Roles Approved Succesfully";
                            lblMessageTempRole.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMessageTempRole.Text = "Error";
                            lblMessageTempRole.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        if (i == 2)
                        {
                            lblMessageTempRole.Text = "Selected Roles Removed Succesfully";
                            lblMessageTempRole.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMessageTempRole.Text = "Error";
                            lblMessageTempRole.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }

        protected void btnShowAllRoles_Click(object sender, EventArgs e)
        {
            bindgrid();
            txtSearchRole.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String SearchRole = txtSearchRole.Text;
                txtSearchRole.Text = "";
                DataTable dt = rolelevel.SearchRole(SearchRole);
                if (dt.Rows.Count > 0)
                {
                    gvRole.DataSource = dt;
                    gvRole.DataBind();
                    gvRole.Visible = true;
                    lblMessageRole.Text = "";
                }
                else
                {
                    gvRole.Visible = false;
                    lblMessageRole.Text = "No Roles Found";
                    lblMessageRole.ForeColor = System.Drawing.Color.Red;

                }
            }
        }

    }
}