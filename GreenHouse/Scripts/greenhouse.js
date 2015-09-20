$(function () {
    $('[data-toggle="popover"]').popover({
        html: true
    });

    $('#datetimepicker12').datepicker({
        inline: true,
        sideBySide: true,
        language: 'ru'
    });

    $('#datetimepicker12').on("changeDate", function (e) {
        var day = e.date.getDate();
        var month = e.date.getMonth() + 1;
        var year = e.date.getFullYear();

        var Date = { Date: day + '.' + month + '.' + year }

        $('#forDate').html('на ' + day + '.' + month + '.' + year);

        $.post("/Home/TableForDate", Date, null, "html").done(function (x) {
            console.log(x);
            $("#cont").html(x);
        });
    });
});

(function ($) {

    $.GreenHouse = $.GreenHouse || {};

    $.extend($.GreenHouse, {

        ReservClick: function (year, month, day, hour, auditorium) {

            var purposeid = "#" + auditorium + "-" + hour;

            var purpose = $(purposeid).val();

            var model =
                {
                    year: year,
                    month: month,
                    day: day,
                    hour: hour,
                    purpose: purpose,
                    auditorium: auditorium,
                    type: 1
                };

            $.post("/Home/AddReservation", model, null, "html").done(function (x) {
                $("#cont").html(x);
            });
        },

        LockClick: function (year, month, day, hour, auditorium) {

            var purposeid = "#" + auditorium + "-" + hour;

            var purpose = $(purposeid).val();

            var model =
                {
                    year: year,
                    month: month,
                    day: day,
                    hour: hour,
                    purpose: purpose,
                    auditorium: auditorium,
                    type: 0
                };

            $.post("/Home/AddReservation", model, null, "html").done(function (x) {
                $("#cont").html(x);
            });
        },

        CancelClick: function (id) {
            var Id = {id: id };
            $.post("/Home/RemoveReservation", Id, null, "html").done(function (x) {
                $("#cont").html(x);
            });
        },
        
        btnRight_Click: function () {
            on_screen_pic = 9;
            slider_diff = 60;
            var x = $("#cont").position();
            if (Math.abs(x.left) + slider_diff * on_screen_pic < $("#slider").width())
            {
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

        btnToday_Click: function (year, month, day) {

            var date = day + '.' + month + '.'+ year 

            $('#datetimepicker12').datepicker('setDate', date);
        },

        btnReview_Click: function ()
        {
            $('#calendar-week').removeClass('hidden');

            $('#calendar-close').addClass('hidden');

           /* $.post("/Home/RoomDate", $('#calendar-week').val(), "html").done(function (x) {
                $("#cont").html(x);
            });*/
        },

        btnWeekCalendar_Click: function ()
        {
            var value = $('#btnWeekCalendar').html();
            if (value == 'День')
            {
                $('#btnWeekCalendar').html('Неделя');
            } else
            {
                $('#btnWeekCalendar').html('День');
            }
            
        },

        btnToggleCalendar_Click: function (elem) {
            element = $(elem);
            $('#calendarContainer').toggle();

            if ($('#table-container').hasClass('col-xs-7')) {
                $('#lenta').width(875);
                $("#cont").animate({ left: "0px" }, 150);
                $('#table-container').removeClass('col-xs-7').addClass('col-xs-10');
                element.html('Показать');

            }
            else if ($('#table-container').hasClass('col-xs-10')) {
                $('#lenta').width(600);
                $('#table-container').removeClass('col-xs-10').addClass('col-xs-7');
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