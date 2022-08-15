PhotoHandler = {
    fields: {},
    methods: {
        loadFailed: () => {
            event.target.src = "/images/NoImage.png";
        }
    },
    eventListeners: () => {
        $("img").on("error", function (e) {
            e.target.src = "/images/NoImage.png";
        });
        $(document).on("change", ".previewImg", (e) => {
            let preImg = $(e.target).parents('.preview-container').find('img.show_preview_img');
            if (e.target.files && e.target.files[0] && preImg) {
                let reader = new FileReader();
                reader.onload = function (ev) {
                    $(preImg).attr('src', ev.target.result);
                };
                reader.readAsDataURL(e.target.files[0]);
            }
        });
    },
    init: () => {
        PhotoHandler.eventListeners();
    }
}

$(document).ready(() => {
    PhotoHandler.init();
})