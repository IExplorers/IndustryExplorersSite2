function ViewModel() {
    var self = this;

    var tokenKey = 'accessToken';
    var tableDefinition = "<table border='4' style='background-color:white' style='width:90%'>";

    self.result = ko.observable();
    self.user = ko.observable();


    function showError(jqXHR) {
        self.result(jqXHR.status + ': ' + jqXHR.statusText);
    }

    self.applicantsApi = function () {
        //self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/participants',
            contentType: "application/json",
            dataType: "json",
            headers: headers
        }).done(function (data) {
            //var TableContent = "<table>";
            var TableContent = tableDefinition +
                    "<th>" + "First Name" + "</th>" +
                        "<th>" + "Last Name" + "</th>" +
                        "<th>" + "Email Address" + "</th>" +
                        "<th>" + "Phone" + "</th>" +
                        "<th>" + "DateCreatedOn" + "</th>" +
                        "<th>" + "City, State" + "</th>" +
                      
                        "<th>" + "ResumeUrl" + "</th>" +
                    "</tr>";
            $.each(data, function (index, element) {

                TableContent += "<tr border='2'>" + "<td>" + element.FirstName + "</td>" +
                                 "<td>" + element.LastName + "</td>" +
                                 "<td>" + element.Email + "</td>" +
                                 "<td>" + element.Phone + "</td>" +
                                 "<td>" + element.DateCreated + "</td>" +
                                 "<td>" + element.City +", "  + element.State + "</td>" +                              
                                 "<td>" + element.ResumeUrl + "</td>" +
                                 "</tr>"
                ;

            });
            TableContent += "</table>";
            $("#UpdatePanel").html(TableContent);
        }).fail(function (response) {

            alert("Unauthorized to access data");
            //alert(response.d);
        }).error(function (response) {
            alert("Unauthorized to access data");
            //alert(response.partners);
        });
    }

    self.applicantsApiExport = function () {
        
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/participants',
            contentType: "application/json",
            dataType: "json",
            headers: headers
        }).done(function (data) {

            var TableContent = tableDefinition +
            "<tr>" + "<th> ParticipantID </th>"                   
                     + "<th> DateCreated </th>"
                     + "<th> FirstName </th>"
                     + "<th> LastName </th>"
                     + "<th> Email </th>"
                     + "<th> Phone </th>"
                     + "<th> StreetAddress </th>"
                     + "<th> City </th>"
                     + "<th> State </th>"
                     + "<th> Postalcode </th>"
                     + "<th> Question1 </th>"
                     + "<th> Question2 </th>"
                     + "<th> ResumeUrl </th>"
                     + "</tr>";

            $.each(data, function (index, element) {

                TableContent += "<tr>" + "<td>" + element.ParticipantID + "</td>" +
                                 "<td>" + element.DateCreated + "</td>" +
                                 "<td>" + element.FirstName + "</td>" +
                                 "<td>" + element.LastName + "</td>" +
                                 "<td>" + element.Email + "</td>" +
                                 "<td>" + element.Phone + "</td>" +
                                 "<td>" + element.StreetAddress + "</td>" +
                                 "<td>" + element.City + "</td>" +
                                 "<td>" + element.State + "</td>" +
                                 "<td>" + element.Postalcode + "</td>" +
                                 "<td>" + element.Question1 + "</td>" +                               
                                 "<td>" + element.Question2 + "</td>" +                                 
                                 "<td>" + element.ResumeUrl + "</td>" +
                                 "</tr>"
                ;

            });
            TableContent += "</table>";
            $("#UpdatePanel").html(TableContent);
                     
            //window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id$=ExcelDiv]').html()));

            var dt = new Date();
            var day = dt.getDate();
            var month = dt.getMonth() + 1;
            var year = dt.getFullYear();
           
            var postfix = day + "_" + month + "_" + year;
            //creating a temporary HTML link element (they support setting file names)
            var a = document.createElement('a');
            //getting data from our div that contains the HTML table
            var data_type = 'data:application/vnd.ms-excel';
            var table_div = document.getElementById('UpdatePanel');
            var table_html = table_div.outerHTML.replace(/ /g, '%20');
            a.href = data_type + ', ' + table_html;
            //setting the file name
            var table_html = table_div.outerHTML.replace(/ /g, '%20');
            console.log("table_html");
            console.log(table_html);
            a.download = 'Applicants_' + postfix + '.xls';
            //triggering the function
            a.click();

        }).fail(function (response) {

            alert("Unauthorized to access data");
            //alert(response.d);
        }).error(function (response) {
            alert("Unauthorized to access data");
            //alert(response.partners);
        });
    }

    self.partnersApi = function () {
        //self.result('');

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'GET',
            url: '/api/partners',
            contentType: "application/json",
            dataType: "json",
            headers: headers
        }).done(function (data) {
            //var TableContent = "<table>";
            var TableContent = tableDefinition +
                            "<th>" + "Organization Name" + "</th>" +
                                "<th>" + "Contact Name" + "</th>" +
                                "<th>" + "Email Address" + "</th>" +
                                "<th>" + "Website" + "</th>" +
                                "<th>" + "DateCreatedOn" + "</th>" +
                            "</tr>";
            $.each(data, function (index, element) {

                TableContent += "<tr border='2' style='width:100%'>" + "<td>" + element.organization_name + "</td>" +
                                 "<td>" + element.contact_name + "</td>" +
                                 "<td>" + element.email + "</td>" +
                                 "<td>" + element.website + "</td>" +
                                 "<td>" + element.date_created + "</td></tr>"
                ;

            });
            TableContent += "</table>";
            $("#UpdatePanel").html(TableContent);
        }).fail(function (response) {

            alert("Unauthorized to access data");
            //alert(response.d);
        }).error(function (response) {
            alert("Unauthorized to access data");
            //alert(response.partners);
        });
    }

}

var app = new ViewModel();
ko.applyBindings(app);

