FacilityList = {
    fields: {
        currentRowData: null,
        editModalId: "edit-facility-modal",
        createModalId: "create-facility-modal",
        KendoGridObject: null,
        deleteTransportUrl: "/delete-facility",
        createFormId: "create-facility-form",
        parentRowData: null
    },
    methods: {
        actionsPopover: (e) => {
            $('.popover').popover('hide');
            FacilityList.fields.KendoGridObject = $("#" + $(e.delegateTarget).attr("id")).data("kendoGrid");
            FacilityList.fields.currentRowData = FacilityList.fields.KendoGridObject
                .dataItem($(e.currentTarget).closest("tr"));

            //FacilityList.fields.parentRowData = $("#grid").data("kendoGrid").dataItem($(e.currentTarget).closest("tr"));

            let poop = $(e.currentTarget).popover({
                placement: "right",
                content: $("<div class='more-popover-content'>" +
                    "<a class='edit-facility btn btn-warning btn-block text-white'>ویرایش</a>" +
                    "</div>"),
                html: true,
                trigger: "click",
                title: "عملیات ها‍",
            })
            $(e.currentTarget).popover("show");
        },
        editTransportCompleteCallback: (response) => {
            if (response.responseJSON.IsSucceeded === true) {
                swal({
                    title: "عملیات مورد نظر با موفقیت انجام شد", type: "success", timer: 2000, showConfirmButton: false
                });
                $("#" + FacilityList.fields.editModalId).modal("hide");
                FacilityList.methods.reloadKendoGrid();
            } else {
                swal("خطا", response.responseJSON.Message, "error");
            }
        },
        createFacilityCompleteCallback: (response) => {
            if (response.responseJSON.IsSucceeded === true) {
                swal({
                    title: "عملیات مورد نظر با موفقیت انجام شد", type: "success", timer: 2000, showConfirmButton: false
                });
                $("#" + FacilityList.fields.createModalId).modal("hide");
                FacilityList.methods.reloadKendoGrid();
            } else {
                swal("خطا", response.responseJSON.Message, "error");
            }
        },
        createReqHandler: (response) => {
            if (response && !response.Errors) {
                $("#suc_list").html("<li>عملیات با موفقیت انجام شد</li>");
                $("#success-area").fadeIn(2000);
                setTimeout(() => {
                    $("#success-area").fadeOut(1000);
                }, 5000);
                return;
            }
            for (let item in response.Errors) {
                $("#error_list").append(`<li>${item}</li>`);
            }
            $("#error_list").fadeIn(2000);
            setTimeout(() => {
                $("#error_list").fadeOut(1000);
            }, 5000);
        },
        editReqHandler: (response) => {
            if (response && !response.Errors) {
                $("#suc_list").html("<li>عملیات با موفقیت انجام شد</li>");
                $("#success-area").fadeIn(2000);
                setTimeout(() => {
                    $("#success-area").fadeOut(1000);
                }, 5000);
                return;
            }
            for (let item in response.Errors) {
                $("#error_list").append(`<li>${item}</li>`);
            }
        },
        reloadKendoGrid: () => {
            $(".k-pager-refresh").trigger("click");
        },
        reqEnded: (e) => {
            if (e.type === "create") {
                FacilityList.methods.createReqHandler(e.response);
            } else if (e.type === "update") {
                FacilityList.methods.editReqHandler(e.response);
            }
            $(".preloader").hide();
        }
    },
    services: {
        deleteTransport: () => {
            let token = $('input[name=__RequestVerificationToken]').val();
            $.ajax({
                url: FacilityList.fields.deleteTransportUrl, headers: {
                    "X-XSRF-Token": token
                }, type: "POST", data: JSON.stringify({
                    Id: FacilityList.fields.currentRowData.id,
                }), contentType: "application/json", beforeSend: function (xhr) {
                    $(".lds-ellipsis").show();
                }, success: function (res) {
                    if (res.IsSucceeded) {
                        $(elementToRemove).remove();
                        swal({
                            title: "موفق",
                            text: "عملیات با موفقیت انجام شد",
                            timer: 1000,
                            showConfirmButton: false,
                            icon: "success"
                        });
                    } else {
                        swal("خطا", res.Message, "error");
                    }
                }, error: function (xhr, status, error) {
                    if (xhr.status == 403 || xhr.status == 401) {
                        alert('دسترسی غیر مجاز!');
                    } else if (xhr.status == 400 || xhr.status == 404) {
                        alert('موردی پیدا نشد!');
                    } else if (xhr.status == 500) {
                        alert('مشکلی بوجود اومد!');
                    }
                }
            });
        }
    },
    eventListeners: () => {
        $(document).on("click", ".edit-facility", (e) => {
            let form = $("#edit-facility-form");
            form.find("#Id").val(FacilityList.fields.currentRowData.id);
            form.find("#ParentId").val(FacilityList.fields.currentRowData.ParentId);
            form.find("#Title").val(FacilityList.fields.currentRowData.Title);
            form.find("#IconClassName").val(FacilityList.fields.currentRowData.IconClassName);
            $("#" + FacilityList.fields.editModalId).modal("show");
        });
        $(document).on('shown.bs.modal', function () {
            $('.popover').popover('hide');
        });
        $(document).on("click", ":not(.k-grid-more) , :not(.popover.show)", function (e) {
            if ($(e.target).parents(".k-grid-more").length === 0 && $(e.target).parents(".popover").length === 0)
                $('.popover').popover('hide');
        });
        $(document).on("click", ".delete-transport", (e) => {
            swal({
                title: "حذف شرکت حمل ونقل",
                text: "آیا از حذف شرکت حمل و نقلی اطمینان دارید ؟",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "تایید",
                cancelButtonText: "انصراف",
            })
                .then((res) => {
                    if (res.value) {
                        FacilityList.services.deleteTransport();
                    }
                });
        });
        $(document).on("click", ".k-grid-create-facility-btn", (e) => {
            let parentId = $(e.currentTarget).attr("parentdata");
            $("#" + FacilityList.fields.createFormId).find("#ParentId").val(parentId);
            $("#" + FacilityList.fields.createModalId).modal("show");
        });
    },
    init: () => {
        FacilityList.eventListeners();
    }
}