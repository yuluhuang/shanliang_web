var app = angular.module("indexApp", ['share.header.directives', 'share.login.service', 'baseurl.service', 'share.footer.directives',
'share.metroB.directives', 'share.metroA.directives', 'share.metroC.directives']);

app.controller('indexCtrl', function ($scope, ylhService) {
    ylhService.post({ flag: "noteSearch" }, function (response) {
        console.info(response
            );
    });

});