var page = require('webpage').create();

if (system.args.length === 0) {
    console.log('Usage: BrowseURL.js <some URL>');
    phantom.exit();
}

searchURL = system.args[1];

page.open(searchURL, function (status) {
    var title = page.evaluate(function () {
        return document;
    });
    console.log('Page = ' + title);
});