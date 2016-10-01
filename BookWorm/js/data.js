const data = (function () {

    const DATA_STORAGE_KEY = 'token';

    function userLogin(user) {
        let promise = new Promise(function (resolve, reject) {

            $.ajax({
                url: "http://bookworm-1.apphb.com/api/user/Login",
                method: "POST",
                data: JSON.stringify(user),
                contentType: "application/json",
                success: function (user) {
                    if (user.Success) {
                        localStorage.setItem(DATA_STORAGE_KEY, user.data);
                        resolve(user);
                    }
                    else {
                        alert(`${user.Message}`)
                    }
                    console.log(user);
                }
            });
        });
        return promise;
    }

    function userRegister(user) {
        let promise = new Promise(function (resolve, reject) {

            $.ajax({
                url: "http://bookworm-1.apphb.com/api/user/Register",
                method: "POST",
                data: JSON.stringify(user),
                contentType: "application/json",
                success: function (user) {
                    if (user.Success) {

                        localStorage.setItem(DATA_STORAGE_KEY, user.data);
                        resolve(user);
                    }
                    else {
                        alert(`${user.Message}`)
                    }
                    console.log(user);
                }
            });
        });
        return promise;
    }

    function userLogout() {
        let promise = new Promise(function (resolve, reject) {
            localStorage.removeItem(DATA_STORAGE_KEY);
            resolve();
        });
        return promise;
    }

    function getCurrentUser() {
        let token = localStorage.getItem(DATA_STORAGE_KEY);
        if (!token) {
            return null;
        }
        return {
            token
        };
    }

    function getWorks(user) {
        let promise = new Promise(function (resolve, reject) {

            $.ajax({
                url: "http://bookworm-1.apphb.com/api/Work/GetAllWorks",
                method: "Get",
                contentType: "application/json",
                success: function (work) {
                    if (work.Success) {

                        localStorage.setItem(DATA_STORAGE_KEY, work.data);
                        resolve(work);
                    }
                    else {
                        alert(`${work.Message}`);
                    }

                    console.log(work);
                }
            });
        });
        return promise;
    }

    return {
        users: {
            login: userLogin,
            register: userRegister,
            logout: userLogout,
            current: getCurrentUser
        },
        library: {
            get: getWorks,
            //add: booksAdd,
            //addComment: booksAddComment
        }
    };
}());
