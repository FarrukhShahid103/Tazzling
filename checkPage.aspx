<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checkPage.aspx.cs" Inherits="Takeout_checkPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form name="PSi3DForm" action="https://psi3d.psigate.com/psi3d/psipa" method="POST">
    <table>
        <tr>
            <td>
                Merchant Account:
            </td>
            <td>
                <input type="text" name="PSi3D_Account" value="">
            </td>
        </tr>
        <tr>
            <td>
                CardholderPAN:
            </td>
            <td>
                <input type="text" name="PSi3D_CardholderPAN" value="">
            </td>
        </tr>
        <tr>
            <td>
                CardExpiryDate:
            </td>
            <td>
                <input type="text" name="PSi3D_CardExpiryDate" value="0812">
            </td>
        </tr>
        <tr>
            <td>
                Amount:
            </td>
            <td>
                <input type="text" name="PSi3D_Amount" value="543254">
            </td>
        </tr>
        <tr>
            <td>
                ReturnURL:
            </td>
            <td>
                <input type="text" name="PSi3D_ReturnURL" value="http://localhost:4068/TastyGo/Default.aspx">
            </td>
        </tr>
        <tr>
            <td>
                Merchant Data:
            </td>
            <td>
                <input type="text" name="PSi3D_MD" value="SessionId=49320759453">
            </td>
        </tr>
        <tr>
            <td>
                Recurring Payment:
            </td>
            <td>
                <input type="text" name="PSi3D_RecurFlg" value="0">
            </td>
        </tr>
    </table>
    <input type="submit" name="submit" value="Authentication">
    </form>
</body>
</html>
