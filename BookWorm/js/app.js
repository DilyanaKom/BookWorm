const sammyApp = Sammy('#main-content', function () {
    var $content = $('#main-content'),
        $root = $('#root');

    if (data.users.current()) {
        templates.get('header')
        .then(function (template) {
            $root.prepend(template);
        });
        templates.get('composeButton')
        .then(function (template) {
            $root.append(template);
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
            var books;
            data.library.get()
            .then(function (res) {
                books = res.Data;
                return templates.get('library')
            })
            .then(function (template) {
                $content.html(template(books));
            });

            // NEEDED SO THAT THE FILTER DROPDOWN WORKS
            setTimeout(function () {
                $('.dropdown-button').dropdown();
            }, 600);
        } else {
            this.redirect("#/home");
        }
    });

    this.get("#/user", function () {
        if (data.users.current()) {
            var books;
            data.library.my()
            .then(function (res) {
                books = res.Data;
                return templates.get('myLibrary')
            })
            .then(function (template) {
                $content.html(template(books));
            });

        } else {
            this.redirect("#/home");
        }
    });

    this.get("#/notifications", function () {
        if (data.users.current()) {
            var books;
            data.library.my()
            .then(function (res) {
                return templates.get('notifications')
            })
            .then(function (template) {
                $content.html(template());
            });

        } else {
            this.redirect("#/home");
        }
    });

    this.get("#/settings", function () {
        if (data.users.current()) {
            var books;
            data.library.my()
            .then(function (res) {
                return templates.get('settings')
            })
            .then(function (template) {
                $content.html(template());
            });

        } else {
            this.redirect("#/home");
        }
    });

    this.get("#/daily", function () {
        if (data.users.current()) {
            var books;
            data.library.my()
            .then(function (res) {
                return templates.get('daily')
            })
            .then(function (template) {
                $content.html(template());
            });

        } else {
            this.redirect("#/home");
        }
    });

    this.get("#/create", function () {
        if (data.users.current()) {
            var books;
            data.library.my()
            .then(function (res) {
                return templates.get('create')
            })
            .then(function (template) {
                $content.html(template());
            });
            
            // NEEDED SO THAT THE SELECT GENRE WORKS
            setTimeout(function () {
                $('select').material_select();
                $('.select-wrapper .caret').remove();
                $('.modal-trigger').leanModal();
            }, 1000);
        } else {
            this.redirect("#/home");
        }
    });

    this.get('#/book/:id', function () {
        var book;

        data.library.getById(this.params.id)
        .then(function (res) {
            book = res.Data;
            console.log(book)

            return templates.get('bookDetails')
        })
        .then(function (template) {
            $content.html(template(book));
        })
    })

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
                  console.log(`${user} Hello`)
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
// .then(function (Data) {
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

