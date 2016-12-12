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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateAccessControl(access.FetchAccess(), 0, null);
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
            DataTable dtFunctionality = access.FetchFunctionalities(child.Value);
            Boolean Add, Update, Delete, Approve;
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

    }
}