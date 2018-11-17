/// <reference path="libs/oidc-client.js" />

var config = {
    authority: "http://localhost:5000/",
    client_id: "js",
    // Адрес страницы, на которую будет перенаправлен браузер после прохождения пользователем аутентификации (Login)
    // и получения от пользователя подтверждений - в соответствии с требованиями OpenId Connect
    redirect_uri: window.location.origin + "/callback.html",
    // Страница, на которую нужно перенаправить пользователя в случае инициированного им (Logout)
    post_logout_redirect_uri: window.location.origin + "/index.html",

    // if we choose to use popup window instead for logins
    popup_redirect_uri: window.location.origin + "/popup.html",
    popupWindowFeatures: "menubar=yes,location=yes,toolbar=yes,width=1200,height=800,left=100,top=100;resizable=yes",

    // Response Type определяет набор токенов, получаемых от Authorization Endpoint
    // Данное сочетание означает, что мы используем Implicit Flow. Обязательно для silentRenew
    response_type: "id_token token",
    scope: "openid profile custom.profile MainApi UserDbApi",

    // Доступ profile scope
    // делать ли запрос к UserInfo endpoint для того, чтоб добавить данные в профиль пользователя
    // loadUserInfo: true,

    // интервал в миллисекундах, раз в который нужно проверять сессию пользователя, по умолчанию 2000
    checkSessionInterval: 30000,

    // отзывает access_token в соответствии со стандартом https://tools.ietf.org/html/rfc7009
    revokeAccessTokenOnSignout: true,

    //ОБНОВЛЕНИЕ access_token---------------------------------------------------------------------
    // если true, клиент попытается обновить access_token перед его истечением, по умолчанию false
    automaticSilentRenew: true,
    // эта страница используется для "фонового" обновления токена пользователя через iframe
    silent_redirect_uri: 'http://localhost:5003/callback-silent.html',
    // за столько секунд до истечения oidc-client постарается обновить access_token
    accessTokenExpiringNotificationTime: 60,


    // this will allow all the OIDC protocol claims to be visible in the window. normally a client app 
    // wouldn't care about them or want them taking up space
    //filterProtocolClaims: false
};
Oidc.Log.logger = window.console;
Oidc.Log.level = Oidc.Log.DEBUG;

var mgr = new Oidc.UserManager(config);

mgr.events.addUserLoaded(function (user) {
    log("User loaded");
    showTokens();
});
mgr.events.addUserUnloaded(function () {
    log("User logged out locally");
    showTokens();
});
mgr.events.addAccessTokenExpiring(function () {
    log("Access token expiring...");
});
mgr.events.addSilentRenewError(function (err) {
    log("Silent renew error: " + err.message);
});
mgr.events.addUserSignedOut(function () {
    log("User signed out of OP");
});

function login(scope, response_type) {
    var use_popup = false;
    if (!use_popup) {
        mgr.signinRedirect({ scope: scope, response_type: response_type, acr_values:"" });
    }
    else {
        mgr.signinPopup({ scope: scope, response_type: response_type }).then(function () {
            log("Logged In");
        });
    }
}

function logout() {
    mgr.signoutRedirect();
}

function revoke() {
    mgr.revokeAccessToken();
}

function renewToken() {
    mgr.signinSilent()
        .then(function () {
            log("silent renew success");
            showTokens();
        }).catch(function (err) {
            log("silent renew error", err);
        });
}
function callApi() {
    mgr.getUser().then(function (user) {
        var xhr = new XMLHttpRequest();
        xhr.onload = function (e) {
            if (xhr.status >= 400) {
                display("#ajax-result", {
                    status: xhr.status,
                    statusText: xhr.statusText,
                    wwwAuthenticate: xhr.getResponseHeader("WWW-Authenticate")
                });
            }
            else {
                display("#ajax-result", xhr.response);
            }
        };
        var url = "https://localhost:6001/api/values";
        xhr.open("GET", url, true);
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

if (window.location.hash) {
    handleCallback();
}

[].forEach.call(document.querySelectorAll(".request"), function (button) {
    button.addEventListener("click", function () {
        login(this.dataset["scope"], this.dataset["type"]);
    });
});

document.querySelector(".renew").addEventListener("click", renewToken, false);
document.querySelector(".call").addEventListener("click", callApi, false);
document.querySelector(".revoke").addEventListener("click", revoke, false);
document.querySelector(".logout").addEventListener("click", logout, false);


function log(data) {
    document.getElementById('response').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('response').innerHTML += msg + '\r\n';
    });
}

function display(selector, data) {
    if (data && typeof data === 'string') {
        try {
            data = JSON.parse(data);
        }
        catch (e) { }
    }
    if (data && typeof data !== 'string') {
        data = JSON.stringify(data, null, 2);
    }
    document.querySelector(selector).textContent = data;
}

function showTokens() {
    mgr.getUser().then(function (user) {
        if (user) {
            display("#id-token", user);
        }
        else {
            log("Not logged in");
        }
    });
}
showTokens();

function handleCallback() {
    mgr.signinRedirectCallback().then(function (user) {
        var hash = window.location.hash.substr(1);
        var result = hash.split('&').reduce(function (result, item) {
            var parts = item.split('=');
            result[parts[0]] = parts[1];
            return result;
        }, {});

        log(result);
        showTokens();

        window.history.replaceState({},
            window.document.title,
            window.location.origin + window.location.pathname);

    }, function (error) {
        log(error);
    });
}