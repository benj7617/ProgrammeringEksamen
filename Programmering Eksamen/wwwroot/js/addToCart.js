

function search() {
    var searchTerm = document.getElementById("searchTerm").value
    $.ajax({
        url: '/Home/Search',
        type: 'POST',
        data: { searchTerm: searchTerm },
        success: function (response) {
            displaySearchResults(response);
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });

}


function displaySearchResults(products) {
    console.log(products);
    var $resultsContainer = $('#searchResults');
    $resultsContainer.empty();


    var $card = $('<div class="card"></div>');
    var $img = $('<img src="' + products[0].imgURL + '" style="width:100%">');
    var $title = $('<h1>' + products[0].name + '</h1>');
    var $price = $('<p class="price">' + products[0].cost + '</p>');
    var $description = $('<p>' + products[0].description + '</p>');
    var $button = $('<button onclick="addToCart(' + products[0].dbid + ', \'' + products[0].name + '\')">Add to Cart</button>');

    $card.append($img);
    $card.append($title);
    $card.append($price);
    $card.append($description);
    $card.append($button);

    $resultsContainer.append($card);
}



