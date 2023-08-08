function setAmountInput(input){
    let value = input.value;
    const regExp = /((?![\d]).)+/g;
    let isError = regExp.test(value);
    if (isError){
        value = value.replace(regExp, '');
    }
    input.value = getThousandSeperatorValue(value);
}

function getThousandSeperatorValue(number){
    const numberFormatter = Intl.NumberFormat('en-US');
    return numberFormatter.format(number)
}

function bindAmount(id){
    $(id).keyup(function(){
        let value = $(this).val();
        const regExp = /((?![\d]).)+/g;
        let isError = regExp.test(value);
        if (isError){
            value = value.replace(regExp, '');
        }
        $(this).val(getThousandSeperatorValue(value));
    })
}
