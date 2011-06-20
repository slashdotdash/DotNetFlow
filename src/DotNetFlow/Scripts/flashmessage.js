$.fn.flashMessage = function (options) {
    var target = this;
    options = $.extend({}, options, { timeout: 3000 });
    if (!options.message) {
        options.message = getFlashMessageFromCookie();
        deleteFlashMessageCookie();
    }
    if (options.message) {
        if (typeof options.message === "string") {
            target.html("<span>" + options.message + "</span>");
        } else {
            target.empty().append(options.message);
        }
    }

    if (target.children().length === 0) return;

    target.fadeIn().one("click", function () {
        $(this).fadeOut();
    });

    if (options.timeout > 0) {
        setTimeout(function () { target.fadeOut(); }, options.timeout);
    }

    return this;

    function getFlashMessageFromCookie() {
        return $.cookie("Flash.Notice");
    }

    function deleteFlashMessageCookie() {
        $.cookie("Flash.Notice", null, { path: '/' });
        $.cookie("Flash.Warning", null, { path: '/' });
        $.cookie("Flash.Message", null, { path: '/' });
    }
};