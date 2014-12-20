var inits = $data.initService;
$data.initService = function () {
    var args = arguments;
    return $.Deferred(function (defer) {
        $('.odata-syntax').empty();
        inits.apply(inits, args)
            .then(function (api, factory, apiType) {
                api.prepareRequest = function (args) {
                    $('.odata-syntax').append("<div>" + args[0].requestUri + "</div>");
                }
                defer.resolve(api, factory, apiType);
            })
            .fail(defer.reject);
    }).promise();
        
}
var helper = {};

helper.showJSON = function (data, msg) {
    var results = $('#results').length ? $('#results') :
        $("<h4 class='results'>Results</h4>").appendTo(document.body) &&
        $("<div id='results'></div>").appendTo(document.body);
    if (msg) {
        helper.log(msg);
    }
    $(results).append("<pre>" + JSON.stringify(data, undefined, 2) + "</pre>");
}
helper.log = function (msg) {
    var results = $('#results').length ? $('#results') :
        $("<h4 class='results'>Results</h4>").appendTo(document.body) &&
        $("<div id='results'></div>").appendTo(document.body);
    $(results).append("<h3>" + msg + "</h3>");
}

var display = helper;
function promised(item) {
    return function () {
        return item;
    }
}
$(function () {
    var title = $("<h1 class='pagetitle'></h1>")
            .prependTo(document.body)
            .html(document.location.href.split("/").pop().replace(/\%20|\.html/g,' '));
    
    var title = $("<div class='pageurl'></div>")
                .prependTo(document.body)
                .html(document.location.href);

    var code = $('.example-code').html();
    var precode = $('pre code').length ? $('pre code') : $(document.body)
                                                                .append("<h4>Code</h4>")
                                                                .append("<pre><code></code></pre>").find("code");

    precode.html(code);

    precode.each(function (i, block) {
        hljs.highlightBlock(block);
    });

    $data.initService("/odata", { DefaultNamespace: 'ProductApi' }).then(function (apiInstance, apiFactory, apiType) {
        api = apiInstance;
        window.run && window.run.apply(window, arguments);
    });


    $.fn.showJson = function (data) {
        var elm = this[0];
        $(elm).html("<pre>" + JSON.stringify(data,undefined, 2) + "</pre>");
    }
})

function dumpArray(items, target, template) {
    items.forEach(function (item) {
        target.append("<li>" + JSON.stringify(item) + "</li>");
    });
}
