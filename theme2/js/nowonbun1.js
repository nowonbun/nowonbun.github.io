$(function(){
	temp = $("ul.category_list").html();
	console.log(temp);
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
});
function menu(){
	dom = $("aside");
	if(dom.hasClass("on")){
		dom.removeClass("on");
		dom.addClass("off");
		$("div.mainBody").addClass("mainBodyOff");
		$("div.mainBody").removeClass("mainBodyOn");
	}else{
		dom.removeClass("off");
		dom.addClass("on");
		$("div.mainBody").addClass("mainBodyOn");
		$("div.mainBody").removeClass("mainBodyOff");
	}
}
