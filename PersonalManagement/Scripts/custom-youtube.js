﻿$(document).ready(function () {
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
                    var channelId = sub.snippet.resourceId.channelId;
                    var imgUrl = sub.snippet.thumbnails.medium.url;
                    var imgEle = $("<img>");
                    imgEle.attr("src", imgUrl);
                    imgEle.attr("width", 240)
                    imgEle.attr("height",240)
                    imgEle.attr("class", "card-img-top");
                    var divImg = $("<div></div>")
                    divImg.attr("class", `card col-md-${12 / colCount} mb-4`)
                    
                    divImg.append(imgEle)
                    var cardBody = $("<div></div")
                    cardBody.attr("class", "card-body")
                    var cardTitle = $("<h5></h5>")
                    cardTitle.attr("class", "card-title").text(sub.snippet.title)
                    var desPara = $($.parseHTML("<p class='card-text'><b>Mô tả:</b></p>"));
                    desPara.html(desPara.html() + ' ' + sub.snippet.description)
                    
                    cardBody.append(cardTitle)
                    executeChannel(channelId, cardBody, desPara)
                    
                    divImg.append(cardBody)
                    divRow.append(divImg);
                    i++;
                    if (i == colCount + 1 || index==subs.length-1) {
                        $($(".container-fluid")[0]).append(divRow); i = 1;
                    }
                });
            },
                function (err) { console.error("Execute error", err); });
    }
    function executeChannel(id, cardBody, desPara) {
        console.log(id)
        return gapi.client.youtube.channels.list({
            "part": "snippet,contentDetails,statistics",
            "id": id
        })
            .then(function (response) {
                // Handle the results here (response.result has the parsed body).
                console.log("Response channel", response);
                var publishDate = $($.parseHTML("<p class='card-text'><b>Ngày lập kênh:</b></p>"));
                var pd = new Date(response.result.items[0].snippet.publishedAt);
                publishDate.html(publishDate.html() + ` ${pd.getDate()}/${pd.getMonth() + 1}/${pd.getFullYear()}`)
                var totalSub = $($.parseHTML("<p class='card-text'><b>Lượt đăng ký:</b></p>"));
                totalSub.html(totalSub.html() + ' ' + parseInt(response.result.items[0].statistics.subscriberCount).toLocaleString('vi-VN'))
                var totalView = $($.parseHTML("<p class='card-text'><b>Lượt xem:</b></p>"));
                totalView.html(totalView.html() + ' ' + parseInt(response.result.items[0].statistics.viewCount).toLocaleString('vi-VN'))
                cardBody.append(totalSub)
                cardBody.append(totalView)
                cardBody.append(publishDate);
                cardBody.append(desPara)


            },
                function (err) { console.error("Execute error", err); });
    }
    gapi.load("client:auth2", function () {
        gapi.auth2.init({ client_id: "755793331580-j80vi8bnrd2nivd0qobflrvubrsfhmre.apps.googleusercontent.com" }).then(authenticate).then(loadClient).then(execute)
    });
});