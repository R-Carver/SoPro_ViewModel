function switchSidebar() {
    var elem = document.getElementById('sidebar-wrapper');

    if ($(elem).width() != 0) {
        hideSidebar();
    }
    else {
        showSidebar();
    }
    /*elem.style.width = (elem.style.width != "30px" ? "30px" : "250px");*/
}

function hideSidebar() {
    $("#sidebar-wrapper").animate({ 'width': '0px' }, 'normal');
    if ($(window).width() < 800) {
        $("#sidebar-menu-item").animate({ 'right': '-40px' }, 'normal');
        /*$("#breadcrumb").animate({ 'padding-left': '40px' }, 'normal');*/
    } else {
        $("#sidebar-menu-item").animate({ 'right': '-70px' }, 'normal');
        $("#breadcrumb").animate({ 'padding-left': '50px' }, 'normal');
    }
}

function showSidebar() {
    $("#sidebar-wrapper").animate({ 'width': '250px' }, 'normal');
    $("#sidebar-menu-item").animate({ 'right': '0px' }, 'normal');
    $("#breadcrumb").animate({ 'padding-left': '0px' }, 'normal');
}

/*
var resizeTimer;
$(document).ready(function () {
    $(window).resize(function () {
        clearTimeout(resizeTimer);
        resizeTimer = setTimeout(controlSize(), 100);
    });
});

function controlSize(){
    if ($(window).width() < 800) {
        hideSidebar()
    } else showSidebar();
}*/

if ($(window).width() < 800) {
    hideSidebar()
} else showSidebar();