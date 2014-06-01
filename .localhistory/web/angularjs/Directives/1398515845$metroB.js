var app = angular.module('share.metrob.directives', []);
app.directive("metroB", function (registerAPIService) {
    return {
        restrict: 'A',
        //controller: 'registerController',
        templateUrl: './angularjs/Directives/metroB.html',
        link: function ($scope, $element, $attrs) {
           
        }
    };
});
