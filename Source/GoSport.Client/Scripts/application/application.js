var citiesDropDown = $('#city-drop-down');
var neighbourhoodsDropDown = $('#neighbour-drop-down');
var addImageBtn = $('#add-image-button');


//Ajax requests
function getAllNeighbourhoods(city) {
    $.ajax({
        type: 'POST',
        url: "../../Address/GetAllNeighbours",
        dataType: 'json',
        data: JSON.stringify({ city: city }),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            neighbourhoodsDropDown.html('');

            //if (!data.length) {
            //    neighbourhoodsDropDown.append(
            //        $('<option></option>').val("None").html("None")
            //    );
            //}
            neighbourhoodsDropDown.append($('<option></option>').val("None").html("None"));
            for (var i = 0; i < data.length; i++) {
                neighbourhoodsDropDown.append(
                    $('<option></option>').val(data[i].Id).html(data[i].Neighborhood)
                );
            }
        },
        error: function (data) {
            console.log('error: ' + data)
        }
    });
}

function rateSportCenter(centerId,type,val,sender) {
    $.ajax({
        type: 'POST',
        url: "../../Rating/RateSportCenter",
        dataType: 'json',
        data: JSON.stringify({ sportCenterId: centerId, ratingType: type, value: val }),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            sender.html((data.value*1).toFixed(2))
        },
        error: function (data) {
            console.log('error: ' + data)
        }
    });
}


//Event listeners
citiesDropDown.on('change', function () {
    var selectedCity = $('option:selected', citiesDropDown).text();
    getAllNeighbourhoods(selectedCity);
})

addImageBtn.on('click', function () {
    var lastFileName = $(":file").last().attr('name');
    var elementToAppendNewOne = $(":file").last().parent();
    numberOfimage = null;

    if (lastFileName) numberOfimage = lastFileName.substr(lastFileName.length - 1);

    if (!numberOfimage && isNaN(numberOfimage * 1)) {
        return;
    }
    var nextNumberOfImage = numberOfimage * 1 + 1;

    if (nextNumberOfImage.toString().length > 1) return;

    var html = ' <div class="col-md-5 upload"><input type="file" name="UplodadedImage' + nextNumberOfImage + '" /></div>';

    elementToAppendNewOne.after(html);
});

$('#add-comment').on('click', function (ev) {
    var btn = $(ev.target);
    if (btn.text() === 'Add Comment') btn.text('Cancel');
    else btn.text('Add Comment');
    $('.add-comment').toggleClass('hidden');

    return false;
});

$('#add-message').on('click', function (ev) {
    var btn = $(ev.target);
    if (btn.text() === 'Add Message') btn.text('Cancel');
    else btn.text('Add Message');
    $('.add-message').toggleClass('hidden');

    return false;
});

$('.starRate').on('click', function (ev) {
    var btn = $(ev.target);
    if (btn.is('span')) btn = btn.parent();
    var ratingType = btn.parent().attr('data-rating-type');
    var sportCenterId = btn.parent().attr('data-sport-center-id');
    var value = btn.val();

    rateSportCenter(sportCenterId * 1, ratingType,value * 1, btn.parent().prev());
});





