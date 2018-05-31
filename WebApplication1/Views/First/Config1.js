

var removeHandlerJS = function () {
    // Here, do whatever you want
    $("#item").click(function () {
        var path = $(this).text();
        localStorage.setItem('objectToPass', path);
        localStorage.setItem('objectToPass2', this);
        window.location.href = "RemoveHandler";

    });
};
    

function hideJS(x) {
    $(x).hide;
}

