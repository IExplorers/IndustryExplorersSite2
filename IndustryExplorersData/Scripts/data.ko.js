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
                        "<th>" + "Organization" + "</th>" +
                        "<th>" + "DateCreatedOn" + "</th>" +
                    "</tr>";
            $.each(data, function (index, element) {

                TableContent += "<tr border='2'>" + "<td>" + element.first_name + "</td>" +
                                 "<td>" + element.last_name + "</td>" +
                                 "<td>" + element.email + "</td>" +
                                 "<td>" + element.organization + "</td>" +
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
