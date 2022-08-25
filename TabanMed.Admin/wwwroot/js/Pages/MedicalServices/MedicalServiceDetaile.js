MedicalServiceDetaile = {
    fields: {
        
        editMedcialServiceModalId: "edit-medical-service-modal",
        
    },
    methods: {
        createmedicalServiceCompleteCallback: (response) => {
            if (response.responseJSON.IsSucceeded === true) {
                swal({
                    title: " تغیرات مورد نظر با موفقیت انجام شد", type: "success", timer: 2000, showConfirmButton: false
                });
                $("#" + MedicalServiceDetaile.fields.editMedcialServiceModalId).modal("hide");
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
            $("#" + MedicalServiceDetaile.fields.editMedcialServiceModalId).modal("show")
        });
    },
    init: () => {
        MedicalServiceDetaile.eventListeners();
    }
}