(function ($) {

    $.GreenHouse = $.GreenHouse || {};

    $(function () {

        $('[data-toggle="popover"]').popover({
            html: true
        });

        $('#datetimepicker12').datepicker({
            inline: true,
            sideBySide: true,
            language: 'ru',
            todayBtn: true
        });
    })

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
                $('#lenta').width(1000);
                $("#cont").animate({ left: "0px" }, 150);
                $('#table-container').removeClass('col-xs-6').addClass('col-xs-10');
                element.html('Показать');

            }
            else if ($('#table-container').hasClass('col-xs-10')) {
                $('#lenta').width(600);
                $('#table-container').removeClass('col-xs-10').addClass('col-xs-6');
                element.html('Скрыть');
            }
        },

        btnUpdateUserData_Click: function () {
            $('#user_data').hide('slow');
            $('#update_user_data').removeClass('hidden');
        },

        btnCancelUpdateUserData_Click: function () {
            $('#user_data').show('slow');
            $('#update_user_data').addClass('hidden');
        },

        btnUpdatePassword_Click: function () {
            $('#btn_new_pass_room').hide('slow');
            $('#new_password').removeClass('hidden');
        },

        btnCancelUpdatePassword_Click: function () {
            $('#btn_new_pass_room').show('slow');
            $('#new_password').addClass('hidden');
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