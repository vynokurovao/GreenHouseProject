(function ($) {

    $.GreenHouse = $.GreenHouse || {};

    $.extend($.GreenHouse, {

        btnRight_Click: function () {
            on_screen_pic = 9;
            slider_diff = 60;
            var x = $("#cont").position();
            if (Math.abs(x.left) + slider_diff * on_screen_pic < $("#slider").width()) {
                newcoord = (x.left - slider_diff);
                if (Math.abs(newcoord) + slider_diff >= $("#slider").width())
                    newcoord = -$("#slider").width() + slider_diff;
                $("#cont").animate({ left: newcoord + "px" }, 150);
            }
        },
        btnLeft_Click: function () {
            on_screen_pic = 9;
            slider_diff = 60;
            var x = $("#cont").position();
            if (x.left <= 0) {
                newcoord = (x.left + slider_diff);
                if (newcoord >= 0)
                    newcoord = 0;
                $("#cont").animate({ left: newcoord + "px" }, 150);
            }
        },

        btnToggleCalendar_Click: function (elem) {
            element = $(elem);
            $('#calendarContainer').toggle();

            if ($('#table-container').hasClass('col-xs-6')) {
                $('#table-container').removeClass('col-xs-6').addClass('col-xs-10');
                element.html('Показать');

            }
            else if ($('#table-container').hasClass('col-xs-10')) {
                $('#table-container').removeClass('col-xs-10').addClass('col-xs-6');
                element.html('Скрыть');
            }
        },

        bookRoom: function (id) {
            var td = $("#" + id);
            var content = td.next().find('textarea').val();

            $.ajax('/Daily/BookRoom', {
                method: "POST",
                data: { date: id, content: content },
                success: function (data) {
                    console.log(data);
                }
            });
            td.popover('hide');
        }
    });
})(jQuery);