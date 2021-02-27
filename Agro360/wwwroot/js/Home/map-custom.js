(function ($) {
    "use strict";

        $(document).ready(function () {

            var selector_map = $('#google_map');
            var img_pin = selector_map.attr('data-pin');
            var data_map_x = selector_map.attr('data-map-x');
            var data_map_y = selector_map.attr('data-map-y');
            var scrollwhell = selector_map.attr('data-scrollwhell');
            var draggable = selector_map.attr('data-draggable');
            var map_zoom = selector_map.attr('data-zoom');

            if (img_pin === null) {
                img_pin = 'images/icons/location.png';
            }
            if (data_map_x === null || data_map_y === null) {
                data_map_x = 6.927079;
                data_map_y = 79.861244;
            }
            if (scrollwhell === null) {
                scrollwhell = 0;
            }

            if (draggable === null) {
                draggable = 0;
            }

            if (map_zoom === null) {
                map_zoom = 5;
            }

            var style = [];

            var latitude = 6.927079,
                longitude = 79.861244;

            var locations = [
                ['<div class="infobox"><h4>Hello</h4><p>Have a doubt?' +
                ' <br>Visit our office when you come come to colombo</p></div>'
                    , latitude, longitude, 2]
            ];

            if (selector_map !== undefined) {
                var map = new google.maps.Map(document.getElementById('google_map'), {
                    zoom: Number(map_zoom),
                    zoomControl: false,  
                    disableDoubleClickZoom: true,
                    scrollwheel: scrollwhell,
                    navigationControl: true,
                    mapTypeControl: false,
                    scaleControl: false,
                    draggable: draggable,
                    styles: style,
                    center: new google.maps.LatLng(latitude, longitude),
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });
            }

            var infowindow = new google.maps.InfoWindow();

            var marker, i;

            for (i = 0; i < locations.length; i++) {

                marker = new google.maps.Marker({
                    position: new google.maps.LatLng(locations[i][1], locations[i][2]),
                    map: map,
                    icon: img_pin
                });

                google.maps.event.addListener(marker, 'click', (function(marker, i) {
                    return function() {
                        infowindow.setContent(locations[i][0]);
                        infowindow.open(map, marker);
                    }
                })(marker, i));
            }

        });

})(jQuery);