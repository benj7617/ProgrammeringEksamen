let url = "https://www.thecocktaildb.com/api/json/v1/1/search.php?f=b"


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
            AmountArr = [];
            CostArr = [];
            
            for (var i = 0; i < data.drinks.Length(); i++) {
                DBIDArr.push(data.drinks[i].idDrink);
                NameArr.push(data.drinks[i].StrDrink);
                ImgURLArr.push(data.drinks[i].strDrinkThumb);
                DescriptionArr.push(data.drinks[i].strInstructions);
                AmountArr.push(0);
                CostArr.push(0);
            }


            sessionStorage.setItem("fetched", "true")
            console.log(relevantInfo.DBID);
            console.log(relevantInfo.Cost);
            window.location.href = "Home/Index2?DBID=" + DBIDArr + "&Name=" + NameArr + "&ImgURL=" + ImgURLArr + "&Description=" + DescriptionArr + "&Cost=" + CostArr + "&Amount=" + AmountArr;
            

        })
        .catch(err => console.error(err));
    
  
}
