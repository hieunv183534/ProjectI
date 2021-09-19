myEmployee = '';
myPosiitons = [];


$(document).ready(function () {
    myEmployee = new EmployeeJS();
})




class EmployeeJS {
    checkedEmployees = [];
    flagAdd = 1;
    myEmployeeId = '';

    constructor() {
        this.loadLocalData();
        this.loadData();
        this.loadDropdown();
        this.initEvent();
    }

    // lấy dữ liệu vi trí trước
    loadLocalData() {
        $.ajax({
            url: "http://localhost:60060/api/v1/Positions",
            method: 'GET'
        }).done(res => {
            myPosiitons = res;
        })
    }


    /**
    * hàm load dữ liệu
    * Author: hieunv (21/07/2021)
    * */
    loadData() {
        $('.loader-wrapper').css('visibility', 'visible');
        var seft = this;
        $('.grid table tbody').empty();
        try {
            this.dataUrl = "http://localhost:60060/api/v1/Employees";
            //Lấy thông tin tương ứng với các cột để map vào
            $.ajax({
                url: this.dataUrl,
                method: 'GET',
            }).done(function (res) {
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
        var cols = $('.grid table thead th');
        $.each(objs, function (index, obj) {
            var tr = $(`<tr></tr>`);
            $.each(cols, function (index, th) {
                var td = $(`<td></td>`);
                var fieldName = $(th).attr('fieldName');
                if (fieldName == 'check') {
                    var value = $(`<input type="checkbox" style="width:46px; height:20px;"/>`);
                } else if (fieldName == 'EmployeeId') {
                    var value = "NV-" + obj[fieldName];
                } else if (fieldName == 'Gender') {
                    switch (obj[fieldName]) {
                        case 1:
                            var value = "Nam";
                            break;
                        case 2:
                            var value = "Nữ";
                            break;
                        case 3:
                            var value = "Không xác định";
                            break;
                        default:
                            var value = "";
                    }
                } else if (fieldName == 'WorkStatus') {
                    switch (obj[fieldName]) {
                        case 1:
                            var value = "Đang làm việc";
                            break;
                        case 2:
                            var value = "Đã nghỉ việc";
                            break;
                        case 3:
                            var value = "Đang thử việc";
                            break;
                        default:
                            var value = "";
                    }
                } else if (fieldName == 'PositionName') {
                    var value;
                    var positionId = obj['PositionId'];
                    $.each(myPosiitons, (index, item) => {
                        if (item.PositionId == positionId) {
                            value = item.PositionName;
                        }
                    })
                } else {
                    var value = obj[fieldName];
                }
                var formatType = $(th).attr('formatType');
                switch (formatType) {
                    case 'ddmmyyyy':
                        value = CommonJS.formatDate(value);
                        break;
                    case 'money':
                        value = CommonJS.formatSalary(value);
                        break;
                    default:
                        break;
                }
                $(td).append(value);
                $(tr).append(td);
            })
            tr.attr('bgindex', index);
            tr.attr('idobj', obj.EmployeeId);

            $('.grid table tbody').append(tr);
        });
        $('.loader-wrapper').css('visibility', 'hidden');
    }


    /**
     * Khởi tạo các sự kiện cho trang employee
     * Author hieunv 26/07/2021
     * */
    initEvent() {
        var seft = this;
        //1.Sự kiện khi click thêm mới
        $('#btnAdd').click(function () {
            seft.btnAddOnClick(seft, this);
        });

        //2.Sự kiện khi click exit trên form thông tin
        $('#btnExit').click(function () {
            seft.btnExitOnClick(seft, this);
        });

        //3.Sự kiện khi click save trên form thông tin
        $('#btnSave').click(function () {
            seft.btnSaveOnClick(seft, this);
        });

        //4.Sự kiện khi click hủy trên form thông tin
        $('#btnCancel').click(function () {
            seft.btnCancelOnClick(seft, this);
        });

        //5.Sự kiện khi click refresh trên form thông tin
        $('#btnRefresh').click(function () {
            seft.btnRefreshOnClick(seft, this);
        });


        //6.Sự kiện khi dbclick 1 hàng trên bảng
        $('.grid table').on('dblclick', 'tbody tr', function () {
            seft.tableRowOnDbClick(seft, this);
        });

        //14.Click vào checkbox trên các hàng
        $('.grid table tbody').on('click', 'tr td input', function () {
            seft.checkcheck(seft, this);
        });

        //7.Sự kiện khi click vào toggle
        $('#btnToggle').click(function () {
            seft.btnToggleOnClick(seft, this);
        });

        //8.Validate dữ liệu
        // Các trường bắt buộc nhập:Mã nhân viên, họ và tên, cmnd, số điện thoại, email
        $('input[required]').blur(function () {
            seft.requiredNote(seft, this);
        });

        //9.Email đúng định dạng
        $('input#inputEmail').blur(function () {
            CommonJS.checkEmail(seft, this);
        });

        //10. Lương đúng định dạng
        $('input#inputSalary').blur(function () {
            CommonJS.checkSalary(seft, this);
        });

        //11. Xóa các nhân viên được chọn
        $('#btnDelete').click(function () {
            seft.btnDeleteOnClick(seft, this);
        });

        //12.Format Salary when inputSalary keydown
        $('input#inputSalary').keyup(function () {
            seft.inputSalaryOnKeyup(seft, this);
        });

        //13.Click vào nút exit trên popup
        $('.popup button.btn-exit').click(() => {
            $('.popup').css('display', 'none');
        })


    }

    /**
     * Đổ dữ liệu vào các dropdown
     * Author hieunv 26/07/2021
     * */
    loadDropdown() {

        //1.Dropdown vị trí filter
        setTimeout(() => {
            this.loadDropdownData("Position", 1);
        }, 0)

        //2.Dropdown vị trí form
        setTimeout(() => {
            this.loadDropdownData("Position", 0);
        }, 1000)
    }

    /**
    * Load dữ liệu lên dropdown có tên trường tương ứng với fieldName
    * Auto select item đầu tiên trong sanh sách item
    * @param {any} fieldName
     */
    loadDropdownData(fieldName, isFilter) {
        var myUrl = "http://localhost:60060/api/v1/Positions";
        if (isFilter == 1) fieldName += '1';
        $.ajax({
            url: myUrl,
            method: 'GET'
        }).done(res => {
            $(`#input${fieldName}Name .dropdown-data`).empty();
            $(`#input${fieldName}Name .dropdown-main p`).empty();
            if (isFilter == 1) {
                var name = "Tất cả vị trí";
                $(`#input${fieldName}Name`).attr("value", '');
                let dropdownItemHTML = $(`<div valueid="" valuename="${name}" class="dropdown-item item-selected">
                                            <i class="fas fa-check"></i>
                                            <p>${name}</p>
                                        </div>`);
                $(`#input${fieldName}Name .dropdown-data`).append(dropdownItemHTML);
                $(`#input${fieldName}Name .dropdown-main p`).append(name);
                $(`#input${fieldName}Name`).attr("value", '');
            } else {
                $(`#input${fieldName}Name .dropdown-main p`).append(res[0][`${fieldName}Name`]);
                $(`#input${fieldName}Name`).attr("value", `${res[0][`${fieldName}Id`]}`);
            }
            $.each(res, function (idex, item) {
                var id = '';
                var name = '';
                if (isFilter == 1) {
                    name = item[`PositionName`];
                    id = item[`PositionId`];

                } else {
                    name = item[`${fieldName}Name`];
                    id = item[`${fieldName}Id`];
                }
                let dropdownItemHTML = $(`<div valueid="${id}" valuename="${name}" class="dropdown-item">
                                            <i class="fas fa-check"></i>
                                            <p>${name}</p>
                                        </div>`);
                $(`#input${fieldName}Name .dropdown-data`).append(dropdownItemHTML);
                if (id == $(`#input${fieldName}Name`).attr('value')) {
                    dropdownItemHTML.addClass('item-selected');
                }
            });
        })
    }

    /**
     * Xử lí sự kiện khi click vào button add
     * @param {any} e
     * Author hieunv 26/07/2021
     */
    btnAddOnClick(seft, thisElement) {
        seft.flagAdd = 1;
        $('.dialog').css("visibility", "visible");
        $('.dialog input').val(null);
        $('.dialog input').removeClass('border-red');
        $('.autofocus').focus();
        //lấy mã nhân viên mới binding vào form
        $.ajax({
            url: "http://localhost:60060/api/v1/Employees/NewId",
            method: 'GET',

        }).done(res => {
            $('#inputEmployeeId').val("NV-" + res);
        }).fail(res => {

        });
    }


    /**
     * Xử lí sư kiện khi click button delete
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 27/07/2021
     */
    btnDeleteOnClick(seft, thisElement) {
        var employees = seft.checkedEmployees;
        var delSuccess = 0;
        $('.popup').css('display', 'block');
        PopupJS.showPopup('danger', 'Bạn có chắc chắn muốn xóa các bản ghi được chọn?', "Xóa các bản ghi!");
        $('.popup button.btn-z').click(() => {
            $.each(employees, function (index, item) {
                $.ajax({
                    url: "http://localhost:60060/api/v1/Employees/" + item,
                    method: 'DELETE'
                }).done(res => {
                    delSuccess++;
                    seft.checkedEmployees = seft.checkedEmployees.filter(e => e !== item);
                    seft.loadData();
                    ToolTipJS.showMes('success', "Đã xóa thành công " + delSuccess + "/" + (employees.length) + " bản ghi!")
                    $('#btnDelete').css('visibility', 'hidden');
                    $('.popup').css('display', 'none');
                }).fail(res => {
                    $('.popup').css('display', 'none');
                })
            })
        })
        $('.popup button.btn-y').click(() => {
            $('.popup').css('display', 'none');
        })
    }


    /**
     * Xử lí sự kiện khi click vào button exit trên form
     * @param {any} e
     */
    btnExitOnClick(seft, thisElement) {
        $('.dialog').css("visibility", "hidden");
    }

    /**
     * Xử lí sự kiện khi click button cancel
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 26/07/2021
     */
    btnCancelOnClick(seft, thisElement) {
        $('.dialog').css("visibility", "hidden");
    }

    /**
     * Xử lí sự kiện khi click button refresh
     * @param {any} seft
     * @param {any} thisElement
     */
    btnRefreshOnClick(seft, thisElement) {
        seft.checkedEmployees = [];
        seft.loadData();
        seft.matchItemDropdown('Position1Name', "");
        $('#inputSearch').val(null);
    }

    /**
     * Xử lí sự kiện khi click vào button save
     */
    btnSaveOnClick(seft, thisElement) {
        var myUrl = "http://localhost:60060/api/v1/Employees";
        var method = 'POST';
        if (seft.flagAdd != 1) {
            myUrl = "http://localhost:60060/api/v1/Employees/" + seft.myEmployeeId;
            method = 'PUT';
        }
        var employeeId = $('#inputEmployeeId').val();
        var fullName = $('#inputFullName').val();
        if (!(employeeId == '' || fullName == '')) {
            var mes = 'Bạn có chắc chắn muốn câp nhật bản ghi này?';
            var title = 'Chỉnh sửa bản ghi!';
            if (seft.flagAdd == 1) {
                mes = 'Bạn có chắc chắn muốn thêm bản ghi này?';
                title = 'Thêm mới bản ghi!';
            }
            PopupJS.showPopup('warning', mes, title);
            $('.popup').css('display', 'block');
            $('.popup button.btn-x').click(() => {
                var employee = {};
                employee.EmployeeId = $('#inputEmployeeId').val().replaceAll('NV-', '') == "" ? null : parseInt($('#inputEmployeeId').val().replaceAll('NV-', ''));
                employee.FullName = $('#inputFullName').val();
                employee.DateOfBirth = $('#inputDateOfBirth').val() == "" ? null: $('#inputDateOfBirth').val();
                employee.Gender = $('#inputGender').attr("value") == "" ? null : parseInt($('#inputGender').attr("value"));
                employee.IdentityNumber = $('#inputIdentityNumber').val();
                employee.Address = $('#inputAddress').val();
                employee.Email = $('#inputEmail').val();
                employee.PhoneNumber = $('#inputPhoneNumber').val();
                employee.PositionId = $('#inputPositionName').attr("value") == "" ? null : parseInt($('#inputPositionName').attr("value"));
                employee.Salary = $('#inputSalary').val().replaceAll('.', '') == "" ? null : parseInt($('#inputSalary').val().replaceAll('.', ''));
                employee.JoinDate = $('#inputJoinDate').val() == "" ? null : $('#inputJoinDate').val();
                employee.WorkStatus = $('#inputWorkStatus').attr("value") == "" ? null : parseInt($('#inputWorkStatus').attr("value"));
                console.log(employee)
                console.log(JSON.stringify(employee));
                // gọi ajax post dữ liệu
                $.ajax({
                    url: myUrl,
                    method: method,
                    data: JSON.stringify(employee),
                    dataType: 'json',
                    contentType: 'application/json'
                }).done(res => {
                    $('.dialog').css("visibility", "hidden");
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
        } else {
            ToolTipJS.showMes('danger', "Bạn chưa nhập đủ các trường bắt buộc!");
        }
    }

    /**
     * Hiển thị form và đổ dữ liệu vào form thông tin người được chọn
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 26/07/2021
     */
    tableRowOnDbClick(seft, thisElement) {
        seft.flagAdd = 0;
        $('.dialog input').removeClass('border-red');
        try {
            // Gọi api lấy dữ liệu
            const employeeId = $(thisElement).attr('idobj');
            seft.myEmployeeId = employeeId;
            var myUrl = "http://localhost:60060/api/v1/Employees/" + employeeId;
            $.ajax({
                url: myUrl,
                method: 'GET'
            }).done(function (res) {
                $('#inputEmployeeId').val("NV-" + res['EmployeeId']);
                $('#inputFullName').val(res['FullName']);
                $('#inputDateOfBirth').val(CommonJS.formatDateToValue(res['DateOfBirth']));
                //match drropdown GenderName
                var value1 = res['Gender'];
                seft.matchItemDropdown('Gender', value1);
                $('#inputIdentityNumber').val(res['IdentityNumber']);
                $('#inputAddress').val(res['Address']);
                $('#inputEmail').val(res['Email']);
                $('#inputPhoneNumber').val(res['PhoneNumber']);
                //match dropdown PostionName
                var value2 = res['PositionId'];
                seft.matchItemDropdown('PositionName', value2);

                var valueSalary = res['Salary'];
                $('#inputSalary').val(CommonJS.formatSalary(valueSalary));
                $('#inputJoinDate').val(CommonJS.formatDateToValue(res['JoinDate']));
                //match dropdown WorkStatus
                var value4 = res['WorkStatus'];
                seft.matchItemDropdown('WorkStatus', value4);
            }).fail(function (res) {

            })

            $('.dialog').css("visibility", "visible");
            $('.autofocus').focus();
        } catch (e) {

        }
    }

    /**
     * load dữ liệu của người được chọn lên các dropdown
     * @param {any} fieldName
     * @param {any} value
     * Author hieunv 26/07/2021
     */
    matchItemDropdown(fieldName, value) {
        var myDropdownItems = $(`#input${fieldName} .dropdown-data .dropdown-item`);
        $(`#input${fieldName}`).attr('value', value);
        $.each(myDropdownItems, function (index, item) {
            $(item).removeClass('item-selected');
            if (value == $(item).attr('valueid')) {
                $(item).addClass('item-selected');
                $(`#input${fieldName} .dropdown-main p`).empty();
                $(`#input${fieldName} .dropdown-main p`).append($(item).attr('valuename'));
            }
        });
    }


    /**
     * Cảnh báo khi bỏ trống trường bắt buộc
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 26/07/2021
     */
    requiredNote(seft, thisElement) {
        let value = $(thisElement).val();
        if (value == '') {
            // chuyển border thành màu đỏ cảnh báo và khi hover hiện thông tin cảnh báo
            $(thisElement).addClass('border-red');
            $(thisElement).attr('title', 'Thông tin này bắt buộc nhập!');
            ToolTipJS.showMes('danger', 'Thông tin này bắt buộc nhập!')
        } else {
            $(thisElement).removeClass('border-red');
            $(thisElement).removeAttr('title');
        }
    }

    /**
     * xử lí sự kiện khi click vào checkbox trên các hàng
     * @param {any} seft
     * @param {any} thischeck
     */
    checkcheck(seft, thisElement) {
        var thischeck = $(thisElement);
        if (thischeck.is(":checked")) {
            thischeck.parents('tr').css('background-color', "#E3F3EE");
            seft.checkedEmployees.push($(thisElement).parents('tr').attr('idobj'));
            $('#btnDelete').css('visibility', 'visible');
        } else {

            if (thischeck.parents('tr').attr('bgindex') % 2 == 0) {
                thischeck.parents('tr').css('background-color', '#FFF3EB');
            } else {
                thischeck.parents('tr').css('background-color', "#ffffff");
            }

            seft.checkedEmployees = seft.checkedEmployees.filter(e => e !== thischeck.parents('tr').attr('idobj'));
            if (seft.checkedEmployees == 0) {
                $('#btnDelete').css('visibility', 'hidden');
            }
        }
    }


    /**
     * format tiềnlương khi input
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 27/07/2021
     */
    inputSalaryOnKeyup(seft, thisElement) {
        var valueSalary = $(thisElement).val().replaceAll('.', '');
        $(thisElement).val(CommonJS.formatSalary(valueSalary));
    }

    /**
     * Xử lí menu collapse khi click button toggle
     * @param {any} seft
     * @param {any} thisElement
     * Author hieunv 27/07/2021
     */
    btnToggleOnClick(seft, thisElement) {
        /*e.preventDefault();*/
        if ($('.menu').width() > 50) {
            $('.menu').width(48);
            $('.menu p').hide();
            $('.menu .menu-header .logo').hide();
            $(".content").animate(
                {
                    left: "52px",
                    width: "100%",
                },
                0,
                () => {
                    $(".content").css("width", "calc(100% - 52px)")
                }
            )
        } else {
            $('.menu').width(225);
            setTimeout(function () {
                $('.menu p').show();
                $('.menu .menu-header .logo').show();
            }, 500);
            $(".content").animate(
                {
                    left: "227px",
                },
                0,
                () => {
                    $(".content").css("width", "calc(100% - 227px)")
                }
            )
        }
    }
}