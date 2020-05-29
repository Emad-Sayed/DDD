(function($) {
    "use strict"; // Start of use strict

    //Activate bootstrip tooltips
    $('[data-toggle="tooltip"]').tooltip();

    // $(".app-sidebar").niceScroll(); // let's do the magic! 😀
    var wow = new WOW({
        boxClass: 'wow', // animated element css class (default is wow)
        animateClass: 'animated', // animation css class (default is animated)
        offset: 0, // distance to the element when triggering the animation (default is 0)
        mobile: true, // trigger animations on mobile devices (default is true)
        live: true, // act on asynchronously loaded content (default is true)
        callback: function(box) {
            // the callback is fired every time an animation is started
            // the argument that is passed in is the DOM node being animated
        },
        scrollContainer: null // optional scroll container selector, otherwise use window
    });
    wow.init();

    function detectBrowserVersion() {
        var ua = navigator.userAgent,
            tem,
            M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];

        if (/trident/i.test(M[1])) {
            tem = /\brv[ :]+(\d+)/g.exec(ua) || [];

            return 'IE' + (tem[1] || '');
        }
        if (M[1] === 'Chrome') {
            tem = ua.match(/\bOPR\/(\d+)/)
            if (tem != null) return 'Opera ' + tem[1];
        }
        M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
        if ((tem = ua.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);

        return M.join('');
    };
    //Detect your browser version and set class name in html tag
    var _detectBrowser = detectBrowserVersion();
    $("html").addClass(_detectBrowser);
    //Go to Top
    $('.custom-nav').slimScroll({
        height: '',
        position: "left",
        allowPageScroll: true,
        disableFadeOut: true
    });
    $(".goTop").click(function() {

        $('html , body').animate({ scrollTop: 0 }, 5000)

    });
    $(".search-btn").click(function() {
        $(".app-search").addClass("open");
    });
    $(".close-search").click(function() {
        $(".app-search").removeClass("open");
    });
    $(".navbar-toggler").click(function() {
        $(".mobile-nav-backdrop").addClass("open");
    });
    $(".mobile-nav-backdrop").click(function(event) {
        $(".navbar-collapse").collapse('hide');
        $(".mobile-nav-backdrop").removeClass("open");
    });
    $('.collapse-header').on('click', function() {
        $('.collapse-header').not(this).each(function() {
            $(this).closest(".card").removeClass('opened');
        });
        $(this).closest(".card").toggleClass('opened');
    });

    // Off Canvas
    //  When user clicks on tab, this code will be executed
    $("[rel='off-canvas'].canvas-btn").click(function() {
        //  First remove class "active" from currently active tab
        $("[rel='off-canvas'].canvas-btn").removeClass('active');
        //  Now add class "active" to the selected/clicked tab
        $(this).addClass("active");

        //  Hide all tab content
        $(".off-canvas").removeClass("off-canvas-active");
        $(".off-canvas-overlay").removeClass("active");
        //$("<div class='off-canvas-overlay'></div>").appendTo("body");
        //  Here we get the href value of the selected tab
        var selected_tab = $(this).attr("href");
        $(".off-canvas-overlay").addClass("active");
        //  Show the selected tab content
        $(selected_tab).addClass("off-canvas-active");

        //  At the end, we add return false so that the click on the link is not executed
        return false;
    });

    $(".close-off-canvas , .off-canvas-overlay").click(function() {
        //  First remove class "active" from currently active tab
        $("[rel='off-canvas'].canvas-btn").removeClass('active');

        //  Now add class "active" to the selected/clicked tab
        $(this).addClass("active");

        //  Hide all tab content
        $(".off-canvas").removeClass("off-canvas-active");
        $(".off-canvas-overlay").removeClass("active");
        $(".off-canvas-overlay").removeClass("active");

        //  At the end, we add return false so that the click on the link is not executed
        return false;
    });


})(jQuery); // En of use strict