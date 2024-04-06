let url = "https://thecocktaildb.com/api/json/v1/1/lookup.php?iid=";
let ingredientLimit = 13; // Dette tal kan desvære ikke ændres fordi API er for langtsom RIP
let cost = 0;
let amount = 0;
let BOZOingredients = [];
let isAlreadyFetched = sessionStorage.getItem("fetched");

console.log(isAlreadyFetched);

if (isAlreadyFetched == null) {
    let fetchPromises = [];
    for (let i = 1; i < ingredientLimit; i++) { 
        let newURL = url + i;
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
                return { DBID, Name, ImgURL,  Description};
            })
            .catch(err => console.error(err));

        fetchPromises.push(fetchPromise);
    }

    // Vent på, at alle fetch-anmodninger er færdige
    Promise.all(fetchPromises)
        .then(ingredients => {
            console.log("Alle fetch-anmodninger er færdige:", ingredients);
            $.ajax({
                type: "POST",
                url: "/Home/Index2",
                contentType: "application/json",
                data: JSON.stringify(ingredients),
                success: function (response) {
                    // Behandl svaret fra controlleren, f.eks. vis en bekræftelsesbesked
                    console.log("Data blev sendt succesfuldt til serveren.");
                },
                error: function (xhr, status, error) {
                    // Håndter fejl, hvis noget går galt
                    console.error("Fejl ved afsendelse af data til serveren:", error);
                }
            });
            // Gør noget med ingredienserne her, f.eks. gem dem i session storage
        })
        .catch(err => console.error("Fejl ved håndtering af fetch-anmodninger:", err));
}