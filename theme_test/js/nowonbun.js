/*OnLoad*/
$(function(){
	initMenu();
	initAside();
});
/*onResize*/
$(window).resize(function(){
	initAside();
});
/*초기화*/
function initMenu(){
	/*메뉴 만들기*/
	temp = $("ul.category_list").html();
	$("ul.top-nav").html(temp);
	$("ul.side-nav").html(temp);
	/*모바일 메뉴에서 중간 버튼 클릭 없애기*/
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
	/*사이드 메뉴 하위가 있을경우 생성*/
	$("div.side-list > ul.side-nav > li > ul").parent().children("a").append("<a href='#' class='glyphicon glyphicon-triangle-bottom pull-right' aria-hidden='true' onclick='openSublist($(this));'></a>");
	changeClassState($("div.side-list > ul.side-nav ul"),false);
	/*사이드 메뉴 아래 활성화 처리*/
	if($("div.tistorytoolbar").hasClass("tt_menubar_login")){
		changeClassState($("ul.bs-glyphicons-list > li.logout"),true);
		changeClassState($("ul.bs-glyphicons-list > li.login"),false);
		changeClassState($("ul.bs-glyphicons-list > li.admin"),false);
		$("div#loginstate").addClass("logout");
	}else{
		changeClassState($("ul.bs-glyphicons-list > li.logout"),false);
		changeClassState($("ul.bs-glyphicons-list > li.login"),true);
		changeClassState($("ul.bs-glyphicons-list > li.admin"),false);
		$("div#loginstate").addClass("login");
		myUrl = $("input#myUrl").val();
		$("a.tt_menubar_link").each(function(){
			if($(this).attr("href") === myUrl){
				$("div#loginstate").addClass("admin");
				changeClassState($("ul.bs-glyphicons-list > li.admin"),true);
			}
		});
	}
}
/*메뉴에서 서브리스트-누르면 들어가고 누르면 나오는 구조*/
function openSublist(obj){
	/*상위 메뉴 버튼 이미지 교체*/
	if(obj.hasClass("glyphicon-triangle-bottom")){
		obj.removeClass("glyphicon-triangle-bottom");
		obj.addClass("glyphicon-triangle-top");
	}else if(obj.hasClass("glyphicon-triangle-top")){
		obj.removeClass("glyphicon-triangle-top");
		obj.addClass("glyphicon-triangle-bottom");
	}
	dom = obj.parent().parent().children("ul");
	if(dom.hasClass("on")){
		changeClassState(dom,false);
	}else{
		changeClassState(dom,true);
	}
}
/*메뉴 초기 설정 - 메뉴숨기기,사이즈 변경때마다도 요청한다.(메뉴가 가끔씩 튀어나오는 버그때문에)*/
function initAside(){
	dom = $("aside");
	changeClassState(dom,false);
	changeClassState($("main"),false);
	dom.css("height",$(window).height());
	dom.css("left",$(window).width() > 300 ? -$(window).width() : -300);
	dom.css("width",0);
	dom.hide();
	$('html').css("overflow","auto");
	$("aside img").prop("src",$("div#blogImage").html());
	
}
/*메뉴 상태.. 처음 누르면 메뉴가 나옴. 다시 누르면 메뉴가 들어감*/
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
		$("aside > div > div.side-list").css("min-height",$("aside> div").height()-210);
		changeClassState($("main"),true);
		changeClassState(dom,true);
	}
}
/*클래스 상태 변경*/
function changeClassState(dom,state){
	if(dom.hasClass("on")){
		dom.removeClass("on");
	}
	if(dom.hasClass("off")){
		dom.removeClass("off");
	}
	dom.addClass(state?"on":"off");
}
/*로그인*/
function login(){
	if($("div#loginstate").hasClass("logout")){
		location.href='https://www.tistory.com/login';
	}
	return false;
}
/*로그아웃*/
function logout(){
	if($("div#loginstate").hasClass("login")){
		location.href='https://www.tistory.com/logout/?requestURI='+$("input#myUrl").val();
	}
	return false;
}
/*링크추가 팝업*/
function addLink(){
	if($("div#loginstate").hasClass("login")){
		window.open("/toolbar/popup/link/");
		//location.href="/toolbar/popup/link/";
	}
	return false;
}
/*글쓰기로 이동*/
function writeTistory(){
	if($("div#loginstate").hasClass("admin")){
		location.href="/admin/entry/post/";
	}
	return false;
}
/*관리 메뉴로 이동*/
function adminTistory(){
	if($("div#loginstate").hasClass("admin")){
		location.href="/admin/center/";
	}
	return false;
}
