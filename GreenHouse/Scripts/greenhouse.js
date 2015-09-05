(function ($) {

    $.GreenHouse = $.GreenHouse || {};

    $.extend($.GreenHouse, {       
        btnCalendarMode_Click: function(elem) {
			element = $(elem)
			
			var selectedDayTd = $("#datetimepicker12 td.active");
			
			if (selectedDayTd.length) {
				selectedDayTd.parent().toggleClass('activeWeek');
			}
			
			var mode = element.attr('data-mode');
			if (mode == 1) {
				element.text('День')
				mode = 0;
			} else {
				$(element).text('Неделя')
				mode = 1;
			}
			
			element.attr('data-mode', mode);
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