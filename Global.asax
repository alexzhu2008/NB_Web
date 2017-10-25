<%@ Application Language="C#" %>

<%@ Import Namespace="NB_Web.Model" %>
<%@ Import Namespace="NB_Web.Common" %>

<script runat="server">


    UDP_Srv objUDPSrv = new UDP_Srv();
    UDP_Model obj_udp_model = new UDP_Model();

    // NB_Web.Model.UDP_Model.txt_Record_Log = "";
    void Application_Start(object sender, EventArgs e)
    {
        // 在应用程序启动时运行的代码
        UDP_Model.Udp_Listen_Res = "";
        //LogUtility.SetLogPath("C:\\log");
        /*
        string sTmp = objUDPSrv.UDP_Listen("", 11111);
        if (sTmp !="")
        {
            UDP_Model.Udp_Listen_Res = sTmp;
            LogUtility.WriteInfo("UDP_Listen failed: "+sTmp);
        }

        objUDPSrv.udp_msgboxHandler += UDP_OperateTextWhileGetMsg;
        */
        
    }

    protected void UDP_OperateTextWhileGetMsg(string Buf)
    {
        UDP_Model.txt_Record_Log_static += "UDP Recv from NB "+UDP_Model.UDP_Remote_IP+ UDP_Model.UDP_Remote_Port.ToString()+":"+ Buf+"\n";
        // txt_Record_Log += "UDP Recv from NB Station: " + Buf+"\n";
    }

    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码
        //objUDPSrv.UDP_Close();
    }

    void Application_Error(object sender, EventArgs e)
    {
        // 在出现未处理的错误时运行的代码
        //objUDPSrv.UDP_Close();
    }

    void Session_Start(object sender, EventArgs e)
    {
        // 在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
        // 或 SQLServer，则不引发该事件。
        //objUDPSrv.UDP_Close();
    }

</script>
