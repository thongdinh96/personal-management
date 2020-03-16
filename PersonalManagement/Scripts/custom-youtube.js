$(document).ready(function () {
    function authenticate() {
        return gapi.auth2.getAuthInstance()
            .signIn({ scope: "https://www.googleapis.com/auth/youtube.readonly" })
            .then(function () { console.log("Sign-in successful"); },
                function (err) { console.error("Error signing in", err); });
    }
    function loadClient() {
        gapi.client.setApiKey("AIzaSyDk-_n1aTaA2dnIR0NDOsy0ky3jqkUnTe4");
        return gapi.client.load("https://www.googleapis.com/discovery/v1/apis/youtube/v3/rest")
            .then(function () { console.log("GAPI client loaded for API"); },
                function (err) { console.error("Error loading GAPI client for API", err); });
    }
    function execute() {
        return gapi.client.youtube.subscriptions.list({
            "part": "snippet,contentDetails",
            "maxResults": 50,
            "mine": true
        })
            .then(function (response) {
                console.log(response);
                // Handle the results here (response.result has the parsed body).
                var subs = response.result.items;
                const colCount = 6;
                var i = 1;
                var divRow;
                $.each(subs, function (index, sub) {

                    if (i == 1) {
                        divRow = $("<div></div>");
                        divRow.attr("class", "row");
                    }
                    var channelId = sub.id;
                    var imgUrl = sub.snippet.thumbnails.medium.url;
                    var imgEle = $("<img>");
                    imgEle.attr("src", imgUrl);
                    imgEle.attr("width", 240)
                    imgEle.attr("height",240)
                    imgEle.attr("class", "card-img-top");
                    var divImg = $("<div></div>")
                    divImg.attr("class", `card col-md-${12 / colCount}`)
                    
                    divImg.append(imgEle)
                    var cardBody = $("<div></div")
                    cardBody.attr("class", "card-body")
                    var cardTitle = $("<h5></h5>")
                    cardTitle.attr("class", "card-title").text(sub.snippet.title)
                    cardBody.append(cardTitle)
                    divImg.append(cardBody)
                    divRow.append(divImg);
                    i++;
                    if (i == colCount+1) {
                        $($(".container-fluid")[0]).append(divRow); i = 1;
                    }
                });
            },
                function (err) { console.error("Execute error", err); });
    }
    gapi.load("client:auth2", function () {
        gapi.auth2.init({ client_id: "755793331580-j80vi8bnrd2nivd0qobflrvubrsfhmre.apps.googleusercontent.com" }).then(authenticate).then(loadClient).then(execute);;
    });
});