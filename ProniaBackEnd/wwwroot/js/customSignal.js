const connection = new signalR.HubConnectionBuilder()
    .withUrl("/userStatusHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

try {
    connection.start();
    console.log("SignalR Connected.");
} catch (err) {
    console.log(err);
    setTimeout(start, 5000);
}

connection.on("UserStatus", data => {
   
    const elementId = "online-tracked-" + data.userId;
    const element = document.getElementById(elementId);
 

    if (element) {
        if (data.status === true) {
            element.textContent = "Online";
            element.classList.remove("badge-soft-danger"); 
            element.classList.add("badge-soft-success"); 
        } else {
            element.textContent = "Offline";
            element.classList.remove("badge-soft-success"); 
            element.classList.add("badge-soft-danger"); 
        }
    }
    console.log(element)
});




