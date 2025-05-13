document.addEventListener("DOMContentLoaded", function () {
    function updateNavbar() {

        var authorized = document.querySelector('meta[name="authorized"]').getAttribute('content');

        if (authorized) {
            document.getElementById("login").style.display = "none";
            document.getElementById("logout").style.display = "block";
            document.getElementById("board").style.display = "block";
            document.getElementById("my-board").style.display = "block";
        } else {
            document.getElementById("login").style.display = "block";
            document.getElementById("logout").style.display = "none";
            document.getElementById("board").style.display = "none";
            document.getElementById("my-board").style.display = "none";
        }
    }
    updateNavbar();
});