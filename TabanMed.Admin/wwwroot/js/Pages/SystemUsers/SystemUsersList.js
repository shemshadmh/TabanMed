

$(document).ready(function () {

    $('#dt_config').DataTable({
        paging: true,
        "language": {
            "lengthMenu": "تعداد نمایش در صفحه: _MENU_",
            "zeroRecords": "متاسفانه موردی یافت نشد!",
            "info": "نمایش صفحه _PAGE_ از _PAGES_",
            "infoEmpty": "رکوردی ثبت نشده است !",
            "infoFiltered": "(فیلتر شده از میان _MAX_ رکورد)",
            "search": "جستجو :",
            "paginate": {
                "first": "اولین",
                "last": "آخرین",
                "next": "بعدی",
                "previous": "قبلی"
            }
        },
        scrollY: 300,
        serverSide: true,
        lengthMenu: [5, 10,20],
        "ajax": {
            "url": "/SystemUsers/Index",
            "type": "POST",
            "datatype": "json",

        },
        "columnDefs":
            [{
                "targets": [0],
                "visible": true,
                "searchable": false,
                "orderable": false
            }],
        "columns": [
            {
                "data": "id", "name": "Id", "title": "ردیف" ,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "data": "fullname", "name": "Name", "title": "نام",
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<a class="text-dark btn" href="/Admin/SystemUsers/Details/' + row['id'] + '">' + data + '</a>';
                    }
                    return data;
                }
            },
            {
                "data": "rolesAndLabels", "name": "RolesAndLabels", "title": "نقش ها",
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        var res = "";
                        data.forEach(function (currentValue, index, arr) {

                            res += '<span class="m-1 label label-' + currentValue.roleLabelColor + '">' + currentValue.roleDisplayName + '</span>';

                        });
                        data = res;
                    }
                    return data;
                }
            },
            { "data": "createdOn", "name": "CreatedOn", "title": "تاریخ ثبت" }
        ],
        "order": [[1, 'asc']]
    });

});
