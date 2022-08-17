
// add related styles => .required:after {color: red;content: "*";} .characterCount {display: inline - block;font - size: 80 %;color: #6a7a8c;}

$(document).ready(function () {

    $('[data-val-maxlength-max]').each(function () {
        $(this).before("<div class='characterCount'>(<span class='typed-length'>0</span>/<span class='max-length'>0</span>)</div>");
    });

    SetCharacterLimit();
    $(document).on('keyup', 'input[data-val-maxlength-max],textarea[data-val-maxlength-max]', CharactersCount);
    $(document).on('loadeddata', 'input[data-val-maxlength-max],textarea[data-val-maxlength-max]', CharactersCount);
});

function CharactersCount() {
    //debugger;
    let textField = event.target;
    let count = $(textField).val().length;
    let counterContainer = $(textField).prev();
    let maxLength = $(textField).attr("data-val-maxlength-max");
    counterContainer.show(1000);
    if (counterContainer.hasClass('characterCount')) {
        counterContainer.find('.typed-length').text(count);
        if (count == maxLength) {
            $(counterContainer).addClass("text-danger");
        } else {
            $(counterContainer).removeClass("text-danger");
        }
    }
}

function SetCharacterLimit() {
    let allLimitedInputs = $("[data-val-maxlength-max]");
    for (let item of allLimitedInputs) {
        let maxLength = $(item).attr("data-val-maxlength-max");
        let maxLengthSpan = $(item).prev();
        if (maxLengthSpan.hasClass('characterCount'))
            maxLengthSpan.find('.max-length').text(maxLength);
    }
}