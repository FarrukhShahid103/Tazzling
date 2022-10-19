<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default11.aspx.cs" Inherits="Default11" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link rel="stylesheet" type="text/css" href="http://assets2.grouponcdn.com/static/stylesheets/dale/base-8T6kos4l.css" />--%>
    <style type="text/css">
        .combox
        {
            background-image: url("http://redsignal.de/attendance/report/images/new/combobox.png");
            border: 1px solid #E4E4E4;
            float: right;
            height: 38px;
            margin-top: 10px;
            text-align: left;
            width: 381px;
        }
        
        .SelectDiv
        {
            background: url("images/Select.png") no-repeat scroll left top transparent;
            border: 0 none;
            color: #9EC700;
            font-size: 13px;
            font-weight: bold;
            height: 32px;
            line-height: 32px;
            overflow: hidden;
            padding-left: 10px;
            text-indent: 2px;
            width: 165px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 100px; display: none;">
        <div class="sweetselect" style="cursor: pointer; float: left; position: relative;
            background: url(&quot;http://assets1.grouponcdn.com/images/sweetselect/select-bg.gif&quot;) repeat-x scroll 0% 0% transparent;
            height: 37px; width: 180px; margin-right: 20px;">
            <span class="select-arrow" style="cursor: pointer; display: block; position: absolute;
                right: 4px; top: 40%; background: url(&quot;http://assets1.grouponcdn.com/images/sweetselect/select-arrow.gif&quot;) no-repeat scroll 0% 0% transparent;
                height: 9px; width: 12px;"></span><span class="right-edge" style="cursor: pointer;
                    display: block; position: absolute; right: -5px; width: 5px; background: url(&quot;http://assets1.grouponcdn.com/images/sweetselect/select-right.gif&quot;) no-repeat scroll 0% 0% transparent;
                    height: 37px;"></span><span class="left-edge" style="cursor: pointer; display: block;
                        position: absolute; left: 0px; width: 5px; background: url(&quot;http://assets1.grouponcdn.com/images/sweetselect/select-left.gif&quot;) no-repeat scroll 0% 0% transparent;
                        height: 37px;"></span><span class="value" style="cursor: pointer; display: inline-block;
                            font-weight: bold; overflow: hidden; font-size: 14px; line-height: 17px; padding: 10px 10px 8px;
                            width: 145px;">All</span>
            <select name="filter" id="filter" style="cursor: pointer; left: 0px; opacity: 0;
                position: absolute; top: 0px; height: 37px; width: 180px;">
                <option value="/users/li6421/groupons/available?dl=d47388">Available</option>
                <option value="/users/li6421/groupons/used?dl=d47388">Redeemed</option>
                <option value="/users/li6421/groupons/expired?dl=d47388">Expired</option>
                <option selected="selected" value="/users/li6421/groupons/all?dl=d47388">All</option>
            </select>
        </div>
    </div>
    <div style="margin: 40px; display: none;">
        <!-- <div name="arow" class="arow" ></div> -->
        <div style="margin-top: 5px; float: right;">
            <select class="SelectDiv" name="jumpMenu" id="jumpMenu">
                <option value="">Select Name</option>
                <option value="4">Shahid Hussain</option>
                <option value="5">Kashif Altaf</option>
                <option value="6">Muhammad Awais</option>
                <option value="7">Muhammad Qaiser</option>
                <option value="8">Irfan Danish</option>
                <option value="9">Sher Azam</option>
                <option value="10">Rauf Aamir</option>
                <option value="11">Maryam Javed</option>
                <option value="12">Shahzad Younis</option>
                <option value="13">Muhammad Naeem</option>
                <option value="14">Aleem Ahmad</option>
                <option value="15">Suhail ahmad</option>
                <option value="16">Asbar Ali Shah</option>
                <option value="17">Faisal Naseer</option>
                <option value="18">Muhammad Waqas Iqbal</option>
                <option value="19">Rabia Guftar</option>
                <option value="20">Muhammad Zubair</option>
                <option value="21">Muzammil Qurashi</option>
                <option value="22">Samia Adnan</option>
                <option value="23">Anum Shahid</option>
                <option value="24">Saima Najib</option>
                <option value="25">Sulman Shahid</option>
                <option value="26">Balal Rasheed</option>
                <option value="27">Abdul Hanan</option>
                <option value="28">Rizwan Javed</option>
                <option value="29">Umar Saeed</option>
                <option value="30">Zeeshan Hameed</option>
                <option value="31">Waqar Ali</option>
                <option value="32">Asjad Ali</option>
                <option value="33">Muhammad Faheem</option>
                <option value="34">Suhail Azhar</option>
                <option value="35">Mustafa Sadiq</option>
                <option value="36">Muhammad Hanif</option>
            </select>
        </div>
    </div>
    



    
    </form>
</body>
</html>
