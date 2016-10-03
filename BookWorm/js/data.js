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
                        $('.error').html(user.Message)
                            .fadeIn(200)
                            .removeClass('hidden');
                    }
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
                        $('.error').html(user.Message)
                            .fadeIn(200)
                            .removeClass('hidden');
                    }
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

    function getLibrary() {
        var promise = new Promise(function (resolve, reject) {
            $.getJSON("api/work/WorkLibrary?offset=0&count=1000", function (books) {
                resolve(books);
            })
        })
        return promise;
    }

    function getMyLibrary() {
        var promise = new Promise(function (resolve, reject) {
            $.ajax({
                url: '/api/work/myWorkLibrary?offset=0&count=1000',
                method: 'GET',
                data: JSON.stringify(),
                headers: {
                    'X-token': localStorage.getItem(DATA_STORAGE_KEY)
                },
                contentType: 'application/json',
                success: function (res) {
                    resolve(res);
                }
            })
        })
        return promise;
    }

    function bookById(id) {
        var promise = new Promise(function (resolve, reject) {
            $.getJSON(`/api/work/WorkDetails?workId=${id}`, function (res) {
                resolve(res);
            })
        })
        return promise;
    }

    // function getWorks(user) {
    //     let promise = new Promise(function (resolve, reject) {

    //         $.ajax({
    //             url: "http://bookworm-1.apphb.com/api/Work/GetAllWorks",
    //             method: "Get",
    //             contentType: "application/json",
    //             success: function (work) {
    //                 if (work.Success) {

    //                     localStorage.setItem(DATA_STORAGE_KEY, work.data);
    //                     resolve(work);
    //                 }
    //                 else {
    //                     alert(`${work.Message}`);
    //                 }

    //                 console.log(work);
    //             }
    //         });
    //     });
    //     return promise;
    // }



    return {
        users: {
            login: userLogin,
            register: userRegister,
            logout: userLogout,
            current: getCurrentUser
        },
        library: {
            get: getLibrary,
            my: getMyLibrary,
            getById: bookById,
            //add: booksAdd,

        }
    };
}());
