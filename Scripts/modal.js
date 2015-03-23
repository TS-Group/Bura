var modal = {
    elem: null,
    show: function (elm) {
        var canv = $("#canvas");
        canv.css("position", 'fixed');
        canv.css("top", '0px');
        canv.css("left", '0px');
        canv.css("zIndex", '999');
        canv.width($(window).width());
        canv.height($(window).height());


        var elem = $("#" + elm); modal.elem = elem;
        var w = parseInt(elem.width());
        var h = parseInt(elem.height());       

        elem.css("top", (parseInt($(window).height()) / 2) - (h / 2));
        elem.css("left", (parseInt($(window).width()) / 2) - (w / 2));
        elem.css("position", 'absolute');
        elem.css("zIndex", '1000');
        //alert(canv.attr("id"));
        canv.show();
        elem.show();
        canv.fadeTo(200, 0.8);
        elem.fadeIn(200);
    },
    close: function () {
        // popup-i fanjaris kontenti unda iyos HEAD / BODY tegebis gareshe, anu HTML blokis saxit
        modal.elem.fadeOut(200);
        $("#canvas").fadeOut(200);
    }

}