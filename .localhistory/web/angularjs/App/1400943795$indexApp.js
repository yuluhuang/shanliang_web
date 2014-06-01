var app = angular.module("indexApp", ['share.header.directives', 'share.login.service', 'baseurl.service', 'share.footer.directives',
'share.metroB.directives', 'share.metroA.directives', 'share.metroC.directives']);

app.controller('indexCtrl', function ($scope, ylhService) {
    ylhService.post({ flag: "noteSearch" }, function (response) {
        //console.info("111",response);
        if (response[0] && response[0].noteSearch) {
             $scope.notes=response[0].noteSearch[0].notes;
        }
    });

});