myItem = '';

$(document).ready(function () {
    myItem = new ItemJS();
})

class ItemJS {
    flagAdd = 1;
    myItemId = '';

    constructor() {
        this.loadData();
        this.initEvent();
    }

    /**
    * hàm load dữ liệu
    * Author: hieunv (13/08/2021)
    * */
    loadData() {
        var seft = this;
        $('.grid-items').empty();
        try {
            this.dataUrl = "http://localhost:60060/api/v1/Items";
            //Lấy thông tin tương ứng với các cột để map vào
            $.ajax({
                url: this.dataUrl,
                method: 'GET',
            }).done(function (res) {
                console.log(res);
                seft.bindingData(res);
            }).fail(function (res) {
                console.log(res);
            });
        } catch (e) {
            console.log(e);
        }
    }

    /**
    * Binding data to table
    * @param {any} objs
    */
    bindingData(objs) {
        objs = objs.reverse();
        // lấy thông tin các cột dữ liệu
        $.each(objs, (index, obj) => {
            var itemHtml = $(`<div class="item-item" id="Item${obj.ItemId}" value="${obj.ItemId}">
                    <div class="item-img"></div>
                    <span class="item-name">
                        <p>${obj.ItemName}</p>
                        <p>${CommonJS.formatSalary(obj.ItemPrice)} VND</p>
                    </span>
                    <div class="item-btn">
                        <button class="button button-icon btn-edit" id="Edit${obj.ItemId}" value="${obj.ItemId}">
                            <i class="fas fa-edit"></i>
                            <p>Chỉnh sửa</p>
                        </button>
                        <button class="button button-icon btn-delete" id="Remove${obj.ItemId}" value="${obj.ItemId}">
                            <i class="fas fa-trash-alt"></i>
                            <p>Xóa</p>
                        </button>
                    </div>
                </div>`);
            $('.grid-items').append(itemHtml);
        })
    }

    /**
    * Khởi tạo các sự kiện cho trang employee
    * Author hieunv 26/07/2021
    * */
    initEvent() {
        var seft = this;
        // khi bấm thêm mới sản phẩm
        $('#btnAdd').click(function () {
            seft.btnAddOnClick(seft, this);
        })
        // khi bấm chỉnh sửa một sp
        $('.grid-items').on('click', '.item-item .btn-edit', function () {
            seft.editOnClick(seft, this);
        });
        //khi bấm xóa một sản phẩm
        // khi bấm chỉnh sửa một sp
        $('.grid-items').on('click', '.item-item .btn-delete', function () {
            seft.deleteOnClick(seft, this);
        });
        // khi bấm exit
        $('#btnExit').click(() => {
            $('.dialog-item').css("visibility", "hidden");
        })
        // khi bấm hủy
        $('#btnCancel').click(() => {
            $('.dialog-item').css("visibility", "hidden");
        })
        // khi bấm save
        $('#btnSave').click(() => {
            seft.btnSaveOnClick(seft, this);
        })
        //Click vào nút exit trên popup
        $('.popup button.btn-exit').click(() => {
            $('.popup').css('display', 'none');
        })

        //Click vào nút exit trên popup
        $('#btnRefresh').click(() => {
            seft.btnRefreshOnClick(seft, this);
        })
    }

    btnAddOnClick(seft, thisElement) {
        seft.flagAdd = 1;
        $('.dialog-item').css("visibility", "visible");
        $('.dialog-item input').val(null);
        $('.dialog-item input').removeClass('border-red');
        $('.autofocus').focus();
        //lấy mã nhân viên mới binding vào form
        $.ajax({
            url: "http://localhost:60060/api/v1/Items/NewId",
            method: 'GET',

        }).done(res => {
            seft.myItemId = res;
        }).fail(res => {

        });
    }

    editOnClick(seft, thisElement) {
        $('.dialog-item').css("visibility", "visible");
        seft.flagAdd = 0;
        try {

            const itemId = $(thisElement).attr('value');
            seft.myItemId = itemId;
            var myUrl = "http://localhost:60060/api/v1/Items/" + itemId;
            $.ajax({
                url: myUrl,
                method: 'GET'
            }).done(function (res) {
                $('#inputItemName').val(res.ItemName);
                $('#inputItemPrice').val(CommonJS.formatSalary(res.ItemPrice));
            }).fail(function (res) {

            })

        } catch (e) {
            console.log(e);
        }
    }

    deleteOnClick(seft, thisElement) {
        $('.popup').css('display', 'block');
        PopupJS.showPopup('danger', 'Bạn có chắc chắn muốn xóa các bản ghi được chọn?', "Xóa các bản ghi!");
        const itemId = $(thisElement).attr('value');
        $('.popup button.btn-z').click(() => {
            $.ajax({
                url: "http://localhost:60060/api/v1/Items/" + itemId,
                method: 'DELETE'
            }).done(res => {
                seft.loadData();
                ToolTipJS.showMes('success', "Đã xóa thành công bản ghi!")
                $('#btnDelete').css('visibility', 'hidden');
                $('.popup').css('display', 'none');
            }).fail(res => {
                $('.popup').css('display', 'none');
            })
        })
    }

    btnSaveOnClick(seft, thisElement) {
        var myUrl = "http://localhost:60060/api/v1/Items";
        var method = 'POST';
        if (seft.flagAdd != 1) {
            myUrl = "http://localhost:60060/api/v1/Items/" + seft.myItemId;
            method = 'PUT';
        }
        var mes = 'Bạn có chắc chắn muốn câp nhật bản ghi này?';
        var title = 'Chỉnh sửa bản ghi!';
        if (seft.flagAdd == 1) {
            mes = 'Bạn có chắc chắn muốn thêm bản ghi này?';
            title = 'Thêm mới bản ghi!';
        }
        PopupJS.showPopup('warning', mes, title);
        $('.popup').css('display', 'block');


        $('.popup button.btn-x').click(() => {
            var item = {};
            item.ItemName = $('#inputItemName').val();
            item.ItemPrice = $('#inputItemPrice').val().replaceAll('.', '') == "" ? null : parseInt($('#inputItemPrice').val().replaceAll('.', ''));
            item.ItemId = seft.myItemId;
            console.log(item)
            // gọi ajax post dữ liệu
            $.ajax({
                url: myUrl,
                method: method,
                data: JSON.stringify(item),
                dataType: 'json',
                contentType: 'application/json'
            }).done(res => {
                $('.dialog-item').css("visibility", "hidden");
                $('.popup').css('display', 'none');
                seft.loadData();
                if (seft.flagAdd == 1) {
                    ToolTipJS.showMes('success', 'Thêm mới bản ghi thành công!');
                } else {
                    ToolTipJS.showMes('success', 'Cập nhật bản ghi thành công!');
                }
                $('.popup').css('display', 'none');
            }).fail(res => {
                ToolTipJS.showMes('danger', 'Đã có lỗi xảy ra!');
            });
        })
    }

    btnRefreshOnClick(seft, thisElement) {
        seft.loadData();
        $('#inputSearch').val(null);
    }
}