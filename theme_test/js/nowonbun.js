
/*OnLoad*/
$(function(){
	initMenu();
	initAside();
});
/*onResize*/
$(window).resize(function(){
	initAside();
});
/*스크롤링시 헤더처리*/
var lastScroll = 0;
$(window).scroll(function(event){
	var st = $(this).scrollTop();
	if (st > lastScroll && st > 30){
		$("header").css("width","");
		$("header").css("position","static");
	}else{
		$("header").css("position","fixed");
		$("header").css("width",$(window).width());
	}
	lastScroll = st;
});
/*초기화*/
function initMenu(){
	/*메뉴 만들기*/
	temp = $("ul.category_list").html();
	/*탑메뉴 삭제*/
	/*$("ul.top-nav").html(temp);*/
	$("ul.side-nav").html(temp);
	/*모바일 메뉴에서 중간 버튼 클릭 없애기*/
	/*
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
	});*/
	/*사이드 메뉴 하위가 있을경우 위 아래 이미지생성*/
	$("div.side-list > ul.side-nav > li > ul").parent().children("a").append("<a href='#' class='glyphicon glyphicon-triangle-bottom pull-right' aria-hidden='true' onclick='openSublist($(this));'></a>");
	/*사이드 메뉴 상위 메뉴 초기화*/
	changeClassState($("div.side-list > ul.side-nav ul"),false);
	/*사이드 메뉴에서 아이콘 메뉴 활성화 처리*/
	if($("div.tistorytoolbar").hasClass("tt_menubar_login")){
		/*비로그인시*/
		changeClassState($("ul.bs-glyphicons-list > li.logout"),true);
		changeClassState($("ul.bs-glyphicons-list > li.login"),false);
		changeClassState($("ul.bs-glyphicons-list > li.admin"),false);
		$("div#loginstate").addClass("logout");
	}else{
		/*로그인시*/
		changeClassState($("ul.bs-glyphicons-list > li.logout"),false);
		changeClassState($("ul.bs-glyphicons-list > li.login"),true);
		changeClassState($("ul.bs-glyphicons-list > li.admin"),false);
		$("div#loginstate").addClass("login");
		myUrl = $("input#myUrl").val();
		$("a.tt_menubar_link").each(function(){
			/*관리자 일경우*/
			if($(this).attr("href") === myUrl){
				$("div#loginstate").addClass("admin");
				changeClassState($("ul.bs-glyphicons-list > li.admin"),true);
			}
		});
	}
}
/*사이드 메뉴에서 서브리스트-누르면 들어가고 누르면 나오는 구조*/
function openSublist(obj){
	/*사이드 메뉴 에서 - 상위 메뉴 버튼 이미지 교체*/
	if(obj.hasClass("glyphicon-triangle-bottom")){
		obj.removeClass("glyphicon-triangle-bottom");
		obj.addClass("glyphicon-triangle-top");
	}else if(obj.hasClass("glyphicon-triangle-top")){
		obj.removeClass("glyphicon-triangle-top");
		obj.addClass("glyphicon-triangle-bottom");
	}
	/* 하위 메뉴 펼침 접힘*/
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
	/*사이드 메뉴 높이 설정*/
	dom.css("height",$(window).height());
	sideOff(dom);
	/*이미지 집어넣기*/
	$("aside img").prop("src",$("div#blogImage").html());
	
}
/*메뉴 상태.. 처음 누르면 메뉴가 나옴. 다시 누르면 메뉴가 들어감*/
function menu(){
	dom = $("aside");
	if(dom.hasClass("on")){
		sideOff(dom);
	}else{
		sideOn(dom);
	}
}
/*사이드바 없어질 때 처리*/
function sideOff(dom){
	changeClassState(dom,false);
	changeClassState($("main"),false);
	/*화면이 작을때 튀어나오는 버그처리*/
	dom.css("left",$(window).width() > 300 ? -$(window).width() : -300);
	dom.css("width",0);
	dom.hide();
	/*화면 깨짐 버그처리*/
	$('body').css("width","");
	$('html').css("position","static");
	/*화면 고정처리*/
	$('html').css("overflow","auto");
}
/*사이드바 생길 때 처리*/
function sideOn(dom){
	/*화면 고정처리*/
	$('html').css("overflow","hidden");
	/*화면 깨짐 버그처리*/
	$('html').css("position","fixed");
	$('body').css("width",$(window).width());
	/*화면이 작을때 튀어나오는 버그처리*/
	dom.show();
	dom.css("width",$(window).width());
	dom.css("left",0);
	/*하단 아이콘처리*/
	$("aside > div > div.side-list").css("min-height",$("aside> div").height()-210);
	changeClassState($("main"),true);
	changeClassState(dom,true);
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
