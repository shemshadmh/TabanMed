MedicalServiceList = {
    fields: {},
    methods: {
        createReqHandler: (response) => {

            if (response && !response.Errors) {
                $("#suc_list").html(`<li>${response.UserMessage}</li>`);
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
                $("#error-area").fadeOut(1000);
            }, 5000);
        },
        editReqHandler: (response) => {
            if (response && !response.Errors) {
                $("#suc_list").html(`<li>${response.UserMessage}</li>`);
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
        reqEnded: (e) => {
            if (e.type === "create") {
                MedicalServiceList.methods.createReqHandler(e.response);
            } else if (e.type === "update") {
                MedicalServiceList.methods.editReqHandler(e.response);
            }
            $(".preloader").hide();
        }
    },
    services: {},
    eventListeners: () => { },
    init: () => {
        MedicalServiceList.eventListeners();
    }
}