var $ = function (id) {
	return "string" == typeof id ? document.getElementById(id) : id;
};

function checkMaxLength(sender, length)
{
    if(sender.value.length > length)
    {
        sender.value = sender.value.substr(0, length);
    }
    return;
}

function clearDefaultValue(sender, defaultValue)
{
    if(sender.value == defaultValue)
    {
        sender.value = "";
        sender.style.color = "#000000";
    }
}

function setDefaultValue(sender, defaultValue, defaultColor)
{
    if(sender.value == "")
    {
        sender.value = defaultValue;
        sender.style.color = defaultColor;
    }
}

function check_text_enter(e, txtObj, txtObjID, btnObjID)
{
    var keynum;
    var keychar;
    var numcheck;
    
    if(window.event) // IE
    {
        keynum = e.keyCode;
    }
    else if(e.which) // Netscape/Firefox/Opera
    {
        keynum = e.which;
    }
    
    if(keynum == 13)
    {
        $(txtObj.id.replace(txtObjID, btnObjID)).click();
        if(window.event)
        {
            window.event.keyCode = 0;
            window.event.returnValue = false;
        }
        else if(e.which)
        {
            e.preventDefault();
        }
    }
        
    if(window.event) // IE
    {
        window.event.cancelBubble = true;
    }
    else if(e.stopPropagation)
    {
        e.stopPropagation();
    }
};

function ChangeContainerState(sender, objId, objId1)
{
    var obj = document.getElementById(objId);
    var obj1 = document.getElementById(objId1);
    
    if(obj != null)
    {
        if(obj.style.display.toLowerCase() == "block")
            obj.style.display = "none";
        else
            obj.style.display = "block";
    }
    if(obj1 != null)
    {
        if(obj1.style.display.toLowerCase() == "block")
            obj1.style.display = "none";
        else
            obj1.style.display = "block";
    }
}

function WindowOpen(id, e)
{      
      var top = (window.screen.availHeight - 350) / 2; 
      var left = (window.screen.availWidth - 500) / 2; 
    window.open("/takeout/googlemap.aspx?id=" +id, 'map', 'height=350,width=500,top='+top+',left='+left+',toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');
}


function WindowOpenRest(id, e) {
    var top = (window.screen.availHeight - 350) / 2;
    var left = (window.screen.availWidth - 500) / 2;
    window.open("Takeout/googlemap.aspx?id=" + id, 'map', 'height=350,width=500,top=' + top + ',left=' + left + ',toolbar=no,menubar=no,scrollbars=no,resizable=no,location=no,status=no');
}



String.prototype.Trim = function(){
return this.replace(/(^\s*)|(\s*$)/g, "");}

function DeliveryOptionsUpdateControlsValueAndDisabled(sender, id1, id2, id3)
{
    var obj1 = $(sender.id.replace(id1, id2));
    var obj2 = null;
    if(id3 != null)
    {
        obj2 = $(sender.id.replace(id1, id3));
    }
    
    if(sender.value.Trim() != "")
    {
        obj1.value = "0";
        if(obj2 != null)
        {
            obj2.value = "0";
        }
    }
}

function txtTipAmount_TextChanged(sender, id1, id2)
{
     var objE = $(sender.id.replace(id1, id2));
     if(sender.value.Trim() != "")
     {
        objE.selectedIndex = 0;
    }
}

function ddlTipsAmount_SelectedIndexChanged(sender, id1, id2)
{
    var objE = $(sender.id.replace(id1, id2));
    var selectIndex = sender.selectedIndex;
    if(selectIndex != 0)
    {
        objE.value = "";
    }
}

var __popupContainer = null;
var __containerBodyId = "";
function ViewPopupContainer(containerId, containerBodyId, id1, id2)
{
    var popupContainerBody = $(containerBodyId);
    if(id2 != null)
    {
        __containerBodyId = containerBodyId;
        var obj2 = $(id2);
        popupContainerBody.innerHTML = obj2.innerHTML;
        
        __popupContainer = new LightBox(containerId);
        __popupContainer.Center = true; __popupContainer.OverLay.Opacity = 0; __popupContainer.Show();
        return false;
    }
    else
    {
        var incholAjax = new IncholAJAX("/takeout/ajax.aspx?id=" + id1, containerId, containerBodyId);
        IncholAJAX.prototype.ProcessIncholOverride = ProcessEndingLoadHtml;  
        incholAjax.RemoteServer(incholAjax); 
    }
}

function ProcessEndingLoadHtml()
{
    var text = this.responseText;
    if(text != "")
    {
        $(this.parameter2).innerHTML = text;
    }
    
    __popupContainer = new LightBox(this.parameter1);
    __popupContainer.Center = true; __popupContainer.OverLay.Opacity = 0; __popupContainer.Show();
    return false;
}

function HidPopupContainer()
{
    if(__containerBodyId != "")
    {
        var popupContainerBody = $(__containerBodyId);
        popupContainerBody.innerHTML = "";
        __containerBodyId = "";
    }
    
    __popupContainer.Close(); return false;
}

function addWindowScrollEvent(func) {
    var oldfun = window.onscroll;
    if(typeof window.onscroll != 'function') 
    {
        window.onscroll = func;
    } 
    else 
    {
        window.onscroll = function() {
            oldfun();
            func();
        }
    }
};

function addWindowResizeEvent(func) {
    var oldfun = window.onresize;
    if(typeof window.onresize != 'function') 
    {
        window.onresize = func;
    } 
    else 
    {
        window.onresize = function() {
            oldfun();
            func();
        }
    }
};