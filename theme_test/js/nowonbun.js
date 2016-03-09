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
	dom.css("width",$(window).width());
	dom.css("height",$(window).height());
	dom.css("left",-$(window).width());
	dom.show();
	if(dom.hasClass("on")){
		dom.removeClass("on");
	}
}
function menu(){
	dom = $("aside");
	if(dom.hasClass("on")){
		dom.removeClass("on");
		$("aside").css("left",-$(window).width());
		$('html').css("overflow","auto");
	}else{
		dom.addClass("on");
		$("aside").css("left",0);
		$('html').css("overflow","hidden");
	}
}
