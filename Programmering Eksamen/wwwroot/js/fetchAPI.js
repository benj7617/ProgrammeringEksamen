let url = "https://www.thecocktaildb.com/api/json/v1/1/search.php?f="

let alphabet = [];
for (let i = 97; i <= 122; i++) {
    alphabet.push(String.fromCharCode(i));
}

console.log(url + alphabet[1]);


let isAlredyFetched = sessionStorage.getItem("fetched");

console.log(isAlredyFetched);

    if (isAlredyFetched == null) {
        console.log("TEST");
        fetch(newURL)
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
                    NameArr.push(data.drinks[i].strDrink);
                    ImgURLArr.push(data.drinks[i].strDrinkThumb);
                    DescriptionArr.push(data.drinks[i].strInstructions);

                }
                console.log(NameArr);
                console.log(NameArr.length)

                let filtreretNameArr = NameArr.map(str => str.replace(/&/g, 'JENSEN'))
                let filtreretImgURLArr = ImgURLArr.map(str => str.replace(/&/g, 'JENSEN'))
                let filtreretDescriptionArr = DescriptionArr.map(str => str.replace(/&/g, 'JENSEN'))


                sessionStorage.setItem("fetched", "true");
                window.location.href = "Home/Index2?DBID=" + DBIDArr + "&Name=" + filtreretNameArr + "&ImgURL=" + filtreretImgURLArr + "&Description=" + filtreretDescriptionArr;

            })
            .catch(err => console.error(err));

    }