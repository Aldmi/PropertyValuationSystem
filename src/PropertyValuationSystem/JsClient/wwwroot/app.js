function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("getRoles").addEventListener("click", getRoles, false);
document.getElementById("logout").addEventListener("click", logout, false);
document.getElementById("getUser").addEventListener("click", displayUser, false);

var config = {
    authority: "http://localhost:5000",
    client_id: "js",
    // Адрес страницы, на которую будет перенаправлен браузер после прохождения пользователем аутентификации
    // и получения от пользователя подтверждений - в соответствии с требованиями OpenId Connect
    redirect_uri: "http://localhost:5003/callback.html",
    // Response Type определяет набор токенов, получаемых от Authorization Endpoint
    // Данное сочетание означает, что мы используем Implicit Flow
    response_type: "id_token token",
    // Получить subject id пользователя, а также поля профиля в id_token, а также получить access_token для доступа к api1 (см. наcтройки IdentityServer)
    scope: "openid profile custom.profile MainApi UserDbApi",
    // Страница, на которую нужно перенаправить пользователя в случае инициированного им логаута
    post_logout_redirect_uri: "http://localhost:5003/index.html",
    // интервал в миллисекундах, раз в который нужно проверять сессию пользователя, по умолчанию 2000
    checkSessionInterval: 30000,
    // отзывает access_token в соответствии со стандартом https://tools.ietf.org/html/rfc7009
    revokeAccessTokenOnSignout: true,
    // делать ли запрос к UserInfo endpoint для того, чтоб добавить данные в профиль пользователя
    //loadUserInfo: true,

    //ОБНОВЛЕНИЕ access_token---------------------------------------------------------------------
    // если true, клиент попытается обновить access_token перед его истечением, по умолчанию false
    automaticSilentRenew: true,
    // эта страница используется для "фонового" обновления токена пользователя через iframe
    silent_redirect_uri: 'http://localhost:5003/callback-silent.html',
    // за столько секунд до истечения oidc-client постарается обновить access_token
    accessTokenExpiringNotificationTime: 60,
};
var mgr = new Oidc.UserManager(config);

// отобразить данные о пользователе после загрузки
displayUser();


function displayUser() {
    mgr.getUser().then(function (user) {
        if (user) {
            log("User logged in", user.profile);
        }
        else {
            log("User not logged in");
        }
    });
}

function login() {
    mgr.signinRedirect();
}

function api() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:6001/api/values";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}


function getRoles() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:7000/api/values";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function() {
            log(xhr.status, JSON.parse(xhr.responseText));

        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}