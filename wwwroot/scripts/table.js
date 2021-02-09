



window.setDataTable =()=>{

    

    let url="https://localhost:5001/";
    $("#testbook").css("color","blue");


    $(document).ready(function() {


$.get(url+"api/values/GetColumns",function(response) {



for (let i = 0; i < response.length; i++) {
    $("#tr1").append(`<th>${response[i].index}</th/>`);
    $("#tr2").append(`<th class="gray">${response[i].englishTerm}</th/>`);
    $("#tr3").append(`<th>${response[i].hebrewTerm}</th/>`);

}


$("#tr1").append(`<th></th/>`);
$("#tr2").append(`<th class="gray"></th/>`);
$("#tr3").append(`<th>הוספת הערה</th/>`); 

}).then(function () {


 var dt=   $('#table').DataTable({

        "ajax": url+"api/values/GetFileData",
        "columns": [
            { "data": "entry" },
            { "data": "description" },
            {
                data: null,
                className: "center",
                defaultContent: '<a href="" class="editor_edit">Edit</a> / <a href="" class="editor_remove">Delete</a>'
            }
        ]

        
        
    });

    var filterdt= $('#table').DataTable().columns(1).data().filter(function (value,index) {
        console.log(value);
return value=="hub"?true:false;

    });


   



});



    
    } );






    }
