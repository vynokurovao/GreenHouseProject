(function ($) {

    $.GreenHouse = $.GreenHouse || {};

    $.extend($.GreenHouse, {

        btnToday_Click: function () {
            $('#datetimepicker12').data
        },

        btnToggleCalendar_Click: function (elem) {
            element = $(elem);
            $('#calendarContainer').toggle();

            if ($('#table_container').hasClass('col-xs-7')) {
                $('#table_container').removeClass('col-xs-7').addClass('col-xs-11');
                element.html('Показать');
            }
            else if ($('#table_container').hasClass('col-xs-11')) {
                $('#table_container').removeClass('col-xs-11').addClass('col-xs-7');
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