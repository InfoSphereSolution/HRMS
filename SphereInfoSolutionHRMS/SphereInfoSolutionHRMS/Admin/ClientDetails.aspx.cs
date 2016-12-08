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
                GetClientNames();
                BindgvHolidayList();
                BindgvTempHolidayList();
                DisplayState();
                DisplayCity();
                DisplayStateCity();
                DisplayMasterClient();
                Displaytemptable();
                //DataTable dt = new DataTable();
                //dt.Columns.AddRange(new DataColumn[5] { new DataColumn("ShiftName"), new DataColumn("StartTime"), new DataColumn("EndTime"), new DataColumn("Hours"), new DataColumn("Id") });
                //ViewState["CustomShift"] = dt;
                //this.bindgrid();

            }
        }
        //Bind client Name to dropdown
        private void GetClientNames()
        {
            Employee employee = new Employee();
            EmployeeModel employeeModel = new EmployeeModel();
            dt = employee.FetchClient();
            ddlClientName.DataSource = dt;
            ddlClientName.DataValueField = "ClientId";
            ddlClientName.DataTextField = "ClientName";
            ddlClientName.DataBind();
            ddlClientName.Items.Insert(0, new ListItem("Select Client", "0"));

        }

        //Get Holiday List and bind it to gridview
        private void BindgvHolidayList()
        {
            client = new Client();
            clientmodel = new ClientModel();
            DataTable dtHolidayList = client.GetHolidayList();
            if (dt.Rows.Count > 0)
            {
                gvHolidayList.DataSource = dtHolidayList;
                gvHolidayList.DataBind();
            }
            else
            {
                //No records found
            }
        }

        //Get Pending Holiday List and bind it to gridview
        private void BindgvTempHolidayList()
        {
            client = new Client();
            DataTable dtPendingHolidayList = client.GetPendingHolidayList();
            if (dt.Rows.Count > 0)
            {
                gvTempHolidayList.DataSource = dtPendingHolidayList;
                gvTempHolidayList.DataBind();
            }
            else
            {
                //No records found
            }
        }
        //Display Master Table Client
        protected void DisplayMasterClient()
        {
            dt = client.MasterClient();
            if (dt.Rows.Count > 0)
            {
                gvClientList.DataSource = dt;
                gvClientList.DataBind();
                gvClientList.Visible = true;
                lblMessageClientList.Text = "";
            }
            else
            {

                gvClientList.Visible = false;
                lblMessageClientList.Text = "No Roles Found";
                lblMessageClientList.ForeColor = System.Drawing.Color.Red;

            }
        }

        //Display Temp Table Client Details
        protected void Displaytemptable()
        {
            dt = client.TempClient();
            if (dt.Rows.Count > 0)
            {
                gvPendingClientList.DataSource = dt;
                gvPendingClientList.DataBind();
                gvPendingClientList.Visible = true;
                lblMessagePendingClientList.Text = "";
                btnApproveClient.Visible = true;
                btnRejectClient.Visible = true;
            }
            else
            {
                gvPendingClientList.Visible = false;
                lblMessagePendingClientList.Text = "No Record Found";
                btnApproveClient.Visible = false;
                btnRejectClient.Visible = false;

            }
        }

        //custom grid
        //protected void bindgrid()
        //{
        //    gvCustomShiftList.DataSource = ViewState["CustomShift"];
        //    gvCustomShiftList.DataBind();

        //}
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
        //Clear All Controls
        public void clearall()
        {
            txtClientName.Text = txtAddress.Text = txtStateName.Text = txtCityName.Text = txtPincode.Text = txtContact.Text = txtSite.Text = txtIP.Text = txtNoOfOptionalHolidays.Text = "";

            ddlState.SelectedIndex = ddlCity.SelectedIndex = ddlGeneralShift.SelectedIndex = -1;
            cbIsSaturdayWorking.Checked = false;
            cbFirstSaturday.Checked = cbSecondSaturday.Checked = cbThirdSaturday.Checked = cbFourthSaturday.Checked = cbFifthSaturday.Checked = false;
            cbFlexibleShift.Checked = cbSecondShift.Checked = cbNightShift.Checked = false;
            //txtCustomShiftName.Text = txtCustomShiftStart.Text = txtCustomShiftEnd.Text = txtCustomShiftHours.Text = "";
        }
        protected void cbIsSaturdayWorking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsSaturdayWorking.Checked)
            {
                pnlISSaturdayWorking.Visible = true;
                // cbFifthSaturday.Visible = true;
            }
            else
            {
                pnlISSaturdayWorking.Visible = false;


            }

        }
        protected void btnSaveClient_Click(object sender, EventArgs e)
        {
            bool Is_Sat = false;            
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
            clientmodel.pinCode = Convert.ToInt32(txtPincode.Text);
            clientmodel.WebSite = txtSite.Text;
            clientmodel.ContactNo = txtContact.Text;
            clientmodel.IP_Address = txtIP.Text;
            clientmodel.Is_Sat_Working = Is_Sat;
            clientmodel.AddedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            if (ddlGeneralShift.SelectedItem.Text == "General-1")
            {
                clientmodel.General_Shift1 = 1;
            }
            else
            {

                clientmodel.General_Shift1 = 0;


            }
            if (ddlGeneralShift.SelectedItem.Text == "General-2")
            {

                clientmodel.General_Shift2 = 1;
            }
            else
            {
                clientmodel.General_Shift2 = 0;


            }
            if (ddlGeneralShift.SelectedItem.Text == "General-3")
            {
                clientmodel.General_Shift3 = 1;
            }
            else
            {
                clientmodel.General_Shift3 = 0;


            }

            if (cbSecondShift.Checked)
            {
                clientmodel.SecondShift = 1;
            }
            else
            {
                clientmodel.SecondShift = 0;
            }
            if (cbNightShift.Checked)
            {
                clientmodel.NightShift = 1;
            }
            else
            {
                clientmodel.NightShift = 0;

            }
            if (cbFlexibleShift.Checked)
            {

                clientmodel.FlexibleTime = 1;
            }
            else
            {
                clientmodel.FlexibleTime = 0;
            }
            //if (cbCustomShift.Checked)
            //{

            //    clientmodel.IsCustomShift = true;
            //}
            //else
            //{

            //    clientmodel.IsCustomShift = false;
            //}

            if (cbIsSaturdayWorking.Checked)
            {
                // bool sat1 = false, sat2 = false, sat3 = false, sat4 = false, sat5 = false;
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
                btnupdate.Visible = false;

            }

            clientmodel.Operation = 1;
            clientmodel.optionalholiday = Convert.ToInt32(txtNoOfOptionalHolidays.Text);
            //Returns success value
            //  int i = client.AddClient(clientmodel, ViewState["CustomShift"] as DataTable);
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
            else if (i == 5)
            {
                lblstatus.Text = "Client Updated.. Wating for approval.";
                lblstatus.ForeColor = System.Drawing.Color.Red;
            }
            else if (i == -5)
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
            DisplayMasterClient();
            Displaytemptable();
            clearall();
        }
        ////Add Cutom Shift
        ////protected void btnAddCustomShift_Click(object sender, EventArgs e)
        ////{
        ////    DataTable dtcustomshift = (DataTable)ViewState["CustomShift"];
        ////    dtcustomshift.Rows.Add(txtCustomShiftName.Text, txtCustomShiftStart.Text, txtCustomShiftEnd.Text, txtCustomShiftHours.Text);
        ////    ViewState["CustomShift"] = dtcustomshift;

        ////   // dtcustomshift.Columns.Add("custom");
        ////    this.bindgrid();
        ////    clearall();

        ////}

        ////delete product from cart
        //protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.RowIndex);
        //    DataTable dt = ViewState["CustomShift"] as DataTable;
        //    dt.Rows[index].Delete();
        //    ViewState["CustomShift"] = dt;
        //    bindgrid();

        //}

        protected void btnRejectClient_Click(object sender, EventArgs e)
        {
            rejectrequest(2);
            DisplayMasterClient();
            Displaytemptable();
            lblMessagePendingClientList.Text = "";

        }

        protected void rejectrequest(int operation)
        {
            foreach (GridViewRow gvrow in gvPendingClientList.Rows)
            {
                CheckBox chkdApprove = (CheckBox)gvrow.FindControl("chkboxSelectClientList");
                clientmodel.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                clientmodel.Operation = operation;
                if (chkdApprove.Checked)
                {
                    clientmodel.tempclientId = Convert.ToInt32(gvPendingClientList.DataKeys[gvrow.RowIndex].Value);
                    clientmodel.ClientId = Convert.ToInt32(gvPendingClientList.Rows[gvrow.RowIndex].Cells[2].Text);

                    int i = client.UpdateTempClient(clientmodel);
                    if (i == 1)
                    {
                        lblMessagePendingClientList.Text = "Selected Client Approved Succesfully";
                        lblMessagePendingClientList.ForeColor = System.Drawing.Color.Green;

                    }
                    else if (i == 2)
                    {
                        lblMessagePendingClientList.Text = "Selected Client Remove Succesfully";
                        lblMessagePendingClientList.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        lblMessagePendingClientList.Text = "Error";
                        lblMessagePendingClientList.ForeColor = System.Drawing.Color.Red;


                    }
                }
            }

            DisplayMasterClient();
            Displaytemptable();
        }

        protected void gvClientList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Remove")
            {

                int UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                clientmodel.ClientId = index;
                clientmodel.UpdatedBy = UserId;
                int i = client.RemoveClient(clientmodel);
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
                else if (i == 5)
                {
                    lblstatus.Text = "Client Updated.. Wating for approval.";
                    lblstatus.ForeColor = System.Drawing.Color.Red;
                }
                else if (i == -5)
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

            if (e.CommandName == "change")
            {
                Retrivedata(index);
                btnupdate.Visible = true;
                btnSaveClient.Visible = false;

            }

            if (e.CommandName == "View")
            {

                Retrivedata(index);
                btnupdate.Visible = false;
                btnSaveClient.Visible = false;
            }

            DisplayMasterClient();
            Displaytemptable();
        }

        public void Retrivedata(int index)
        {

            GridViewRow gr = gvClientList.Rows[index];

            int id = Convert.ToInt32(gr.Cells[0].Text);
            ViewState["ClientId"] = id;
            txtClientName.Text = gr.Cells[1].Text;
            txtAddress.Text = gr.Cells[4].Text;
            //ddlState.SelectedItem.Text = gr.Cells[2].Text;
            ddlState.SelectedValue = ddlState.Items.FindByText(gr.Cells[2].Text).Value;
            //ddlCity.SelectedItem.Text = gr.Cells[3].Text;
            ddlCity.SelectedValue = ddlCity.Items.FindByText(gr.Cells[3].Text).Value;
            txtPincode.Text = gr.Cells[5].Text;
            txtContact.Text = gr.Cells[7].Text;
            txtSite.Text = gr.Cells[6].Text;
            txtIP.Text = gr.Cells[8].Text;
            bool abc = Convert.ToBoolean(gr.Cells[9].Text);
            if (abc == true)
            {
                cbIsSaturdayWorking.Checked = true;
                pnlISSaturdayWorking.Visible = true;
            }
            else
            {
                cbIsSaturdayWorking.Checked = false;
                pnlISSaturdayWorking.Visible = false;
            }
            dt = client.RetriveSaturdayWorking(id);
            bool IsSat_1 = false, IsSat_2 = false, IsSat_3 = false, IsSat_4 = false, IsSat_5 = false;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IsSat_1 = Convert.ToBoolean(dt.Rows[i]["IsSat_1"].ToString());
                    IsSat_2 = Convert.ToBoolean(dt.Rows[i]["IsSat_2"].ToString());
                    IsSat_3 = Convert.ToBoolean(dt.Rows[i]["IsSat_3"].ToString());
                    IsSat_4 = Convert.ToBoolean(dt.Rows[i]["IsSat_4"].ToString());
                    IsSat_5 = Convert.ToBoolean(dt.Rows[i]["IsSat_5"].ToString());
                }
                if (IsSat_1 == true)
                {
                    cbFirstSaturday.Checked = true;

                }
                else
                {
                    cbFirstSaturday.Checked = false;
                }
                if (IsSat_2 == true)
                {
                    cbSecondSaturday.Checked = true;

                }
                else
                {
                    cbSecondSaturday.Checked = false;
                }
                if (IsSat_3 == true)
                {
                    cbThirdSaturday.Checked = true;

                }
                else
                {
                    cbThirdSaturday.Checked = false;
                }
                if (IsSat_4 == true)
                {
                    cbFourthSaturday.Checked = true;

                }
                else
                {
                    cbFourthSaturday.Checked = false;
                }
                if (IsSat_5 == true)
                {

                    cbFifthSaturday.Checked = true;
                }
                else
                {
                    cbFifthSaturday.Checked = false;
                }
            }


            dt = client.retrieveshift(id);
            //ddlGeneralShift.DataTextField = "ShiftsName";
            //ddlGeneralShift.DataValueField = "ShiftId";         
            //ddlGeneralShift.DataSource = dt;
            // ddlGeneralShift.DataBind();
            int GeneralshiftID = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GeneralshiftID = Convert.ToInt32(dt.Rows[i]["ShiftId"].ToString());
                }
                if (GeneralshiftID == 1)
                {
                    ddlGeneralShift.SelectedValue = GeneralshiftID.ToString();
                }
                else if (GeneralshiftID == 2)
                {
                    ddlGeneralShift.SelectedValue = GeneralshiftID.ToString();
                }
                else
                {
                    ddlGeneralShift.SelectedValue = GeneralshiftID.ToString();
                }
            }

            int secondshift = Convert.ToInt32(gr.Cells[14].Text);
            if (secondshift == 1)
            {
                cbSecondShift.Checked = true;

            }
            else
            {
                cbSecondShift.Checked = false;



            }
            int nightshift = Convert.ToInt32(gr.Cells[15].Text);

            if (nightshift == 1)
            {
                cbNightShift.Checked = true;

            }
            else
            {
                cbNightShift.Checked = false;



            }
            //bool custom = Convert.ToBoolean(gr.Cells[16].Text);


            //if (custom==true)
            //{
            //    cbCustomShift.Checked = true;
            //    //gvGetCustomShiftDetail.Visible = true;
            //    GetCustumShiftDetail(id);

            //}
            //else
            //{
            //    cbCustomShift.Checked = false;



            //}
            int flexibleshift = Convert.ToInt32(gr.Cells[17].Text);

            if (flexibleshift == 1)
            {
                cbFlexibleShift.Checked = true;

            }
            else
            {
                cbFlexibleShift.Checked = false;

            }

            txtNoOfOptionalHolidays.Text = gr.Cells[20].Text;

        }

        //Retrieve Custom Shift
        //private void GetCustumShiftDetail(int ClientId)
        //{
        //    dt = client.RetriveCustomShiftDetails(ClientId);
        //    if (dt.Rows.Count > 0)
        //    {
        //        pnlCustomShift.Visible = true;
        //        gvGetCustomShiftDetail.Visible = true;
        //        gvGetCustomShiftDetail.DataSource = dt;
        //        gvGetCustomShiftDetail.DataBind();
        //    }
        //    else
        //    {
        //        //no records found
        //        pnlCustomShift.Visible = false;
        //        gvGetCustomShiftDetail.Visible = false;
        //    }
        //}




        protected void gvClientList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int IsActive = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IsActive"));
                Button btnRemoveClient = (Button)e.Row.FindControl("btnRemoveClient");
                Button btnEditClient = (Button)e.Row.FindControl("btnEditClient");
                Button btnViewClient = (Button)e.Row.FindControl("btnViewClient");

                if (IsActive == 0)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5D8DC"); ;
                    btnRemoveClient.Enabled = false;
                    btnEditClient.Enabled = false;
                    btnViewClient.Enabled = false;
                    btnRemoveClient.CssClass = "btn btn-danger btn-xs disabled";

                }
                else if (IsActive == 1)
                {
                    //e.Row.BackColor = System.Drawing.Color.Honeydew;
                    btnRemoveClient.Visible = true;
                    btnRemoveClient.Enabled = true;

                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Blue;
                }
            }
        }
        protected void btnApproveClient_Click(Object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                rejectrequest(1);
                DisplayMasterClient();
                Displaytemptable();
                lblMessagePendingClientList.Text = "";
            }
        }

        //Display Custom Shift Panel
        //protected void cbCustomShift_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cbCustomShift.Checked)
        //    {
        //        pnlCustomShift.Visible = true;
        //        // cbFifthSaturday.Visible = true;
        //    }
        //    else
        //    {
        //        pnlCustomShift.Visible = false;


        //    }
        //}



        //Update Master Table Record
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            bool Is_Sat = false;            
            if (cbIsSaturdayWorking.Checked)
            {
                Is_Sat = true;

            }
            else
            {
                Is_Sat = false;

            }
            clientmodel.ClientId = Convert.ToInt32(ViewState["ClientId"].ToString());
            clientmodel.ClientName = txtClientName.Text;
            clientmodel.ClientAddress = txtAddress.Text;
            clientmodel.StateId = Convert.ToInt32(ddlState.SelectedValue);
            clientmodel.CityId = Convert.ToInt32(ddlCity.SelectedValue);
            clientmodel.ClientAddress = txtAddress.Text;
            clientmodel.pinCode = Convert.ToInt32(txtPincode.Text);
            clientmodel.WebSite = txtSite.Text;
            clientmodel.ContactNo = txtContact.Text;
            clientmodel.IP_Address = txtIP.Text;
            clientmodel.Is_Sat_Working = Is_Sat;
            clientmodel.optionalholiday = Convert.ToInt32(txtNoOfOptionalHolidays.Text);
            clientmodel.AddedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            clientmodel.Operation = 2;
            if (ddlGeneralShift.SelectedItem.Text == "General-1")
            {
                clientmodel.General_Shift1 = 1;
            }
            else
            {

                clientmodel.General_Shift1 = 0;


            }
            if (ddlGeneralShift.SelectedItem.Text == "General-2")
            {

                clientmodel.General_Shift2 = 1;
            }
            else
            {
                clientmodel.General_Shift2 = 0;


            }
            if (ddlGeneralShift.SelectedItem.Text == "General-3")
            {
                clientmodel.General_Shift3 = 1;
            }
            else
            {
                clientmodel.General_Shift3 = 0;


            }

            if (cbSecondShift.Checked)
            {
                clientmodel.SecondShift = 1;
            }
            else
            {
                clientmodel.SecondShift = 0;
            }
            if (cbNightShift.Checked)
            {
                clientmodel.NightShift = 1;
            }
            else
            {
                clientmodel.NightShift = 0;

            }
            if (cbFlexibleShift.Checked)
            {

                clientmodel.FlexibleTime = 1;
            }
            else
            {
                clientmodel.FlexibleTime = 0;
            }
            //if (cbCustomShift.Checked)
            //{

            //    clientmodel.IsCustomShift = true;
            //}
            //else
            //{

            //    clientmodel.IsCustomShift = false;
            //}

            if (cbIsSaturdayWorking.Checked)
            {
                // bool sat1 = false, sat2 = false, sat3 = false, sat4 = false, sat5 = false;
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
            }
            //Returns success value
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
            else if (i == 5)
            {
                lblstatus.Text = "Client Updated.. Wating for approval.";
                lblstatus.ForeColor = System.Drawing.Color.Red;
            }
            else if (i == -5)
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
            DisplayMasterClient();
            Displaytemptable();
            clearall();
        }



        protected void gvPendingClientList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            GridViewRow row = gvPendingClientList.SelectedRow;
            int id = Convert.ToInt32(row.Cells[0].Text);
            txtClientName.Text = row.Cells[1].Text;
            txtAddress.Text = row.Cells[4].Text;
            //ddlState.SelectedItem.Text = gr.Cells[2].Text;
            ddlState.SelectedValue = ddlState.Items.FindByText(row.Cells[2].Text).Value;
            //ddlCity.SelectedItem.Text = gr.Cells[3].Text;
            ddlCity.SelectedValue = ddlCity.Items.FindByText(row.Cells[3].Text).Value;
            txtPincode.Text = row.Cells[5].Text;
            txtContact.Text = row.Cells[7].Text;
            txtSite.Text = row.Cells[6].Text;
            txtIP.Text = row.Cells[8].Text;
            bool abc = Convert.ToBoolean(row.Cells[9].Text);
            if (abc == true)
            {
                cbIsSaturdayWorking.Checked = true;
                pnlISSaturdayWorking.Visible = true;
            }
            else
            {
                cbIsSaturdayWorking.Checked = false;
                pnlISSaturdayWorking.Visible = false;
            }
            dt = client.RetriveSaturdayWorking(id);
            bool IsSat_1 = false, IsSat_2 = false, IsSat_3 = false, IsSat_4 = false, IsSat_5 = false;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IsSat_1 = Convert.ToBoolean(dt.Rows[i]["IsSat_1"].ToString());
                    IsSat_2 = Convert.ToBoolean(dt.Rows[i]["IsSat_2"].ToString());
                    IsSat_3 = Convert.ToBoolean(dt.Rows[i]["IsSat_3"].ToString());
                    IsSat_4 = Convert.ToBoolean(dt.Rows[i]["IsSat_4"].ToString());
                    IsSat_5 = Convert.ToBoolean(dt.Rows[i]["IsSat_5"].ToString());
                }
                if (IsSat_1 == true)
                {
                    cbFirstSaturday.Checked = true;

                }
                else
                {
                    cbFirstSaturday.Checked = false;
                }
                if (IsSat_2 == true)
                {
                    cbSecondSaturday.Checked = true;

                }
                else
                {
                    cbSecondSaturday.Checked = false;
                }
                if (IsSat_3 == true)
                {
                    cbThirdSaturday.Checked = true;

                }
                else
                {
                    cbThirdSaturday.Checked = false;
                }
                if (IsSat_4 == true)
                {
                    cbFourthSaturday.Checked = true;

                }
                else
                {
                    cbFourthSaturday.Checked = false;
                }
                if (IsSat_5 == true)
                {

                    cbFifthSaturday.Checked = true;
                }
                else
                {
                    cbFifthSaturday.Checked = false;
                }
            }
        }


        //Add holiday by clients
        protected void btnAddHoliday_Click(object sender, EventArgs e)
        {
            int Result = 0;
            string Action = Convert.ToString(((Button)sender).CommandArgument);
            if (Action == "Save")
            {
                //Add new Holiday
                Result = client.UpdateHolidayDetails(GetHolidayAttributes(), 1);
            }
            else
            {
                //Update Holiday
                Result = client.UpdateHolidayDetails(GetHolidayAttributes(), 2);
            }
            
            DisplayResultMessage(Result);
            
        }
        //Display Alert msgs for holidays
        private void DisplayResultMessage(int Result)
        {
            BindgvHolidayList();
            BindgvTempHolidayList();
            ClearHolidayControls();
            if (Result == -1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('This Holiday Exists for this client.');", true);
            }
            else if (Result == -2)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('This Holiday Exists for this client waiting for approval.');", true);
            }
            else if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Holiday Added Succesfully.');", true);
            }
            else if (Result == 2)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Holiday Added waiting for approval.');", true);
            }
            else if (Result == 3)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Holiday Updated Successfully.');", true);
            }
            else if (Result == -3)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Request for update of this Holiday Already exists for this client.');", true);
            }
            else if (Result == 4)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Holiday Updated waiting for approval.');", true);
            }
            else if (Result == 5)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Holiday Deleted Successfully.');", true);
            }
            else if (Result == -5)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Request for Delete holiday already exists for this client');", true);
            }
            else if (Result == 6)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Holidaty Deleted waiting for approval');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error.');", true);
            }
        }

        //Add Holiday values to model
        public ClientModel GetHolidayAttributes()
        {
            clientmodel = new ClientModel();
            if (ViewState["ClientHolidayId"] != null)
            {
                clientmodel.Client_HolidayId = Convert.ToInt32(ViewState["ClientHolidayId"].ToString());
            }
            clientmodel.HolidayName = txtHolidayname.Text;
            clientmodel.HolidayOn = Convert.ToDateTime(txtHolidayDate.Text);
            clientmodel.ClientId = Convert.ToInt32(ddlClientName.SelectedValue.ToString());
            clientmodel.IsOptional = Convert.ToBoolean(Convert.ToInt32(ddlIsOptional.SelectedValue));
            clientmodel.AddedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            return clientmodel;
        }
        //Clear Holiday Controls
        private void ClearHolidayControls()
        {
            ddlClientName.Enabled = true;
            txtHolidayname.Text = "";
            txtHolidayDate.Text = "";
            ddlClientName.SelectedIndex = -1;
            ddlIsOptional.SelectedIndex = -1;
            btnAddHoliday.Text = "Save";
            btnAddHoliday.CommandArgument = "Save";
        }

        protected void gvHolidayList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Change")
            {
                //set button command and text
                btnAddHoliday.Text = "Update";
                btnAddHoliday.CommandArgument = "Update";
                ddlClientName.Enabled = false;
                BindHolidaysToControls(index);
                

            }
            else
            {
                try
                {
                    //Remove data
                    client = new Client();
                    clientmodel = new ClientModel();
                    clientmodel.Client_HolidayId = index;
                    clientmodel.AddedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    int Result = client.UpdateHolidayDetails(clientmodel, 3);
                    DisplayResultMessage(Result);
                }
                catch (Exception)
                {
                }

            }
        }

        //Binding selected data to controls (Holiday details)
        private void BindHolidaysToControls(int index)
        {
            try
            {
                int Cl_HolidayId = 0,clientId=0;
                String HolidayName = "", ClientName = "", HolidayOn = "";
                bool IsOptional = false;

                GridViewRow gvrow = gvHolidayList.Rows[index];
                Cl_HolidayId = Convert.ToInt32(gvrow.Cells[0].Text);
                ViewState["ClientHolidayId"] = Cl_HolidayId;
                HolidayName = gvrow.Cells[1].Text;
                clientId = Convert.ToInt32(gvrow.Cells[2].Text);
                ClientName = gvrow.Cells[3].Text;
                HolidayOn = gvrow.Cells[4].Text;
                IsOptional = Convert.ToBoolean(gvrow.Cells[5].Text);
                //Set the values to controls
                txtHolidayname.Text = HolidayName;
                txtHolidayDate.Text = HolidayOn;
                ddlClientName.ClearSelection();
                ddlClientName.SelectedValue = ddlClientName.Items.FindByValue(clientId.ToString()).Value;
                ddlIsOptional.ClearSelection();
               // ddlClientName.Items.FindByValue(Convert.ToInt32(Convert.ToString(ClientName)).ToString()).Selected = true ;
                ddlIsOptional.Items.FindByValue(Convert.ToInt32(Convert.ToBoolean(IsOptional)).ToString()).Selected = true;
            }
            catch(Exception)
            {
            }
        }

        protected void btnApproveHoliday_Click(object sender, EventArgs e)
        {
            ApproveOrRejectHoliday(1);
            BindgvHolidayList();
            BindgvTempHolidayList();
        }
        protected void btnRejectHoliday_Click(object sender, EventArgs e)
        {
            ApproveOrRejectHoliday(2);
            BindgvHolidayList();
            BindgvTempHolidayList();
        }
        //Approve or Reject Holiday
        private void ApproveOrRejectHoliday(int Operation)
        {
            foreach (GridViewRow gvrow in gvTempHolidayList.Rows)
            {
                CheckBox chkdApprove = (CheckBox)gvrow.FindControl("chkboxSelectHoliday");
                clientmodel.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                clientmodel.Operation = Operation;
                if (chkdApprove.Checked)
                {
                    clientmodel.PendingClient_HolidayId = Convert.ToInt32(gvTempHolidayList.DataKeys[gvrow.RowIndex].Value);

                    int i = client.ApproveOrRejectHoliday(clientmodel);

                    if (Operation == 1)
                    {
                        if (i == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Approved.');", true);
                            //lblMessageTempDesignation.Text = "Selected Roles Approved Succesfully";
                            //lblMessageTempDesignation.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error.');", true);
                            //lblMessageTempDesignation.Text = "Error";
                            //lblMessageTempDesignation.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        if (i == 2)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Rejected.');", true);
                            //lblMessageTempDesignation.Text = "Selected Roles Removed Succesfully";
                            //lblMessageTempDesignation.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error.');", true);
                            //lblMessageTempDesignation.Text = "Error";
                            //lblMessageTempDesignation.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }

       




        //protected void gvPendingClientList_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int Index = int.Parse(e.CommandArgument.ToString());
        //  if (e.CommandName == "ViewPendingClient")
        //    {
        //        viewPendingclient(tempid);
        //        btnupdate.Visible = false;
        //        btnSaveClient.Visible = false;

        //    }
        //}

        //protected void viewPendingclient(int Index)
        //{
        //    GridViewRow gvr = gvPendingClientList.Rows[Index];
        //    int id = Convert.ToInt32(gvr.Cells[0].Text);

        //    txtClientName.Text = gvr.Cells[1].Text;
        //    txtAddress.Text = gvr.Cells[4].Text;
        //    //ddlState.SelectedItem.Text = gr.Cells[2].Text;
        //    ddlState.SelectedValue = ddlState.Items.FindByText(gvr.Cells[2].Text).Value;
        //    //ddlCity.SelectedItem.Text = gr.Cells[3].Text;
        //    ddlCity.SelectedValue = ddlCity.Items.FindByText(gvr.Cells[3].Text).Value;
        //    txtPincode.Text = gvr.Cells[5].Text;
        //    txtContact.Text = gvr.Cells[7].Text;
        //    txtSite.Text = gvr.Cells[6].Text;
        //    txtIP.Text = gvr.Cells[8].Text;
        //    bool abc = Convert.ToBoolean(gvr.Cells[9].Text);
        //    if (abc == true)
        //    {
        //        cbIsSaturdayWorking.Checked = true;
        //        pnlISSaturdayWorking.Visible = true;
        //    }
        //    else
        //    {
        //        cbIsSaturdayWorking.Checked = false;
        //        pnlISSaturdayWorking.Visible = false;
        //    }
        //    dt = client.RetriveSaturdayWorking(id);
        //    bool IsSat_1 = false, IsSat_2 = false, IsSat_3 = false, IsSat_4 = false, IsSat_5 = false;
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            IsSat_1 = Convert.ToBoolean(dt.Rows[i]["IsSat_1"].ToString());
        //            IsSat_2 = Convert.ToBoolean(dt.Rows[i]["IsSat_2"].ToString());
        //            IsSat_3 = Convert.ToBoolean(dt.Rows[i]["IsSat_3"].ToString());
        //            IsSat_4 = Convert.ToBoolean(dt.Rows[i]["IsSat_4"].ToString());
        //            IsSat_5 = Convert.ToBoolean(dt.Rows[i]["IsSat_5"].ToString());
        //        }
        //        if (IsSat_1 == true)
        //        {
        //            cbFirstSaturday.Checked = true;

        //        }
        //        else
        //        {
        //            cbFirstSaturday.Checked = false;
        //        }
        //        if (IsSat_2 == true)
        //        {
        //            cbSecondSaturday.Checked = true;

        //        }
        //        else
        //        {
        //            cbSecondSaturday.Checked = false;
        //        }
        //        if (IsSat_3 == true)
        //        {
        //            cbThirdSaturday.Checked = true;

        //        }
        //        else
        //        {
        //            cbThirdSaturday.Checked = false;
        //        }
        //        if (IsSat_4 == true)
        //        {
        //            cbFourthSaturday.Checked = true;

        //        }
        //        else
        //        {
        //            cbFourthSaturday.Checked = false;
        //        }
        //        if (IsSat_5 == true)
        //        {

        //            cbFifthSaturday.Checked = true;
        //        }
        //        else
        //        {
        //            cbFifthSaturday.Checked = false;
        //        }
        //    }
        //    txtNoOfOptionalHolidays.Text = gvr.Cells[10].Text;

        //    dt = client.retrieveshift(id);
        //    //ddlGeneralShift.DataTextField = "ShiftsName";
        //    //ddlGeneralShift.DataValueField = "ShiftId";         
        //    //ddlGeneralShift.DataSource = dt;
        //    // ddlGeneralShift.DataBind();
        //    int GeneralshiftID = 0;
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            GeneralshiftID = Convert.ToInt32(dt.Rows[i]["ShiftId"].ToString());
        //        }
        //        if (GeneralshiftID == 1)
        //        {
        //            ddlGeneralShift.SelectedValue = GeneralshiftID.ToString();
        //        }
        //        else if (GeneralshiftID == 2)
        //        {
        //            ddlGeneralShift.SelectedValue = GeneralshiftID.ToString();
        //        }
        //        else
        //        {
        //            ddlGeneralShift.SelectedValue = GeneralshiftID.ToString();
        //        }
        //    }

        //    int secondshift = Convert.ToInt32(gvr.Cells[14].Text);
        //    if (secondshift == 1)
        //    {
        //        cbSecondShift.Checked = true;

        //    }
        //    else
        //    {
        //        cbSecondShift.Checked = false;



        //    }
        //    int nightshift = Convert.ToInt32(gvr.Cells[15].Text);

        //    if (nightshift == 1)
        //    {
        //        cbNightShift.Checked = true;

        //    }
        //    else
        //    {
        //        cbNightShift.Checked = false;



        //    }
        //    //bool custom = Convert.ToBoolean(gr.Cells[16].Text);


        //    //if (custom==true)
        //    //{
        //    //    cbCustomShift.Checked = true;
        //    //    //gvGetCustomShiftDetail.Visible = true;
        //    //    GetCustumShiftDetail(id);

        //    //}
        //    //else
        //    //{
        //    //    cbCustomShift.Checked = false;



        //    //}
        //    int flexibleshift = Convert.ToInt32(gvr.Cells[17].Text);

        //    if (flexibleshift == 1)
        //    {
        //        cbFlexibleShift.Checked = true;

        //    }
        //    else
        //    {
        //        cbFlexibleShift.Checked = false;

        //    }
        //}




    }
}