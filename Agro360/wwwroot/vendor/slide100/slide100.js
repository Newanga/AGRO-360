(function($){
	"use strict";
    $.fn.extend({ 
         
        slide100: function(options) {
            var defaults = {
            	autoPlay: "true",

            	timeAuto: 3000,

                deLay: 600,

				linkIMG: [],

				linkThumb: []
            }
 
            var options =  $.extend(defaults, options);
 
            return this.each(function() {

				var ob = $(this);
				var auto = options.autoPlay; 
				var timeDelay = options.deLay;
				var linkIMG = options.linkIMG;
				var linkIMGthumb = options.linkThumb;

				var btnBack = $(ob).find('.prev-slide-100');
				var btnNext = $(ob).find('.next-slide-100');

				

				var numOfPic = linkIMG.length;

				var linkIMGsub = [];
				linkIMGsub[0] = linkIMGthumb.slice();
				linkIMGsub[0].push(linkIMGsub[0][0]);
				linkIMGsub[0].shift();

				for(var i=1; i<numOfPic; i++) {
					linkIMGsub[i] = linkIMGsub[i-1].slice();
					linkIMGsub[i].push(linkIMGsub[i][0]);
					linkIMGsub[i].shift();
				}


				$(ob).find('.main-frame, .sub-frame').each(function(){
					var srcPic = $(this).find('.main-pic img').attr('src');
					$(this).prepend('<div class="fix-frame"><img src="'+srcPic+'" alt="IMG"></div>');
				});
				


				/* Clone Element */
				var wWrapMainPic = (linkIMG.length*2)*100 +'%';
				var leftWrapMainPic = (linkIMG.length-1)*(-100) +'%';

				$(ob).find('.wrap-main-pic').css('width',wWrapMainPic);
				$(ob).find('.wrap-main-pic').css('left',leftWrapMainPic);

				var wMainPic = "calc(100% / " + (linkIMG.length*2) + ")";
				$(ob).find('.wrap-main-pic .main-pic').css('width',wMainPic);


				for(var i=1; i<numOfPic; i++) {
					var cloneMainPic = '<div class="main-pic"><img src="'+ linkIMG [i] +'" alt="IMG-SLIDE"></div>'
					$(ob).find('.main-frame .wrap-main-pic').prepend(cloneMainPic);
				}

				for(var i=numOfPic-1; i>=0; i--) {
					var cloneMainPic = '<div class="main-pic"><img src="'+ linkIMG [i] +'" alt="IMG-SLIDE"></div>'
					$(ob).find('.main-frame .wrap-main-pic').append(cloneMainPic);
				}

				for(var i=1; i<numOfPic; i++) {

					for(var j=0; j<numOfPic; j++){
						var cloneMainPic = '<div class="main-pic"><img src="'+ linkIMGsub[i][j] +'" alt="IMG-SLIDE"></div>'
						$(ob).find('.sub-frame.sub-'+i+' .wrap-main-pic').append(cloneMainPic);
					}

					for(var j=numOfPic-2; j>=0; j--) {
						var cloneMainPic = '<div class="main-pic"><img src="'+ linkIMGsub[i][j] +'" alt="IMG-SLIDE"></div>'
						$(ob).find('.sub-frame.sub-'+i+' .wrap-main-pic').prepend(cloneMainPic);
					}	
					
				}

				var wMainPic = "calc(100% / "+(linkIMG.length*2)+")";
				$(ob).find('.wrap-main-pic .main-pic').css('width',wMainPic);

				
				
				function nextSlide(numOfTurn, deLayRun) {

					/* Next Main Slide */
					$(ob).find('.main-frame .wrap-main-pic').css('left',(linkIMG.length-1+numOfTurn)*(-100) +'%');
					$(ob).find('.main-frame .wrap-main-pic').animate({left: leftWrapMainPic},deLayRun);

					for(var i=0; i<numOfTurn; i++) {
						$(ob).find('.main-frame .wrap-main-pic').each(function(){
							$(this).find('.main-pic:last-child').clone().prependTo(this);
							$(this).find('.main-pic:last-child').remove();
						});
					}

					/* Back Sub Slide */
					$(ob).find('.sub-frame .wrap-main-pic').css('left',(linkIMG.length-1-numOfTurn)*(-100) +'%');
					$(ob).find('.sub-frame .wrap-main-pic').animate({left: leftWrapMainPic},deLayRun);

					for(var i=0; i<numOfTurn; i++) {
						$(ob).find('.sub-frame .wrap-main-pic').each(function(){
							$(this).find('.main-pic:first-child').clone().appendTo(this);
							$(this).find('.main-pic:first-child').remove();
						});
					}

				}

				function backSlide(numOfTurn, deLayRun) {

					/* Back Main Slide */
					$(ob).find('.main-frame .wrap-main-pic').css('left',(linkIMG.length-1-numOfTurn)*(-100) +'%');
					$(ob).find('.main-frame .wrap-main-pic').animate({left: leftWrapMainPic},deLayRun);

					for(var i=0; i<numOfTurn; i++) {
						$(ob).find('.main-frame .wrap-main-pic').each(function(){
							$(this).find('.main-pic:first-child').clone().appendTo(this);
							$(this).find('.main-pic:first-child').remove();
						});
					}

					/* Next Sub Slide */
					$(ob).find('.sub-frame .wrap-main-pic').css('left',(linkIMG.length-1+numOfTurn)*(-100) +'%');
					$(ob).find('.sub-frame .wrap-main-pic').animate({left: leftWrapMainPic},deLayRun);

					for(var i=0; i<numOfTurn; i++) {
						$(ob).find('.sub-frame .wrap-main-pic').each(function(){
							$(this).find('.main-pic:last-child').clone().prependTo(this);
							$(this).find('.main-pic:last-child').remove();
						});
					}

				}



				function whenPushBtn(deLay) {
					auto = "false";
					$(ob).find('.my-arrow.next').off('click');
					$(ob).find('.my-arrow.back').off('click');
					$(ob).find('.btn-sub-frame.btn-1').off('click');
					$(ob).find('.btn-sub-frame.btn-2').off('click');
					$(ob).find('.btn-sub-frame.btn-3').off('click');
					$(ob).find('.btn-sub-frame.btn-4').off('click');
					$(ob).find('.btn-sub-frame.btn-5').off('click');
					$(ob).find('.btn-sub-frame.btn-6').off('click');

					setTimeout(function(){
						auto = options.autoPlay;
						addEvent();
					},deLay+100);
				}

				
				var addEvent = function(){
					$(ob).find('.my-arrow.next').on('click', function(){
						whenPushBtn(timeDelay)
						nextSlide(1, timeDelay);
					});

					$(ob).find('.my-arrow.back').on('click', function(){
						whenPushBtn(timeDelay)
						backSlide(1, timeDelay);
					});


					$(ob).find('.btn-sub-frame.btn-1').on('click', function(){
						whenPushBtn(timeDelay)
							nextSlide(1, timeDelay);
					});

					$(ob).find('.btn-sub-frame.btn-2').on('click', function(){
						whenPushBtn(timeDelay*1.5)
							nextSlide(2, timeDelay*1.5);
					});

					$(ob).find('.btn-sub-frame.btn-3').on('click', function(){
						whenPushBtn(timeDelay*2)
							nextSlide(3, timeDelay*2);
					});

					$(ob).find('.btn-sub-frame.btn-4').on('click', function(){
						whenPushBtn(timeDelay*2.5)
							nextSlide(4, timeDelay*2.5);
					});

					$(ob).find('.btn-sub-frame.btn-5').on('click', function(){
						whenPushBtn(timeDelay*3)
							nextSlide(3, timeDelay*3);
					});

					$(ob).find('.btn-sub-frame.btn-6').on('click', function(){
						whenPushBtn(timeDelay*3.5)
							nextSlide(4, timeDelay*3.5);
					});
				}

				addEvent();

				var autoRun = setInterval(function(){
					if(auto === "left")backSlide(1, timeDelay);
					else if(auto === "true" || auto === "right")nextSlide(1, timeDelay);
				},options.timeAuto);

            });
        }
    });
     
})(jQuery);