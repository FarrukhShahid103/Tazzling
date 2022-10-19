function UpdateBasket(process, item, status)
{
    var incholAjax = new IncholAJAX("/takeout/ajax.aspx?type=" + process + "&id=" + item + "&status=" + status + "&number=" + Math.random(), "basket");
    IncholAJAX.prototype.ProcessIncholOverride = ProcessLoadBasketHtml;    
    incholAjax.RemoteServer(incholAjax);
}

function ProcessLoadBasketHtml()
{
    var basketBody = $("basketBody");
    var text = this.responseText;
    if(basketBody != null && text != "")
    {
        basketBody.innerHTML = text;
    }
    return false;
}

function SetTopPosition(id, minTopObjId, maxTopObjId)
{
    var o = document.getElementById("" + id + "");
    var minTopObj = document.getElementById("" + minTopObjId + "");
    var maxTopObj = document.getElementById("" + maxTopObjId + "");
    
    if(o != null && minTopObj != null)
    {
        var scrollPos = getScrollTop();
        var minTop = getPosition(minTopObj).y;
        var maxTop = getPosition(maxTopObj).y - o.offsetHeight;
        var newTop = parseInt(scrollPos) + 100;
        if(newTop < minTop)
            newTop = minTop;
        if(newTop > maxTop)
            newTop = maxTop;
        o.style.top = newTop + "px";
    }
}

function getScrollTop()
{
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
    
    return scrollPos;
}

function getPosition(e){
	var left = 0;
	var top  = 0;
	while (e.offsetParent){
		left += e.offsetLeft;
		top  += e.offsetTop;
		e     = e.offsetParent;
	}

	left += e.offsetLeft;
	top  += e.offsetTop;

	return {x:left, y:top};
}

addWindowScrollEvent(ProcessScroll);
addWindowResizeEvent(ProcessScroll);

function ProcessScroll()
{
    SetTopPosition('BasketZone', 'RightItemList','ConfirmZone');
    SetTopPosition('MenuTypesList', 'RightItemList','ConfirmZone');
}