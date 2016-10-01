const sammyApp = Sammy('#main-content', function () {
    var $content = $('#main-content'),
        $root = $('#root');

    if (data.users.current()) {
        templates.get('header')
        .then(function (template) {
            $root.prepend(template);
        });
    }

    this.get("#/", function () {
        if (data.users.current()) {
            this.redirect("#/library");
            return;
        } else {
            this.redirect("#/home");
        }
    });

    this.get("#/home", function () {
        if (data.users.current()) {
            this.redirect("#/library");
            return;
        }

        templates.get('home')
        .then(function (template) {
            $content.html(template());
        });
    });

    this.get("#/library", function () {
        if (data.users.current()) {
            this.redirect("#/library");
            templates.get('library')
            .then(function (template) {
                $content.html(template());
            });

            return;
        } else {
            this.redirect("#/home");
        }
    });

    this.get('#/login', function (context) {
        if (data.users.current()) {
            context.redirect("#/");
            return;
        }

        templates.get('login')
      .then(function (template) {
          $content.html(template());

          $("#btn-login").on("click", function () {
              var user = {
                  username: $("#tb-user").val(),
                  password: $("#tb-pass").val()
              };
              data.users.login(user)
              .then(function (user) {
                  context.redirect("#/");
                  document.location.reload(true);
              });
          });
      });
    });

    this.get('#/register', function (context) {
        if (data.users.current()) {
            context.redirect("#/");
            return;
        }

        templates.get('register')
      .then(function (template) {
          $content.html(template());

          $("#btn-register").on("click", function () {
              var user = {
                  username: $("#tb-user").val(),
                  password: $("#tb-pass").val(),
                  confirmPassword: $("#tb-conf-pass").val()
              };
              data.users.register(user)
              .then(function (user) {
                  context.redirect("#/");
                  document.location.reload(true);
              });
          });
      });
    });

    this.get('#/logout', function (context) {
        data.users.logout()
        .then(function () {
            location = '#/';
            document.location.reload(true);
        });
    });
});

$(function () {
    sammyApp.run('/#/home');
    if (data.users.current()) {
        $("#btn-go-to-login").addClass("hide");
        $("#btn-go-to-register").addClass("hide");
    } else {
        $("#btn-logout").addClass("hide");
    }

    $("#btn-logout").on("click", function () {
        data.users.logout()
        .then(function () {
            location = '#/';
            document.location.reload(true);
        });
    });
});


// Initial code

//http://bookworm-1.apphb.com/api/user/Register
//http://bookworm-1.apphb.com/api/user/Login
////http://bookworm-1.apphb.com/api/user/Logout

//$(function () {

//example of get request, no auth
// httpRequester.getJSON("http://bookworm-1.apphb.com/api/Work/GetAllWorks")
// .then(function (result) {
//     console.log(result.data);
// }, function (e) {
//     console.log(e);
// });


//example of post request, no auth
//var user =
//    {
//        Username: "sashi123",
//        Password: "gosho",
//        ConfirmPassword:"gosho"
//    };
// httpRequester.postJSON("http://bookworm-1.apphb.com/api/user/Register", user)
// .then(function (result) {
//    console.log(result.data);
// }, function (e) {
//    console.log(e);
// });

//example of post request, with auth

// var headers = {
// "X-token": "945aed2f-952a-4bee-9fcf-c1d90805272b" //localStorage.getItem("token")
// };

// httpRequester.postJSON("http://bookworm-1.apphb.com/api/user/Logout", null, headers)
// .then(function (result) {
//     console.log(result.Success);
// }, function (e) {
//     console.log(e);
// });

