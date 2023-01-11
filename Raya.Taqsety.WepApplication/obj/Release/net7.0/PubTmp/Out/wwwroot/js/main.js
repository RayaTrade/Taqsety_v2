
let baseURL = "http://localhost:18965/api/";
let jobType = null;
let attachments = [];
let jopTitle = 0;
let clubs = [];
function getAllGovernerate(lang , selector){
    $.ajax({
        url: baseURL + "Data/GetAllGouvernrates?lang="+lang,
        type: "GET",
        datatype: "json",
        async: false,
        contentType: "application/json",
        success: function (res) {
           
            if (res.succeded) {
                console.log(res)
                $(selector).append(`<option value=""> </option>`)
                $.each(res.content, function (i, governerate) {
                    
                    $(selector).append(`<option value="${governerate.id}">${governerate.name} </option>`)
                })
            }
            else {
                console.log(res);
            }
        },
        error: function (err) {
            console.log("error: ", err)
        }
    });
}



function getAllCitiesByGovernerateId(governerateId,lang , selector) {
    $.ajax({
        url: baseURL + "Data/GetAllGouvernrateCities?gouvernrteId="+governerateId+"&lang=" + lang,
        type: "GET",
        datatype: "json",
        async:false,
        contentType: "application/json",
        success: function (res) {
            
            if (res.succeded) {
                $(selector).empty();
                $.each(res.content, function (i, city) {
                    console.log(city.name)
                    $(selector).append(`<option value="${city.id}">${city.name} </option>`)
                })
            }
            else {
                console.log(res);
            }
        },
        error: function (err) {
            console.log("error: ", err)
        }
    });
}

function getAllClubs(lang) {
    $.ajax({
        url: baseURL + "Data/GetAllClubs",
        type: "GET",
        datatype: "json",

        contentType: "application/json",
        success: function (res) {

            if (res.succeded) {
                console.log(res)
                $("#sportClubId").append(`<option value=""> </option>`)
                $.each(res.content, function (i, club) {

                    $("#sportClubId").append(`<option value="${club.id}">${club.name} </option>`)
                })
            }
            else {
                console.log(res);
            }
        },
        error: function (err) {
            console.log("error: ", err)
        }
    });
}
function formSubmit(customerOBJ) {
    $.ajax({
        url: baseURL + "InstallmentCard/InsertInstallmentCard",
        type: "POST",
        datatype: "json",
        data: JSON.stringify(customerOBJ),
        contentType: "application/json",
        success: function (res) {
            if (res.succeded) {

                window.location.replace(window.location.origin + "/installmentdata/summary");
            }
            else {
                console.log(res);
            }
        },
        error: function (err) {
            console.log("error: ", err)
        }
    });
}
function validateNumber(e) {
    const pattern = /^[0-9]$/;

    return pattern.test(e.key)
}

const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});

async function getBase64(selector) {
    const file = $(selector)[0].files[0];
    try {
        const result = await toBase64(file);
        return result
    } catch (error) {
        console.error(error);
        return;
    }
   
}
const getFileName = selector => $(selector)[0].files[0].name


function validateCustomerName() {
    $("#customerName").keydown(function (e) {
        let fullName = $(this).val().toString().split(' ');
        let bars = $(".nameBar");

        if ($("#customerName").val() == "" && fullName.length == 1) {

            $(bars[0]).css("background-color", "#d2d2d2");
        } if (fullName.length == 1) {
            $(bars[0]).css("background-color", "#f2b611")
            $(bars[1]).css("background-color", "#d2d2d2");
            $(bars[2]).css("background-color", "#d2d2d2");
            $(bars[3]).css("background-color", "#d2d2d2");
            $("#updateBtn").prop("disabled", true)

        }
        if (fullName.length == 2) {
            $(bars[0]).css("background-color", "#f2b611")
            $(bars[1]).css("background-color", "#f2b611")
            $(bars[2]).css("background-color", "#d2d2d2");
            $(bars[3]).css("background-color", "#d2d2d2");
            $("#updateBtn").prop("disabled", true)

        } if (fullName.length == 3) {
            $(bars[0]).css("background-color", "#f2b611");
            $(bars[1]).css("background-color", "#f2b611");
            $(bars[2]).css("background-color", "#f2b611");
            $(bars[3]).css("background-color", "#d2d2d2");
            $("#updateBtn").prop("disabled", true)
        }
        if (fullName.length >= 4) {

            $(bars[0]).css("background-color", "green");
            $(bars[1]).css("background-color", "green");
            $(bars[2]).css("background-color", "green");
            $(bars[3]).css("background-color", "green");

            $("#updateBtn").removeAttr('disabled');

        }
       
    })
}



