using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NB_Web.Common;
using NB_Web.Model;



public partial class config : System.Web.UI.Page
{

    UDP_Srv objUDPSrv = new UDP_Srv();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Btn_UdpClose_Click(object sender, EventArgs e)
    {
        objUDPSrv.UDP_Close();
    }
}