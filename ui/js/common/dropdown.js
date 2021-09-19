


$('.dropdown .dropdown-data').on('click', '.dropdown-item',


    function selectItemDropdown(e) {
        /*e.preventDefault;*/
        var items = $(this).parents('.dropdown-data').find('.dropdown-item');
        $.each(items, function (index, item) {
            $(item).removeClass('item-selected');
        })

        $(this).addClass('item-selected');
        $(this).parents('.dropdown').find('.dropdown-main p').empty();
        var thisValueId = $(this).attr("valueid");
        var thisValueName = $(this).attr("valuename");
        $(this).parents('.dropdown').attr("value", thisValueId);
        $(this).parents('.dropdown').find('.dropdown-main p').append(thisValueName);
        if ($(this).parents('.dropdown').attr('id') == 'inputCustomerType1Name') {
            filterByCustomerType();
        } else if ($(this).parents('.dropdown').attr('id') == 'inputPosition1Name') {
            filterByPosition();
        }
    }

);

/**
 * lọc khách hàng theo nhóm
 * */
function filterByCustomerType() {
    $('.grid table tbody').empty();
    var id = $('#inputCustomerType1Name').attr('value');
    var searchTerms = $('#inputSearch').val();
    console.log(id)
    console.log(searchTerms)
    if (id == "" && searchTerms == "") {

        myCustomer.loadData();

    } else {
        var myUrl = "";
        if (id == "" && searchTerms != "") {
            myUrl = "http://localhost:60060/api/v1/Customers/filter?searchTerms=" + searchTerms;
        } else if (id != "" && searchTerms == "") {
            myUrl = "http://localhost:60060/api/v1/Customers/filter?customerTypeId=" + id;
        } else {
            myUrl = "http://localhost:60060/api/v1/Customers/filter?customerTypeId=" + id + "&searchTerms=" + searchTerms;
        }
        $('.loader-wrapper').css('visibility', 'hidden');

        $.ajax({
            url: myUrl,
            method: 'GET'
        }).done(function (res) {
            console.log(res);
            myCustomer.bindingData(res);
        }).fail(res => {
            ToolTipJS.showMes('warning', 'Lỗi rồi!');
            myEmployee.loadData();
        })
    }
}
/**
 * lọc nhân viên theo vị trí
 * */
function filterByPosition() {
    $('.grid table tbody').empty();
    var id = $('#inputPosition1Name').attr('value');
    var searchTerms = $('#inputSearch').val();
    if (id == "" && searchTerms == "") {

        myEmployee.loadData();

    } else {
        var myUrl = "";
        if (id == "" && searchTerms != "") {
            myUrl = "http://localhost:60060/api/v1/Employees/filter?searchTerms=" + searchTerms;
        } else if (id != "" && searchTerms == "") {
            myUrl = "http://localhost:60060/api/v1/Employees/filter?positionId=" + id;
        } else {
            myUrl = "http://localhost:60060/api/v1/Employees/filter?positionId=" + id + "&searchTerms=" + searchTerms;
        }
        $('.loader-wrapper').css('visibility', 'visible');
        $.ajax({
            url: myUrl,
            method: 'GET'
        }).done(function (res) {
            console.log(res);
            myEmployee.bindingData(res);
        }).fail(res => {
            ToolTipJS.showMes('warning', 'Lỗi rồi!');
            myEmployee.loadData();
        })
    }
}


