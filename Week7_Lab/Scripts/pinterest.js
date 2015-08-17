(function () {
    var app = angular.module('pinterest', []);

    app.controller('PinterestController', function ($scope, $http) {
        

        $scope.loadPins = function () {
            $http.get('/pins/List/')
            .success(function (response) { $scope.pins = response; });
        }

        $scope.loadPins();

        $scope.savePin = function () {
            $http.post('/pins/Create', { URL : $scope.newPinUrl, Notes: $scope.newPinNotes, Image: $scope.newPinImage})
            .then(function (response) {
                console.log("It worked.");
                $scope.loadPins();
                $('#myModal').modal('hide');
            });

        }

    });
})();