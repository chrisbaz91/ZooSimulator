document.addEventListener("DOMContentLoaded", function () {
    setupFeedButtons();
    var passTimeUrl = document.getElementById("PassTimeUrl");
    if (passTimeUrl) {
        setInterval(updateAnimalStatus, 20000);
    }
    setInterval(updateClockTick, 500);
    var deleteButton = document.getElementById("DeleteButton");
    if (deleteButton) {
        deleteButton.addEventListener("click", deleteButtonOnClick);
    }
    var inputs = document.getElementsByClassName("form-control");
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].addEventListener("blur", function () {
            enableSubmitButton(inputs);
        });
    }
    enableSubmitButton(inputs);
});
function setupFeedButtons() {
    var feedButtons = document.getElementsByClassName("feed-button");
    for (var i = 0; i < feedButtons.length; i++) {
        feedButtons[i].addEventListener("click", function (e) {
            feedButtonOnClick(e);
        });
    }
}
function feedButtonOnClick(e) {
    var list = document.getElementById("List");
    var feedButton = e.target;
    var speciesTypeId = feedButton.dataset.typeId;
    var speciesTypeValue = feedButton.dataset.typeValue;
    var feedUrl = document.getElementById("FeedUrl").value + "?Type=" + speciesTypeId;
    fetch(feedUrl, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(function (response) { return response.text(); })
        .then(function (data) {
            if (data) {
                list.innerHTML = data;
                setupFeedButtons();
            }
            else {
                alert("Error feeding ".concat(speciesTypeValue, "s"));
            }
        })
        .catch(function (error) {
            console.error('Error fetching data:', error);
        });
}
function updateAnimalStatus() {
    var list = document.getElementById("List");
    var passTimeUrl = document.getElementById("PassTimeUrl");
    fetch(passTimeUrl.value, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(function (response) { return response.text(); })
        .then(function (data) {
            if (data) {
                list.innerHTML = data;
                setupFeedButtons();
            }
            else {
                alert("Error passing time");
            }
        })
        .catch(function (error) {
            console.error('Error fetching data:', error);
        });
    updateHourOfDay();
}
function updateHourOfDay() {
    var hourOfDayElement = document.getElementById("HourOfDay");
    var hourOfDayValue = Number(hourOfDayElement.innerText);
    var hourOfDayString = "";
    hourOfDayValue++;
    if (hourOfDayValue >= 24) {
        hourOfDayValue = 0;
    }
    hourOfDayString = hourOfDayValue.toString();
    if (hourOfDayValue < 10) {
        hourOfDayString = "0" + hourOfDayString;
    }
    hourOfDayElement.innerText = hourOfDayString;
}
function updateClockTick() {
    var clockTick = document.getElementById("ClockTick");
    clockTick.classList.toggle("hide-tick");
}
function enableSubmitButton(inputs) {
    var submitButton = document.getElementById("SubmitButton");
    var value;
    var disabled = false;
    if (submitButton) {
        for (var i = 0; i < inputs.length; i++) {
            value = inputs[i].value;
            if (value === "") {
                disabled = true;
                break;
            }
            switch (inputs[i].id) {
                case "Name":
                    if (value.length > 50) {
                        disabled = true;
                    }
                    break;
                case "Age":
                    if (value < 0 || value > 100) {
                        disabled = true;
                    }
                    break;
            }
            if (disabled) {
                break;
            }
        }
        if (disabled) {
            submitButton.classList.add("disabled");
        }
        else {
            submitButton.classList.remove("disabled");
        }
    }
}
function deleteButtonOnClick() {
    var name = document.getElementById("Name").value;
    var type = document.getElementById("Type").value;
    if (confirm("Are you sure you're ready to release ".concat(name, " the ").concat(type, " back into the wild?"))) {
        var id = document.getElementById("Id").value;
        var indexUrl_1 = document.getElementById("IndexUrl").value;
        var deleteUrl = document.getElementById("DeleteUrl").value + "/" + id;
        fetch(deleteUrl, {
            method: "GET",
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(function (response) {
                if (response.ok) {
                    alert("".concat(name, " was successfully released!"));
                    window.location.replace(indexUrl_1);
                }
                else {
                    alert("Error releasing Animal");
                }
            })
            .catch(function (error) {
                console.error('Error fetching data:', error);
            });
    }
}
//# sourceMappingURL=Index.js.map