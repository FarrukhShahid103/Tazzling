/*************************************************************************************************
 Legal Notice for the Inchol Framework 2.0
 
 Copyright (c) 2005 Nantong Inchol Co. Ltd. ( http://www.inchol.com )
 Owner and Author: www.inchol.com  

 Permission is restricted to Inchol's employees and its partners ONLY. The code shall be used 
 for the Inchol's projects. Anyone shall be authorized by the Inchol or its owner for the 
 purposes of using, copying, modifying, merging, publishing, distributing, sublicensing, 
 and/or selling of the Software.

 The above copyright notice and this permission notice shall be included in all copies or 
 substantial portions of the Software. Making illegal copies of the Software is prohibited. 
 Individuals who violate copyright law and software licensing agreements may be subject to 
 criminal or civil action by the owner of the copyright.
***************************************************************************************************/

//Global Variables
var g_intSkinOffsetLeft = 0;
var g_intSkinOffsetTop = 0;
var g_strSkinResourceDirectory = null;
var g_strComponentResourceDirectory = null;
var g_currentInputItem = null;

if(document.aspnetForm)
	var g_doc = document.aspnetForm.all;
else
	var g_doc = document.all;

//begin for ftp
function GetCookie(name)
{ 
    var cookieValue = ""; 
    var search = escape(name) + "="; 
    if(document.cookie.length > 0)
    {  
        offset = document.cookie.indexOf(search); 
        if (offset != -1)
        {  
            offset += search.length; 
            end = document.cookie.indexOf(";", offset); 
            if (end == -1) 
                end = document.cookie.length; 
            cookieValue = decodeURIComponent((document.cookie.substring(offset, end)))
        } 
    } 
    return cookieValue; 
}  

function SetCookie(name,value,hours,path,domain,secure) {   
    var expire = "";
    var pathstr = "";
    var domainstr = "";
    var securestr = "";
    if(hours != null)
    { 
        expire = new Date((new Date()).getTime() + hours * 3600000); 
        expire = "; expires=" + expire.toGMTString(); 
    } 
    if(path != null)
       pathstr = "; path=" + path;
    if(domain != null)
       domainstr = "; domain=" + domain;
    if(secure != null)
       securestr = "; secure"
       
    var c = name + "=" + escape (value) +  expire + pathstr + domainstr + securestr;
    document.cookie = c;
//    document.cookie = name + "=" + escape (value) +  expire + 
//    ((path) ? "; path=" + path : "") +   
//    ((domain) ? "; domain=" + domain : "") + ((secure) ? "; secure" : "");   
}  
//end for ftp

function CheckAll(fieldName)
{
    CheckAllNone(fieldName, true);
}

function CheckNone(fieldName)
{
    CheckAllNone(fieldName, false);
}

function CheckAllNone(fieldName, checked)
{
    strFieldName = fieldName.toLowerCase();
	var strFieldNameAlt = strFieldName;

	for(var i = 0; i < document.aspnetForm.elements.length; i++)
	{
		var objElement = document.aspnetForm.elements[i];

		var strObjectName = objElement.name.toLowerCase();
		var intIndex = strObjectName.indexOf("$" + strFieldNameAlt)
		
		if (intIndex < 0)
		    intIndex = strObjectName.indexOf(":" + strFieldNameAlt);

		if(intIndex >= 0)
		{
		    if (objElement.disabled == true)
                continue;
            else
			    objElement.checked = checked;
		}
	}
}

function IsInteger(string ,sign)
{ 
    var integer;
    integer = parseInt(string);
    if (isNaN(integer)) return false;
    else if (integer.toString().length==string.length)
    { 
        if ((sign==null) || (sign=='-' && integer<0) || (sign=='+' && integer>0))
        {
            return true;
        }
        else
            return false; 
    }
    else
        return false;
}

function GetBodySize(widthOrHeight)
{
    var h,w;
    if(widthOrHeight=="w")
    {
        if(document.all)
        {
            w = document.body.clientWidth;
        }
        else
        {
            w = window.innerWidth;
        }
        
        return w;
    }
    else
    {
        if(document.all)
        {
            h = document.body.clientHeight;
        }
        else
        {
            h = window.innerHeight;
        }
        
        return h;
    }
}

/// <summary>
/// Removes a string from text
/// </summary>
/// <param name="strSource">The source text</param>
/// <param name="strRemove">The string to remove</param>
/// <returns>The modified string</returns>
function removeString(strSource, strRemove)
{
	re = eval("/" + strRemove + "/g");
	return strSource.replace(re, "");
}

/// <summary>
/// Gets the specified form element and returns the object
/// </summary>
/// <param name="strFieldName">The name of the object to return</param>
/// <returns>The object corresponding to that name</returns>
function getFormElement(strFieldName)
{
	strFieldName = strFieldName.toLowerCase();
	var strFieldNameAlt = "$" + strFieldName;
	
	for(var i = 0; i < document.aspnetForm.elements.length; i++)
	{
		var objElement = document.aspnetForm.elements[i];

		var strObjectName = objElement.name.toLowerCase();
		var intIndex = strObjectName.indexOf(strFieldNameAlt)
		
		if (intIndex < 0)
		    intIndex = strObjectName.indexOf(":" + strFieldName);
		
		var intAssumedIndex = strObjectName.length - strFieldNameAlt.length;

		if(intIndex > 0 && intIndex == intAssumedIndex)
			return objElement;
	}

	var arrElements = document.aspnetForm.getElementsByTagName("INPUT");
	for(var i = 0; i < arrElements.length; i++)
	{
		var objElement = arrElements[i];

		var strObjectName = objElement.name.toLowerCase();
		var intIndex = strObjectName.indexOf(strFieldNameAlt);
		
		if (intIndex < 0)
		    intIndex = strObjectName.indexOf(":" + strFieldName);
		    
		var intAssumedIndex = objElement.name.length - strFieldNameAlt.length;

		if(intIndex > 0 && intIndex == intAssumedIndex)
			return objElement;
	}

	arrElements = document.aspnetForm.getElementsByTagName("A");
	strFieldNameAlt = strFieldName + "_link";

	for(var i = 0; i < arrElements.length; i++)
	{
		var objElement = arrElements[i];

		var strObjectName = objElement.name.toLowerCase();
		var strObjectID = objElement.id.toLowerCase();

		var intIndexName = strObjectName.indexOf(strFieldNameAlt)
		var intIndexID = strObjectID.indexOf(strFieldNameAlt)
		var intAssumedIndexName = objElement.name.length - strFieldNameAlt.length;
		var intAssumedIndexID = objElement.id.length - strFieldNameAlt.length;

		if(intIndexName > 0 && intIndexName == intAssumedIndexName)
			return objElement;
	}

	return null;
}

function getParentFormElement(strFieldName)
{
	strFieldName = strFieldName.toLowerCase();
	var strFieldNameAlt = "$" + strFieldName;

	for(var i = 0; i < parent.document.aspnetForm.elements.length; i++)
	{
		var objElement = parent.document.aspnetForm.elements[i];

		var strObjectName = objElement.name.toLowerCase();
		var intIndex = strObjectName.indexOf(strFieldNameAlt);
		if (intIndex < 0)
		    intIndex = strObjectName.indexOf(":" + strFieldName);
		    
		var intAssumedIndex = strObjectName.length - strFieldNameAlt.length;

		if(intIndex > 0 && intIndex == intAssumedIndex)
		{
			return objElement;
		}
	}

	var arrElements = parent.document.aspnetForm.getElementsByTagName("INPUT");
	for(var i = 0; i < arrElements.length; i++)
	{
		var objElement = arrElements[i];

		var strObjectName = objElement.name.toLowerCase();
		var intIndex = strObjectName.indexOf(strFieldNameAlt);
		if (intIndex < 0)
		    intIndex = strObjectName.indexOf(":" + strFieldName);
		    
		var intAssumedIndex = objElement.name.length - strFieldNameAlt.length;

		if(intIndex > 0 && intIndex == intAssumedIndex)
			return objElement;
	}

	arrElements = parent.document.aspnetForm.getElementsByTagName("A");
	strFieldNameAlt = strFieldName + "_link";

	for(var i = 0; i < arrElements.length; i++)
	{
		var objElement = arrElements[i];

		var strObjectName = objElement.name.toLowerCase();
		var strObjectID = objElement.id.toLowerCase();

		var intIndexName = strObjectName.indexOf(strFieldNameAlt)
		var intIndexID = strObjectID.indexOf(strFieldNameAlt)
		var intAssumedIndexName = objElement.name.length - strFieldNameAlt.length;
		var intAssumedIndexID = objElement.id.length - strFieldNameAlt.length;

		if(intIndexName > 0 && intIndexName == intAssumedIndexName)
			return objElement;
	}

	return null;
}

/// <summary>
/// Simulate a click on a form button. This will allow normal form
/// processing, including validation.
/// </summary>
/// <param name="strName">The name of the button to click</param>
function clickButtonOnEnter(strName)
{
	if (event.keyCode == 13)
	{
		if(getFormElement(strName) != null);
		{
			var objElement = getFormElement(strName);
			objElement.click();
		}
		return false;
	}
}

/// <summary>
/// Clicks a submit button on a form when the enter key is pressed. This will
/// allow normal form processing including validation.
/// </summary>
/// <param name="strFormID">Unique identifier for form submit button</param>
function submitFormOnEnter(strFormID)
{
	if (event.keyCode == 13)
	{
		var arrElements = document.aspnetForm.getElementsByTagName("INPUT");
		for(var i = 0; i < arrElements.length; i++)
		{
			var objElement = arrElements[i]; 
			if(objElement.formID == strFormID + '_submit')
			    objElement.click();
		}
		return false;
	}
}


/// <summary>
/// Inserts an object into a specified element slot in an ordered array,
///	moving the rest down one element slot
/// </summary>
/// <param name="obj">The object being inserted into the array</param>
/// <param name="intIndex">The requested slot to put the object</param>
function weldElement(obj, intIndex){

    var blnFound = false;
    var strTemp, strTemp2;

    if(intIndex < this.length) {
        var intLength = this.length + 1;
        for(var i = intIndex; i < (intLength); i++) {
            if(i == intIndex) {
                strTemp = this[i];
                this[i] = obj;
                blnFound = true;
            } else if (blnFound == true && i < this.length) {
                strTemp2 = this[i];
                this[i] = strTemp;
                strTemp = strTemp2;
            } else if (blnFound == true && i == this.length) {
                this[i] = strTemp;
            }
        }
    } else {
        this[this.length] = obj;
    }
}

Array.prototype.weld = weldElement;

/// <summary>
/// Checks to see if a string is null or empty
/// </summary>
/// <param name="str">The string to check</param>
/// <returns>True if the string is null or empty</returns>
function isBlank(str)
{
    return((str == "" || str == null) ? true : false);
}

/// <summary>
/// Decodes a Guid
/// </summary>
/// <param name="str">The string to decode</param>
/// <returns>The decoded Guid</returns>
function decodeGuid(str)
{
	if(str != null)
	    return(str.replace(/_/g, "-"));
	else
		return null;
}

/// <summary>
/// Encodes a Guid
/// </summary>
/// <param name="str">The string to encode</param>
/// <returns>The encoded Guid</returns>
function encodeGuid(str)
{
	if(str != null)
	    return(str.replace(/-/g, "_"));
	else
		return null;
}

/// <summary>
/// Writes a message to the page
/// </summary>
/// <param name="strText">The string to write</param>
/// <param name="blnAppend">If true, appends the message to the current set of messages</param>
function message(strText, blnAppend)
{
	var objMessage = document.getElementById("MessageWrite");
	if(objMessage == null)
	{
		var msg = document.createElement("SPAN");
		msg.setAttribute("id", "MessageWrite");
		msg.setAttribute("style", "font-size:11px; color: #000000; font-family:Verdana");
		document.body.appendChild(msg);
		objMessage = document.getElementById("MessageWrite");
	}

	if(objMessage.innerHTML == "")
		objMessage.innerHTML = "<br/>";

	if(!blnAppend)
	{
		objMessage.innerHTML = "<br/><br/>" + strText;
	}
	else
	{
		objMessage.innerHTML += "<br/>" + strText;
	}
}

/// <summary>
/// Trims trailing and leading spaces
/// </summary>
/// <param name="strValue">The string to trim</param>
/// <returns>The trimmed string</returns>
function trim(strValue)
{
	//not Netscape tested
	if (strValue) {
		while(strValue.charCodeAt(0) == 32 || strValue.charCodeAt(0) == 13
            || strValue.charCodeAt(0) == 10) {
			strValue = strValue.substring(1, strValue.length)
		}

		while(strValue.charCodeAt(strValue.length - 1) == 32 ||
            strValue.charCodeAt(strValue.length - 1) == 13 ||
            strValue.charCodeAt(strValue.length - 1) == 10) {

            strValue = strValue.substring(0, strValue.length - 1)
		}
	}

    return strValue;
}

/// <summary>
/// Given an image source, will replace keywords in the source
/// </summary>
/// <param name="str">The string representing the image source</param>
/// <param name="strLookFor">The string to search for</param>
/// <param name="strReplaceWith">The replacement string</param>
/// <returns>The altered image source</returns>
function getImage(str, strLookFor, strReplaceWith)
{
	re = eval("/" + strLookFor + "/g");
	return(str.replace(re, strReplaceWith));
}

/// <summary>
/// Creates a simple popup window with the specified options.
/// </summary>
/// <param name="strUrl">String representing the url to display</param>
/// <param name="intHeight">Height of the window, if this value is not 
///specified it will calculate the height based on screen size.</param>
/// <param name="intWidth">Width of the window, if this value is not 
///specified it will calculate the width based on screen size.</param>
/// <param name="intLeft">Position of the left side of the window, if this 
///value is not specified it will be centered from the left based on screen 
///size.</param>
/// <param name="intTop">Position of the top of the window, if this value is 
///not specified it will be centered from the top based on screen size.</param>
/// <returns>Reference to the newly created window</returns>
function openSimpleWindow(strUrl, intHeight, intWidth, intLeft, intTop)
{
	if (window.screen) {
	    if(intWidth == null)
			intWidth = Math.floor(screen.availWidth/3);
		if(intHeight == null)
			intHeight = Math.floor(screen.availHeight/3);
	    if(intLeft == null)
			intLeft = Math.floor((screen.availWidth-intWidth)/2);
	    if(intTop == null)
			intTop = Math.floor((screen.availHeight-intHeight)/2);
	}
	else{
		intWidth = 640/3;
		intHeight = 480/3;
		intLeft = intWidth/2;
		intTop = intHeight/2;
	}

  return window.open(strUrl, "_blank", "scrollbars=no,status=no,resizable=no,height=" + intHeight + 
",location=0,width=" + intWidth + ",left=" + intLeft + ",top=" + intTop);
}

/// <summary>
/// Creates a new popup window with the specified options.
/// </summary>
/// <param name="strUrl">String representing the url to display</param>
/// <param name="sName">Name of the window</param>
/// <param name="bScrollbars">True if you want to display scroll bar</param>
/// <param name="bStatus">True if you want to display status bar</param>
/// <param name="bResizable">True if you want to resize the window</param>
/// <param name="intHeight">Height of the window</param>
/// <param name="intWidth">Width of the window</param>
/// <param name="intLeft">Position of the left side of the window</param>
/// <param name="intTop">Position of the top of the window</param>
/// <returns>Reference to the newly created window</returns>
function openWindow(strUrl, sName, bScrollbars, bStatus, bResizable, 
intHeight, intWidth, intLeft, intTop)
{
  var scroll = 0;
  var status = 0;
  var resizable = 0;

  if(bScrollbars)
	scroll = 1;

  if(bStatus)
    status = 1;

  if(bResizable)
    resizable = 1;

  return window.open(strUrl, sName, "scrollbars=" + scroll + ",status=" + 
status + ",resizable=" + resizable + ",height=" + intHeight + 
",location=0,width=" + intWidth + ",left=" + intLeft + ",top=" + intTop);
}

/// <summary>
/// Creates a new popup window centered in the screen.
/// </summary>
/// <param name="strUrl">String representing the url to display</param>
/// <param name="sName">Name of the window</param>
/// <param name="bScrollbars">True if you want to display scroll bar</param>
/// <param name="bStatus">True if you want to display status bar</param>
/// <param name="bResizable">True if you want to resize the window</param>
/// <param name="intHeight">Height of the window</param>
/// <param name="intWidth">Width of the window</param>
/// <returns>Reference to the newly created window</returns>
function openCenteredWindow(strUrl, sName, bScrollbars, bStatus, bResizable, intHeight, intWidth)
{
  var thisWindow;
  var cw = intWidth/2, ch = intHeight/2;
	if (window.screen) {
	    cw = Math.floor((screen.availWidth-intWidth)/2);
	    ch = Math.floor((screen.availHeight-intHeight)/2);
	}

  return openWindow(strUrl, sName, bScrollbars, bStatus, bResizable, intHeight, intWidth, cw, ch)
}

function ConfirmToRemove(LinkTo, MsgText){
   var where_to= confirm(MsgText);
   if (where_to== true)
    {
      window.location= LinkTo;
      return true;
    }
   else
    {
     return false;
    }
}

function ConfirmRemove(MsgText){
   var where_to= confirm(MsgText);
   if (where_to== true)
    {
      return true;
    }
   else
    {
     return false;
    }
}

//Open close category left bar
function IncholOpenClose(node, targetZone, openSize, imgClose, imgOpen, direction)
{
    var oLeft =  document.getElementById(targetZone);

    if (oLeft.style.display == "none")
    {
        oLeft.style.display = "block";
        if(direction == "V")
            oLeft.height = openSize; //"20%";
        else
            oLeft.width = openSize; //"20%";
        
        if(node != null)
        {
            node.src = imgClose;  
        }
        
        Set_Cookie( targetZone, 'block', '', '/', '', '' );
    }
    else
    { 
        oLeft.style.display = "none";
        if(direction == "V")
		    oLeft.width = "1";
		else
		    oLeft.width = "1";
		    
		if(node != null)
		{
            node.src = imgOpen;
        }
        
        Set_Cookie( targetZone, 'none', '', '/', '', '' );
    }
}

function LoadOpenClose(imgNode, targetZone, imgClose, imgOpen)
{
    var leftNode = document.getElementById(targetZone);
    
    var c = Get_Cookie(targetZone);
    if(c)
    {
        if (c == "none" && imgNode!=null)
        {
            imgNode.src = imgOpen;
        }
        else if(imgNode!=null)
            imgNode.src = imgClose;
            
        leftNode.style.display = c;
    }
}


function SetValueToOpener(objAddButton, openerFieldName, sourceFieldName)
{
    var theSelTo = getParentFormElement(openerFieldName);
    var theSelFrom = getFormElement(sourceFieldName);
	var selLength = theSelFrom.length;
	var selectedText = new Array();
	var selectedValues = new Array();
	var selectedCount = 0;
	var toHidden = getParentFormElement("Hidden" + openerFieldName);

	var i;
 
	// Find the selected Options in reverse order
	// and delete them from the 'from' Select.
	for(i=selLength-1; i>=0; i--)
	{
		if(theSelFrom.options[i].selected)
		{
			selectedText[selectedCount] = theSelFrom.options[i].text;
			selectedValues[selectedCount] = theSelFrom.options[i].value;
			//deleteOption(theSelFrom, i);
			
			//var selLength = theSelFrom.length;
	        if(theSelFrom.length > 0)
		        theSelFrom.options[i] = null;
		        
			selectedCount++;
		}
	}
	
	// Add the selected text/values in reverse order.
	// This will add the Options to the 'to' Select
	// in the same order as they were in the 'from' Select.
	for(i=selectedCount-1; i>=0; i--)
	{
		//addOption(theSelTo, selectedText[i], selectedValues[i]);
		
		var newOpt = new Option(selectedText[i], selectedValues[i]);
	    var selLength = theSelTo.length;
		
	    theSelTo.options[selLength] = newOpt;
	}

    AssignValueToHiddenField(theSelTo, toHidden);
    
	if (theSelFrom.length == 0)
	    objAddButton.disabled = true;
	else
	    objAddButton.disabled = false;
}

function RemoveByUser(field)
{
    var theSel = getFormElement(field + "ByUser");
    var selLength = theSel.length;
    var toHidden = getFormElement("Hidden" + field + "ByUser");

    for(i=selLength-1; i>=0; i--)
	{
		if(theSel.options[i].selected)
		{
	        if(theSel.length > 0)
		        theSel.options[i] = null;
		}
	}
	
	AssignValueToHiddenField(theSel, toHidden);
}

function AssignValueToHiddenField(fromSelectField, toHidden)
{
    var v = "";

    for(var i=0;i<fromSelectField.options.length;i++)
    {        
        if (v != "")
            v += ",";
            
        v += fromSelectField.options[i].value;
    }
    
    toHidden.value = v;
}

//Select all options in the select box
function SelectAllOptons(objField)
{
    for(var i=0;i<objField.options.length;i++)
    {
        objField.options[i].selected = true;
    }
}
//======================================================================================
//End of Category functions
//======================================================================================

/// <summary>
/// Disables a button from being clicked twice
/// </summary>
function VoidDoubleClick(obj)
{
	obj.onclick = function onclick(event){return false}
	obj.style.filter = "progid:DXImageTransform.Microsoft.BasicImage(grayscale=1), progid:DXImageTransform.Microsoft.Alpha(Opacity=25)";
	return true;
}

function GetQueryVariable(variable)
{
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i=0;i<vars.length;i++)
    {
        var pair = vars[i].split("=");
        if (pair[0] == variable)
        {
            return pair[1];
        }
    }
    
    return null;
} 

//for: show left length we can enter.
//checkInputSize:
//get how many chars we can enter into this item, show it in a span named "CHKitemName"(if item's name is itemName).
//if we can't enter chars any more, alert and cut the surplus chars.
function CheckInputSize(itemName, displayLabel, limitLength, alertLabel)
{
    var formField = getFormElement(itemName);
    var fieldTextLength = formField.value.length; 
    leftLength = limitLength - fieldTextLength;
    
    if(displayLabel != "none")
    {
        var fieldHolder = eval("CHK" + itemName);
        fieldHolder.innerHTML = displayLabel + ':' + leftLength;
    }
    
    if (fieldTextLength > limitLength) 
    {
	    alert(alertLabel);
	    formField.value = formField.value.substring(0,limitLength); 
    }
	
	g_currentInputItem = itemName;
}

//for: display left length we can enter.
//if we no longer need to show left length of an input item, hide the span.
function InputItemLoseFocus(itemName, fieldValue)
{
    if( itemName == g_currentInputItem )
    {
	    var fieldHolder = eval("CHK" + itemName);
	    fieldHolder.innerHTML = fieldValue;
	    g_currentInputItem = null;
	}
}

//for: display left length we can enter.
function InputItemGetFocus(itemName, fieldValue)
{
	var fieldHolder = eval("CHK" + itemName);
	fieldHolder.innerHTML = fieldValue; 
	g_currentInputItem = itemName;
}

function Set_Cookie( name, value, expires, path, domain, secure ) 
{
    // set time, it's in milliseconds
    var today = new Date();
    today.setTime( today.getTime() );

    /*
    if the expires variable is set, make the correct 
    expires time, the current script below will set 
    it for x number of days, to make it for hours, 
    delete * 24, for minutes, delete * 60 * 24
    */
    if ( expires )
    {
        expires = expires * 1000 * 60 * 60 * 24;
    }
    var expires_date = new Date( today.getTime() + (expires) );

    document.cookie = name + "=" +escape( value ) +
    ( ( expires ) ? ";expires=" + expires_date.toGMTString() : "" ) + 
    ( ( path ) ? ";path=" + path : "" ) + 
    ( ( domain ) ? ";domain=" + domain : "" ) +
    ( ( secure ) ? ";secure" : "" );
}

// this function gets the cookie, if it exists
function Get_Cookie( name ) {
    var start = document.cookie.indexOf( name + "=" );
    var len = start + name.length + 1;
    if ( ( !start ) &&
    ( name != document.cookie.substring( 0, name.length ) ) )
    {
        return null;
    }
    if ( start == -1 ) return null;
    var end = document.cookie.indexOf( ";", len );
    if ( end == -1 ) end = document.cookie.length;
    return unescape( document.cookie.substring( len, end ) );
}

// this deletes the cookie when called
function Delete_Cookie( name, path, domain ) {
    if ( Get_Cookie( name ) ) document.cookie = name + "=" +
    ( ( path ) ? ";path=" + path : "") +
    ( ( domain ) ? ";domain=" + domain : "" ) +
    ";expires=Thu, 01-Jan-1970 00:00:01 GMT";
}

function AddFavorite(url, title) 
{ 
    if (window.sidebar) { 
        // Mozilla Firefox Bookmark		
        window.sidebar.addPanel(title, url,"");	
    } 
    else if( window.external ) { 
        // IE Favorite		
        window.external.AddFavorite( url, title); 
    }	
    else if(window.opera && window.print) 
    { 
        // Opera Hotlist		
        return true; 
    } 
}

//Check all or none with HTML input checkbox
function CheckboxAllNone(fieldCheck)
{
    CheckListID = 'CE_ContentID';
    if(fieldCheck.checked)
        CheckBoxListOnOff(CheckListID, true);
    else
        CheckBoxListOnOff(CheckListID, false);
} 

function SelectAllNone(fieldCheck) 
{
    CheckBoxListOnOff("CE_ContentID", null);
} 

function CheckBoxListOnOff(fieldName, checked)
{
    strFieldName = fieldName.toLowerCase();
    var strFieldNameAlt = strFieldName;
    var objHidden = document.aspnetForm.ProcessInfoIDs;
    objHidden.value = "";

    for(var i = 0; i < document.aspnetForm.elements.length; i++)
    {
	    var objElement = document.aspnetForm.elements[i];
	    var strObjectName = objElement.name.toLowerCase();

	    if(strFieldName == strObjectName)
	    {
            if(checked != null)
	            objElement.checked = checked;
	        
	        if(objElement.checked)
	        {
	            if (objHidden.value != "")
	                objHidden.value += ",";
	                
	            objHidden.value += objElement.value;
	        }
	    }
    }          
            
    OnOffCntDeleteBtn(objHidden.value);
} 

function OnOffCntDeleteBtn(v)
{
    var o = getFormElement("BtnDeleteCnt");
    
    if(v == '')
        o.disabled = true;
    else
        o.disabled = false;
}

function setPosition(id)  
{  
  if(document.getElementById(id) == null)
      return;
  var scrollPos = 0;
  if (typeof window.pageYOffset != 'undefined') {
     scrollPos = window.pageYOffset;
  }
  else if (typeof document.compatMode != 'undefined' &&
       document.compatMode != 'BackCompat') {
     scrollPos = document.documentElement.scrollTop;
  }
  else if (typeof document.body != 'undefined') {
     scrollPos = document.body.scrollTop;
  }
  document.getElementById(id).style.top = parseInt(scrollPos) + 200 + "px"; 
}

function ConfirmToRemove(LinkTo, MsgText){
   var where_to= confirm(MsgText);
   if (where_to== true)
    {
      window.location= LinkTo;
      return true;
    }
   else
    {
     return false;
    }
}

//=============================================================================
// Start global Process when the page is loading...
//=============================================================================
function ProcessSessionEnd(url){
    var ia = new IncholAJAX(url);
    ia.RemoteServer(ia);

    IncholAJAX.prototype.ProcessIncholOverride = ProcessEndingUserSession;
}

function ProcessEndingUserSession()
{
    //Do nothing
}

//=============================================================================
// End global Process when the page is loading...
//=============================================================================

Array.prototype.insertAt = function(index, value)
{
   var part1 = this.slice(0, index);
   var part2 = this.slice(index);
   part1.push(value);
   return(part1.concat(part2));    
}
 
Array.prototype.removeAt = function(index)
{         
   var part1 = this.slice(0, parseInt(index) + 1);
   var part2 = this.slice(parseInt(index) + 1); 
   part1.pop();
   return(part1.concat(part2));    
} 

function AttachEvent(elementObj, eventName, eventHandlerFunctionName){
    if (elementObj.addEventListener) 
    { // Non-IE browsers
      elementObj.addEventListener(eventName, eventHandlerFunctionName, false);		
    } 
    else if (elementObj.attachEvent) 
    { // IE 6+
      elementObj.attachEvent('on' + eventName, eventHandlerFunctionName);
    } 
    else 
    { // Older browsers 
      var currentEventHandler = elementObj['on' + eventName];
      if (currentEventHandler == null) 
      {
        elementObj['on' + eventName] = eventHandlerFunctionName;
      } 
      else 
      {
        elementObj['on' + eventName] = function(e) { currentEventHandler(e); eventHandlerFunctionName(e); }
      }
    }
}
  
//=============================================================================
// AJAX Process
//=============================================================================
function IncholAJAX(urlToServer, parameter1, parameter2, parameter3)
{
    this.urlToServer = urlToServer;
    this.parameter1 = parameter1;
    this.parameter2 = parameter2;
    this.parameter3 = parameter3;

    if (typeof XMLHttpRequest != "undefined")
        this.http = new XMLHttpRequest();
    else if (typeof ActiveXObject != "undefined")
        this.http = new ActiveXObject("MSXML2.XmlHttp");
    else
        alert("No XMLHttpRequest object available. This functionality will not work.");
}

IncholAJAX.prototype.responseText;

IncholAJAX.prototype.GetResponseText = function() {
    return this.responseText;
}

IncholAJAX.prototype.RemoteServer = function(incholAJAX){        
    var oHttp = this.http;
    if (oHttp.readyState != 0 )
        oHttp.abort();
    
    oHttp.open("get", this.urlToServer, true);

    oHttp.onreadystatechange = function(){
        if (oHttp.readyState == 4)
        {
            incholAJAX.responseText = oHttp.responseText;
            incholAJAX.ProcessIncholOverride();
        }

    };

    oHttp.send(null);
};

IncholAJAX.prototype.ProcessIncholOverride = function()
{
    /*alert('You need to implement your ProcessIncholOverride method. For example\n\n' + 
        'IncholAJAX.prototype.ProcessIncholOverride = ProcessIncholOverride;\n' +
        'function ProcessIncholOverride(){alert("This is my Override method...");}');
    */
};

//=============================================================
// Global Auto Loading
//=============================================================
//For updating progress panel
window.onscroll = function(){setPosition("loadingBox");};

