let url = "https://thecocktaildb.com/api/json/v1/1/lookup.php?iid="

let ingredientLimit = 10;

let DBID;
let Name;
let ImgURL;
let Description;


let isAlredyFetched = sessionStorage.getItem("fetched");

console.log(isAlredyFetched);

for (var i = 1; i < ingredientLimit; i++) {
    newURL = url + i;
    if (isAlredyFetched == null) {
        console.log("TEST");
        fetch(newURL)
            .then(respone => respone.json())
            .then(data => {
                
                console.log("data");
                console.log(data);
                
                DBID = (data.ingredients[0].idIngredient);
                Name = (data.ingredients[0].strIngredient);
                ImgURL = "https://www.thecocktaildb.com/images/ingredients/" + Name + ".png"
                Description = (data.ingredients[0].strDescription);

                FirstLineDescription = Description.split(".")[0];

                console.log("");


                console.log(Name + ImgURL + Description);
               
               


                sessionStorage.setItem("fetched", "true");
                window.location.href = "Home/Index2?DBID=" + DBID + "&Name=" + Name + "&ImgURL=" + ImgURL + "&Description=" + FirstLineDescription;

            })
            .catch(err => console.error(err));

    }
}

   