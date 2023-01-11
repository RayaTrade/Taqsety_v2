
//let baseURL = "http://localhost:18965/api/";
let baseURL = "http://www.rayatrade.com/taqsetyApi/api/"; //mac Url

//let fronApplicationPath = "www.rayatrade.com/taqsety";

let fronApplicationPath = "/taqsety";

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
                    
                    $(selector).append(`<option value=" ${governerate.id}">${governerate.name} </option>`)
                })
            }
            else {
                failureToaster(res.message);
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
                failureToaster(res.message);
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

                window.location.replace(window.location.origin.concat(fronApplicationPath+ "/installmentData/summary") );
            }
            else {
                failureToaster(res.message);;
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
    $("#customerName").keypress(function (e) {

        if (e.target.value.substr(-1) === ' ' && e.code === 'Space') {
            e.preventDefault()
            
        }
            
        $(this).val().toString().replace("  ", " ")
        $(this).val().toString().replace(".", "")

        let fullName = $(this).val().toString().split(' ');
        fullName.forEach(function (value,index) {
            if (value == "") {
                fullName.splice(index);
            }
        })
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


function successToaster(message) {

    const Toast = Swal.mixin({
        toast: true,
        position: "center",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener("mouseenter", Swal.stopTimer);
            toast.addEventListener("mouseleave", Swal.resumeTimer);
        },
    });

    Toast.fire({
        icon: "success",
        title: message,
    });
  
}
function failureToaster(message) {
    const Toast = Swal.mixin({
        toast: true,
        position: "center",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener("mouseenter", Swal.stopTimer);
            toast.addEventListener("mouseleave", Swal.resumeTimer);
        },
    });

    Toast.fire({
        icon: "error",
        title: message,
    });


}

function warningAlert(msg) {
    Swal.fire({
        title: " سوف يتم الحذف , هل أنت متأكد ؟ ",
        text: "لن تتمكن من التراجع عن هذا!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#8e24aa",
        cancelButtonColor: "#d33",
        confirmButtonText: "نعم ، احذفها!",
        cancelButtonText: "الغاء"
    }).then(res => {
        if (res.isConfirmed) {

            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                type: "DELETE",
                url: url,
                data: JSON.stringify(item),
                success: function (result) {
                    if (result.succeded) {
                        customAlert(result.message, "success");

                    } else {
                        //alert(result.message)
                        customAlert(result.message, "error");
                    }
                    $(gridId).jsGrid("loadData");

                },
                error: function (error) {
                    customAlert("حدث خطأ خاص بالاتصال بالسرفر ", "error");

                }
            });


        }
        else {
            $(gridId).jsGrid("loadData");
        }
    });;
}