const url = "https://thecocktaildb.com/api/json/v1/1/lookup.php?iid=";
const ingredientLimit = 13; // Dette tal kan desvære ikke ændres fordi API er for langtsom



let isAlreadyFetched = sessionStorage.getItem("fetched");

console.log(isAlreadyFetched);

if (isAlreadyFetched == null) {
    let fetchPromises = [];
    for (let i = 1; i < ingredientLimit; i++) { 
        let newURL = url + i;
        console.log("det nye URL er: " + newURL);
        console.log("Henter data for ingrediens med id: " + i);
        let fetchPromise = fetch(newURL)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Fejl ved fetch-anmodning. Statuskode: " + response.status);
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
                let DBID = data.ingredients[0].idIngredient;
                let Name = data.ingredients[0].strIngredient;
                let ImgURL = "https://www.thecocktaildb.com/images/ingredients/" + Name + ".png";
                let WholeDescription = data.ingredients[0].strDescription;
                let FirstLineDescription = WholeDescription.split(".")[0];
                let Description = FirstLineDescription.toString();
                console.log(DBID);
                const Amount = 0;
                const Cost = 0;
                return { DBID, Name, ImgURL,  Description, Cost, Amount};
            })
            .catch(err => console.error(err));

        fetchPromises.push(fetchPromise);
    }

    sessionStorage.setItem("fetched","true")

    
    // Venter på at alle fetch-anmodninger er færdige, kommer fra https://dmitripavlutin.com/promise-all/
    Promise.all(fetchPromises)
        .then(ing => {
            console.log("Alle fetch-anmodninger er færdige:", ing);
            // kommer fra https://api.jquery.com/ som er dokumentation for jquery. bruges over hele projektet
            $.ajax({
                type: "POST",
                url: "/Home/Index2",
                contentType: "application/json",
                data: JSON.stringify(ing),
                success: function (response) {
                    // svaret fra controlleren
                    console.log("Data blev sendt succesfuldt til serveren.");
                },
                error: function (xhr, status, error) {
                    // Håndter fejl hvis noget går galt men det gør det jo heldigvis aldrig
                    console.error("Fejl ved afsendelse af data til serveren:", error);
                }
            });
        })
        .catch(err => console.error("Fejl ved håndtering af fetch-anmodninger:", err));
}