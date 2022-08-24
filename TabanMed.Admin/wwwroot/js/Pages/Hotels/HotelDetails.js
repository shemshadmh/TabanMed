HotelDetails = {
    fields: {
        
        editHotelFacilitiesModalId: "edit-hotel-facilities-modal",
        
    },
    methods: {
        createFacilityCompleteCallback: (response) => {
            if (response.responseJSON.IsSucceeded === true) {
                swal({
                    title: "awASDASD مورد نظر با موفقیت انجام شد", type: "success", timer: 2000, showConfirmButton: false
                });
                $("#" + HotelDetails.fields.editHotelFacilitiesModalId).modal("hide");
                window.location.reload();
            } else {
                swal("خطا", response.responseJSON.Message, "error");
            }
        },
    },
    services: {
        
    },
    eventListeners: () => {
        $(document).on("click", ".edit-hotel-Facilities", (e) => {
            $("#" + HotelDetails.fields.editHotelFacilitiesModalId).modal("show")
        });
    },
    init: () => {
        HotelDetails.eventListeners();
    }
}