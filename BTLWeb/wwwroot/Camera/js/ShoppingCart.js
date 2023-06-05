var temp = []

function loadData() {
    $.ajax({
        url: 'https://localhost:44368/shoppingcart/getall',
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            var str = '';
            var count = 0;
            res.forEach(function (item) {
                temp.push(item)
                count += 1;
                var money = item.product.giaLonNhat * item.quantity;
                str +=
                    `<tr>
                        <td class="stt">${count}</td>
                        <td><img src="../img/ImageCamera/${item.product.anhDaiDien}" alt="" style="width: 50px;"></td>
                        <td>${item.product.tenSp}</td>
                        <td class="align-middle price">${item.product.giaLonNhat}</td>
                        <td class="align-middle">
                            <div class="input-group quantity mx-auto" style="width: 100px;">
                                <div class="input-group-btn">
                                    <button class="btn btn-sm btn-primary btn-minus">
                                        <i class="fa fa-minus"></i>
                                    </button>
                                </div>
                                <input type="text" class="form-control form-control-sm bg-secondary border-0 text-center" value="${item.quantity}">
                                <div class="input-group-btn">
                                    <button class="btn btn-sm btn-primary btn-plus">
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                        </td>
                        <td class="align-middle numPrice" id="${count}">${money}</td>
                    </tr>`
            })

            if (count == 0) {
                str = "<tr><td colspan='7'><p>Giỏ hàng trống</p></td></tr>"
            }

            $('#showCart').html(str);

            $('.quantity button').on('click', function () {
                var button = $(this);
                var oldValue = button.parent().parent().find('input').val();
                if (button.hasClass('btn-plus')) {
                    var newVal = parseFloat(oldValue) + 1;
                } else {
                    if (oldValue > 0) {
                        var newVal = parseFloat(oldValue) - 1;
                    } else {
                        newVal = 0;
                    }
                }
                button.parent().parent().find('input').val(newVal);
                var parent = $(this).closest("tr");
                // Tìm đến phần tử có class là "numPrice" trong phần tử cha
                var numPrice = parent.find(".numPrice");
                // Thay đổi giá trị của phần tử "numPrice"
                var price = parent.find(".price").text();
                var id = parent.find(".stt").text();
                id = parseInt(id) - 1;
                temp[id].quantity = newVal;
                const num = parseFloat(price.replace(',', '.'));
                numPrice.text(newVal * 1 * num);

                $.ajax({
                    url: 'https://localhost:44368/shoppingcart/update',
                    data: {
                        cartData: JSON.stringify(temp)
                    },
                    type: 'POST',
                    dataType: 'json',
                    success: function (res) {
                        updateSumMoney()
                    }
                })
            });
            updateSumMoney()

        }
    })
}

function updateSumMoney() {
    $.ajax({
        url: 'https://localhost:44368/shoppingcart/getall',
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            var sum = 0
            res.forEach(function (item) {
                var money = item.product.giaLonNhat * item.quantity;
                sum += money;
            })
            $('#sumMoney').html(sum)
            $('#sumAllMoney').html(sum + 10000)
        }
    })
}

loadData();

function btnDeleteAll(check) {
    $.ajax({
        url: 'https://localhost:44368/shoppingcart/DeleteAll',
        type: 'POST',
        dataType: 'json',
        success: function (res) {
            if (res.status) {
                if (check == true) {
                    $('#noiDung').html("Successfully deleted!");
                    $('#success_tic').modal('show');
                }

            } else {
                alert('Bạn không có sản phẩm nào để xóa!')
            }
            loadData();
        }
    })
}

function btnContinue() {
    window.location.href = "../";
}

function btnCheckout() {
    $.ajax({
        url: 'https://localhost:44368/shoppingcart/checkkhachhang',
        type: 'Get',
        success: function (res) {
            if (res.status) {
                var tongtien = $('#sumAllMoney').text();
                var note = $('#note').val()

                var hoaDonBan = {
                    TongTienHD: tongtien,
                    PhuongThucThanhToan: 0,
                    GhiChu: note,
                    TrangThai: 0,
                }
                
                $.ajax({
                    url: 'https://localhost:44368/shoppingcart/CreateOrderNoCreateKhachHang',
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        orderViewModel: JSON.stringify(hoaDonBan)
                    },
                    success: function (res) {
                        $('#noiDung').html("Order has been recorded");
                        $('#success_tic').modal('show');
                        btnDeleteAll(false);
                    }
                })
            } else {
                $('.bd-example-modal-lg').modal('show');
            }
        }
    })
}

function confirmCheckout() {

    var tongtien = $('#sumAllMoney').text();
    var note = $('#note').val()

    var hoaDonBan = {
        TongTienHD: tongtien,
        PhuongThucThanhToan: 0,
        GhiChu: note,
        TrangThai: 0,
    }

    var yourname = $('#yourname').val()
    var birthDay = $('#birthDay').val()
    var phone = $('#phone').val()
    var address = $('#address').val()


    var _khachhang = {
        TenKhachHang: yourname,
        NgaySinh: new Date(birthDay.split("/").reverse().join("-")),
        SoDienThoai: phone,
        DiaChi: address
    }

    $.ajax({
        url: 'https://localhost:44368/shoppingcart/CreateOrder',
        type: 'POST',
        dataType: 'json',
        data: {
            orderViewModel: JSON.stringify(hoaDonBan),
            khachHang: JSON.stringify(_khachhang),
        },
        success: function (res) {
            if (res.status) {
                $('#noiDung').html("Order has been recorded");
                $('#success_tic').modal('show');
                btnDeleteAll(false);
            } else {
                alert("Bạn chưa có sản phẩm nào trong giỏ hàng!")
            }
        }
    })
}


