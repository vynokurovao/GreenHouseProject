$(function () {
    $('[data-toggle="popover"]').popover({
        html: true
    });

    $('#datetimepicker12').datepicker({
        inline: true,
        sideBySide: true,
        language: 'ru'
    });

    $('#datetimepicker').datepicker({
        inline: true,
        sideBySide: true,
        language: 'ru'
    });

    $('#datetimepicker12').on("changeDate", function (e) {

        var day = e.date.getDate();

        var month = e.date.getMonth() + 1;

        var year = e.date.getFullYear();

        var Date = { Date: day + '.' + month + '.' + year }

        $('#info').addClass("hidden");
        $('#info').html("");
        $('#info1').addClass("hidden");
        $('#info1').html("");

        $('#forDate').html('на ' + day + '.' + month + '.' + year);

        $.post("/Home/TableForDate", Date, null, "html").done(function (x) {
            $("#cont").html(x);
        });
    });

    $('#datetimepicker').on("changeDate", function(e) {

        var day = e.date.getDate();

        var month = e.date.getMonth() + 1;

        var year = e.date.getFullYear();

        var model = {
            date: 'на ' + day + '.' + month + '.' + year,
            auditorium: $("#room_name").html()
        }
        var interval = $('#btnWeekCalendar').html();

        $('#info').addClass("hidden");
        $('#info').html("");
        $('#info1').addClass("hidden");
        $('#info1').html("");

        if (interval != null) {

            if (interval == "Неделя") {
                $.post("/Home/RoomDate", model, null, "html").done(function(x) {
                    $("#cont").html(x);
                });
            } else {
                $.post("/Home/RoomWeek", model, null, "html").done(function(x) {
                    $("#cont").html(x);
                });
            }
        }

        $('#info').addClass("hidden");
        $('#info').html("");
        $('#info1').addClass("hidden");
        $('#info1').html("");

    });
});

(function ($) {

    $.GreenHouse = $.GreenHouse || {};

    $.extend($.GreenHouse, {
        ReservClick: function(year, month, day, hour, auditorium) {

            var purposeid = "#" + auditorium + "-" + hour;

            var purpose = $(purposeid).val();

            var view = 0;

            var interval = $('#btnWeekCalendar').html();

            if (interval != null) {

                if (interval == "Неделя") {
                    view = 1;
                } else {
                    view = 2;
                }
            }

            var model =
            {
                year: year,
                month: month,
                day: day,
                hour: hour,
                purpose: purpose,
                auditorium: auditorium,
                type: true,
                view: view
            };

            $.ajax({
                url: "/Home/AddReservation",
                type: "POST",
                data: model,
                dataType: "html",
                success: function(x) {
                    $("#cont").html(x);
                }
            });
        },

        LockClick: function(year, month, day, hour, auditorium) {

            var purposeid = "#" + auditorium + "-" + hour;

            var purpose = $(purposeid).val();

            var view = 0;

            var interval = $('#btnWeekCalendar').html();

            if (interval != null) {

                if (interval == "Неделя") {
                    view = 1;
                } else {
                    view = 2;
                }
            }

            var model =
            {
                year: year,
                month: month,
                day: day,
                hour: hour,
                purpose: purpose,
                auditorium: auditorium,
                type: false,
                view: view
            };
            $.ajax({
                url: "/Home/AddReservation",
                type: "POST",
                data: model,
                dataType: "html",
                success: function(x) {
                    $("#cont").html(x);
                }
            });

        },

        CancelReservationClick: function(id) {

            var view = 0;

            var model =
            {
                reservation: id,
                view: view
            };

            $.post("/Home/RemoveReservation", model, null, "html");

            $.ajax("/Cabinet/ChangePlace").done(function(x) {
                $("#change-place").html(x);
            });
        },

        CancelClick: function(id) {

            var view = 0;

            var interval = $('#btnWeekCalendar').html();

            if (interval != null) {
                if (interval == "Неделя") {
                    view = 1;
                } else {
                    view = 2;
                }
            }

            var model =
            {
                reservation: id,
                view: view
            };

            $.post("/Home/RemoveReservation", model, null, "html").done(function(x) {
                $("#cont").html(x);
            });
        },

        btnRight_Click: function() {
            var on_screen_pic = 9;

            var slider_diff = 60;

            var x = $("#cont").position();

            if (Math.abs(x.left) + slider_diff * on_screen_pic < $("#slider").width())
            {
                var newcoord = (x.left - slider_diff);

                if (Math.abs(newcoord) + slider_diff >= $("#slider").width())
                {
                    newcoord = -$("#slider").width() + slider_diff;
                }

                $("#cont").animate({ left: newcoord + "px" }, 150);
            }
        },
        btnLeft_Click: function() {

            var slider_diff = 60;

            var x = $("#cont").position();

            if (x.left <= 0) {
                var newcoord = (x.left + slider_diff);

                if (newcoord >= 0) {
                    newcoord = 0;
                }

                $("#cont").animate({ left: newcoord + "px" }, 150);
            }
        },

        btnToday_Click: function(year, month, day) {

            var date = day + '.' + month + '.' + year;

            $('#datetimepicker12').datepicker('setDate', date);
        },

        btnReservOnPeriod_Click: function (room, date, nowYear, nowMonth, nowDay, nowHour) {

            var interval = $('#btnWeekCalendar').html();

            var period = true;

            var hour = 9;

            var view = 0;

            if (interval != null) {
                if (interval == "Неделя") {
                    period = true;

                    view = 1;
                } else {
                    period = false;

                    view = 2;
                }
            }
            var day = date.getDate();

            var month = date.getMonth() + 1;

            var year = date.getFullYear();

            var Date = day + '.' + month + '.' + year;

            var model = {
                auditorium: room,
                period: period,
                date: Date
            }

            if (year == nowYear && month == nowMonth && day == nowDay) {
                hour = nowHour + 1;
            }

            $.post("/Room/IsCanBlockOnPeriod", model, null, "html").done(function (x) {
                   if (x == 2) {

                       $('#info').removeClass("hidden");
                       $('#info').html("Вы не можете заблокировать эту комнату, так как указаное время уже прошло.");
                   }
                   else if (x == 1) {

                       var model =
                       {
                           year: year,
                           month: month,
                           day: day,
                           hour: hour,
                           finish_hour: 22,
                           purpose: "",
                           auditorium_name: room,
                           type: false,
                           view: view
                       };
                       $.ajax({
                           url: "/Home/AddReservation",
                           type: "POST",
                           data: model,
                           dataType: "html",
                           success: function (result) {
                               $("#cont").html(result);
                           }
                       });

                   } else {

                       $('#info').removeClass("hidden");
                       $('#info').html("Вы не можете заблокировать эту комнату, так как для нее существует бронь.");
                       $('#info1').removeClass("hidden");
                       $('#info1').html("Отмените все запланиронные события в данной комнате и попробуйте еще раз.");
                   }
               });
           
        },

        btnRoomToday_Click: function (year, month, day) {

            var date = day + '.' + month + '.' + year;

            $('#datetimepicker').datepicker('setDate', date);
        },

        btnReview_Click: function() {

            var auditoriumName = $("#room-name").val();

            $('#btnWeekCalendar').html('Неделя');

            $('#calendar-week').removeClass('hidden');

            $('#calendar-close').addClass('hidden');

            var room= {
                Name:auditoriumName
            }

            $.getJSON("/Room/IsRoomExist", room).done(function (result) {

                if (result.IsValid) {
                    window.location.href = "/Room/ShowRoom?room=" + auditoriumName;
                } else {
                    $("#room-name").addClass("has-error");
                }
            });
        },

        btnWeekCalendar_Click: function (auditoriumName) {
            var date = $('#th-date').html();

            var model =
            {
                date: date,
                auditorium: auditoriumName
            };

            var value = $('#btnWeekCalendar').html();

            if (value == 'Неделя') {
                $('#btnWeekCalendar').html('День');

                $("#td").addClass("td-day");

                $.post("/Home/RoomWeek", model, "html").done(function(x) {
                    $("#cont").html(x);
                });
            } else {
                $('#btnWeekCalendar').html('Неделя');

                $("#td").removeClass("td-day");

                $.post("/Home/RoomDate", model, "html").done(function(x) {
                    $("#cont").html(x);
                });
            }
        },

        btnToggleCalendar_Click: function(elem) {

            element = $(elem);

            $('#calendarContainer').toggle();

            if ($('#table-container').hasClass('col-xs-7')) {
                $('#lenta').width(875);

                $("#cont").animate({ left: "0px" }, 150);

                $('#table-container').removeClass('col-xs-7').addClass('col-xs-10');

                element.html('Показать');
            } else if ($('#table-container').hasClass('col-xs-10')) {
                $('#lenta').width(600);

                $('#table-container').removeClass('col-xs-10').addClass('col-xs-7');

                element.html('Скрыть');
            }
        },

        btnSaveUserData_Click: function() {
           

            var model =
            {
                Email: $("#email").val(),

                FirstName: $("#firstname").val(),

                Surname: $("#surname").val()
            };

            $("#user_information").removeClass("hidden");

            $.getJSON("/Cabinet/ValidateUserData", model).done(function (result)
            {
                if (result.IsValid)
                {
                    $.post("/Cabinet/Save", model, "html").done(function (x)
                    {
                        $("#change-place").html(x);
                    });

                    $('#user_data').show();

                    $('#update_user_data').addClass('hidden');

                    $("#email").val("");

                    $("#firstname").val("");

                    $("#surname").val("");
                }
                else
                {
                    $("#user_information").html(result.Message);
                }
            });
        },

        btnUpdateUserData_Click: function() {
            $('#user_data').hide();

            $('#update_user_data').removeClass('hidden');
        },

        btnSavePassword_Click: function() {

            $("#user_information").addClass("hidden");

            var pass = $("#newpass").val();

            var confirm = $("#confirm").val();

            if (pass == "" || confirm == "") {
                $("#information").html("Заполните все поля");

                $("#information").removeClass("hidden");
            } else if (pass == confirm) {
                $('#user_data').show();

                $('#update_user_data').addClass('hidden');

                var model =
                {
                    password: pass,

                    confirm: confirm
                };

                $("#information").html("Пароль успешно изменен");

                $("#information").removeClass("hidden");

                $('#btn_new_pass_room').show();

                $('#new_password').addClass('hidden');

                $.post("/Cabinet/SavePassword", model, "html");
            } else {
                $("#information").html("Пароли не совпадают");

                $("#information").removeClass("hidden");
            }
        },

        btnCancelUpdateUserData_Click: function() {
            $('#user_data').show();
            $('#update_user_data').addClass('hidden');
            $("#information").addClass("hidden");
            $("#user_information").addClass("hidden");
        },

        btnUpdatePassword_Click: function() {
            $('#btn_new_pass_room').hide();
            $('#new_password').removeClass('hidden');
        },

        btnCancelUpdatePassword_Click: function() {
            $('#btn_new_pass_room').show();
            $('#new_password').addClass('hidden');
            $("#information").addClass("hidden");
            $("#user_information").addClass("hidden");
        },

        btnNewRoom_Click: function() {
            $('#btn_new_pass_room').hide();
            $('#new-room').removeClass('hidden');
        },

        btnCancelNewRoom_Click: function() {
            $('#btn_new_pass_room').show();
            $('#new-room').addClass('hidden');
            $("#information").addClass("hidden");
            $("#user_information").addClass("hidden");
        },

        btnAddNewRoom_Click: function() {
            $('#btn_new_pass_room').show();
            $('#new-room').addClass('hidden');
            $("#information").addClass("hidden");
            $("#user_information").addClass("hidden");
            window.location.href = "/Cabinet";
        },

        btnPast_Click: function () {
            $('#info').removeClass("hidden");
            $('#info').html("Резервировать комнату в прошлом нельзя");
        },

        btnHide_Info: function() {
            $('#info').addClass("hidden");
            $('#info').html("");
            $('#info1').addClass("hidden");
            $('#info1').html("");
            
            $(".popover").remove();
        },

        btnEditRoom: function () {
            $('#showInfo').addClass('hidden');
            $('#editInfo').removeClass('hidden');
        },

        btnSaveSelectedRoom: function () {

            var wifi = false;
            if ($("#wi-fi").is(':checked')) {
                wifi = true;
            }

            var mon = false;
            if ($("#mon").is(':checked')) {
                mon = true;
            }

            var proj = false;
            if ($("#proj").is(':checked')) {
                proj = true;
            }

            var mic = false;
            if ($("#mic").is(':checked')) {
                mic = true;
            }

            var newCapacity = $("#new-capacity").val();

            var roomName = $("#room_name").html();

            if (newCapacity == "" || newCapacity > 200) {
                $("#new-capacity").addClass("has-error");
            } else {

                var model = {
                    Name: roomName,
                    Capacity: newCapacity,
                    Wifi: wifi,
                    Projector: proj,
                    Microphone: mic,
                    Monitor: mon
                }

                $.post("/Room/ChangeRoom", model, "html").done(function(x) {
                    window.location.href = "/Room/ShowRoom?room=" + $("#room_name").html();
                });

                $('#showInfo').removeClass('hidden');
                $('#editInfo').addClass('hidden');
            }

        },

        ChangeReserv: function (id) {

            var newpurpose = $("#" + id).val();

            var view = 0;

            var interval = $('#btnWeekCalendar').html();

            if (interval != null) {
                if (interval == "Неделя") {

                    view = 1;
                } else {

                    view = 2;
                }
            }

            var model = {
                purpose: newpurpose,
                id: id,
                view:view
            }
            $.post("/Home/ChangeReservation", model, "html").done(function (x) {
                $("#cont").html(x);
            });
        }

    });
})(jQuery);

