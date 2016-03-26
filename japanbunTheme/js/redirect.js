/*메인 화면 리다이렉트*/
var url = decodeURIComponent(location.href);
url = url.replace('http://','').replace('https://','');
url = url.split('#')[0];
url = url.split('?')[0];
url = url.split('/');
if(url[1] == '') { 
	location.href = '/notice/2'; 
}
