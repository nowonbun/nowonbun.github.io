$(function(){
	temp = $("ul.category_list").html();
	$("ul.top-nav").html(temp);
	$("ul.side-nav").html(temp);
	$("ul.navbar-nav ul").addClass("none");
	$("ul.navbar-nav > li > a").prop("href","#");
	$("ul.navbar-nav > li > a").addClass("dropdown");
	$("a.dropdown").bind("click",function(){
		dom = $(this).parent().children("ul");
		if(dom.hasClass("none")){
			dom.removeClass("none");
		}else{
			dom.addClass("none");
		}
	});
	initAside();
});
$(window).resize(function(){
	initAside();
});
function initAside(){
	dom = $("aside");
	if(dom.hasClass("on")){
		dom.removeClass("on");
	}
	dom.css("height",$(window).height());
	dom.css("left",$(window).width() > 300 ? -$(window).width() : -300);
	dom.css("width",0);
	dom.hide();
	$('html').css("overflow","auto");
}
function menu(){
	dom = $("aside");
	if(dom.hasClass("on")){
		changeClassState(dom,false);
		changeClassState($("main"),false);
		dom.css("left",$(window).width() > 300 ? -$(window).width() : -300);
		dom.css("width",0);
		dom.hide();
		$('html').css("overflow","auto");
	}else{
		$('html').css("overflow","hidden");
		dom.show();
		dom.css("width",$(window).width());
		dom.css("left",0);
		changeClassState($("main"),true);
		changeClassState(dom,true);
	}
}
function changeClassState(dom,state){
	if(dom.hasClass("on")){
		dom.removeClass("on");
	}
	if(dom.hasClass("off")){
		dom.removeClass("off");
	}
	dom.addClass(state?"on","off");
}

