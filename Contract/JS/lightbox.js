var isIE = (document.all) ? true : false;
var isIE6 = isIE && ([/MSIE (\d)\.0/i.exec(navigator.userAgent)][0][1] == 6);
var Class = {
	create: function() {
		return function() { this.initialize.apply(this, arguments); }
	}
}
var Extend = function(destination, source) {
	for (var property in source) {
		destination[property] = source[property];
	}
}
var Bind = function(object, fun) {
	return function() {
		return fun.apply(object, arguments);
	}
}
var Each = function(list, fun){
	for (var i = 0, len = list.length; i < len; i++) { fun(list[i], i); }
};
var Contains = function(a, b){
	return a.contains ? a != b && a.contains(b) : !!(a.compareDocumentPosition(b) & 16);
}
var OverLay = Class.create();
OverLay.prototype = {
  initialize: function(options) {
	this.SetOptions(options);
	this.Lay = $(this.options.Lay) || document.body.insertBefore(document.createElement("div"), document.body.childNodes[0]);
	this.Color = this.options.Color;
	this.Opacity = parseInt(this.options.Opacity);
	this.zIndex = parseInt(this.options.zIndex);
	with(this.Lay.style){ display = "none"; zIndex = this.zIndex; left = top = 0; position = "fixed"; width = height = "100%"; }
	if(isIE6){
		this.Lay.style.position = "absolute";
		this._resize = Bind(this, function(){
			this.Lay.style.width = Math.max(document.documentElement.scrollWidth, document.documentElement.clientWidth) + "px";
			this.Lay.style.height = Math.max(document.documentElement.scrollHeight, document.documentElement.clientHeight) + "px";
		});
		this.Lay.innerHTML = '<iframe style="position:absolute;top:0;left:0;width:100%;height:100%;filter:alpha(opacity=0);"></iframe>'
	}
  },
  SetOptions: function(options) {
    this.options = {
		Lay:		null,
		Color:		"#fff",
		Opacity:	50,
		zIndex:		1000
    };
    Extend(this.options, options || {});
  },
  Show: function() {
	if(isIE6){ this._resize(); window.attachEvent("onresize", this._resize); }
	with(this.Lay.style){
		isIE ? filter = "alpha(opacity:" + this.Opacity + ")" : opacity = this.Opacity / 100;
		backgroundColor = this.Color; display = "block";
	}
  },
  Close: function() {
	this.Lay.style.display = "none";
	if(isIE6){ window.detachEvent("onresize", this._resize); }
  }
};



var LightBox = Class.create();
LightBox.prototype = {
  initialize: function(box, options) {
	this.Box = $(box);
	this.OverLay = new OverLay(options);
	this.SetOptions(options);
	this.Fixed = !!this.options.Fixed;
	this.Over = !!this.options.Over;
	this.Center = !!this.options.Center;
	this.onShow = this.options.onShow;
	this.Box.style.zIndex = this.OverLay.zIndex + 1;
	this.Box.style.display = "none";
	if(isIE6){
		this._top = this._left = 0; this._select = [];
		this._fixed = Bind(this, function(){ this.Center ? this.SetCenter() : this.SetFixed(); });
	}
  },
  SetOptions: function(options) {
    this.options = {
		Over:	true,
		Fixed:	false,
		Center:	false,
		onShow:	function(){}
	};
    Extend(this.options, options || {});
  },
  SetFixed: function(){
	this.Box.style.top = document.documentElement.scrollTop - this._top + this.Box.offsetTop + "px";
	this.Box.style.left = document.documentElement.scrollLeft - this._left + this.Box.offsetLeft + "px";
	
	this._top = document.documentElement.scrollTop; this._left = document.documentElement.scrollLeft;
  },
  SetCenter: function(){
	this.Box.style.marginTop = document.documentElement.scrollTop - this.Box.offsetHeight / 2 + "px";
	this.Box.style.marginLeft = document.documentElement.scrollLeft - this.Box.offsetWidth / 2 + "px";
  },
  Show: function(options) {
	this.Box.style.position = this.Fixed && !isIE6 ? "fixed" : "absolute";
	this.Over && this.OverLay.Show();
	this.Box.style.display = "block";
	if(this.Center){
		this.Box.style.top = this.Box.style.left = "50%";
		if(this.Fixed){
			this.Box.style.marginTop = - this.Box.offsetHeight / 2 + "px";
			this.Box.style.marginLeft = - this.Box.offsetWidth / 2 + "px";
		}else{
			this.SetCenter();
		}
	}
	
	if(isIE6){
		if(!this.Over){
			this._select.length = 0;
			Each(document.getElementsByTagName("select"), Bind(this, function(o){
				if(!Contains(this.Box, o)){ o.style.visibility = "hidden"; this._select.push(o); }
			}))
		}
		this.Center ? this.SetCenter() : this.Fixed && this.SetFixed();
		this.Fixed && window.attachEvent("onscroll", this._fixed);
	}

	this.onShow();
  },
  Close: function() {
	this.Box.style.display = "none";
	this.OverLay.Close();
	if(isIE6){
		window.detachEvent("onscroll", this._fixed);
		Each(this._select, function(o){ o.style.visibility = "visible"; });
	}
  }
};
