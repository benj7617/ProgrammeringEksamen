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
            relevantInfo = {
                DBID: data.drinks[0].idDrink,
                Cost: 0.0,
                Name: data.drinks[0].strDrink,
                Amount: 0
            }
            
            localStorage.setItem("dataStore", JSON.stringify(relevantInfo));
            sessionStorage.setItem("fetched", "true")
            console.log(relevantInfo.DBID);
            console.log(relevantInfo.Cost);
            

        })
        .catch(err => console.error(err));
    
    const modelData = JSON.parse(localStorage.getItem("dataStore"));
    window.location.href = "Home/Index2?DBID=" + modelData.DBID + "&Cost=" + modelData.Cost + "&Amount=" + modelData.Amount;
}
