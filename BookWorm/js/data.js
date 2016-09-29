var data = (function() {

  const DATA_STORAGE_KEY = 'token';

  function userLogin(user){
    var promise = new Promise(function(resolve, reject){
    // TO DO: Encrypt password!!!

    //   var reqUser={
    //     username: user.username,
    //     passHash: CryptoJS.SHA1(user.password).toString()
    //   }

      $.ajax({
        url: "http://bookworm-1.apphb.com/api/user/Login",
        method: "POST",
        data: JSON.stringify(user),
        contentType: "application/json",
        success: function(user){
          if(user.Success){
          localStorage.setItem(DATA_STORAGE_KEY, user.data);        
          resolve(user);    
          }
          else{
              alert(`${user.Message}`)
          }
          console.log(user);
        }
      });
    });
    return promise;
  }

  function userRegister(user){
    var promise = new Promise(function(resolve, reject){
    // TO DO: Encrypt!!

    //   var reqUser={
    //     username: user.username,
    //     passHash: CryptoJS.SHA1(user.password).toString(),
    //     confPassHash: CryptoJS.SHA1(user.password).toString()
    //   }
      $.ajax({
        url: "http://bookworm-1.apphb.com/api/user/Register",
        method: "POST",
        data: JSON.stringify(user),
        contentType: "application/json",
        success: function(user){
            if(user.Success){
            
            localStorage.setItem(DATA_STORAGE_KEY, user.data);
            resolve(user);
           }
           else{
               alert(`${user.Message}`)
           }
           console.log(user);  
        }
      });
    });
    return promise;
  }

  function userLogout(){
    var promise = new Promise(function(resolve, reject){
      localStorage.removeItem(DATA_STORAGE_KEY);
      resolve();
    });
    return promise;
  }

  function getCurrentUser(){
    var token = localStorage.getItem(DATA_STORAGE_KEY);
    if(!token){
      return null;
    }
    return{
      token
    };
  }



  return {
    users: {
      login: userLogin,
      register: userRegister,
      logout: userLogout,
      current: getCurrentUser
    },
    // library: {
    //   get: booksGet,
    //   add: booksAdd,
    //   addComment: booksAddComment
    // }
  };
}());