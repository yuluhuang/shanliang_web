var app = angular.module('share.metroa.directives', []);
app.directive("metroA", function (registerAPIService) {
    return {
        restrict: 'A',
        //controller: 'registerController',
        templateUrl: './angularjs/Directives/metroA.html',
        link: function ($scope, $element, $attrs) {
           
        }
    };
});
