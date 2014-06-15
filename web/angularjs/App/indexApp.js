var app = angular.module("indexApp", ['share.header.directives', 'share.login.service', 'baseurl.service', 'share.footer.directives',
'share.metroB.directives', 'share.metroA.directives', 'share.metroC.directives', 'ngRoute', 'ngSanitize']);

app.config(function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "indexShowCtrl",
        templateUrl: "indexShow.html"
    });
    $routeProvider.when("/note/:noteid", {
        controller: "noteShowCtrl",
        templateUrl: "noteShowCtrl.html"
    });
   
});
app.controller('indexCtrl', function ($scope, $location) {
    //解决section造成noteshow无法滚动问题
    $scope.ifTrueDelSection = true;
});

app.controller('indexShowCtrl', function ($scope, $location, ylhService) {
    //解决section造成noteshow无法滚动问题
    $scope.$parent.ifTrueDelSection = true;
    $scope.metroWidth = 1380;

    $scope.metroWidthPX = $scope.metroWidth + 'px';

    $scope.notePage = {};
    $scope.notePage.count = 1;
    var notePage = $scope.notePage;
    ylhService.post({ flag: "getNotes" }, notePage, function (response) {
        //console.info(response[0].noteSearch[0].notes.length);
        if (response[0] && response[0].noteSearch) {
            var aa = response[0].noteSearch[0].notes;
            $scope.notes = aa;
        }
    });

    var a = -1;
    var lock = true;
    angular.element('.content-wrapper').bind('mousewheel', function (data) {
        if (a == data.offsetX && lock) {
            lock = false;
            ctrl = getBusyOverlay("viewport");
            //console.info('qq', $scope.metroWidth);
            $scope.notePage.count += 1;
            var notePage = $scope.notePage;
            ylhService.post({ flag: "getNotes" }, notePage, function (response) {
                if (response[0] && response[0].noteSearch) {
                    var aa = response[0].noteSearch[0].notes;
                    var nt = {};
                    angular.forEach(aa, function (v, k) {
                        nt = $scope.notes;
                        nt.push(v);
                    });
                    if (nt.length) {
                       
                        $scope.metroWidth = $scope.metroWidth + 1300;
                        $scope.metroWidthPX = $scope.metroWidth + 'px';
                        $scope.notes = nt;
                        try { ctrl.remove(); delete ctrl; } catch (e) { }
                        if (typeof (ctrl) == 'object') { ctrl.remove(); ctrl = ""; }
                        lock = true;
                    }else {
                        try { ctrl.remove(); delete ctrl; } catch (e) { }
                        if (typeof (ctrl) == 'object') { ctrl.remove(); ctrl = ""; }
                }
                    
                } 
            });
            //console.info(data.offsetX);
        } else if (a != data.offsetX) {
            a = data.offsetX;
        }
    });


    $scope.noteNext = function () {

        $scope.notePage.count += 1;
        var notePage = $scope.notePage;
        ylhService.post({ flag: "getNotes" }, notePage, function (response) {
            // console.info(response[0].noteSearch[0].notes.length);
            if (response[0] && response[0].noteSearch && response[0].noteSearch[0].notes.length) {
                var aa = response[0].noteSearch[0].notes;
                $scope.notes = aa;
            } else {
                alert("已是最后一页");
            }
        });
    }

    $scope.notePrev = function () {
        $scope.notePage.count -= 1;
        if ($scope.notePage.count >= 1) {
            var notePage = $scope.notePage;
            ylhService.post({ flag: "getNotes" }, notePage, function (response) {
                if (response[0] && response[0].noteSearch) {
                    var aa = response[0].noteSearch[0].notes;
                    $scope.notes = aa;
                }
            });
        }
    }

    $scope.showNote = function (note) {
        //console.info(note);
        $location.path("note/" + note.noteID);
        $scope.blog = note;
    }
});


app.controller('noteShowCtrl', function ($scope, $location, ylhService, $routeParams, $sce) {
    //解决section造成noteshow无法滚动问题
    $scope.$parent.ifTrueDelSection = false;

    $scope.note = {};
    $scope.note.noteId = $routeParams.noteid;
    var note = $scope.note;
    ylhService.post({ flag: "getNoteByNoteId" }, note, function (response) {
        console.info(response);
        if (response[0] && response[0].note) {
            var n = response[0].note[0];
            n.noteContent = $sce.trustAsHtml(decodeURIComponent(n.noteContent));
            $scope.myblog = n;
        }
    });
    $scope.backIndex = function () {
        $location.path("/");
    }
});