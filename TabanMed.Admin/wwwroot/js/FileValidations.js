$(document).on("submit", "form", (e) => {
    let inputFiles = $(e.currentTarget).find("input[type=file]");
    if (!inputFiles) {
        return;
    }
    for (let inputFile of inputFiles) {
        let filename = inputFile.files[0].name;
        let ext = filename.substring(filename.lastIndexOf('.'));
        console.log(ext)
        var allowExtentions = $(inputFile).attr('accept').split(',');
        if (!allowExtentions.includes(ext)) {
            console.log("nok")
            e.preventDefault();
            appendValidationErrors(e.currentTarget,["فرمت فایل انتخابی مورد قبول نیست"])


        }
    }
})



//appendValidationErrors: (formSelector, errors) => {
function appendValidationErrors(formSelector, errors) {
    let validationSummeryDiv = $(formSelector).find("[data-valmsg-summary=true]");
    validationSummeryDiv.removeClass("validation-summary-valid");
    validationSummeryDiv.addClass("validation-summary-errors");
    let validationSummeryUl = validationSummeryDiv.find("ul");
    validationSummeryUl.html("");
    for (let item of errors) {
        let itemElement = $("<li>" + item + "</li>");
        validationSummeryUl.append(itemElement);
    }
}