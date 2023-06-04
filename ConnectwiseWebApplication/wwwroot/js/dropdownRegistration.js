var dropdownObject = {
    "India": {
        "Delhi": ["Delhi"],
        "Maharashtra": ["Mumbai", "Nagpur", "Pune"],
        "Tamil Nadu": ["Chennai"],
        "Telangana": ["Hyderabad"],
        "Uttar Pradesh": ["Greater Noida","Noida"]
    }
}
function dropDown() {
    var countrySel = document.getElementById("selectCountry");
    var stateSel = document.getElementById("selectState");
    var citySel = document.getElementById("selectCity");
    for (var x in dropdownObject) {
        countrySel.options[countrySel.options.length] = new Option(x, x);
    }
    countrySel.onchange = function () {
        //empty Chapters- and Topics- dropdowns
        citySel.length = 1;
        stateSel.length = 1;
        //display correct values
        for (var y in dropdownObject[this.value]) {
            stateSel.options[stateSel.options.length] = new Option(y, y);
        }
    }
    stateSel.onchange = function () {
        //empty Chapters dropdown
        citySel.length = 1;
        //display correct values
        var z = dropdownObject[countrySel.value][this.value];
        for (var i = 0; i < z.length; i++) {
            citySel.options[citySel.options.length] = new Option(z[i], z[i]);
        }
    }
}