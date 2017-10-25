<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NB_Main.aspx.cs" Inherits="NB_Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width,height=device-height, initial-scale=1.0, minimum-scale=0.5, maximum-scale=2.0, user-scalable=yes" /> 
    
    <title>NB</title>

    <script src="NM_Main.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        Record of the lock
        <br />
        <asp:textbox ID="txt_Record" runat="server" Height="245px" Width="333px" TextMode="MultiLine"></asp:textbox>
        <br />
        Send:
        <asp:TextBox ID="txt_StrSend" runat="server"></asp:TextBox>

        <asp:Button ID="Btn_UdpSend" runat="server" OnClick="Btn_UdpSend_Click" Text="UdpSend" />
        <br />
        
        <asp:Button ID="Btn_UdpListen" runat="server" OnClick="Btn_UdpListen_Click" Text="UdpListen" />
        <asp:Button ID="Btn_UdpClose" runat="server" OnClick="Btn_UdpClose_Click" Text="UdpClose" />
        <br/>
        <asp:Button ID="Btn_UnLock" runat="server" OnClick="Btn_UnLock_Click" Text="UnLock" />
        <br />
        
        <asp:Button ID="Btn_UdpRecv" runat="server" OnClick="Btn_UdpRecv_Click" Text="UdpRecv" />

        
        
        <p>
            <asp:Button ID="Btn_ClearTxt" runat="server" OnClick="Btn_ClearTxt_Click" Text="ClearTxt" />
        </p>

        
        
    </form>
</body>

</html>
