
const userNotificationConnection = new signalR.HubConnectionBuilder()
    .withUrl("/userMessageHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

try {
    userNotificationConnection.start();
    console.log("SignalR userNotificationConnection Connected.");
} catch (err) {
    console.log(err);
    setTimeout(start, 5000);
}



userNotificationConnection.on("UserNotificationFromAdmin", data => {
    const userNotificationWrapper = document.querySelector(".user-notification-wrapper");

    console.log("userNotification", data)

    const card = document.createElement("div");
    card.classList.add("card mt-5");

    const cardHeader = document.createElement("div");
    cardHeader.classList.add("card-header");
    cardHeader.textContent = `From: ${data.sender} | To: ${data.reciever} | Date: ${data.date}`;
    card.appendChild(cardHeader);

    const cardBody = document.createElement("div");
    cardBody.classList.add("card-body");

    const cardTitle = document.createElement("h5");
    cardTitle.classList.add("card-title");
    cardTitle.textContent = data.title;
    cardBody.appendChild(cardTitle);

    const cardText = document.createElement("p");
    cardText.classList.add("card-text");
    cardText.textContent = data.desc;
    cardBody.appendChild(cardText);

    card.appendChild(cardBody);

    userNotificationWrapper.appendChild(card);
});