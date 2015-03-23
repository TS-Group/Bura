/*
*   TotalSoft JavaScript library for www.crystalbet.com Gambling
*   www.totalsoft.ge
*/

function BoardEvent(params) {
    __doPostBack('ctl00$ctl00$GamblingContentPlaceHolder$BuraContentPlaceHolder$BoardEvent', params);
}


var timer;
var timerPlayerId;
var timerTimeElapsed;

function StartTimer(playerId, startingElapsed, interval) {
    timerPlayerId = playerId;
    timerTimeElapsed = startingElapsed;
    StopTimer();
    timer = setInterval("PlayerTimer()", interval);
}

function PlayerTimer() {
    timerTimeElapsed++;
    if (timerTimeElapsed >= 407) {
        StopTimer();
        //alert(timerPlayerId);
        return;
    }
    $("#TimerPlayer" + timerPlayerId + " div.RemainingContent").css("width", (407 - timerTimeElapsed).toString() + "px");
}

function StopTimer() {
    try {
        if (timer != null)
            clearInterval(timer);
    } catch (ex) {
        alert(ex);
    }
}

var selectedCardInterval;

function initBoard() {
    $(".Card").click(
        function () {
            if ($(this).hasClass("Selected")) {
                $(this).addClass("Moving");
                $(this).animate({ 'top': '600' }, 20, 'linear', function () { $(this).removeClass("Selected"); });
                $(this).removeClass("Moving");
            } else {
                $(this).addClass("Moving");
                $(this).animate({ 'top': '580' }, 50, 'linear', function () { $(this).addClass("Selected"); });
                $(this).removeClass("Moving");
            }
        });

    $(".Card").mouseover(function () { if (!$(this).hasClass("Selected")) $(this).animate({ 'top': '590' }, 'fast', 'swing'); });
    $(".Card").mouseout(function () { if (!$(this).hasClass("Selected")) $(this).animate({ 'top': '600' }, 'fast', 'swing'); });
    try {
        if (selectedCardInterval == null) {
            selectedCardInterval = setInterval('fixSelectedCards()', 100);
        }
    }
    catch (err)
    { }
}

function fixSelectedCards() {
    cards = $(".Card");
    for (index = 0; index < cards.length; index++) {
        if (!$(cards[index]).hasClass("Moving")) {
            if ($(cards[index]).hasClass("Selected")) {
                $(cards[index]).css('top', '580px');
            } else {
                if ($(cards[index]).css('top') == '580' || $(cards[index]).css('top') == '580px') {
                    $(cards[index]).css('top', '600px');
                }
            }
        }
    }
}

function getSelectedCards() {
    selectedCards = "";
    cards = $(".Card.Selected");
    for (index = 0; index < cards.length; index++) {
        selectedCards += $(cards[index]).attr("id") + ";";
    }
    return selectedCards;
}

function placeCards(takeCards) {
    if (takeCards) {
        BoardEvent("TakeCard:" + getSelectedCards());
    } else {
        BoardEvent("PassCard:" + getSelectedCards());
    }
}

function DoublingOffer() {
    BoardEvent("DoublingOffer");
}

// Lobby scripts

function InitCheckBox(id) {
    prefix = '#ctl00_ctl00_GamblingContentPlaceHolder_BuraContentPlaceHolder_';

    checkBoxId = prefix + id.replace("Label", "CheckBox");

    checkedAttr = $(checkBoxId).attr('checked');

    if (checkedAttr == true) {
        $(prefix + id).addClass('active');
    } else {
        $(prefix + id).removeClass('active');
    }

    $(prefix + id).unbind('click');
    $(prefix + id).click(function () {

        checkBoxId = prefix + id.replace("Label", "CheckBox");

        if ($(this).hasClass('check_label active')) {
            $(this).removeClass('active');
            $(checkBoxId).attr('checked', false);
        } else {
            $(this).addClass('active');
            $(checkBoxId).attr('checked', true);
        }
    });
}

function InitRadioBox(objects) {

    prefix = '#ctl00_ctl00_GamblingContentPlaceHolder_BuraContentPlaceHolder_';

    for (i = 0; i < objects.length; i++) {
        id = prefix + objects[i];

        radioBoxId = id.replace("Label", "RadioBox")

        if ($(radioBoxId).attr('checked') == true) {
            $(id).addClass('active');
        } else {
            $(id).removeClass('active');
        }


        $(id).unbind('click');
        $(id).click(function () {

            for (j = 0; j < objects.length; j++) {
                id = prefix + objects[j];
                radioBoxId = id.replace("Label", "RadioBox")
                $(id).removeClass('active');
                $(radioBoxId).attr('checked', false);
            }

            radioBoxId = $(this).attr('id').replace("Label", "RadioBox");

            if ($(this).hasClass('radio_label active')) {
                $(this).removeClass('active');

                $('#' + radioBoxId).attr('checked', false);
            } else {
                $(this).addClass('active');
                $('#' + radioBoxId).attr('checked', true);
            }

        });
    }
}

function DoJoinToGame(param) {
    __doPostBack("ctl00$GamblingContentPlaceHolder$JoinToGame", param);
}


function getScreenWidth() {
    var viewportwidth;
    var viewportheight;

    // the more standards compliant browsers (mozilla/netscape/opera/IE7) use window.innerWidth and window.innerHeight
    if (typeof window.innerWidth != 'undefined') {
        viewportwidth = window.innerWidth;
        viewportheight = window.innerHeight;
    }

    // IE6 in standards compliant mode (i.e. with a valid doctype as the first line in the document)
    else if (typeof document.documentElement != 'undefined'
            && typeof document.documentElement.clientWidth != 'undefined'
            && document.documentElement.clientWidth != 0) {
        viewportwidth = document.documentElement.clientWidth;
        viewportheight = document.documentElement.clientHeight;
    }

    // older versions of IE
    else {
        viewportwidth = document.getElementsByTagName('body')[0].clientWidth;
        viewportheight = document.getElementsByTagName('body')[0].clientHeight;
    }
    return viewportwidth;
}

function getScreenHeight() {
    var viewportwidth;
    var viewportheight;

    // the more standards compliant browsers (mozilla/netscape/opera/IE7) use window.innerWidth and window.innerHeight
    if (typeof window.innerWidth != 'undefined') {
        viewportwidth = window.innerWidth;
        viewportheight = window.innerHeight;
    }

    // IE6 in standards compliant mode (i.e. with a valid doctype as the first line in the document)
    else if (typeof document.documentElement != 'undefined'
            && typeof document.documentElement.clientWidth != 'undefined'
            && document.documentElement.clientWidth != 0) {
        viewportwidth = document.documentElement.clientWidth;
        viewportheight = document.documentElement.clientHeight;
    }

    // older versions of IE
    else {
        viewportwidth = document.getElementsByTagName('body')[0].clientWidth;
        viewportheight = document.getElementsByTagName('body')[0].clientHeight;
    }
    return viewportheight;
}


function resizeBoard() {
    boardHeight = 900;
    screenHeight = getScreenHeight() - 24;
    scale = screenHeight / boardHeight;
    $(".GamblingBoard").css("height", screenHeight + "px");
    $("#BuraBoard").css("margin-top", ((screenHeight - boardHeight) / 2) + "px");
    $("#BuraBoard")
        .css("transform", "scale(" + scale + ")")
        .css("-moz-transform", "scale(" + scale + ")")
        .css("-ms-transform", "scale(" + scale + ")")
        .css("-webkit-transform", "scale(" + scale + ")")
        .css("-o-transform", "scale(" + scale + ")");
}

$(document).ready(function () {
    resizeBoard();
    $(window).resize(function () { resizeBoard(); });
    $(".sound").click(function () { toggleSound(); });
    $(".iconExit").click(function () { BoardEvent("LeaveGame"); });
    /*
    window.onbeforeunload =
        function confirmExit() {
            if (confirm("Are you sure 2?")) {
                //BoardEvent("LeaveGame");
            } else {
                return false;
            }
        }
        */
    toggleSound();
    toggleSound();
});


function InitAvatars() {
    for (i = 0; i < 25; i++) {
        id = '#Avatar_' + i.toString();

        $(id).unbind('click');
        $(id).click(function () {

            for (j = 0; j < 25; j++) {
                id2 = '#Avatar_' + j.toString();

                $(id2).removeClass('selected');
            }

            imageId = this.id.substr(7, 2);
            $(this).addClass('selected');
            $('#ctl00_GamblingContentPlaceHolder_hfImageId').attr('value', parseInt(imageId) + 1);
        });
    }
}

function InitTableSorter() {
    for (i = 1; i < 8; i++) {
        id = '#header_' + i.toString();

        //   $(id).unbind('click');
        $(id).click(function () {

            if ($(this).hasClass('sortActive')) {
                $(this).removeClass('sortActive');
                $(this).addClass('sortUp');
            } else {
                $(this).removeClass('sortUp');
                $(this).addClass('sortActive');
            }


            for (j = 1; j < 8; j++) {
                id2 = '#header_' + j.toString();

                if ('#' + this.id != id2) {
                    $(id2).removeClass('sortActive');
                    $(id2).removeClass('sortUp');
                }
            }

        });
    }

}

function InitLobbyScripts() {    
    // Init avatars
    InitAvatars();

    $('input').ezMark();
    $('input[type="radio"]').ezMark({})
    //$('.opac').fadeOut();
    //$('#canv').click(function () { modal.close(); });
    try {
        // $("table").tablesorter({sortList: [[0,0], [1,0], [2,0], [3,0], [4,0], [5,0], [6,1]]});
        $("table").tablesorter();
    } catch(ex) {
    }
    //
    InitTableSorter();
}


var disableSound = false;

function playSound(soundName) {
    try {
        if (!disableSound) {
            var thissound = document.getElementById(soundName);
            thissound.Play();
        }
    } catch (ex) {
        // unable to play sound, do nothing
    }
}

function toggleSound() {
    disableSound = !disableSound;
    if (disableSound) {
        $(".soundOn").css("display", "none");
        $(".soundOff").css("display", "block");
    } else {
        $(".soundOn").css("display", "block");
        $(".soundOff").css("display", "none");
    }
}

