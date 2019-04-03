<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemoSendMessage.aspx.cs" Inherits="DemoWebsite.DemoSendMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gửi tin nhắn</title>
    <meta charset="utf-8" />
    <link rel="stylesheet" href="Content/bootstrap.css" />
    <script src="Scripts/jquery-3.3.1.js"></script>
    <script src="Scripts/bootstrap.js"></script>

    <script src="Scripts/tinymce/tinymce.min.js?apiKey=2yegk8qfew9q9aewfvwa59l2ite18jlczx5k5i0cv5ntgxmf"></script>
    <script>tinymce.init({ selector: 'textarea' });</script>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

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
                    <li><a runat="server" href="~/DemoSendMessage">Demo Gửi Tin Nhắn</a></li>
                </ul>
            </div>
        </div>
    </div>
    <br />
    <br />
    <center>
        <h2>Demo Gửi Tin Nhắn</h2>
        <asp:Label ID="lblKetQua" runat="server" />
    </center>
    <br />
    <br />
    <form id="form1" runat="server">
        <div>
            <p><b><i>Người nhận</i></b></p>
            <%--<select class="select2 form-control" style="width: 100%" id="txtNguoiNhan" name="txtNguoiNhan[]" multiple="multiple">
            </select>--%>
            <asp:ListBox ID="listNguoiNhan" runat="server" CssClass=".select2 form-control" Width="100%" SelectionMode="Multiple"></asp:ListBox>
            <br />
            <br />

            <p><b><i>Nhập tiêu đề tin nhắn</i></b></p>
            <asp:TextBox ID="txtTieuDe" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
            <br />

            <p><b><i>Nhập nội dung tin nhắn</i></b></p>
            <asp:TextBox ID="txtNoiDung" runat="server" Height="300px" TextMode="MultiLine" Width="100%" CssClass="form-control"></asp:TextBox>
            <br />

            <asp:Button ID="btnGuiTinNhan" runat="server" OnClick="btnGuiTinNhan_Click" Text="Gửi tin nhắn" CssClass="btn btn-danger" />
            <br />
            <br />
            <asp:Label ID="lblLoi" runat="server" />

            <br />
            <br />
            <asp:Label ID="lblTinGuiDi" runat="server" />
        </div>
    </form>

    <script>
        $(document).ready(function () {
            var data = [
                {
                    id: 0,
                    text: 'enhancement'
                },
                {
                    id: 1,
                    text: 'bug'
                },
                {
                    id: 2,
                    text: 'duplicate'
                },
                {
                    id: 3,
                    text: 'invalid'
                },
                {
                    id: 4,
                    text: 'wontfix'
                }
            ];

            $(".select2").select2({
                placeholder: "Vui lòng nhập ít nhất 2 ký tự để tìm kiếm",
                allowClear: true,
                data: data
            });
        });
    </script>
</body>
</html>
