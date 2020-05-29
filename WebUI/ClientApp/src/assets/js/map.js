

// button click action
function showHideSidebar() {
    var x = document.getElementById("mapSidebarBody");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }

    // button arrow toggle
    {
        const button = $("#showHide")
        if (button.html() == '<span class="fas fa-angle-left"></span>') {
            button.html('<span class="fas fa-angle-right"></span>');
        } else {
            button.html('<span class="fas fa-angle-left"></span>');
        }
    }

}

$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        html: true,
        content: function () {
            return '<div class="news d-flex">'
                + '<img src="images/users/1.jpg" alt="" class="profile rounded-circle">' +
                '<div>' +
                '<p class="tx-medium mb-0 text-dark">أحمد محمود الحنوني</p>' +
                '<small class="tx-12 text-muted">مشرف امن</small>' +
                '</div>' +

                '</div>';
        }
    });
    $('[data-toggle="popover1"]').popover({
        html: true,
        content: function () {
            return '<div class="news d-flex">'
                + '<img src="images/users/2.jpg" alt="" class="profile rounded-circle">' +
                '<div>' +
                '<p class="tx-medium mb-0 text-dark">ابراهيم محمد عبد العليم</p>' +
                '<small class="tx-12 text-muted">مشرف امن</small>' +
                '</div>' +

                '</div>';
        }
    });
    $('[data-toggle="popover2"]').popover({
        html: true,
        content: function () {
            return '<div class="news d-flex">'
                + '<img src="images/users/3.jpg" alt="" class="profile rounded-circle">' +
                '<div>' +
                '<p class="tx-medium mb-0 text-dark">على اشرف بكر</p>' +
                '<small class="tx-12 text-muted">مشرف امن</small>' +
                '</div>' +

                '</div>';
        }
    });
    $('[data-toggle="popover3"]').popover({
        html: true,
        content: function () {
            return '<div class="news d-flex">'
                + '<img src="images/users/4.jpg" alt="" class="profile rounded-circle">' +
                '<div>' +
                '<p class="tx-medium mb-0 text-dark">محمد السعيد أحمد</p>' +
                '<small class="tx-12 text-muted">مشرف امن</small>' +
                '</div>' +

                '</div>';
        }
    });
    $('[data-toggle="popover4"]').popover({
        html: true,
        content: function () {
            return '<div class="news d-flex">'
                + '<img src="images/users/5.jpg" alt="" class="profile rounded-circle">' +
                '<div>' +
                '<p class="tx-medium mb-0 text-dark">محمد على محمد</p>' +
                '<small class="tx-12 text-muted">مشرف امن</small>' +
                '</div>' +

                '</div>';
        }
    });
    $('[data-toggle="popover5"]').popover({
        html: true,
        content: function () {
            return '<div class="news d-flex">'
                + '<img src="images/users/7.jpg" alt="" class="profile rounded-circle">' +
                '<div>' +
                '<p class="tx-medium mb-0 text-dark">محمد حسن محمد</p>' +
                '<small class="tx-12 text-muted">مشرف امن</small>' +
                '</div>' +

                '</div>';
        }
    });
    $('[data-toggle="popover6"]').popover({
        html: true,
        content: function () {
            return '<div class="news d-flex">'
                + '<img src="images/users/6.jpg" alt="" class="profile rounded-circle">' +
                '<div>' +
                '<p class="tx-medium mb-0 text-dark">أحمد سمير أحمد</p>' +
                '<small class="tx-12 text-muted">مشرف امن</small>' +
                '</div>' +

                '</div>';
        }
    });
    $('[data-toggle="popover7"]').popover({
        html: true,
        content: function () {
            return '<div class="news d-flex">'
                + '<img src="images/users/9.jpg" alt="" class="profile rounded-circle">' +
                '<div>' +
                '<p class="tx-medium mb-0 text-dark">انس اسماعيل محمد</p>' +
                '<small class="tx-12 text-muted">مشرف امن</small>' +
                '</div>' +

                '</div>';
        }
    });
});

