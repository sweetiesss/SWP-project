// Get the modal
    var modalTopic = document.getElementById("myModalTopic");

    // Get the button that opens the modal
    var btnTopic = document.getElementById("myBtnTopic");

    // Get the <span> element that closes the modal
    var spanTopic = document.getElementsByClassName("closeTopic")[0];

    // When the user clicks on the button, open the modal
    if (btnTopic != null) {
        btnTopic.onclick = function () {
            modalTopic.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        spanTopic.onclick = function () {
            modalTopic.style.display = "none";
        }
    }

    // When the user clicks anywhere outside of the modal, close it


    // Get the modal
    var modalTeam = document.getElementById("myModalTeam");

    // Get the button that opens the modal
    var btnTeam = document.getElementById("myBtnTeam");

    // Get the <span> element that closes the modal
    var spanTeam = document.getElementsByClassName("closeTeam")[0];

    // When the user clicks on the button, open the modal
if (btnTeam != null) {
    btnTeam.onclick = function () {
        modalTeam.style.display = "block";
    }

    // When the user clicks on <span> (x), close the modal
    spanTeam.onclick = function () {
        modalTeam.style.display = "none";
    }
}

    // When the user clicks anywhere outside of the modal, close it
    var modalTask = document.getElementById("myModalTask");

    // Get the button that opens the modal
    var btnTask = document.getElementById("myBtnTask");

    // Get the <span> element that closes the modal
    var spanTask = document.getElementsByClassName("closeTask")[0];

    // When the user clicks on the button, open the modal
    if (btnTask != null) {
        btnTask.onclick = function () {
            modalTask.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        spanTask.onclick = function () {
            modalTask.style.display = "none";
        }
    }
