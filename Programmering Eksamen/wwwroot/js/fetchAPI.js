let url = "https://www.thecocktaildb.com/api/json/v1/1/search.php?f=a"


let isAlredyFetched = sessionStorage.getItem("fetched");

console.log(isAlredyFetched);

if (isAlredyFetched == null) {
    console.log("TEST");
    fetch(url)
        .then(respone => respone.json())
        .then(data => {
           
            console.log("data");
            console.log(data);
            DBIDArr = [];
            NameArr = [];
            ImgURLArr = [];
            DescriptionArr = [];

            
            for (var i = 0; i < data.drinks.length; i++) {
                DBIDArr.push(data.drinks[i].idDrink);
                NameArr.push(data.drinks[i].StrDrink);
                ImgURLArr.push(data.drinks[i].strDrinkThumb);
                DescriptionArr.push(data.drinks[i].strInstructions);

            }
            print(NameArr);

            sessionStorage.setItem("fetched", "true");
            window.location.href = "Home/Index2?DBID=" + DBIDArr + "&Name=" + NameArr + "&ImgURL=" + ImgURLArr + "&Description=" + DescriptionArr;  

        })
        .catch(err => console.error(err));

}
