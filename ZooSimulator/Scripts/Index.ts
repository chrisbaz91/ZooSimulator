document.addEventListener("DOMContentLoaded", () => {
    setupFeedButtons();

    const passTimeUrl = document.getElementById("PassTimeUrl") as HTMLInputElement;
    if (passTimeUrl) {
        setInterval(updateAnimalStatus, 20000);
    }

    setInterval(updateClockTick, 500);

    const deleteButton = document.getElementById("DeleteButton");
    if (deleteButton) {
        deleteButton.addEventListener("click", deleteButtonOnClick);
    }

    const inputs = document.getElementsByClassName("form-control");
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].addEventListener("blur", () => {
            enableSubmitButton(inputs);
        });
    }

    enableSubmitButton(inputs)
});

function setupFeedButtons() {
    const feedButtons = document.getElementsByClassName("feed-button");
    for (var i = 0; i < feedButtons.length; i++) {
        feedButtons[i].addEventListener("click", e => {
            feedButtonOnClick(e);
        });
    }
}

function feedButtonOnClick(e: Event) {
    const list = document.getElementById("List") as HTMLDivElement;
    const feedButton = e.target as HTMLButtonElement;
    const speciesTypeId = feedButton.dataset.typeId;
    const speciesTypeValue = feedButton.dataset.typeValue;
    const feedUrl = (document.getElementById("FeedUrl") as HTMLInputElement).value + "?Type=" + speciesTypeId;

    fetch(feedUrl, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.text())
        .then(data => {
            if (data) {
                list.innerHTML = data;
                setupFeedButtons();
            }
            else {
                alert(`Error feeding ${speciesTypeValue}s`);
            }
        })

        .catch(error => {
            console.error('Error fetching data:', error);
        });
}

function updateAnimalStatus() {
    const list = document.getElementById("List") as HTMLDivElement;
    const passTimeUrl = document.getElementById("PassTimeUrl") as HTMLInputElement;

    fetch(passTimeUrl.value, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.text())
        .then(data => {
            if (data) {
                list.innerHTML = data;
                setupFeedButtons();
            }
            else {
                alert("Error passing time");
            }
        })

        .catch(error => {
            console.error('Error fetching data:', error);
        });

    updateHourOfDay();
}

function updateHourOfDay() {
    const hourOfDayElement = document.getElementById("HourOfDay") as HTMLSpanElement;
    let hourOfDayValue = Number(hourOfDayElement.innerText);
    let hourOfDayString = "";

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
    const clockTick = document.getElementById("ClockTick") as HTMLSpanElement;

    clockTick.classList.toggle("hide-tick");
}

function enableSubmitButton(inputs) {
    const submitButton = document.getElementById("SubmitButton");
    let value;
    let disabled = false;

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
    const name = (document.getElementById("Name") as HTMLInputElement).value;
    const type = (document.getElementById("Type") as HTMLInputElement).value;

    if (confirm(`Are you sure you're ready to release ${name} the ${type} back into the wild?`)) {
        const id = (document.getElementById("Id") as HTMLInputElement).value;
        const indexUrl = (document.getElementById("IndexUrl") as HTMLInputElement).value;
        const deleteUrl = (document.getElementById("DeleteUrl") as HTMLInputElement).value + "/" + id;

        fetch(deleteUrl, {
            method: "GET",
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    alert(`${name} was successfully released!`);
                    window.location.replace(indexUrl);
                }
                else {
                    alert("Error releasing Animal");
                }
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    }
}