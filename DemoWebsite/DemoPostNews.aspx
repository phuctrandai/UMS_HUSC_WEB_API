<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemoPostNews.aspx.cs" Inherits="DemoWebsite.DemoPostNews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng thông báo</title>
    <meta charset="utf-8" />
    <link rel="stylesheet" href="Content/bootstrap.css" />

    <script src="Scripts/tinymce/tinymce.min.js?apiKey=2yegk8qfew9q9aewfvwa59l2ite18jlczx5k5i0cv5ntgxmf"></script>
    <script>tinymce.init({ selector: 'textarea' });</script>

</head>
<body class="container">
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" runat="server" href="#">Demo Website</a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li><a runat="server" href="~/DemoPostNews">Demo Đăng Thông Báo</a></li>
                    <li><a runat="server" href="~/Contact">Contact</a></li>
                </ul>
            </div>
        </div>
    </div>
    <br />
    <br />
    <center>
        <h2>Demo Đăng Thông Báo</h2>
        <asp:Label ID="lblKetQua" runat="server" />
    </center>
    <br />
    <br />
    <form id="form1" runat="server">
        <div>
            <p><b><i>Nhập tiêu đề thông báo</i></b></p>
            <asp:TextBox ID="txtTieuDe" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
            <br />

            <p><b><i>Nhập nội dung thông báo</i></b></p>
            <asp:TextBox ID="txtNoiDung" runat="server" Height="300px" TextMode="MultiLine" Width="100%" CssClass="form-control"></asp:TextBox>
            <br />

            <asp:Button ID="btnGui" runat="server" OnClick="btnGui_Click" Text="Đăng thông báo" CssClass="btn btn-danger" />
            <br />
            <br />
            <asp:Label ID="lblLoi" runat="server"/>

            <br /> <br />
            <asp:Label ID="lblTinGuiDi" runat="server"/>
        </div>
    </form>

    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.3.1.js"></script>
</body>
</html>
