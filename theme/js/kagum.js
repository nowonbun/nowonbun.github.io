// tooltip

$('i.fa').tooltip();

// owl carousel

$(document).ready(function() {
 
  	$("#owl-header").owlCarousel({
 
      	navigation : false, // Show next and prev buttons
      	slideSpeed : 300,
      	autoPlay: true,
      	pagination: false,
      	paginationSpeed : 1000,
      	singleItem:true
 
      	// "singleItem:true" is a shortcut for:
      	// items : 1, 
      	// itemsDesktop : false,
      	// itemsDesktopSmall : false,
      	// itemsTablet: false,
      	// itemsMobile : false
 
  	});
 
});