/*global $, alert, console, jQuery, Facebook*/

$(document).ready(function () {

   
    "use strict";
    
    var myImgHeight, myFixedNavbar, myScroll, lg,  scrollButton, filter_wrapper;
    
    
    // Slide Show images #Home 
    $('#home').backstretch([
        "images/home-bg-slideshow1.jpg",
        "images/home-bg-slideshow2.jpg",
        "images/home-bg-slideshow3.jpg"
    ],  {duration: 2000, fade: 750});
    
    
    $("#home .gradient-overlay").css({
        backgroundColor: "#c21500",
        background: "-webkit-linear-gradient(to left, #c21500 , #ffc500)",
        background: "linear-gradient(to left, #c21500 , #ffc500)"
    });
    
    
    
    $("#home .txt").css({color: "#FFF"});
         
    
    
    // height of Slide Show images #Home 
    $(window).resize(function () {
        
        $("#home .gradient-overlay").css({
            backgroundColor: "#c21500",
            background: "-webkit-linear-gradient(to left, #c21500 , #ffc500)",
            background: "linear-gradient(to left, #c21500 , #ffc500)"
        });
        
        
        // Center Text on  Slide Show images #Home 
        //$("#home .txt").css('paddingTop', ($(window).height() - $("#home .txt").height()) / 2);

        $("#home .txt").css({color: "#FFF"});


        $("html").niceScroll();

    });
    
    
    
    
    // Navbar Fixed
    myImgHeight = $("#home");
        
    myFixedNavbar = $(".navbar-default");
    
    $(window).scroll(function () {
        
        myScroll = $(window).scrollTop();
                
        if (myScroll >= myImgHeight.outerHeight()) {
            
            
            myFixedNavbar.addClass("fixed");
            
        } else {
        
            myFixedNavbar.removeClass("fixed");
        }
         
    });
    
    
    
    // Can also be used with $(document).ready()
    $(window).load(function () {
        
        $('.flexslider').flexslider({
            animation: "slide"
        });
    });
    
    
    $('.popup-youtube').magnificPopup({
        type: 'iframe'
    });
    

    
    // #Scroll-top
    scrollButton = $("#Scroll-top");

    $(window).scroll(function () {

        
        if ($(this).scrollTop() >= $(window).height()) {

            scrollButton.fadeIn(1000);
            
        } else {

            scrollButton.fadeOut(1000);
        }
        
        
        scrollButton.click(function () {
            
            $("html,body").animate({scrollTop: 0}, 2000);
        });

    });
    
    
    
    $("html").niceScroll();
    
    
});


/*  ************************************************************************************** */


$(document).ready(function () {

  /* Nivo lightbox
    -----------------------------------------------*/
    
    "use strict";
    
    $('#gallery .col-md-4 a').nivoLightbox({
        effect: 'fadeScale'
    });

});


/* ***************************************************************************************** */


/* Istope Portfolio
-----------------------------------------------*/
jQuery(document).ready(function ($) {
    
    "use strict";
    
    var filter_wrapper, $container, $imgs;

    if ($('.iso-box-wrapper').length > 0) {

        $container  = $('.iso-box-wrapper');
        $imgs     = $('.iso-box img');

        $container.imagesLoaded(function () {

            $container.isotope({
                layoutMode: 'fitRows',
                itemSelector: '.iso-box'
            });

            $imgs.load(function () {
                $container.isotope('reLayout');
            });

        });

      //filter items on button click

        $('.filter-wrapper li a').click(function () {

            var $this = $(this), filterValue = $this.attr('data-filter');

            $container.isotope({
                filter: filterValue,
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
            });

      // don't proceed if already selected 

            if ($this.hasClass('selected')) {
                return false;
            }

            filter_wrapper = $this.closest('.filter-wrapper');
            filter_wrapper.find('.selected').removeClass('selected');
            $this.addClass('selected');

            return false;
        });

    }

});



/* ******************************************************************************************** */

