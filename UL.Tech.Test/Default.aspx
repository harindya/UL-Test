<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UL.Tech.Test._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .error {
            color: red;
        }
    </style>
    <div class="jumbotron">
        <h3>Infix Expression Parser</h3>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h4>Expression to evaluate</h4>     
            <asp:TextBox runat="server" ID="txtInput" />
            <asp:Button ID="btnEvaluate" Text="Evaluate" runat="server" OnClick="btnEvaluate_Click" />
            <asp:DropDownList ID="ddlParser" runat="server">
                <asp:ListItem Text="Evaluate without brackets" Value="0" Selected="True" />
                <asp:ListItem Text="Evaluate with brackets" Value="1" />
            </asp:DropDownList>
        </div>              
    </div>
    <div class="row">
        <div class="col-md-4">
            <span>Result : </span>&nbsp;&nbsp;
            <asp:Label runat="server" ID="lblResult" />           
        </div>              
    </div>

</asp:Content>
