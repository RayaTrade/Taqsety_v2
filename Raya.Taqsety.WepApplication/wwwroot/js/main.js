

let baseURL = "http://localhost:18965/api/";
//let baseURL = "http://localhost:5290/api/"; //for mark

//let baseURL = "http://www.rayatrade.com/taqsetyApi/api/"; //mac Url

let fronApplicationPath = "";

//let fronApplicationPath = "/taqsety";

let jobType = null;
let attachments = [];
let jopTitle = 0;
let clubs = [];
let installmentCard = null;
let user = null;
if (sessionStorage.getItem("userToken")) {
    user = parseJwt(sessionStorage.getItem("userToken"));
}

let statuses = [];

function getAllGovernerate(lang , selector){
    $.ajax({
        url: baseURL + "Data/GetAllGouvernrates?lang="+lang,
        type: "GET",
        datatype: "json",
        async: false,
        contentType: "application/json",
        success: function (res) {   
           
            if (res.succeded) {
              
                $(selector).append(`<option value="0"> </option>`)
                $.each(res.content, function (i, governerate) {
                    
                    $(selector).append(`<option  value="${governerate.id}">${governerate.name} </option>`)
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

function getAllStatuses(lang) {
    $.ajax({
        url: baseURL + "Data/GetAllStatuses?lang=" + lang,
        type: "GET",
        datatype: "json",
        async: false,
        contentType: "application/json",
        success: function (res) {

            if (res.succeded) {

                //$(selector).append(`<option value="0"> </option>`)
                //$.each(res.content, function (i, governerate) {

                //    $(selector).append(`<option  value="${governerate.id}">${governerate.name} </option>`)
                //})
                if (user) {

                    statuses = res.content;
                    //if (user.RoleId == 2) {  //operation
                    //    statuses = res.content.filter((res) => { return res.id == 1 || res.id == 2 || res.id == 5 })
                    //}
                    //if (user.RoleId == 3) { //credit team 
                    //    statuses = res.content.filter((res) => { return res.id == 2 || res.id == 3 || res.id == 5  })
                    //}
                }

                //statuses = ;
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

function getAllGovernerate(lang, selector , selectedValue) {
    $.ajax({
        url: baseURL + "Data/GetAllGouvernrates?lang="+lang,
        type: "GET",
        datatype: "json",
        async: false,
        contentType: "application/json",
        success: function (res) {
           
            if (res.succeded) {
              
                $(selector).append(`<option value="0"> </option>`)
                $.each(res.content, function (i, governerate) {
                    
                    $(selector).append(`<option selected="${selectedValue}" value="${governerate.id}">${governerate.name} </option>`)
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
        url: baseURL + "Data/GetAllClubs?langId=en",
        type: "GET",
        datatype: "json",

        contentType: "application/json",
        success: function (res) {

            if (res.succeded) {
               
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
                localStorage.setItem("mobileNumber", $("#mobileNumber").val())
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

function readURL(input, selectorToPreview) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            console.log(e.target.result)
            $(selectorToPreview).attr('src',   e.target.result );
            $(selectorToPreview).hide();
            $(selectorToPreview).fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
var selectedStatus = null;
function GetAllInstallmentCards() {
    getAllStatuses("en");
    $.ajax({
        url: baseURL + "InstallmentCard/GetAllInstallmentCards",
        beforeSend: function (xhr, settings) { xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem("userToken")); },
        type: "GET",
        datatype: "json",
        async: false,
        contentType: "application/json",
        success: function (res) {

            if (res.succeded) {
                console.log(res.content)
                let jsonOBJ = {};
                $.each(res.content,  function (i, installmentCard) {
                      selectedStatus = statuses;
                    if (installmentCard.statusId == 3) {
                        selectedStatus = selectedStatus.filter((res) => { return  res.id == 3 || res.id == 5 || res.id == 4 })
                    } else if (installmentCard.statusId == 1) {
                        selectedStatus = selectedStatus.filter((res) => { return res.id == 1 || res.id == 2 || res.id == 5 })
                    } else if (installmentCard.statusId == 2) {
                        selectedStatus = selectedStatus.filter((res) => { return res.id == 2 || res.id == 3 || res.id == 5 })
                    }
                    jsonOBJ = (installmentCard);
                    $("#installmentData tbody").append(`
                        <tr>
                        <td>${installmentCard.nationalId}  </td>
                        <td> ${installmentCard.name} </td>
                        <td>${installmentCard.mobileNumber}</td>
                        <td class="status" >
                        <span class="statusName" value='${installmentCard.id}'> ${selectedStatus.filter((status) => status.id == installmentCard.statusId)[0].name } </span>

                        <select class="form-control statusDDL" onchange="updateStatus()" style="display:none">
                        ${ selectedStatus.map((status) => "<option value='" + status.id + "'>" + status.name + "</option>") }
                        </select>
                
                        </td >

                        <td class="text-center">  <i onclick="getCustomerAdressDetails(${installmentCard.addressDetailsId})" class="text-primary fa-solid iconInfo fa-circle-info " data-toggle="modal" data-target="#adressDetails"></i></td>
                        <td class="text-center">  <i class="text-primary fa-solid iconInfo fa-circle-info " data-toggle="modal" onclick='getJobDetailsData("${installmentCard.jobDetailsId}")' data-target="#jobDetails"></i> </td>
                        <td class="text-center">  <i onclick="getCustomerAttachments(${installmentCard.customerId })" class="text-primary fa-solid iconInfo fa-circle-info " data-toggle="modal" data-target="#attachment"></i></td>
                        
                        <td class="text-center">  <i onclick="getCustomerInfo( ${installmentCard.id},${installmentCard.nationalId} ,'${installmentCard.name}' ,'${installmentCard.mobileNumber}' ,${installmentCard.approvedCreditLimit})" class="text-primary fa-solid iconInfo fa-circle-info " data-toggle="modal" data-target="#customerInfo"></i></td>
                        <td class="text-center">  <i onclick="getGuarntorInfo(${installmentCard.grantorId })" class="text-primary fa-solid iconInfo fa-circle-info " data-toggle="modal" data-target="#guarntorInfo"></i></td>

                        </tr>
                    `)
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


let currentCustomerId = null;
function getCustomerAttachments(customerId) {

    setImagesToDefault()
    currentCustomerId = customerId;
    $.ajax({
        url: baseURL + `Customer/GetAllCustomerAttachments?customerId=${customerId}`,
        type: "GET",
        datatype: "json",
        async: false,
        contentType: "application/json",
        success: function (res) {

            if (res.succeded) {
                console.log(res.content)
               

                res.content.forEach(function (currentVal,index) { //filling attachments
                    if (currentVal.attachmentTypeId == 1) { //front national id
                        $("#frontIdPreview").attr('src', currentVal.imageBase64);
                    }
                    if (currentVal.attachmentTypeId == 2) { //back national id 
                        $("#backIdPreview").attr('src', currentVal.imageBase64);
                    }
                    if (currentVal.attachmentTypeId == 3) { //Medical card 
                        $("#medicalIdPreview").attr('src', currentVal.imageBase64);
                    }
                    if (currentVal.attachmentTypeId == 4) { //car lisnce card 
                        $("#carIdPreview").attr('src', currentVal.imageBase64);
                    }
                    if (currentVal.attachmentTypeId == 5) { //club lisnce card 
                        $("#clubIdPreview").attr('src', currentVal.imageBase64);
                    }
                })

                res.content.forEach(function (currentVal, index) { //clearing the images base64 from the obj to save it to the session storage
                    currentVal.imageBase64 = '';
                })
                if (!sessionStorage.uploadedAttachments) {
                   
                    sessionStorage.setItem("uploadedAttachments", JSON.stringify(res.content))
                } else {
                    sessionStorage.uploadedAttachments = JSON.stringify(res.content)
                }
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
function setImagesToDefault() {
    $("#frontIdPreview").attr('src', "https://www.contentviewspro.com/wp-content/uploads/2017/07/default_image.png");
    $("#backIdPreview").attr('src', "https://www.contentviewspro.com/wp-content/uploads/2017/07/default_image.png");
    $("#medicalIdPreview").attr('src', "https://www.contentviewspro.com/wp-content/uploads/2017/07/default_image.png");
    $("#carIdPreview").attr('src', "https://www.contentviewspro.com/wp-content/uploads/2017/07/default_image.png");
    $("#clubIdPreview").attr('src', "https://www.contentviewspro.com/wp-content/uploads/2017/07/default_image.png");
}

function UpdateAddressDetails(addressDetails) {
    $.ajax({
        url: baseURL + "AddressDetails/UpdateAddressDetails",
        type: "PUT",
        beforeSend: function (xhr, settings) { xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem("userToken")); },
        datatype: "json",
        data: JSON.stringify(addressDetails),
        contentType: "application/json",
        async: false,
        success: function (res) {
            if (res.succeded) {
                console.log(res.content)
                fillAddress(res.content)
                successToaster(res.message);
                $("#jobDetails").modal("hide");
            }
            else {

            }
        },
        error: function (err) {
            console.log("error: ", err)
        }
    });
}

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
};


function redirectToSummary() {
    window.location.replace(window.location.origin.concat(fronApplicationPath + '/installmentdata/summary'))
}
function ShowLoader() {
    $(".loaderContainer").show()
    $(".loaderContainer").fadeTo(1000,0.7)
}
function hideLoader() {
    $(".loaderContainer").fadeTo(1000, 0)
    $(".loaderContainer").hide(1200)

}

async function fetchPut(url, obj) {
    ShowLoader()
    var response = await fetch(baseURL + url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(obj),
    });
    const data = await response.json()
    hideLoader()
    return data;
}

async function fetchGet(url) {
    ShowLoader()
    var response = await fetch(baseURL + url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
       // body: JSON.stringify(obj),
    });
    const data = await response.json()
    hideLoader()
    return data;
}

async function ajaxPost(url,obj) {
    $.ajax({
        url: baseURL + url,
        type: "GET",
        //beforeSend: function (xhr, settings) { xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem("userToken")); },
        datatype: "json",
        data: JSON.stringify(obj),
        contentType: "application/json",
        async: false,
        success: function (res) {
            if (!res.succeded) {
                failureToaster(res.message)
            }
            return res;
        },
        error: function (err) {
            console.log("error: ", err)
        }
    });
}
   
 $(document).ajaxSend(function () {
        ShowLoader()
    });
    $(document).ajaxComplete(function () {
        hideLoader()
    });

