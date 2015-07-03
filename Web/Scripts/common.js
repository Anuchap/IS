var loader = {
    options: {
        icon: baseUrl + 'Content/images/loader.gif'
    },
    showWaitBlock: function () {
        $.blockUI({
            css: {
                border: 'none',
                backgroundColor: '',
            },
            message: '<img src="' + loader.options.icon + '"/>'
        });

    },
    hideWaitBlock: function () {
        $.unblockUI();
    }
};

var http = {
    get: function (url, callback, blockui) {
        if (blockui)
            loader.showWaitBlock();
        $.ajax({
            url: url,
            dataType: 'json',
            cache: false,
            success: function (json) {
                loader.hideWaitBlock();
                if (json.exception) {
                    bootbox.alert('<h1 class="text-danger">' + json.exception + '<h1>');
                    return;
                }
                callback(json);
            },
            error: function (e) {
                loader.hideWaitBlock();
                callback(e.responseText);
            }
        });
    },
    post: function (url, data, callback, blockui) {
        if (blockui)
            loader.showWaitBlock();
        $.ajax({
            url: url,
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            cache: false,
            success: function (json) {
                loader.hideWaitBlock();
                if (json.exception) {
                    bootbox.alert('<h1 class="text-danger">' + json.exception + '<h1>');
                    return;
                }
                callback(json);
            },
            error: function (e) {
                loader.hideWaitBlock();
                callback(e.responseText);
            }
        });
    }
};

var standardFormat = 'YYYY-MM-DD HH:mm';
var displayFormat = 'DD/MM/YYYY HH:mm';

function log(msg) {
    console.log(msg);
}

ko.bindingHandlers.date = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor();
        var allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        // Date formats: http://momentjs.com/docs/#/displaying/format/
        var pattern = allBindings.format || 'DD/MM/YYYY';

        var output = "-";
        if (valueUnwrapped !== null && valueUnwrapped !== undefined) {
            output = moment(valueUnwrapped).format(pattern);
        }

        if ($(element).is("input") === true) {
            $(element).val(output);
        } else {
            $(element).text(output);
        }
    }
};

ko.bindingHandlers.datetimepicker = {
    init: function (element, valueAccessor) {
        $(element).parent().datetimepicker();

        ko.utils.registerEventHandler($(element).parent(), "change.dp", function () {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                var thedate = $(element).parent().data("DateTimePicker").getDate();
                value(thedate);
            }
        });
    },
    update: function (element, valueAccessor) {
        var widget = $(element).parent().data("DateTimePicker");
        //when the view model is updated, update the widget
        var thedate = ko.utils.unwrapObservable(valueAccessor());
        widget.setDate(thedate);
    }
};

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor) {
        $(element).parent().datetimepicker({
            pickTime: false
        });

        ko.utils.registerEventHandler($(element).parent(), "change.dp", function () {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                var thedate = $(element).parent().data("DateTimePicker").getDate();
                value(thedate);
            }
        });
    },
    update: function (element, valueAccessor) {
        var widget = $(element).parent().data("DateTimePicker");
        //when the view model is updated, update the widget
        var thedate = ko.utils.unwrapObservable(valueAccessor());
        widget.setDate(thedate);
    }
};

ko.bindingHandlers.numeric = {
    init: function (element) {
        $(element).on('keydown', function (event) {
            // Allow: backspace, delete, tab, escape, enter, and dash
            if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 || event.keyCode == 109 || event.keyCode == 189 ||
                // Allow: Ctrl+A
                (event.keyCode == 65 && event.ctrlKey === true) ||
                // Allow: Ctrl+V
                (event.keyCode == 86 && event.ctrlKey === true) ||
                // Allow: Ctrl+C
                (event.keyCode == 67 && event.ctrlKey === true) ||
                // Allow: . ,
                (event.keyCode == 188 || event.keyCode == 190 || event.keyCode == 110) ||
                // Allow: home, end, left, right
                (event.keyCode >= 35 && event.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            } else {
                // Ensure that it is a number and stop the keypress
                if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                    event.preventDefault();
                }
            }
        });
    }
};

ko.bindingHandlers.nextFocus = {
    init: function (element) {
        $(element).on('click', function (event) {
            event.preventDefault();
            $(this).select();
        }).on('keydown', function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                var next = $(this).parent().next().find('input');
                if (next.length) {
                    next.focus().select();
                } else {
                    $(this).parent().parent().next('tr').find('td > input:first').click().focus();
                }
            } else if (event.keyCode == 38) {
                $(this).parent().parent().prev('tr').find('td:eq(' + $(this).parent().index() + ') > input').click().focus();
            } else if (event.keyCode == 40) {
                $(this).parent().parent().next('tr').find('td:eq(' + $(this).parent().index() + ') > input').click().focus();
            }
        });
    }
};

