let things = [];





function addToCart(id, name) {   
    var liste = document.getElementById("purchaseList");

    // Opret et nyt listeelement
    var nytElement = document.createElement("li");

    // Tilføj tekst til det nye element
    var tekstNode = document.createTextNode(name);
    nytElement.appendChild(tekstNode);

    // Tilføj det nye element til den eksisterende liste
    liste.appendChild(nytElement);
    things.push(id)
    console.log(things)
}

function confirmPurchace() {

    console.log(things);

    $.ajax({
        type: "POST",
        url: "/Home/Cart",
        contentType: "application/json",
        data: JSON.stringify(things),
        success: function (response) {
            // Behandl svaret fra controlleren, f.eks. vis en bekræftelsesbesked
            console.log("Data blev sendt succesfuldt til serveren.", things);
        },
        error: function (xhr, status, error) {
            // Håndter fejl, hvis noget går galt
            console.error("Fejl ved afsendelse af data til serveren:", error);
        }
    });



}