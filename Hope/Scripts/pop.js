
$(document).ready(function () {
	_pop_background = $("#pop_background");
	_pop_content = $("#pop_content");
	_pop_content_body = $("#pop_content_body");
});

function _pop_center(el) {
	/*
	windowWidth = document.documentElement.clientWidth;
	windowHeight = document.documentElement.clientHeight;*/
	windowWidth = document.body.clientWidth;

	if (el)
		_top = el.offsetTop || el.pageY;
	else
		_top = "200";
	/*
	if(window.event){
	e = window.event;
	_top = e.screenY;
	}
	else{
	e = event;
	//_top = e.layerY;
	//_top = e.screenY;
	_top = e.pageY;
	}*/


	if (_top > 50);
	_top -= 50;


	popupHeight = _pop_content.height();
	popupWidth = _pop_content.width();
	_pop_content.css(
    {
    	"position": "absolute",
    	"top": _top,
    	"left": windowWidth / 2 - popupWidth / 2
    });
}
function pop_open(val, el) {
	_pop_content_body.html(val);
	_pop_center(el);
	_pop_background.css({ "opacity": "0.3" });
	_pop_background.fadeIn("slow");
	_pop_content.fadeIn("slow");
}
function pop_open_front(val, el) {
	_pop_content_body.html(val);
	_pop_center(el);
	_pop_content.fadeIn("slow");
}


function pop_close() {
	_pop_background.fadeOut("slow");
	_pop_content.fadeOut("slow");
	if (g_close_reload)
		window.location.reload();
}

function pop_reset(val, close_reload) {
	_pop_content_body.html(val);
	//_pop_center();
	g_close_reload = close_reload ? true : false;
}

var g_close_reload = false;