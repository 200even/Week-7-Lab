(function () {
    var app = angular.module('pinterest', []);

    app.controller('PinterestController', function ($scope, $http) {
        $http.get('/pins/List/')
            .success(function (response) { $scope.pins = response; });
    });
})();