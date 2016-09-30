//TO DO
const templatesCompilator = (function() {
  let handlebars = window.handlebars || window.Handlebars,
    Handlebars = window.handlebars || window.Handlebars;

  function get(templateName) {
    let promise = new Promise(function(resolve, reject) {
      let url = `/templates/${templateName}.handlebars`;

      $.get(url, function(templateHtml) {
        let template = handlebars.compile(templateHtml);
        resolve(template);
      });
    });

    return promise;
  }

  return {
    get
  };
}());

export { templatesCompilator };