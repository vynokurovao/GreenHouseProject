(function ($) {

    $.GreenHouse = $.GreenHouse || {};

    $.extend($.GreenHouse, {

        btnToday_Click: function ()
        {
            $('#datetimepicker12').data
        },

        btnToggleCalendar_Click: function (elem)
        {
            element = $(elem);
            $('#calendarContainer').toggle();

            if ($('#table_container').hasClass('col-xs-6'))
            {
                $('#table_container').removeClass('col-xs-6').addClass('col-xs-8');
                element.html('Показать');
            }
            else if ($('#table_container').hasClass('col-xs-8'))
            {
                $('#table_container').removeClass('col-xs-8').addClass('col-xs-6');
                element.html('Скрыть');
            }
        },

        bookRoom: function(id) {
            var td = $("#" + id);
            var content = td.next().find('textarea').val();

            $.ajax('/Daily/BookRoom', {
                method: "POST",
                data: { date: id, content: content },
                success: function(data) {
                    console.log(data);
                }
            });

            td.popover('hide');
        }
    });
})(jQuery);