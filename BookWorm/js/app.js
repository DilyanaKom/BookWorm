const sammyApp = Sammy('#content', function(){
        var $content = $('#content');
        this.get("#/",function(){
        this.redirect("#/books");
        })

        this.get('#/login', function(context){
        if(data.users.current()){
          context.redirect("#/");
          return;
        }

            templates.get('login')
          .then(function(template){
            $content.html(template());

            $("#btn-login").on("click",function(){
                var user={
                    username: $("#tb-user").val(),
                    password: $("#tb-pass").val()
                };
                data.users.login(user)
                .then(function(user){
                    context.redirect("#/");
                    document.location.reload(true);
                });
            });
          })
        })

        this.get('#/register', function(context){
            if(data.users.current()){
            context.redirect("#/");
            return;
            }

            templates.get('register')
          .then(function(template){
            $content.html(template());

           $("#btn-register").on("click",function(){
                var user={
                    username: $("#tb-user").val(),
                    password: $("#tb-pass").val(),
                    confirmPassword: $("#tb-conf-pass").val()
                };
                data.users.register(user)
                .then(function(user){
                    context.redirect("#/");
                    document.location.reload(true);
                })
            });
        });
    }); 
});

    $(function(){
      sammyApp.run('/#');
      if(data.users.current()){
        $("#btn-go-to-login").addClass("hidden");
        $("#btn-go-to-register").addClass("hidden");
      }else{
        $("#btn-logout").addClass("hidden");
      }

      $("#btn-logout").on("click",function(){
        data.users.logout()
        .then(function(){
          location='#/';
          document.location.reload(true);
        })
      })
    })


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

