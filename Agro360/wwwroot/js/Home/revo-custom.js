(function ($) {
    "use strict";
    jQuery(document).ready(function() {

        /*==================================================================
        [ Revo1 ]*/
        var screenH1 = 0;
        var offsetArrow1 = 0;
        if ($(window).width() >= 992) {
            screenH1 = $(window).height();
            offsetArrow1 = 120;
        }
        else {
            screenH1 = $(window).height() - 70;
            offsetArrow1 = 30;
        }

        jQuery('#rev_slider_1').show().revolution({
            
            responsiveLevels: [1900, 992, 768, 576],
            gridwidth:[1900, 992, 768, 576],
            minHeight: screenH1,
            delay: 7000,

            sliderLayout: 'fullwidth',
            spinner: 'spinner2',

            navigation: {

                keyboardNavigation: 'on',
                keyboard_direction: 'horizontal',
                onHoverStop: 'off',

                touch: {
 
                    touchenabled: 'on',
                    swipe_threshold: 75,
                    swipe_min_touches: 1,
                    swipe_direction: 'horizontal',
                    drag_block_vertical: true
             
                },
 
                arrows: {
                    enable: true,
                    style: 'gyges',
                    hide_onmobile: true,
                    hide_onleave: true,
                    left: {
                        container: 'slider',
                        h_align: 'left',
                        v_align: 'center',
                        h_offset: offsetArrow1,
                        v_offset: 0
                    },
             
                    right: {
                        container: 'slider',
                        h_align: 'right',
                        v_align: 'center',
                        h_offset: offsetArrow1,
                        v_offset: 0
                    }
                },
 
                bullets: {
                    enable: false,
                    style: 'uranus',
                    tmp: '<span class="tp-bullet-inner"></span>',
                    hide_onleave: false,
                    h_align: 'center',
                    v_align: 'bottom',
                    h_offset: 0,
                    v_offset: 50,
                    space: 10
                }
            }
        });


        /*==================================================================
        [ Revo2 ]*/
        var screenH2 = 0;
        var offsetArrow2 = 0;
        if ($(window).width() >= 992) {
            screenH2 = $(window).height() - 123;
            offsetArrow2 = 100;
        }
        else {
            screenH2 = $(window).height() - 70;
            offsetArrow2 = 20;
        }

        if($(window).height() < 768 && $(window).width() >= 992) {
           screenH2 = $(window).height() - 90;
        }

        jQuery('#rev_slider_2').show().revolution({
            
            responsiveLevels: [1900, 992, 768, 576],
            gridwidth:[1900, 992, 768, 576],
            minHeight: screenH2,
            delay: 7000,

            sliderLayout: 'fullwidth',
            spinner: 'spinner2',

            navigation: {

                keyboardNavigation: 'on',
                keyboard_direction: 'horizontal',
                onHoverStop: 'off',

                touch: {
 
                    touchenabled: 'on',
                    swipe_threshold: 75,
                    swipe_min_touches: 1,
                    swipe_direction: 'horizontal',
                    drag_block_vertical: true
             
                },
 
                arrows: {
                    enable: false,
                    style: 'gyges',
                    hide_onmobile: true,
                    hide_onleave: true,
                    left: {
                        container: 'slider',
                        h_align: 'left',
                        v_align: 'center',
                        h_offset: offsetArrow2,
                        v_offset: 0
                    },
             
                    right: {
                        container: 'slider',
                        h_align: 'right',
                        v_align: 'center',
                        h_offset: offsetArrow2,
                        v_offset: 0
                    }
                },
 
                bullets: {
                    enable: true,
                    style: 'persephone',
                    tmp: '<div class="tp-bullet-inner"></div>',
                    hide_onleave: false,
                    h_align: 'center',
                    v_align: 'bottom',
                    h_offset: 0,
                    v_offset: 40,
                    space: 12
                }
            }
        });


        /*==================================================================
        [ Revo3 ]*/
        var screenH3 = 0;
        var offsetArrow3 = 0;
        if ($(window).width() >= 992) {
            screenH3 = $(window).height();
            offsetArrow3 = 120;
        }
        else {
            screenH3 = $(window).height() - 70;
            offsetArrow3 = 30;
        }

        jQuery('#rev_slider_3').show().revolution({
            
            responsiveLevels: [1200, 992, 768, 576],
            gridwidth:[1200, 992, 768, 576],
            minHeight: screenH3,
            delay: 7000,

            sliderLayout: 'fullwidth',
            spinner: 'spinner2',

            navigation: {

                keyboardNavigation: 'on',
                keyboard_direction: 'horizontal',
                onHoverStop: 'off',

                touch: {
 
                    touchenabled: 'on',
                    swipe_threshold: 75,
                    swipe_min_touches: 1,
                    swipe_direction: 'horizontal',
                    drag_block_vertical: true
             
                },
 
                arrows: {
                    enable: false,
                    style: 'gyges',
                    hide_onmobile: true,
                    hide_onleave: true,
                    left: {
                        container: 'slider',
                        h_align: 'left',
                        v_align: 'center',
                        h_offset: offsetArrow3,
                        v_offset: 0
                    },
             
                    right: {
                        container: 'slider',
                        h_align: 'right',
                        v_align: 'center',
                        h_offset: offsetArrow3,
                        v_offset: 0
                    }
                },
 
                bullets: {
                    enable: true,
                    style: 'hephaistos',
                    tmp: '<span class="tp-bullet-inner"></span>',
                    hide_onleave: false,
                    h_align: 'center',
                    v_align: 'bottom',
                    h_offset: 0,
                    v_offset: 50,
                    space: 16
                }
            }
        });



        /*==================================================================
        [ Revo4 ]*/
        var screenH4 = 0;
        var offsetArrow4 = 0;

        var offsetBullet4 = [0,0];
        var directionBullet4 = 'horizontal';
        var alignBullet4 = ['center','bottom']

        if ($(window).width() >= 992) {
            screenH4 = $(window).height() - 123;
            offsetArrow4 = 100;

            offsetBullet4 = [120,0];
            directionBullet4 = 'vertical';
            alignBullet4 = ['right','center']
        }
        else {
            screenH4 = $(window).height() - 70;
            offsetArrow4 = 20;

            offsetBullet4 = [0,20];
            directionBullet4 = 'horizontal';
            alignBullet4 = ['center','bottom']
        }

        if($(window).height() < 768 && $(window).width() >= 992) {
           screenH4 = $(window).height() - 90;
        }

        jQuery('#rev_slider_4').show().revolution({
            
            responsiveLevels: [1900, 992, 768, 576],
            gridwidth:[1900, 992, 768, 576],
            minHeight: screenH4,
            delay: 7000,

            sliderLayout: 'fullwidth',
            spinner: 'spinner2',

            navigation: {

                keyboardNavigation: 'on',
                keyboard_direction: 'horizontal',
                onHoverStop: 'off',

                touch: {
 
                    touchenabled: 'on',
                    swipe_threshold: 75,
                    swipe_min_touches: 1,
                    swipe_direction: 'horizontal',
                    drag_block_vertical: true
             
                },
 
                arrows: {
                    enable: false,
                    style: 'gyges',
                    hide_onmobile: true,
                    hide_onleave: true,
                    left: {
                        container: 'slider',
                        h_align: 'left',
                        v_align: 'center',
                        h_offset: offsetArrow4,
                        v_offset: 0
                    },
             
                    right: {
                        container: 'slider',
                        h_align: 'right',
                        v_align: 'center',
                        h_offset: offsetArrow4,
                        v_offset: 0
                    }
                },
 
                bullets: {
                    enable: true,
                    style: 'hermes',
                    direction: directionBullet4,
                    tmp: '<div class="tp-bullet-inner"></div>',
                    hide_onleave: false,
                    h_align: alignBullet4[0],
                    v_align: alignBullet4[1],
                    h_offset: offsetBullet4[0],
                    v_offset: offsetBullet4[1],
                    space: 12
                }
            }
        });

        /*==================================================================
        [ Revo5 ]*/
        var screenH5 = 0;
        var offsetArrow5 = 0;
        var offsetBullet5 = 0;
        if ($(window).width() >= 992) {
            screenH5 = $(window).height();
            offsetArrow5 = 120;
            offsetBullet5 = 50;
        }
        else {
            screenH5 = $(window).height() - 70;
            offsetArrow5 = 30;
            offsetBullet5 = 20;
        }

        jQuery('#rev_slider_5').show().revolution({
            
            responsiveLevels: [1900, 1200, 768, 576],
            gridwidth:[1900, 1200, 768, 576],
            minHeight: screenH5,
            delay: 7000,

            sliderLayout: 'fullwidth',
            spinner: 'spinner2',

            navigation: {

                keyboardNavigation: 'on',
                keyboard_direction: 'horizontal',
                onHoverStop: 'off',

                touch: {
 
                    touchenabled: 'on',
                    swipe_threshold: 75,
                    swipe_min_touches: 1,
                    swipe_direction: 'horizontal',
                    drag_block_vertical: true
             
                },
 
                arrows: {
                    enable: false,
                    style: 'gyges',
                    hide_onmobile: true,
                    hide_onleave: true,
                    left: {
                        container: 'slider',
                        h_align: 'left',
                        v_align: 'center',
                        h_offset: offsetArrow5,
                        v_offset: 0
                    },
             
                    right: {
                        container: 'slider',
                        h_align: 'right',
                        v_align: 'center',
                        h_offset: offsetArrow5,
                        v_offset: 0
                    }
                },
 
                bullets: {
                    enable: true,
                    style: 'hephaistos',
                    tmp: '<span class="tp-bullet-inner"></span>',
                    hide_onleave: false,
                    h_align: 'center',
                    v_align: 'bottom',
                    h_offset: 0,
                    v_offset: offsetBullet5,
                    space: 10
                }
            }
        });


        /*==================================================================
        [ Revo6 ]*/
        var screenH6 = 0;
        var offsetArrow6 = 0;
        if ($(window).width() >= 992) {
            screenH6 = $(window).height() - 123;
            offsetArrow6 = 100;
        }
        else {
            screenH6 = $(window).height() - 70;
            offsetArrow6 = 20;
        }

        if($(window).height() < 768 && $(window).width() >= 992) {
           screenH6 = $(window).height() - 90;
        }

        jQuery('#rev_slider_6').show().revolution({
            
            responsiveLevels: [1200, 992, 768, 576],
            gridwidth:[1200, 992, 768, 576],
            minHeight: screenH6,
            delay: 7000,

            sliderLayout: 'fullwidth',
            spinner: 'spinner2',

            navigation: {

                keyboardNavigation: 'on',
                keyboard_direction: 'horizontal',
                onHoverStop: 'off',

                touch: {
 
                    touchenabled: 'on',
                    swipe_threshold: 75,
                    swipe_min_touches: 1,
                    swipe_direction: 'horizontal',
                    drag_block_vertical: true
             
                },
 
                arrows: {
                    enable: false,
                    style: 'gyges',
                    hide_onmobile: true,
                    hide_onleave: true,
                    left: {
                        container: 'slider',
                        h_align: 'left',
                        v_align: 'center',
                        h_offset: offsetArrow6,
                        v_offset: 0
                    },
             
                    right: {
                        container: 'slider',
                        h_align: 'right',
                        v_align: 'center',
                        h_offset: offsetArrow6,
                        v_offset: 0
                    }
                },
 
                bullets: {
                    enable: true,
                    style: 'hephaistos',
                    tmp: '<span class="tp-bullet-inner"></span>',
                    hide_onleave: false,
                    h_align: 'center',
                    v_align: 'bottom',
                    h_offset: 0,
                    v_offset: 50,
                    space: 10
                }
            }
        });




    });

})
(jQuery);