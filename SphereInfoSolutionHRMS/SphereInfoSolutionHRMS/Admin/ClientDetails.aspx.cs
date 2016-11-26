using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Models;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
namespace SphereInfoSolutionHRMS.Admin
{


    public partial class ClientDetails : System.Web.UI.Page
    {
        BAL.Client client = new Client();
        Models.ClientModel clientmodel = new ClientModel();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DisplayState();
                DisplayCity();
                DisplayStateCity();

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("ClientId"), new DataColumn("ShipName"), new DataColumn("StartTime"), new DataColumn("EndTime"), new DataColumn("Hours") });
                ViewState["CustomShift"] = dt;
                this.bindgrid();
            }
        }

        //custom grid
        protected void bindgrid()
        {
            gvCustomShiftList.DataSource = ViewState["CustomShift"];
            gvCustomShiftList.DataBind();

        }
        //Display State
        public void DisplayState()
        {
            dt = client.State();
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "State_Id";
            ddlState.DataSource = dt;
            ddlState.DataBind();
            ddlState.Items.Insert(0, "--Select State--");
        }
        //Display City
        public void DisplayCity()
        {
            dt = client.City();
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "City_Id";
            ddlCity.DataSource = dt;
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, "--Select City--");

        }
        //Display City
        public void DisplayStateCity()
        {
            dt = client.State();

            ddlCityState.DataTextField = "StateName";
            ddlCityState.DataValueField = "State_Id";
            ddlCityState.DataSource = dt;
            ddlCityState.DataBind();
            ddlCityState.Items.Insert(0, "--Select State--");

        }
        protected void btnAddNewState_Click(object sender, EventArgs e)
        {
            clientmodel.StateName = txtStateName.Text;
            int i = client.AddState(clientmodel);
            if (i == -1)
            {

                lblState.Text = "State Already Added";
                lblState.ForeColor = System.Drawing.Color.Red;
            }
            else if (i == 1)
            {

                lblState.Text = "State Added Succesfully";
                lblState.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lblState.Text = "Error";
                lblState.ForeColor = System.Drawing.Color.Red;

            }
            DisplayState();
            clearall();
        }

        protected void btnAddNewCity_Click(object sender, EventArgs e)
        {
            clientmodel.CityName = txtCityName.Text;
            clientmodel.StateId = Convert.ToInt32(ddlCityState.SelectedValue);
            int i = client.AddCity(clientmodel);
            if (i == -1)
            {
                lblCity.Text = "City Already Added";
                lblCity.ForeColor = System.Drawing.Color.Red;
            }
            else if (i == 1)
            {

                lblCity.Text = "City Added Succesfully";
                lblCity.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lblCity.Text = "Error";
                lblCity.ForeColor = System.Drawing.Color.Red;
            }
            DisplayCity();
            clearall();

        }
        public void clearall()
        {
            txtStateName.Text = "";

            txtCityName.Text = "";
            ddlCityState.SelectedIndex = -1;
        }

        protected void btnSaveClient_Click(object sender, EventArgs e)
        {
            bool Is_Sat = false;
            bool custom = false;
            if (cbIsSaturdayWorking.Checked)
            {
                Is_Sat = true;

            }
            else
            {
                Is_Sat = false;

            }

            clientmodel.ClientName = txtClientName.Text;
            clientmodel.ClientAddress = txtAddress.Text;
            clientmodel.StateId = Convert.ToInt32(ddlState.SelectedValue);
            clientmodel.CityId = Convert.ToInt32(ddlCity.SelectedValue);
            clientmodel.ClientAddress = txtAddress.Text;
            clientmodel.pinCode =Convert.ToInt32(txtPincode.Text);
            clientmodel.WebSite = txtSite.Text;
            clientmodel.ContactNo = txtContact.Text;
            clientmodel.IP_Address = txtIP.Text;
            clientmodel.Is_Sat_Working = Is_Sat;
            clientmodel.AddedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            if (ddlGeneralShift.SelectedItem.Text == "General-1")
            {
                clientmodel.General_Shift1 = 1;
            }
            else if (ddlGeneralShift.SelectedItem.Text == "General-2")
            {

                clientmodel.General_Shift2 = 1;
            }
            else if (ddlGeneralShift.SelectedItem.Text == "General-3")
            {
                clientmodel.General_Shift3 = 1;
            }


            if (cbSecondShift.Checked)
            {
                clientmodel.SecondShift = 1;
            }
            if (cbNightShift.Checked)
            {
                clientmodel.NightShift = 1;
            }
            if (cbFlexibleShift.Checked)
            {

                clientmodel.FlexibleTime = 1;
            }
            if (cbCustomShift.Checked)
            {

                clientmodel.IsCustomShift = true;
            }
            

            if (cbIsSaturdayWorking.Checked)
            {
                bool sat1 = false, sat2 = false, sat3 = false, sat4 = false, sat5 = false;
                if (cbFirstSaturday.Checked)
                {
                    clientmodel.sat1 = true;

                }
                else
                {
                    clientmodel.sat1 = false;
                }
                if (cbSecondSaturday.Checked)
                {
                    clientmodel.sat2 = true;

                }
                else
                {
                    clientmodel.sat2 = false;
                }
                if (cbThirdSaturday.Checked)
                {
                    clientmodel.sat3 = true;

                }
                else
                {
                    clientmodel.sat3 = false;
                }
                if (cbFourthSaturday.Checked)
                {

                    clientmodel.sat4 = true;
                }
                else
                {

                    clientmodel.sat4 = false;
                }
                if (cbFifthSaturday.Checked)
                {
                    clientmodel.sat5 = true;
                }
                else
                {

                    clientmodel.sat5 = false;
                }
                int i = client.AddClient(clientmodel);
                if (i == -1)
                {
                    lblstatus.Text = "Client Already Added";

                    lblstatus.ForeColor = System.Drawing.Color.Red;
                }
                else if (i == -2)
                {
                    lblstatus.Text = "Client Name is already exists..Waiting for approval";
                    lblstatus.ForeColor = System.Drawing.Color.Green;
                }
                else if (i == 1)
                {
                    lblstatus.Text = "Client Added Succesfully";
                    lblstatus.ForeColor = System.Drawing.Color.Green;
                }
                else if (i == 2)
                {
                    lblstatus.Text = "Client Updated Succesfully";
                    lblstatus.ForeColor = System.Drawing.Color.Green;
                }
                else if (i == 3)
                {

                    lblstatus.Text = "Client Deleted Succesfully";
                    lblstatus.ForeColor = System.Drawing.Color.Green;
                }
                else if (i == 4)
                {

                    lblstatus.Text = "Client Added.. Wating for approval.";
                    lblstatus.ForeColor = System.Drawing.Color.Red;
                }
                else if(i==5)
                {
                    lblstatus.Text = "Client Updated.. Wating for approval.";
                    lblstatus.ForeColor = System.Drawing.Color.Red;
                }
                else if(i==-5)
                {
                    lblstatus.Text = "Request already exists.. Wating for approval.";
                    lblstatus.ForeColor = System.Drawing.Color.Red;
                }
                else if (i == 6)
                {
                    lblstatus.Text = "Deleted .. Wating for approval.";
                    lblstatus.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblstatus.Text = "Error";
                    lblstatus.ForeColor = System.Drawing.Color.Red;
                }


            }

            }
             protected void btnAddCustomShift_Click(object sender ,EventArgs e)
             {
                 DataTable dt = (DataTable)ViewState["CustomShift"];
                 dt.Rows.Add(txtCustomShiftName.Text, txtCustomShiftStart.Text, txtCustomShiftEnd.Text, txtCustomShiftHours.Text);
                 ViewState["CustomShift"] = dt;
                 this.bindgrid();
                 txtCustomShiftEnd.Text = "";
                 txtCustomShiftStart.Text = "";
                 txtCustomShiftName.Text = "";
                 txtCustomShiftHours.Text = "";

             }
           
  

      

    }
}