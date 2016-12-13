using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using BAL;

namespace SphereInfoSolutionHRMS.Admin
{
    public partial class AccessControl : System.Web.UI.Page
    {
        Access access = new Access();
        Int32 UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        Boolean IsAdd = false;
        Boolean IsUpdate = false;
        Boolean IsDelete = false;
        Boolean IsApprove = false;
        String CurrentPageID = "";
        DataTable dt = new DataTable();
        DataTable dtFunctionality = new DataTable();
        Boolean Add, Update, Delete, Approve;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAccessControl(access.FetchAccess(), 0, null);
                bindDesignation();
            }
        }

        private void PopulateAccessControl(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["MenuText"].ToString(),
                    Value = row["Menu_Id"].ToString()
                };
                if (parentId == 0)
                {
                    tvAccessControl.Nodes.Add(child);
                    addFunctionality(child);
                    DataTable dtChild = this.access.FetchAccessChild(child.Value);
                    PopulateAccessControl(dtChild, int.Parse(child.Value), child);
                }
                else
                {
                    treeNode.ChildNodes.Add(child);
                    addFunctionality(child);

                }
            }
        }

        protected void addFunctionality(TreeNode child)
        {
            dtFunctionality = access.FetchFunctionalities(child.Value);            
            Add = Convert.ToBoolean(dtFunctionality.Rows[0]["Add"]);
            Update = Convert.ToBoolean(dtFunctionality.Rows[0]["Update"]);
            Delete = Convert.ToBoolean(dtFunctionality.Rows[0]["Delete"]);
            Approve = Convert.ToBoolean(dtFunctionality.Rows[0]["Approve"]);


            if (Add)
            {
                child.ChildNodes.Add(new TreeNode
                {
                    Text = "Add",
                    Value = "1"
                });
            }
            if (Update)
            {
                child.ChildNodes.Add(new TreeNode
                {
                    Text = "Edit",
                    Value = "1"
                });
            }
            if (Delete)
            {
                child.ChildNodes.Add(new TreeNode
                {
                    Text = "Delete",
                    Value = "1"
                });
            }
            if (Approve)
            {
                child.ChildNodes.Add(new TreeNode
                {
                    Text = "Approve/Reject",
                    Value = "1"
                });
            }
        }

        protected void bindDesignation()
        {
            DataTable dt = access.FetchDesignation();
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Desig_Id";
            ddlDesignation.DataTextField = "Designation_Name";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("--Select Designation", "0"));
        }
        


        protected void btnGiveAccess_Click(object sender, EventArgs e)
        {
            dt.Columns.Add("Access_Id", typeof(Int32));
            dt.Columns.Add("DesignationId", typeof(Int32));
            dt.Columns.Add("MenuId", typeof(Int32));
            dt.Columns.Add("IsAdd", typeof(Boolean));
            dt.Columns.Add("IsUpdate", typeof(Boolean));
            dt.Columns.Add("IsDelete", typeof(Boolean));
            dt.Columns.Add("IsApprove", typeof(Boolean));
            dt.Columns.Add("AddedBy", typeof(Int32));
            dt.Columns.Add("AddedOn", typeof(DateTime));
            TreeNodeCollection tvAccessControl = this.tvAccessControl.Nodes;            
            Boolean i = access.GrantAccess(GetCheckedNodes(tvAccessControl));

        }




        public DataTable GetCheckedNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode aNode in nodes)
            {
                //edit
                if (aNode.Checked && (aNode.Text != "Add" && aNode.Text != "Edit" && aNode.Text != "Delete" && aNode.Text != "Approve/Reject"))
                {
                    CurrentPageID = aNode.Value;
                    GetCheckedFunctions(aNode.ChildNodes);
                    //Insert
                    Int32 DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
                    dt.Rows.Add(1, DesignationID, CurrentPageID, IsAdd, IsUpdate, IsDelete, IsApprove, UserID, DateTime.Now);
                    IsAdd = false;
                    IsUpdate = false;
                    IsDelete = false;
                    IsApprove = false;

                }

                if (aNode.ChildNodes.Count != 0)
                {
                    GetCheckedNodes(aNode.ChildNodes);
                }
            }

            return dt;
        }

        protected void GetCheckedFunctions(TreeNodeCollection aNode)
        {
            foreach (TreeNode function in aNode)
            {
                if (function.Checked && function.Text == "Add")
                {
                    IsAdd = true;
                }

                else if (function.Checked && function.Text == "Edit")
                {
                    IsUpdate = true;
                }
                else if (function.Checked && function.Text == "Delete")
                {
                    IsDelete = true;
                }
                else if (function.Checked && function.Text == "Approve/Reject")
                {
                    IsApprove = true;
                }
            }
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDesignation.SelectedIndex != 0)
            {
                while (tvAccessControl.CheckedNodes.Count > 0)
                {
                    tvAccessControl.CheckedNodes[0].Checked = false;
                }

                DataTable dt = access.FetchGivenAccess(Convert.ToInt32(ddlDesignation.SelectedValue));
                tvAccessControl.Visible = true;
                CallRecursive(tvAccessControl, dt);
            }
            else
            {
                tvAccessControl.Visible = false;
            }
        }
        private void PrintRecursive(TreeNode treeNode, DataTable dt)
        {

           foreach(DataRow row in dt.Rows)
            {
                if (treeNode.Value == ((row[0]).ToString()) && treeNode.Text == ((row[1]).ToString()))
            {
                treeNode.Checked = true;
                if (treeNode.ChildNodes.Count > 0)
                {                    
                        addGivenFunctionality(treeNode);                    
                }

            }
            }
            // Print each node recursively.
            foreach (TreeNode tn in treeNode.ChildNodes)
            {
                PrintRecursive(tn, dt);
            }
        }

        // Call the procedure using the TreeView.
        private void CallRecursive(TreeView treeView, DataTable dt)
        {
            // Print each node recursively.
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode n in nodes)
            {
                PrintRecursive(n, dt);
            }
        }

        protected void addGivenFunctionality(TreeNode child)
        {
            dtFunctionality = access.FetchGivenFunctionalities(child.Value, Convert.ToInt32(ddlDesignation.SelectedValue));
            if (dtFunctionality.Rows.Count > 0 && child.ChildNodes.Count > 0)
            {
                Add = Convert.ToBoolean(dtFunctionality.Rows[0]["IsAdd"]);
                Update = Convert.ToBoolean(dtFunctionality.Rows[0]["IsUpdate"]);
                Delete = Convert.ToBoolean(dtFunctionality.Rows[0]["IsDelete"]);
                Approve = Convert.ToBoolean(dtFunctionality.Rows[0]["IsApprove"]);

                if (Add || Update || Delete || Approve)
                {
                    foreach (TreeNode nnn in child.ChildNodes)
                    {
                        if (nnn.Text == "Add" && Add)
                        {
                            nnn.Checked = true;
                        }
                        else if (nnn.Text == "Edit" && Update)
                        {
                            nnn.Checked = true;
                        }
                        else if (nnn.Text == "Delete" && Delete)
                        {
                            nnn.Checked = true;
                        }
                        else if (nnn.Text == "Approve/Reject" && Approve)
                        {
                            nnn.Checked = true;
                        }
                        else { }
                    }
                }
            }
            
        }       

       
    }
}