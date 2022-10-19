//Menu Script Starts   


var timeout	= 500;
var closetimer	= 0;
var ddmenuitem	= 0;
// open hidden layer
function mopen(id)
{	
	// cancel close timer
	mcancelclosetime();
	// close old layer
	if(ddmenuitem) ddmenuitem.style.visibility = 'hidden';
	// get new layer and show it
	ddmenuitem = document.getElementById(id);
	ddmenuitem.style.visibility = 'visible';
}
// close showed layer
function mclose()
{
	if(ddmenuitem) ddmenuitem.style.visibility = 'hidden';
}
// go close timer
function mclosetime()
{
	closetimer = window.setTimeout(mclose, timeout);
}
// cancel close timer
function mcancelclosetime()
{
	if(closetimer)
	{
		window.clearTimeout(closetimer);
		closetimer = null;
	}
}
// close layer when click-out
document.onclick = mclose; 
//Menu Script Ends

function isPostCode(oSrc, args)
{
    
     // checks cdn codes only
    entry = args.Value.trim();
    //alert(entry);
    
    strlen=entry.length;
    if(strlen!=7)
    {
        args.IsValid = false; 
        return;
    }
    entry=entry.toUpperCase(); //in case of lowercase
    //Check for legal characters,index starts at zero
    s1='ABCEGHJKLMNPRSTVXY';s2=s1+'WZ';d3='0123456789';
    
    
    if(s1.indexOf(entry.charAt(0))<0)
    {
        args.IsValid = false; 
        return;
    }
    if(d3.indexOf(entry.charAt(1))<0)
    {
        args.IsValid = false; 
        return;
    }
    if(s2.indexOf(entry.charAt(2))<0)
    {
        args.IsValid = false; 
        return;
    }
    if(entry.charAt(3) != '-')
    {
        
        args.IsValid = false; 
        return;
    }
    if(d3.indexOf(entry.charAt(4))<0)
    {
        args.IsValid = false; 
        return;
    }
    if(s2.indexOf(entry.charAt(5))<0)
    {
        args.IsValid = false; 
        return;
    }
    if(d3.indexOf(entry.charAt(6))<0)
    {
        args.IsValid = false; 
        return;
    }
    args.IsValid = true; 
    return;
}