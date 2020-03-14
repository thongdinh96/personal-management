$(document).ready(function () {
    String.prototype.insert = function (index, string) {
        if (index > 0) {
            return this.substring(0, index) + string + this.substring(index, this.length);
        }

        return string + this;
    };
    Date.prototype.addDays = function (days) {
        var date = new Date(this.valueOf());
        date.setDate(date.getDate() + days);
        return date;
    }
    function authenticate() {
        return gapi.auth2.getAuthInstance()
            .signIn({ scope: "https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/calendar.events https://www.googleapis.com/auth/calendar.events.readonly https://www.googleapis.com/auth/calendar.readonly" })
            .then(function () { console.log("Sign-in successful"); },
                function (err) { console.error("Error signing in", err); });
    }
    function loadClient() {
        gapi.client.setApiKey("AIzaSyDk-_n1aTaA2dnIR0NDOsy0ky3jqkUnTe4");
        return gapi.client.load("https://content.googleapis.com/discovery/v1/apis/calendar/v3/rest")
            .then(function () { console.log("GAPI client loaded for API"); },
                function (err) { console.error("Error loading GAPI client for API", err); });
    }
    function execute() {
        var cd = new Date();
        var cdS = cd.toISOString()
        var ncdS = cd.addDays(3).toISOString()
        return gapi.client.calendar.events.list({
            "calendarId": "dt96dn@gmail.com",
            "timeMax": ncdS,
            "timeMin": cdS
        })
            .then(function (response) {
                // Handle the results here (response.result has the parsed body).
                var events = response.result.items;
                events.sort(function (e1, e2) {
                    var e1sd = new Date(e1.start.dateTime)
                    var e2sd = new Date(e2.start.dateTime)
                    if (e1sd < e2sd) {
                        return -1;
                    } else if (e1sd == e2sd) {
                        return 0;
                    }
                    return 1;
                });
                var ul = $('<ul></ul>')
                $.each(events, function (index, value) {
                    var event = value
                    var startDateStr = event.start.dateTime
                    var endDateStr = event.end.dateTime
                    var sd = new Date(startDateStr)
                    var ed = new Date(endDateStr)

                    var weekday = sd.getDay()
                    switch (weekday) {
                        case 0:
                            weekday = 'Chủ nhật'
                            break;
                        case 1:
                            weekday = 'Thứ 2'
                            break;
                        case 2:
                            weekday = 'Thứ 3'
                            break;
                        case 3:
                            weekday = 'Thứ 4'
                            break;
                        case 4:
                            weekday = 'Thứ 5'
                            break;
                        case 5:
                            weekday = 'Thứ 6'
                            break;
                        default:
                            weekday = 'Thứ 7'
                    }
                    var liVal = `${weekday}, ${sd.getDate()}/${sd.getMonth() / 10 == 0 ? '0' + (sd.getMonth() + 1) : sd.getMonth() + 1}/${sd.getFullYear()}: ${sd.getHours()}:${sd.getMinutes()} - ${ed.getHours()}:${ed.getMinutes()}: ${event.summary}`;
                    var li = $("<li></li>")
                    li.text(liVal)
                    ul.append(li)
                });
                $($("#incoming-events").find("div.card-body")[0]).append(ul);

            },
                function (err) { console.error("Execute error", err); });
    }
    $.ajax({
        url: "https://geolocation-db.com/jsonp",
        jsonpCallback: "callback",
        dataType: "jsonp",
        success: function (location) {
            var state = location.state;
            var url = `https://api.openweathermap.org/data/2.5/weather?q=${state}&appid=339eadb18d83b9a8fbd2ef12e749fb25&mode=html`
            $.ajax({
                url: url,
                jsonpCallback: "callback",
                dataType: "html",
                success: function (data) {
                    data = data.replace(" City", "")
                    data = data.insert(data.indexOf(state), "Thành phố ")
                    data = data.replace("Clouds", "Sương mù")
                    data = data.replace("Humidity", "Độ ẩm")
                    data = data.replace("Wind", "Gió")
                    data = data.replace("Pressure", "Áp suất")
                    $($("#weather-con").find("div.card-body")[0]).append($(data))
                }
            });
        }
    });
    
    gapi.load("client:auth2", function () {
        gapi.auth2.init({ client_id: "755793331580-j80vi8bnrd2nivd0qobflrvubrsfhmre.apps.googleusercontent.com" }).then(authenticate).then(loadClient).then(execute);
    });
})
