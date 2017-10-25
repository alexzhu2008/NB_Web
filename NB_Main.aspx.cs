using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using NB_Web.Common;
using NB_Web.Model;

public partial class NB_Main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UDP_Model.Udp_Listen_Res !="")
        {
            txt_Record.Text += "Udp socket init or listen failed: " + UDP_Model.Udp_Listen_Res + "\n";
            UDP_Model.Udp_Listen_Res = "";
        }
    }

    public UDP_Srv objUDPSrv = new UDP_Srv();
    UDP_Model obj_udp_model = new UDP_Model();

    //UDP_Model.txt_Record_Log = "";

    protected void Btn_UdpListen_Click(object sender, EventArgs e)
    {
        LogUtility.WriteInfo("btn listen clicked");
        Log.WriteLog("btn listen clicked");
        int iPort = 11111;

        txt_Record.Text += "准备监听UPD端口"+ iPort.ToString()+"\n";

        UDP_Model.Udp_Listen_Res = "";
        string sTmp = objUDPSrv.UDP_Listen("", 11111);
        if (sTmp != "")
        {
            UDP_Model.Udp_Listen_Res = sTmp;
            LogUtility.WriteInfo("UDP_Listen failed: " + sTmp);
        }

        objUDPSrv.udp_msgboxHandler += UDP_OperateTextWhileGetMsg;
        

    }

    protected void Btn_UdpSend_Click(object sender, EventArgs e)
    {
        txt_Record.Text += "Ready to send to NB "+UDP_Model.UDP_Remote_IP+" "+UDP_Model.UDP_Remote_Port.ToString()+"\n";
        string sTmp = objUDPSrv.Send_Udp_Msg(txt_StrSend.Text);
        if (sTmp != "")
        {
            txt_Record.Text += sTmp + "\n";
            LogUtility.WriteInfo(sTmp);
        }
        txt_Record.Text += "UDP Sended to NB Station: "+ txt_StrSend.Text + "\n";
    }

    protected void Btn_UnLock_Click(object sender, EventArgs e)
    {
        //send udp string 'unlock'to NB station        
        objUDPSrv.Send_Udp_Msg("Unlock");
        txt_Record.Text += "UDP Send to NB Station: " + "Unlock" +"\n";
    }

    protected void UDP_OperateTextWhileGetMsg(string Buf)
    {
        //if (iIndexGetMsg % 1000 == 0) txt_UDP.Clear();
        //iIndexGetMsg++;

        
        //UDP_Model.txt_Record_Log_static += "UDP Recv from NB Station: " + Buf + "\n";

        UDP_Model.txt_Record_Log_static += "UDP Recv from NB " + UDP_Model.UDP_Remote_IP + UDP_Model.UDP_Remote_Port.ToString() + ":" + Buf + "\n";

        
        //txt_Record.Text += sLog + "/n";
    }


    protected void Btn_UdpRevc_Click(object sender, EventArgs e)
    {

    }

    protected void Btn_UdpRecv_Click(object sender, EventArgs e)
    {
        if (UDP_Model.txt_Record_Log_static == "") return;
        txt_Record.Text += UDP_Model.txt_Record_Log_static;

        /*
        txt_Record.se

        txt_Record.Select(txt_Record.Text.Length, 0);
        txt_Record.ScrollToCaret();


        this.textBox1.Focus();//获取焦点
        this.textBox1.Select(this.textBox1.TextLength, 0);//光标定位到文本最后
        this.textBox1.ScrollToCaret();//滚动到光标处


        
        var txt = document.getElementById("txt_Record");
        document.body.focus(); // 加上这个
        txt.scrollTop = txt.scrollHeight;        // IE，FF，这句就是滚动 
        */

        UDP_Model.txt_Record_Log_static = "";
    }

    protected void Btn_UdpClose_Click(object sender, EventArgs e)
    {
        string sTmp = objUDPSrv.UDP_Close();
        if (sTmp !="")
        {
            txt_Record.Text += "close UDP error:" + sTmp + "\n";
        }else
        {
            txt_Record.Text += "close UDP done" + "\n";
        }

    }

    protected void Btn_ClearTxt_Click(object sender, EventArgs e)
    {
        txt_Record.Text = "";
    }
}